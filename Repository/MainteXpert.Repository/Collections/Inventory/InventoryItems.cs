namespace MainteXpert.Repository.Collections.Inventory
{
    [BsonCollection("InventoryItems")]
    public class InventoryItemCollection : Document.Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DocumentStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedById { get; set; }
    }
}