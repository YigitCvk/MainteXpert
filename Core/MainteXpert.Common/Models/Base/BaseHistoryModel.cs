namespace MainteXpert.Common.Models.Base
{
    public class BaseHistoryModel
    {
        public Guid HistoryId { get; set; } = Guid.NewGuid();
        public DateTime HistoryTime { get; set; } = DateTime.Now;
        public WorkerModel WorkerModel { get; set; }
    }
}
