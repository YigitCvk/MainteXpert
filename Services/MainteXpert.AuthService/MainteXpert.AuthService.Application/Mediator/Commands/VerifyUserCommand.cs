namespace Authentication.Application.Mediator.Commands
{
    public class VerifyUserCommand : IRequest<ResponseModel<AuthUserModel>>
    {
        public string RegisterNumber { get; set; }

    }
}
