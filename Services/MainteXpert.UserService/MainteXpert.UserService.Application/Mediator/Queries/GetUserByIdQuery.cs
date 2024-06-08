namespace MainteXpert.UserService.Application.Mediator.Queries
{
    public class GetUserByIdQuery : IRequest<ResponseModel<UserModel>>
    {

        public string Id { get; set; }
    }
}
