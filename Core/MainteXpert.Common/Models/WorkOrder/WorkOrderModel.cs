namespace MainteXpert.Common.Models.WorkOrder
{
    public class WorkOrderModel : BaseResponseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }  // Örneğin, "Pending", "Completed"
    }
}
