namespace MainteXpert.UserService.Application.Mediator.Handlers.User
{
    public class ChangeUserPasswordHandler : IRequestHandler<ChangeUserPasswordCommand, ResponseModel>
    {
        private readonly IMongoRepository<UserCollection> _collection;

        public ChangeUserPasswordHandler(IMongoRepository<UserCollection> collection)
        {
            _collection = collection;
        }

        public async Task<ResponseModel> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _collection.GetUserId();

                var filter = Builders<UserCollection>.Filter.Where(x => x.Id == userId);

                var updateDefination = Builders<UserCollection>.Update
                    .Set(x => x.Password, request.Password);

                await _collection.UpdateDocumentWithSelectedFieldsAsync(filter, updateDefination);
                return ResponseModel.Success(message: "şifre değiştirildi");
            }
            catch (Exception ex)
            {
                return ResponseModel.Fail(message: ex.Message);

            }

        }
    }
}
