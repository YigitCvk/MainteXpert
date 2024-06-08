namespace Authentication.Application.Models
{
    public class AuthUserModel
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegisterNumber { get; set; }
        public string CitizenNumber { get; set; }
        public string Email { get; set; }

        public PhotoDocumentModel Photo { get; set; }
        public UserRoleModel UserRole { get; set; }
        public TokenModel Token { get; set; }


    }
}
