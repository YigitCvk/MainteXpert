
namespace MainteXpert.UserService.Application.Mediator.Commands
{
    public class UpsertUserRolePermissionCommand : IRequest<ResponseModel>
    {
        public string RoleId { get; set; }
        public List<PermissionModel> Permissions { get; set; }
    }
}
