namespace MainteXpert.UserService.Application.Mediator.Queries
{
    public class GetUserRoleByIdQuery : IRequest<ResponseModel<UserRoleModel>>
    {
        public string Id { get; set; }
    }
}
