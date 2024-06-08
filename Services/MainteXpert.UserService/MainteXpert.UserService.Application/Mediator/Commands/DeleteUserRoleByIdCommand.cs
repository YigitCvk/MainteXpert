namespace MainteXpert.UserService.Application.Mediator.Commands
{
    public class DeleteUserRoleByIdCommand : IRequest<ResponseModel>
    {
        public string Id { get; set; }
    }
}
