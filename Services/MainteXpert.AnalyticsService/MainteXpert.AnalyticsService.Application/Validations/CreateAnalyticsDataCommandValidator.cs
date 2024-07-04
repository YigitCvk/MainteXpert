namespace MainteXpert.AnalyticsService.Application.Validations
{
    public class CreateAnalyticsDataCommandValidator : AbstractValidator<CreateAnalyticsDataCommand>
    {
        public CreateAnalyticsDataCommandValidator()
        {
            RuleFor(x => x.Date).NotEmpty().WithMessage("Tarih boş olamaz.");
            RuleFor(x => x.Value).GreaterThan(0).WithMessage("Değer pozitif olmalıdır.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Kategori boş olamaz.");
        }
    }
}
