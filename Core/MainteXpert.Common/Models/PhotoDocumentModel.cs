using MainteXpert.Common.Enums;
using MainteXpert.Common.Models.Base;

namespace MainteXpert.Common.Models
{
    public class PhotoDocumentModel : BaseResponseModel
    {
        public string Name { get; set; }
        public string ExtensionType { get; set; }
        public string Data { get; set; }
        public AttachmentType Type { get; set; }
    }
}
