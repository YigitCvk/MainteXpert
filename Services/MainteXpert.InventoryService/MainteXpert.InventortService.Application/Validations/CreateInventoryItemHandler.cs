namespace MainteXpert.InventoryService.Application.Mediator.Validators
{
    public class CreateInventoryItemCommandValidator : AbstractValidator<CreateInventoryItemCommand>
    {
        public CreateInventoryItemCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(command => command.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(command => command.Quantity)
                .GreaterThan(0).WithMessage("Quantity should be greater than 0.");

            RuleFor(command => command.Price)
                .GreaterThan(0).WithMessage("Price should be greater than 0.");
        }
    }
}
