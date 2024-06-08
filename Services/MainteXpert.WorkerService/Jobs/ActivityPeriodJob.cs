namespace MainteXpert.WorkerService.Jobs
{
    public class ActivityPeriodJob : IJob
    {
        private ILogger? _logger;
        private IMongoRepository<ActivityCollection> _activiyCollection;
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
            _logger.LogInformation("\nActivityPeriodJob başladı\n");

            var builder = Builders<ActivityCollection>.Projection;

            try
            {
                var projection = Builders<ActivityCollection>.Projection
                                                        .Include(x => x.Id)
                                                        .Include(x => x.LastChildCreatedDate)
                                                        .Include(x => x.ChildCreatedDates)
                                                        .Include(x => x.Period);


                FindOptions<ActivityCollection> findOptions = new FindOptions<ActivityCollection>
                {
                    Projection = projection
                };

                FilterDefinition<ActivityCollection> filter = new ExpressionFilterDefinition<ActivityCollection>
                                                        (x => x.Status != DocumentStatus.Deleted
                                                        && x.ActivityType == ActivityTypeEnum.Parent
                                                        && (x.ActivityStatus != ActivityStatus.Rejected
                                                        || x.ActivityStatus != ActivityStatus.Suggested
                                                        || x.ActivityStatus != ActivityStatus.Approved));

                var activityList = _activiyCollection.FindAllWithProjection(filter, findOptions);

                _logger.LogInformation($"Kontrol edilecek aktivite sayısı: {activityList.Count()}\n");

                foreach (var activity in activityList)
                {
                    _logger.LogInformation($"Kontrol edilen aktivite ID: {activity.Id}\n");

                    foreach (var period in activity.Period)
                    {

                        DateTime today = DateTime.Now;
                        DateTime lastChildCreatedDate = activity.LastChildCreatedDate.ToLocalTime();
                        _logger.LogInformation($"\nToday: {today.ToString("dd/mm/yy hh:mm:ss:ms")}\n" +
                                               $"Aktivite ID: {activity.Id} \n" +
                                               $"Period ID: {period.Id}\n" +
                                               $"PeriodType: {period.PeriodType}\n" +
                                               $"PeriodName: {period.PeriodName}\n" +
                                               $"LastChildCreatedDate: {lastChildCreatedDate}\n");

                        if (period.PeriodType != PeriodTypeEnum.Shifting)
                        {
                            var nextChildCreateDate = period.PeriodType switch
                            {
                                PeriodTypeEnum.None => DateTime.MinValue,
                                PeriodTypeEnum.Hour => lastChildCreatedDate.AddHours(1),
                                PeriodTypeEnum.Daily => lastChildCreatedDate.AddDays(1),
                                PeriodTypeEnum.Weekly => lastChildCreatedDate.AddDays(7),
                                PeriodTypeEnum.Monthly => lastChildCreatedDate.AddMonths(1),
                                PeriodTypeEnum.Yearly => lastChildCreatedDate.AddYears(1),
                                PeriodTypeEnum.Shifting => DateTime.MinValue,
                            };
                            if (today >= nextChildCreateDate)
                            {
                                _logger.LogInformation($"Activite oluşturulmalı today > NextChildCreateDate [{today} > {nextChildCreateDate}]\n");
                                await ReCreateActivity(activity.Id, period.Id);
                                break;
                            }
                        }
                        if (period.PeriodType == PeriodTypeEnum.Shifting)
                        {

                            var todayCreatedChilds = activity.ChildCreatedDates.Where(x => x.CreatedDate.DayOfYear.Equals(today.DayOfYear)
                                                                                        && x.CreatedDate.Year.Equals(today.Year))
                                                                                        .Select(x => x.PeriodId).ToList();
                            _logger.LogInformation($"Gün içerisinde vardiya ile ilgili oluşturulmuş aktivite sayısı: {todayCreatedChilds.Count}\n");
                            if (!todayCreatedChilds.Contains(period.Id))
                            {
                                _logger.LogInformation($"Vardiya: {period.PeriodName} oluşturulmamış.\n");
                                if (TimeOnly.FromDateTime(today).CompareTo(period.StartDate) >= 0 && TimeOnly.FromDateTime(today).CompareTo(period.EndDate) <= 0)
                                {
                                    _logger.LogInformation($"Aktivite oluşturulmalı Today.Date >= Period.StartDate && Today.Date <= Period.EndDate  ==> [{today.Date} >= {period.StartDate} && {today.Date} >= {period.EndDate} \n]");
                                    await ReCreateActivity(activity.Id, period.Id);
                                    break;
                                }
                            }
                            else
                            {
                                _logger.LogInformation($"Vardiya: {period.PeriodName} gün içerisinde oluşturulmuş.\n");

                            }
                        }
                    }
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

        private async Task ReCreateActivity(string activityId, string periodId)
        {
            try
            {
                _logger.LogInformation($"Activite ID: {activityId} Periyot ID:{periodId} oluşturuluyor\n");
                var builder = Builders<ActivityCollection>.Projection;

                var projection = Builders<ActivityCollection>.Projection
                     .Exclude(x => x.CurrentOperator)
                     .Exclude(x => x.ControlValue)
                     .Exclude(x => x.ActivityHistory)
                     .Exclude(x => x.ActivityUpdater)
                     .Exclude(x => x.ActivityDeleter)
                     .Exclude(x => x.ActivityScorer)
                     .Exclude(x => x.ActivityCompletor)
                     .Exclude(x => x.ActivityOtonomScore)
                     .Exclude(x => x.ActivityStatus)
                     .Exclude(x => x.CreatedDate)
                     .Exclude(x => x.UpdatedById)
                     .Exclude(x => x.DeletedById)
                     .Exclude(x => x.UpdatedDate)
                     .Exclude(x => x.DeletedDate)
                     .Exclude(x => x.Status);


                FindOptions<ActivityCollection> findOptions = new FindOptions<ActivityCollection>
                {
                    Projection = projection
                };

                FilterDefinition<ActivityCollection> filter = new ExpressionFilterDefinition<ActivityCollection>
                    (x => x.Id == activityId);

                var activity = await _activiyCollection.FindWithProjection(filter, findOptions);
                _logger.LogInformation($"Ana aktvite bulundu ID: {activity.Id}\n");
                ActivityCollection reActiviy = new ActivityCollection
                {
                    ActivityCommonId = activity.ActivityCommonId,
                    ReActivityId = activity.Id,
                    CreatedById = activity.CreatedById,
                    NameOfActivity = activity.NameOfActivity,
                    ActivityName = activity.ActivityName,
                    ControlType = activity.ControlType,
                    ActivityGroupName = activity.ActivityGroupName,
                    MeasurementMethod = activity.MeasurementMethod,
                    StationGroup = activity.StationGroup,
                    Stations = activity.Stations,
                    ControlValue = "",
                    FactoryGroup = activity.FactoryGroup,
                    Photos = activity.Photos,
                    NumberOfPhotos = activity.NumberOfPhotos,
                    Documents = activity.Documents,
                    NumberOfDocuments = activity.NumberOfDocuments,
                    MinValue = activity.MinValue,
                    MaxValue = activity.MaxValue,
                    IsSuggested = activity.IsSuggested,
                    ActivitySuggestion = activity.ActivitySuggestion,
                    ActivityCreater = activity.ActivityCreater,
                    ActivityStatus = ActivityStatus.Ready,
                    ActivityType = ActivityTypeEnum.Child
                };

                var childActivity = await _activiyCollection.InsertOneAsync(reActiviy);
                _logger.LogInformation($"Aktivite oluşturuldu ID: {childActivity.Id}\n");
                activity.NumberChildCreated++;
                activity.ChildCreatedDates.Add(new ChildCreatedDateModel(periodId, activity.Period.FirstOrDefault().PeriodType, DateTime.Now));
                activity.LastChildCreatedDate = DateTime.Now;

                UpdateDefinition<ActivityCollection> updateParent = Builders<ActivityCollection>.Update
                                .Set(x => x.NumberChildCreated, activity.NumberChildCreated)
                                .Set(x => x.ChildCreatedDates, activity.ChildCreatedDates)
                                .Set(x => x.LastChildCreatedDate, activity.LastChildCreatedDate);

                await _activiyCollection.UpdateDocumentWithSelectedFieldsAsync(filter, updateParent);
                _logger.LogInformation($"Ana aktivite güncelleştirildi\n");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

    }
}
