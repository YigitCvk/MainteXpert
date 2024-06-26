namespace MainteXpert.Common.Models.Equipment
{
    public class EquipmentModel : BaseResponseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Status { get; set; }
    }
}
