namespace MainteXpert.Common.Models
{
    public class AttachmentModel : BaseResponseModel
    {
        public string Name { get; set; }
        public AttachmentType Type { get; set; }
        public string ExtensionType { get; set; }
        public string Data { get; set; }
        public float ImageHeight { get; set; }
        public float ImageWidth { get; set; }
    }
}
