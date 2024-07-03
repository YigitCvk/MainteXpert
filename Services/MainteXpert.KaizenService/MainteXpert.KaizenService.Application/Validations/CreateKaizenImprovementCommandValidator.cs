namespace MainteXpert.KaizenService.Application.Validators
{
    public class CreateKaizenImprovementCommandValidator : AbstractValidator<CreateKaizenImprovementCommand>
    {
        public CreateKaizenImprovementCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("Created By is required.");
        }
    }
}
