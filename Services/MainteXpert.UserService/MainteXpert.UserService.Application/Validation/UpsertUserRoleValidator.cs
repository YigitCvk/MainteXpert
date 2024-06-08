namespace MainteXpert.UserService.Application.Validation
{

    public class UpsertUserRoleValidator : AbstractValidator<UpsertUserRoleCommand>
    {
        private readonly IMongoRepository<UserRoleCollection> _collection;
        public UpsertUserRoleValidator(IMongoRepository<UserRoleCollection> collection)
        {
            _collection = collection;
            RuleFor(x => x.RoleName)
                .NotNull().WithMessage("Role adı boş olamaz")
                .NotEmpty().WithMessage("Role adı boş bırakılamaz")
                .Must(CheckName).WithMessage("Sistemde aynı role adı mevcuttur");


            RuleFor(x => x.Id)
                .Must(CheckId).WithMessage($"Id ile ilişkili role bulunamadı");

        }

        private bool CheckId(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return true;
            }
            else
            {
                var result = _collection.AsQueryable().Any(x => x.Id.Equals(id));
                return result;
            }
        }

        private bool CheckName(string name)
        {
            var result = _collection.AsQueryable().Any(x => x.RoleName.Equals(name));
            return !result;
        }
    }
}
