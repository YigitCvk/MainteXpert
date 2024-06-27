namespace MainteXpert.Common.Models.WorkOrder
{
    public class WorkOrderModel : BaseResponseModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } // Created, In Progress, Completed, etc.
        public string AssignedTo { get; set; } // User ID
    }
}
