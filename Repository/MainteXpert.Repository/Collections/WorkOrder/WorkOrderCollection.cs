namespace MainteXpert.Repository.Collections.WorkOrder
{
    [BsonCollection("WorkOrders")]
    public class WorkOrderCollection : Document.Document
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }  
    }
}
