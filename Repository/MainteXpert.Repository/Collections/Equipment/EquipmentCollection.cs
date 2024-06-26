namespace MainteXpert.Repository.Collections.Equipment
{
    [BsonCollection("EquipmentCollection")]
    public class EquipmentCollection : Document.Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Status { get; set; }
        public int StockQuantity { get; set; }
    }
}
