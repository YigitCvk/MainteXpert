namespace MainteXpert.KaizenService.Application.Validators
{
    public class DeleteKaizenImprovementCommandValidator : AbstractValidator<DeleteKaizenImprovementCommand>
    {
        public DeleteKaizenImprovementCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        }
    }
}
