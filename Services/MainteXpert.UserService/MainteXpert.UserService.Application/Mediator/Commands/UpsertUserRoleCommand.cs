namespace MainteXpert.UserService.Application.Mediator.Commands
{
    public class UpsertUserRoleCommand : IRequest<ResponseModel>
    {
        public string Id { get; set; } = string.Empty;
        public string RoleName { get; set; }
    }
}
