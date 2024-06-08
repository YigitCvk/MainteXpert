namespace MainteXpert.Common.Models.User
{
    public class UserRoleModel : BaseResponseModel
    {
        public string RoleName { get; set; }
        public List<UserRolePermissionModel> Permissions { get; set; } = new List<UserRolePermissionModel>();
    }
}
