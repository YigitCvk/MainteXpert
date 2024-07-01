namespace MainteXpert.Common.Models.Inventory
{
    public class InventoryItemModel : BaseResponseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
