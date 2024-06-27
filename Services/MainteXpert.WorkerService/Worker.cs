using MainteXpert.MessagingService.Producer;

namespace MainteXpert.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly EventBusProducer _producer;
        private readonly IMongoRepository<ActivityCollection> _activityCollection;
        private readonly IMongoRepository<ShiftingGroupCollection> _shitingCollection;
        private readonly IConfiguration _configration;
        public Worker(ILogger<Worker> logger, IMongoRepository<ActivityCollection> activityCollection,
                        IMongoRepository<ShiftingGroupCollection> shitingCollection,
                        EventBusProducer producer,
                        IConfiguration configration)
        {
            _logger = logger;
            _producer = producer;
            _activityCollection = activityCollection;
            _shitingCollection = shitingCollection;
            _configration = configration;
        }


        public async override Task<Task> StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("Worker service started.");
            ISchedulerFactory factory = new StdSchedulerFactory();
            IScheduler _scheduler = await factory.GetScheduler();
            #region ActivityPeriodJob
            var queueIntervalInSec = Convert.ToInt32(_configration.GetSection("ActivityPeriodJob:IntervalTimeInSec").Value
);
            _scheduler.Context.Put("ILogger", _logger);
            _scheduler.Context.Put("activity", _activityCollection);
            _scheduler.Context.Put("shifting", _shitingCollection);
            IJobDetail jobQueue = JobBuilder.Create<ActivityPeriodJob>()
             .WithIdentity("myActivityPeriodJob", "groupActivityPeriodJob")
             .Build();
            ITrigger triggerQueue = TriggerBuilder.Create()
              .WithIdentity("myTriggerActivityPeriodJob", "groupActivityPeriodJob")
              .StartAt(DateTimeOffset.Now.AddSeconds(5))
              .WithSimpleSchedule(x => x
              .WithIntervalInSeconds(queueIntervalInSec)
              .RepeatForever())
              .Build();
            if (await _scheduler.CheckExists(jobQueue.Key))
            {
                await _scheduler.DeleteJob(jobQueue.Key);
            }
            await _scheduler.ScheduleJob(jobQueue, triggerQueue);
            await _scheduler.Start();
            #endregion

            #region ActivityUpdateJob
            var queueIntervalInHour = Convert.ToInt32(_configration.GetSection("ActivityUpdateJob:IntervalTimeInHour").Value);
            var notifyRemainingTimeInDay = Convert.ToInt32(_configration.GetSection("ActivityUpdateJob:NotifyRemainingTimeInDay").Value);


            _scheduler.Context.Put("ILogger", _logger);
            _scheduler.Context.Put("activity", _activityCollection);
            _scheduler.Context.Put("notifyRemainingTimeInDay", notifyRemainingTimeInDay);
            _scheduler.Context.Put("producer", _producer);
            jobQueue = JobBuilder.Create<ActivityUpdateJob>()
            .WithIdentity("myActivityUpdateJob", "groupActivityUpdateJob")
            .Build();
            triggerQueue = TriggerBuilder.Create()
            .WithIdentity("myTriggerActivityUpdateJob", "groupActivityUpdateJob")
            .StartAt(DateTimeOffset.Now.AddSeconds(5))
            .WithSimpleSchedule(x => x
            .WithIntervalInHours(queueIntervalInHour)
            .RepeatForever())
            .Build();
            if (await _scheduler.CheckExists(jobQueue.Key))
            {
                await _scheduler.DeleteJob(jobQueue.Key);
            }
            await _scheduler.ScheduleJob(jobQueue, triggerQueue);
            await _scheduler.Start();
            #endregion



            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }


    }
}
