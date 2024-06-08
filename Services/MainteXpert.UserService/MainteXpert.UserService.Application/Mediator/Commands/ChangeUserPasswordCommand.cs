namespace MainteXpert.UserService.Application.Mediator.Commands
{
    public class ChangeUserPasswordCommand : IRequest<ResponseModel>
    {
        public string Password { get; set; }
        public string ValidatePassword { get; set; }

    }
}
