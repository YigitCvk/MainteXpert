namespace MainteXpert.Repository.Interface
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        string GetUserId();
        IMongoCollection<TDocument> GetCollection();

        Task<TDocument> FindOneAndUpdateAsync(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update);

        Task<List<TDocument>> GetAll();
        Task<TDocument> SoftDeleteByIdAsync(string id);
        Task HardDeleteByIdAsync(string id);
        IEnumerable<TDocument> FindAllWithProjection(FilterDefinition<TDocument> filterDefinition, FindOptions<TDocument> projection);

        Task<TDocument> FindWithProjection(FilterDefinition<TDocument> filterDefinition, FindOptions<TDocument> projection);
        PaginationDocument<TDocument> FindAllWithProjection(FilterDefinition<TDocument> filterDefinition, FindOptions<TDocument> projection, PaginationQuery pagination, SortDefinition<TDocument> sortDefinition);
        Task<TDocument> UpdateDocumentWithSelectedFieldsAsync(FilterDefinition<TDocument> filterDefinition, UpdateDefinition<TDocument> updateDefinition);
        Task<long> CountDocumentsAsync(FilterDefinition<TDocument> filterDefinition);
        Task<TDocument> GetAsync(Expression<Func<TDocument, bool>> predicate);
        Task<IQueryable<TDocument>> GetListAsync(Expression<Func<TDocument, bool>> predicate = null);
        Task<IEnumerable<TDocument>> FilterByBSon(BsonDocument filterExpression);
        IQueryable<TDocument> AsQueryable();
        Task<IQueryable<TDocument>> AsQueryableAsync(Expression<Func<TDocument, bool>> predicate = null);
        Task<IEnumerable<TDocument>> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression);
        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression);
        IEnumerable<TDocument> Find(FilterDefinition<TDocument> filterDefinition);
        Task<IEnumerable<TDocument>> FindAsync(FilterDefinition<TDocument> filterDefinition);
        Task<PaginationDocument<TDocument>> FindAsync(FilterDefinition<TDocument> filterDefinition, PaginationQuery paginationQuery = null);
        TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression);
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);
        TDocument FindById(string id);
        Task<TDocument> FindByIdAsync(string id);
        void InsertOne(TDocument document);
        Task<TDocument> InsertOneAsync(TDocument document);
        Task<IClientSessionHandle> InsertOneBySessionAsync(TDocument document);
        Task<TDocument> InsertOneBySessionAsync(TDocument document, IClientSessionHandle sessionHandle);
        void InsertMany(ICollection<TDocument> documents);
        Task InsertManyAsync(ICollection<TDocument> documents);
        Task InsertManyBySessionAsync(ICollection<TDocument> documents, IClientSessionHandle sessionHandle);
        void ReplaceOne(TDocument document);
        Task<TDocument> ReplaceOneAsync(TDocument document);
        Task<IClientSessionHandle> ReplaceOneBySessionAsync(TDocument document);
        Task<TDocument> ReplaceOneBySessionAsync(TDocument document, IClientSessionHandle sessionHandle);
        Task<UpdateResult> ReplaceManyAsync(FilterDefinition<TDocument> filterDefinition, UpdateDefinition<TDocument> updateDefinition);
        Task<IClientSessionHandle> ReplaceManyBySessionAsync(FilterDefinition<TDocument> filterDefinition, UpdateDefinition<TDocument> updateDefinition);
        Task<UpdateResult> ReplaceManyBySessionAsync(FilterDefinition<TDocument> filterDefinition, UpdateDefinition<TDocument> updateDefinition, IClientSessionHandle sessionHandle);
        void DeleteOne(Expression<Func<TDocument, bool>> filterExpression);
        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);
        Task<IClientSessionHandle> DeleteOneBySessionAsync(Expression<Func<TDocument, bool>> filterExpression);
        Task DeleteOneBySessionAsync(Expression<Func<TDocument, bool>> filterExpression, IClientSessionHandle sessionHandle);
        void DeleteById(string id);
        Task<TDocument> DeleteByIdAsync(string id);
        Task<IClientSessionHandle> DeleteByIdBySessionAsync(string id);
        Task<TDocument> DeleteByIdBySessionAsync(string id, IClientSessionHandle sessionHandle);
        void DeleteMany(Expression<Func<TDocument, bool>> filterExpression);
        Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);
        Task<TResult> InTransactionAsync<TResult>(Func<Task<TResult>> action, Action successAction = null, Action<Exception> exceptionAction = null);

    }
}
