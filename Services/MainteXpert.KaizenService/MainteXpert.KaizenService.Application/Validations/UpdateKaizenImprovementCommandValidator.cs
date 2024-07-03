namespace MainteXpert.KaizenService.Application.Validators
{
    public class UpdateKaizenImprovementCommandValidator : AbstractValidator<UpdateKaizenImprovementCommand>
    {
        public UpdateKaizenImprovementCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required.");
        }
    }
}
