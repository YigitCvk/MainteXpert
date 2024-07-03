using MongoDB.Bson;

namespace MainteXpert.InventortService.Application.Validations
{
    public class UpdateInventoryItemCommandValidator : AbstractValidator<UpdateInventoryItemCommand>
    {
        public UpdateInventoryItemCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(BeAValidObjectId).WithMessage("Invalid Id format.");

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(command => command.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(command => command.Quantity)
                .GreaterThan(0).WithMessage("Quantity should be greater than 0.");

            RuleFor(command => command.Price)
                .GreaterThan(0).WithMessage("Price should be greater than 0.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out _);
        }
    }
}
