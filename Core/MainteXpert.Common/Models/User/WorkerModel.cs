namespace MainteXpert.Common.Models.User
{
    public class WorkerModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegisterNumber { get; set; }
        public string Email { get; set; }

        public DateTime StartJobTime { get; set; }
        public DateTime EndJobTime { get; set; }
        public UserRoleModel UserRole { get; set; }

    }
}
