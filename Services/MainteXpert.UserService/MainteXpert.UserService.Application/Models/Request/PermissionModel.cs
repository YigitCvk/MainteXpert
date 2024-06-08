namespace MainteXpert.UserService.Application.Models.Request
{
    public class PermissionModel
    {
        [BsonId]
        public string id { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        public string icon { get; set; } = string.Empty;
        public string link { get; set; } = string.Empty;
        public List<PermissionModel> children { get; set; } = new List<PermissionModel>();
    }
}
