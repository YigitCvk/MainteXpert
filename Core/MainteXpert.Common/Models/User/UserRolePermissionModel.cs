namespace MainteXpert.Common.Models.User
{
    public class UserRolePermissionModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string icon { get; set; }
        public string link { get; set; }
        public List<UserRolePermissionModel> children { get; set; }
    }
}
