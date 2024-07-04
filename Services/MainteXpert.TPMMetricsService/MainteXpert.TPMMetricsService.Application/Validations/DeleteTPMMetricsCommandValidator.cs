namespace MainteXpert.TPMMetricsService.Application.Validations
{
    public class DeleteTPMMetricsCommandValidator : AbstractValidator<DeleteTPMMetricsCommand>
    {
        public DeleteTPMMetricsCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID boş olamaz.");
        }
    }
}
