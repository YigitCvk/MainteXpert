
namespace MainteXpert.UserService.Application.Mediator.Commands
{
    public class UpsertUserCommand : IRequest<ResponseModel<UserModel>>
    {
        public string Id { get; set; } = String.Empty;
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegisterNumber { get; set; }
        public string CitizenNumber { get; set; } = string.Empty;
        public string Title { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public PhotoDocumentModel? Photo { get; set; } = new PhotoDocumentModel();
        public string RoleId { get; set; }
        public bool IsActive { get; set; }

    }
}
