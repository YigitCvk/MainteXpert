
namespace MainteXpert.Repository.Collections.Equipment
{
    [BsonCollection("EquipmentItems")]
    public class EquipmentItemCollection : Document.Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public DateTime PurchaseDate { get; set; }
        public List<MaintenanceHistoryItem> MaintenanceHistory { get; set; }
    }
}
