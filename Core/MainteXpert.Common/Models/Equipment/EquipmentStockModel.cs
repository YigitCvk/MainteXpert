namespace MainteXpert.Common.Models.Equipment
{
    public class EquipmentStockModel
    {
        public string EquipmentId { get; set; }
        public int AvailableStock { get; set; }
        public bool IsStockSufficient { get; set; }
    }
}
