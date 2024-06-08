namespace MainteXpert.Common.Models.User
{
    public class UserModel : BaseResponseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegisterNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string CitizenNumber { get; set; }
        public string Title { get; set; }
        public PhotoDocumentModel Photo { get; set; } = new PhotoDocumentModel();
        public UserRoleModel Role { get; set; }
        public bool IsActive { get; set; }

    }
}
