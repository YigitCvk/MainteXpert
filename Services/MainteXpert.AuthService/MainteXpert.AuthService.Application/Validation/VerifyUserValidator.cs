namespace Authentication.Application.Validation
{
    public class VerifyUserValidator : AbstractValidator<VerifyUserCommand>
    {
        private readonly IMongoRepository<UserCollection> _collection;

        public VerifyUserValidator(IMongoRepository<UserCollection> collection)
        {
            _collection = collection;


            RuleFor(x => x.RegisterNumber).NotEmpty().WithMessage("Sicil numarası boş bırakılamaz");
            RuleFor(x => x.RegisterNumber).NotNull().WithMessage("Sicil numarası boş olamaz");


            RuleFor(x => x)
                .Must(x => IsUserExists(x.RegisterNumber)).WithMessage("Kimlik ve sicil numaralarına ait kullanıcı bulunamadı");

        }

        private bool IsUserExists(string registerNumber)
        {
            return _collection.AsQueryable().Any(x => x.RegisterNumber == registerNumber);
        }


    }
}
