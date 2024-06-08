namespace MainteXpert.UserService.Application.Mediator.Handlers.UserRole
{
    public class DeleteUserRoleByIdHandler : IRequestHandler<DeleteUserRoleByIdCommand, ResponseModel>
    {
        private readonly IMongoRepository<UserRoleCollection> _collection;
        private readonly IMapper _mapper;

        public DeleteUserRoleByIdHandler(IMongoRepository<UserRoleCollection> collection, IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<ResponseModel> Handle(DeleteUserRoleByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {

                var document = await _collection.FindByIdAsync(command.Id);

                if (document.Status == Common.Enums.DocumentStatus.Deleted)
                {
                    await _collection.HardDeleteByIdAsync(document.Id);
                }
                else
                {
                    await _collection.SoftDeleteByIdAsync(document.Id);
                }

                return ResponseModel.Success();
            }
            catch (Exception ex)
            {
                return ResponseModel.Fail(message: ex.Message);
            }
        }
    }
}
