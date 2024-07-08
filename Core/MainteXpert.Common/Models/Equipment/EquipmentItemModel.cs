namespace MainteXpert.Common.Models.Equipment
{
    public class EquipmentItemModel : BaseResponseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public DateTime PurchaseDate { get; set; }
        public List<MaintenanceHistoryItem> MaintenanceHistory { get; set; }
    }
}
