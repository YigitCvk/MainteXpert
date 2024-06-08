namespace MainteXpert.UserService.Application.Validation
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordValidator()
        {

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş bırakılamaz");
            RuleFor(x => x.Password).NotNull().WithMessage("Şifre alanı boş olamaz");

            RuleFor(x => x.ValidatePassword).NotEmpty().WithMessage("Şifre onay alanı boş bırakılamaz");
            RuleFor(x => x.ValidatePassword).NotNull().WithMessage("Şifre onay alanı boş olamaz");

            RuleFor(x => x)
                .Must(x => IsPasswordValid(x.Password, x.ValidatePassword)).WithMessage("Şifre alanı, şifre onay alanı ile uyuşmuyor");

        }

        private bool IsPasswordValid(string password, string validatePassowrd)
        {
            return password.Equals(validatePassowrd);
        }
    }
}
