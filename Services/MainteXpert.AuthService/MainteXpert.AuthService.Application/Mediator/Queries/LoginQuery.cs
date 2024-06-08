namespace Authentication.Application.Mediator.Queries
{
    public class LoginQuery : IRequest<ResponseModel<AuthUserModel>>
    {
        public string RegisterNumber { get; set; }
        public string Password { get; set; }
    }
}
