namespace MainteXpert.TPMMetricsService.Application.Mediator.Handler
{
    public class DeleteTPMMetricsHandler : IRequestHandler<DeleteTPMMetricsCommand, ResponseModel<bool>>
    {
        private readonly IMongoRepository<TPMMetricsCollection> _collection;

        public DeleteTPMMetricsHandler(IMongoRepository<TPMMetricsCollection> collection)
        {
            _collection = collection;
        }

        public async Task<ResponseModel<bool>> Handle(DeleteTPMMetricsCommand request, CancellationToken cancellationToken)
        {
            var result = await _collection.DeleteByIdAsync(request.Id);

            if (result == null)
            {
                return new ResponseModel<bool>(false, "TPM Metrics not found");
            }

            return new ResponseModel<bool>(true);
        }
    }
}
