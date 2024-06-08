namespace MainteXpert.Repository.Collections.User
{
    [BsonCollection("UserRole")]
    public class UserRoleCollection : Document.Document
    {
        public string RoleName { get; set; }
        public List<UserRolePermissionModel> Permissions { get; set; }
    }
}
