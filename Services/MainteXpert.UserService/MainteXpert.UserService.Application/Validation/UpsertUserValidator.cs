namespace MainteXpert.UserService.Application.Validation
{
    public class UpsertUserValidator : AbstractValidator<UpsertUserCommand>
    {
        private readonly IMongoRepository<UserRoleCollection> _roleCollection;
        private readonly IMongoRepository<UserCollection> _userCollection;
        public UpsertUserValidator(IMongoRepository<UserRoleCollection> roleCollection,
        IMongoRepository<UserCollection> userCollection)
        {
            _roleCollection = roleCollection;
            _userCollection = userCollection;

            RuleFor(x => x.Name)
                .NotNull().WithMessage("Kullanıcı adı boş olamaz")
                .NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz");

            RuleFor(x => x.Surname)
               .NotNull().WithMessage("Soyad boş olamaz")
               .NotEmpty().WithMessage("Soyad boş bırakılamaz");

            RuleFor(x => x.Title)
                .NotNull().WithMessage("Ünvan boş olamaz")
                .NotEmpty().WithMessage("Ünvan boş bırakılamaz");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email adresi uygun değil");

            RuleFor(x => x)
              .Must(x => CheckRegisterNumber(x.Id, x.RegisterNumber)).WithMessage("Sicil numarasına ait kullanıcı mevcut.")
              .Must(x => CheckCitizenNumber(x.Id, x.CitizenNumber)).WithMessage("Kimlik numarasına ait kullanıcı mevcut.");


            RuleFor(x => x.RegisterNumber)
                .Must(x => long.TryParse(x, out _)).WithMessage("Sicil numarası harf içeremez.")
                .NotNull().WithMessage("Sicil No boş olamaz")
                .NotEmpty().WithMessage("Sicil No boş bırakılamaz");

            RuleFor(x => x.CitizenNumber)
                .Must(x => long.TryParse(x, out _)).WithMessage("Kimlik numarası harf içeremez.").When(x => !string.IsNullOrEmpty(x.CitizenNumber))
                .Length(11).WithMessage("Kimlik numarası 11 hane olmalıdır.").When(x => !string.IsNullOrEmpty(x.CitizenNumber));


            RuleFor(x => x.Password)
                .NotNull().WithMessage("Şifre boş olamaz")
                .NotEmpty().WithMessage("Şifre boş bırakılamaz");

            RuleFor(x => x.RoleId)
                .NotNull().WithMessage("Rol boş olamaz")
                .NotEmpty().WithMessage("Rol boş bırakılamaz")
                .Must(CheckIdHasValidFormat).WithMessage("Rol Id formatı uygun değil")
                .Must(CheckRoleExistsById).WithMessage("Rol bulunamadı");

            RuleFor(x => x.Id)
                .Must(CheckIdHasValidFormat).WithMessage("Id formatı uygun değil").When(x => x.Id.Length > 0)
                .Must(CheckId).WithMessage($"Id ile ilişkili kullanıcı bulunamadı");

            RuleFor(x => x)
                .Must(CheckMail).WithMessage("Bu mail adresine ait kullanıcı mevcut");

            RuleFor(x => x)
                .MustAsync(CheckTitleAlreadyUsing).WithMessage("Bu kullanıcı adını başka biri kullanıyor");

        }

        private async Task<bool> CheckTitleAlreadyUsing(UpsertUserCommand arg1, CancellationToken arg2)
        {
            if (string.IsNullOrEmpty(arg1.Id))
            {
                var filter = Builders<UserCollection>.Filter.Eq(x => x.Title, arg1.Title);
                var result = await _userCollection.FindAsync(filter);
                return !result.Any();
            }
            else
            {
                var user = await _userCollection.FindByIdAsync(arg1.Id);
                if (user.Title.Equals(arg1.Title))
                {
                    return true;
                }
                else
                {
                    var filter = Builders<UserCollection>.Filter.Eq(x => x.Name, arg1.Name);
                    var result = await _userCollection.FindAsync(filter);
                    return !result.Any();

                }
            }
        }

        private bool CheckMail(UpsertUserCommand command)
        {
            if (string.IsNullOrEmpty(command.Id))
            {
                return !_userCollection.AsQueryable().Any(user => user.Email == command.Email);
            }
            else
            {
                var currendMail = _userCollection.FindById(command.Id).Email;
                if (currendMail != command.Email)
                {
                    return !_userCollection.AsQueryable().Any(user => user.Email == command.Email);

                }
                else
                {
                    return true;
                }

            }
        }

        private bool CheckRegisterNumber(string id, string registerNumber)
        {
            if (string.IsNullOrEmpty(id))
            {
                return !_userCollection.AsQueryable().Any(x => x.RegisterNumber.Equals(registerNumber));
            }
            else
            {
                var user = _userCollection.FindById(id);
                if (user.RegisterNumber != registerNumber)
                {
                    return !_userCollection.AsQueryable().Any(x => x.RegisterNumber.Equals(registerNumber));
                }
                return true;
            }
        }

        private bool CheckCitizenNumber(string id, string citizenNumber)
        {
            if (string.IsNullOrEmpty(citizenNumber))
                return true;

            if (string.IsNullOrEmpty(id))
            {
                return !_userCollection.AsQueryable().Any(x => x.CitizenNumber.Equals(citizenNumber));
            }
            else
            {
                var user = _userCollection.FindById(id);
                if (user.CitizenNumber != citizenNumber)
                {
                    return !_userCollection.AsQueryable().Any(x => x.CitizenNumber.Equals(citizenNumber));
                }
                return true;
            }
        }


        private bool CheckIdHasValidFormat(string id)
        {

            return ObjectId.TryParse(id, out var result);
        }

        private bool CheckRoleExistsById(string id)
        {
            if (ObjectId.TryParse(id, out var result))
            {
                return _roleCollection.AsQueryable().Any(x => x.Id.Equals(id));
            }
            return false;

        }
        private bool CheckId(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return true;
            }
            else
            {


                var result = _userCollection.AsQueryable().Any(x => x.Id.Equals(id));
                return result;
            }
        }

    }
}
