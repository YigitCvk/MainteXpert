namespace MainteXpert.UserService.Application.Mediator.Commands
{
    public class UpdateUserIsActiveCommand : IRequest<ResponseModel<UserModel>>
    {
        public string UserId { get; set; }
        public bool IsActive { get; set; }
        public UpdateUserIsActiveCommand(string userId, bool isActive)
        {
            UserId = userId;
            IsActive = isActive;
        }
    }
}
