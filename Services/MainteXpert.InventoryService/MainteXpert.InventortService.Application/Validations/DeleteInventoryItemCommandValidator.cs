
namespace MainteXpert.InventoryService.Application.Mediator.Validators
{
    public class DeleteInventoryItemCommandValidator : AbstractValidator<DeleteInventoryItemCommand>
    {
        public DeleteInventoryItemCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(BeAValidObjectId).WithMessage("Invalid Id format.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out _);
        }
    }
}
