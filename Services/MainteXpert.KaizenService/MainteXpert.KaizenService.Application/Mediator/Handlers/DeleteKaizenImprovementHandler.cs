namespace MainteXpert.KaizenService.Application.Mediator.Handlers
{
    public class DeleteKaizenImprovementHandler : IRequestHandler<DeleteKaizenImprovementCommand, ResponseModel<bool>>
    {
        private readonly IMongoRepository<KaizenImprovementCollection> _collection;

        public DeleteKaizenImprovementHandler(IMongoRepository<KaizenImprovementCollection> collection)
        {
            _collection = collection;
        }

        public async Task<ResponseModel<bool>> Handle(DeleteKaizenImprovementCommand request, CancellationToken cancellationToken)
        {
            var result = await _collection.DeleteByIdAsync(request.Id);

            if (result == null)
            {
                return new ResponseModel<bool>(false, "Kaizen Improvement not found");
            }

            return new ResponseModel<bool>(true);
        }
    }
}
