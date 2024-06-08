namespace MainteXpert.WorkerService.Jobs
{
    internal class ActivityUpdateJob : IJob
    {
        private ILogger? _logger;
        private IMongoRepository<ActivityCollection> _activiyCollection;
        private int notifyRemainingTimeInDay;
        private EventBusProducer _producer;
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        public async Task Execute(IJobExecutionContext context)
        {
            await semaphoreSlim.WaitAsync();
            var schedulerContext = context.Scheduler.Context;
            if (_logger == null)
            {
                _logger = (ILogger)schedulerContext.Get("ILogger");
            }
            if (_activiyCollection == null)
            {
                _activiyCollection = (IMongoRepository<ActivityCollection>)schedulerContext.Get("activity");
            }

            if (_producer == null)
            {
                _producer = (EventBusProducer)schedulerContext.Get("producer");
            }

            if (notifyRemainingTimeInDay == 0)
            {
                notifyRemainingTimeInDay = (int)schedulerContext.Get("notifyRemainingTimeInDay");
            }

            _logger.LogInformation("ActivityUpdateJob başladı");

            var builder = Builders<ActivityCollection>.Projection;

            try
            {
                var projection = Builders<ActivityCollection>.Projection
                     .Include(x => x.Id)
                     .Include(x => x.ActivityUpdateTime);

                FindOptions<ActivityCollection> findOptions = new FindOptions<ActivityCollection>
                {
                    Projection = projection
                };
                //notifyRemainingTimeInDay
                var dateTime = DateTime.Now;
                dateTime = dateTime.AddDays(notifyRemainingTimeInDay);

                //      +----------------+
                //-----------------------------------
                FilterDefinition<ActivityCollection> filter = new ExpressionFilterDefinition<ActivityCollection>
                    (x => x.Status != DocumentStatus.Deleted
                    && x.ActivityUpdateTime <= dateTime
                    && x.ActivityStatus != Common.Enums.ActivityStatus.Suggested
                    && x.ActivityStatus != Common.Enums.ActivityStatus.Rejected);

                var activityList = _activiyCollection.FindAllWithProjection(filter, findOptions);
                _logger.LogInformation($"Güncellenmesi gereken activite listesi alındı. \nGüncellenecek toplam aktivite sayısı: {activityList.Count()}");

                foreach (var activity in activityList)
                {
                    var timeDiff = dateTime - activity.ActivityUpdateTime.ToLocalTime();
                    var message = $"Activite(ID: {activity.Id}) güncellenmeli. Kalan süre {timeDiff}";
                    _logger.LogInformation(message);
                    ActivityLogEvent logEvent = new ActivityLogEvent
                    {
                        ActivityId = activity.Id,
                        UserId = "Worker service",
                        LogCategory = LogCategoryEnum.ActivityUpdateJobNotify,
                        LogType = LogTypeEnum.Information,
                        ProcessDescription = message
                    };
                    _producer.Publish(EventBusQueue.ActivityUpdateJobQueue, logEvent);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }
            finally
            {

                semaphoreSlim.Release();
            }
        }
    }
}
