namespace MainteXpert.UserService.Application.Validation
{

    public class DeleteUserRoleByIdValidator : AbstractValidator<DeleteUserRoleByIdCommand>
    {
        private readonly IMongoRepository<UserRoleCollection> _collection;
        private readonly IMongoRepository<UserCollection> _userCollection;

        public DeleteUserRoleByIdValidator(
            IMongoRepository<UserRoleCollection> collection,
            IMongoRepository<UserCollection> userCollection)
        {
            _collection = collection;
            _userCollection = userCollection;

            RuleFor(x => x.Id)
                .Must(CheckId).WithMessage($"Id ile ilişkili role bulunamadı");

            RuleFor(x => x.Id)
                .Must(CheckRoleIsActive).WithMessage($"İlgili rol aktif bir kullanıcıda tanımlı olduğundan rol silinemez");

        }

        private bool CheckId(string id)
        {
            var result = _collection.AsQueryable().Any(x => x.Id.Equals(id));
            return result;
        }

        private bool CheckRoleIsActive(string id)
        {
            var result = !_userCollection.AsQueryable().Any(x => x.RoleId.Equals(id) && x.Status != Common.Enums.DocumentStatus.Deleted);
            return result;
        }
    }
}

