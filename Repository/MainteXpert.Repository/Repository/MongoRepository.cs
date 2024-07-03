using MainteXpert.Repository.Collections.Inventory;
using MongoDB.Driver;

namespace MainteXpert.Repository.Repository
{

    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {

        private readonly IMongoCollection<TDocument> _collection;
        private readonly MongoClient _client;
        private IClientSessionHandle _session;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FindOneAndReplaceOptions<TDocument> _findOneAndReplaceOptions;
        private readonly FindOneAndUpdateOptions<TDocument> _findOneAndUpdateOptions;
        public MongoRepository(IMongoDbSettings settings, IHttpContextAccessor httpContextAccessor)
        {
            _client = new MongoClient(settings.ConnectionString);
            var database = _client.GetDatabase(settings.DatabaseName);
            _httpContextAccessor = httpContextAccessor;
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));

            _findOneAndReplaceOptions = new FindOneAndReplaceOptions<TDocument>
            {
                ReturnDocument = ReturnDocument.After,


            };

            _findOneAndUpdateOptions = new FindOneAndUpdateOptions<TDocument>
            {
                ReturnDocument = ReturnDocument.After,
            };


        }
        public async Task<TDocument> FindOneAndUpdateAsync(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update)
        {
            var options = new FindOneAndUpdateOptions<TDocument> { ReturnDocument = ReturnDocument.After };
            return await _collection.FindOneAndUpdateAsync(filter, update, options);
        }
        private protected string GetCollectionName(Type documentType)
        {
            string nameCollection = ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;

            return nameCollection;
        }


        public virtual async Task<TDocument> SoftDeleteByIdAsync(string id)
        {
            FilterDefinition<TDocument> filter = new ExpressionFilterDefinition<TDocument>
                   (x => x.Id == id);

            UpdateDefinition<TDocument> updateDefinition = Builders<TDocument>.Update
                            .Set(x => x.Status, DocumentStatus.Deleted)
                            .Set(x => x.DeletedDate, DateTime.Now)
                            .Set(x => x.DeletedById, GetUserId());

            var document = await _collection.FindOneAndUpdateAsync(filter, updateDefinition, _findOneAndUpdateOptions);

            return document;
        }

        public virtual async Task<TDocument> DocumentRollBackByIdAsync(string id)
        {
            FilterDefinition<TDocument> filter = new ExpressionFilterDefinition<TDocument>
                   (x => x.Id == id);

            UpdateDefinition<TDocument> updateDefinition = Builders<TDocument>.Update
                            .Set(x => x.Status, DocumentStatus.Updated)
                            .Set(x => x.UpdatedDate, DateTime.Now)
                            .Set(x => x.UpdatedById, GetUserId());

            var document = await _collection.FindOneAndUpdateAsync(filter, updateDefinition, _findOneAndUpdateOptions);

            return document;
        }
        public virtual async Task HardDeleteByIdAsync(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            await _collection.FindOneAndDeleteAsync(filter);
        }

        public virtual async Task HardDelete(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id.Equals(id));
        }

        public virtual async Task<TDocument> UpdateDocumentWithSelectedFieldsAsync(FilterDefinition<TDocument> filterDefinition, UpdateDefinition<TDocument> updateDefinition)
        {

            var result = await _collection.FindOneAndUpdateAsync(filterDefinition, updateDefinition, _findOneAndUpdateOptions);

            return result;
        }

        public virtual async Task<TDocument> FindWithProjection(FilterDefinition<TDocument> filterDefinition, FindOptions<TDocument> projection)
        {

            return await _collection.Find(filterDefinition).Project(projection.Projection).FirstOrDefaultAsync();
        }

        public virtual IEnumerable<TDocument> FindAllWithProjection(FilterDefinition<TDocument> filterDefinition, FindOptions<TDocument> projection)
        {

            return _collection.Find(filterDefinition).Project(projection.Projection).ToEnumerable();
        }

        public virtual PaginationDocument<TDocument> FindAllWithProjection(FilterDefinition<TDocument> filterDefinition,
            FindOptions<TDocument> projection,
            PaginationQuery pagination,
            SortDefinition<TDocument> sortDefinition)
        {


            var data = _collection.Find(filterDefinition).Project(projection.Projection)
                .Sort(sortDefinition)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Limit(pagination.PageSize)
                .ToEnumerable();

            int totalRecords = (int)_collection.Find(filterDefinition).Count();
            int count = data.Count();
            int totalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / (double)pagination.PageSize));

            PaginationDocument<TDocument> paged = new PaginationDocument<TDocument>(data, count, totalPages, totalRecords);

            return paged;
        }
        public virtual IMongoCollection<TDocument> GetCollection()
        {
            return _collection;
        }
        public async Task<List<TDocument>> GetAll()
        {
            return await _collection.Find(Builders<TDocument>.Filter.Empty).ToListAsync();
        }
        public virtual async Task<long> CountDocumentsAsync(FilterDefinition<TDocument> filterDefinition)
        {
            return await _collection.CountDocumentsAsync(filterDefinition);
        }

        public virtual async Task<TDocument> GetAsync(Expression<Func<TDocument, bool>> predicate)
        {
            return await Task.Run(() =>
            {
                return _collection.Find(predicate).FirstOrDefaultAsync();
            });

        }
        public virtual async Task<IQueryable<TDocument>> GetListAsync(Expression<Func<TDocument, bool>> predicate = null)
        {
            return await Task.Run(() =>
            {
                return predicate == null ? _collection.AsQueryable() : _collection.AsQueryable().Where(predicate);
            });
        }
        public virtual IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }
        public virtual async Task<IQueryable<TDocument>> AsQueryableAsync(Expression<Func<TDocument, bool>> predicate = null)
        {
            return await Task.Run(() =>
            {
                return predicate == null ? _collection.AsQueryable() : _collection.AsQueryable().Where(predicate);
            });
        }
        public virtual async Task<IEnumerable<TDocument>> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            return await Task.Run(() =>
            {
                return _collection.Find(filterExpression).ToEnumerable();
            });
        }
        public virtual async Task<IEnumerable<TDocument>> FilterByBSon(BsonDocument filterExpression)
        {
            return await Task.Run(() =>
            {
                return _collection.Find(filterExpression).ToEnumerable();
            });
        }
        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual IEnumerable<TDocument> Find(FilterDefinition<TDocument> filterDefinition)
        {
            return _collection.Find(filterDefinition).ToEnumerable();
        }


        public virtual async Task<IEnumerable<TDocument>> FindAsync(FilterDefinition<TDocument> filterDefinition)
        {
            return await Task.Run(() =>
            {
                return _collection.Find(filterDefinition).ToEnumerable();
            });
        }

        public virtual async Task<PaginationDocument<TDocument>> FindAsync(FilterDefinition<TDocument> filterDefinition, PaginationQuery paginationQuery = null)
        {
            return await Task.Run(async () =>
            {
                //var sortDefinition = new SortDefinition
                //<TDocument>().Descending("UpdatedDate").Descending("CreatedDate");
                //var findOptions = new FindOptions<TDocument>() { Sort = sortDefinition };
                //var findResult = await this._collection.FindAsync(filterDefinition, findOptions);
                //var result1 = findResult.ToEnumerable();

                //var result2 = _collection
                //    .Find(filterDefinition)
                //    .ToEnumerable();

                var result = _collection
                    .Find(filterDefinition)
                    .Sort(Builders<TDocument>.Sort.Descending("UpdatedDate IS NULL THEN 1 ELSE 0 END").Descending("UpdatedDate").Descending("CreatedDate"))
                    .ToEnumerable();

                var totalRecords = await _collection.CountDocumentsAsync(filterDefinition);
                var totalPages = 1;
                if (paginationQuery != null)
                {
                    var skip = (paginationQuery.PageNumber - 1) * paginationQuery.PageSize;
                    result = result.Skip(skip).Take(paginationQuery.PageSize);

                    totalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / (double)paginationQuery.PageSize));
                }

                return new PaginationDocument<TDocument>(result, result.Count(), totalPages, Convert.ToInt32(totalRecords));
            });
        }

        public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            Expression<Func<TDocument, bool>> exp = x => x.Status != DocumentStatus.Deleted;
            var builder = Builders<TDocument>.Filter.And(filterExpression, exp);

            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual TDocument FindById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            return _collection.Find(filter).SingleOrDefault();

            //TODO: 8.2.2022 kapatıldı
            //var objectId = new ObjectId(id);
            //var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            //return _collection.Find(filter).SingleOrDefault();
        }

        public virtual Task<TDocument> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });

        }


        public virtual void InsertOne(TDocument document)
        {
            document = BeforeCreateDocument(document);
            _collection.InsertOne(document);
        }

        public async virtual Task<TDocument> InsertOneAsync(TDocument document)
        {

            return await Task.Run(async () =>
                {

                    document = BeforeCreateDocument(document);


                    await _collection.InsertOneAsync(document);
                    return document;
                });

        }

        public async virtual Task<IClientSessionHandle> InsertOneBySessionAsync(TDocument document)
        {
            document = BeforeCreateDocument(document);

            await _collection.InsertOneAsync(_session, document);
            return _session;
        }

        public async virtual Task<TDocument> InsertOneBySessionAsync(TDocument document, IClientSessionHandle sessionHandle)
        {
            await _collection.InsertOneAsync(sessionHandle, document);
            return document;
        }

        public void InsertMany(ICollection<TDocument> documents)
        {
            _collection.InsertMany(documents);
        }

        public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public virtual async Task InsertManyBySessionAsync(ICollection<TDocument> documents, IClientSessionHandle sessionHandle)
        {
            await _collection.InsertManyAsync(sessionHandle, documents);
        }

        public void ReplaceOne(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }

        public virtual async Task<TDocument> ReplaceOneAsync(TDocument document)
        {


            document = BeforeUpdateDocument(document);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);

            var result = await _collection.FindOneAndReplaceAsync(filter, document, _findOneAndReplaceOptions);

            return result;
        }

        public virtual async Task<IClientSessionHandle> ReplaceOneBySessionAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(_session, filter, document);
            return _session;
        }

        public virtual async Task<TDocument> ReplaceOneBySessionAsync(TDocument document, IClientSessionHandle sessionHandle)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            return await _collection.FindOneAndReplaceAsync(sessionHandle, filter, document);
        }

        public virtual async Task<UpdateResult> ReplaceManyAsync(FilterDefinition<TDocument> filterDefinition, UpdateDefinition<TDocument> updateDefinition)
        {
            return await _collection.UpdateManyAsync(filterDefinition, updateDefinition);
        }

        public virtual async Task<IClientSessionHandle> ReplaceManyBySessionAsync(FilterDefinition<TDocument> filterDefinition, UpdateDefinition<TDocument> updateDefinition)
        {
            await _collection.UpdateManyAsync(_session, filterDefinition, updateDefinition);
            return _session;
        }

        public virtual async Task<UpdateResult> ReplaceManyBySessionAsync(FilterDefinition<TDocument> filterDefinition, UpdateDefinition<TDocument> updateDefinition, IClientSessionHandle sessionHandle)
        {
            return await _collection.UpdateManyAsync(sessionHandle, filterDefinition, updateDefinition);
        }

        public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        public async Task<IClientSessionHandle> DeleteOneBySessionAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            await Task.Run(() => _collection.FindOneAndDeleteAsync(_session, filterExpression));
            return _session;
        }

        public Task DeleteOneBySessionAsync(Expression<Func<TDocument, bool>> filterExpression, IClientSessionHandle sessionHandle)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(sessionHandle, filterExpression));
        }

        public void DeleteById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            _collection.FindOneAndDelete(filter);

            //TODO: 8.2.2022 kapatıldı
            //var objectId = new ObjectId(id);
            //var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            //_collection.FindOneAndDelete(filter);
        }

        public Task<TDocument> DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
                return _collection.FindOneAndDeleteAsync(filter);
            });
        }

        public async virtual Task<IClientSessionHandle> DeleteByIdBySessionAsync(string id)
        {
            await Task.Run(async () =>
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
                await _collection.FindOneAndDeleteAsync(_session, filter);
            });
            return _session;
        }

        public async virtual Task<TDocument> DeleteByIdBySessionAsync(string id, IClientSessionHandle sessionHandle)
        {
            return await Task.Run(() =>
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
                return _collection.FindOneAndDeleteAsync(sessionHandle, filter);
            });
        }

        public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }

        public Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }

        public async Task<TResult> InTransactionAsync<TResult>(Func<Task<TResult>> action, Action successAction = null, Action<Exception> exceptionAction = null)
        {
            var result = default(TResult);
            try
            {
                using (var session = await _client.StartSessionAsync())
                {
                    try
                    {
                        session.StartTransaction();
                        _session = session;
                        result = await action();
                        _session = session;
                        await session.CommitTransactionAsync();
                    }
                    catch (Exception)
                    {
                        await session.AbortTransactionAsync();
                        throw;
                    }
                }

                successAction?.Invoke();
            }
            catch (Exception ex)
            {
                if (exceptionAction == null)
                    throw;
                else
                    exceptionAction(ex);
            }
            return result;
        }


        private TDocument BeforeUpdateDocument(TDocument document)
        {
            document.Status = DocumentStatus.Updated;
            document.UpdatedDate = DateTime.Now;
            document.UpdatedById = GetUserId();

            return document;
        }
        private TDocument BeforeCreateDocument(TDocument document)
        {


            document.CreatedById = GetUserId() ?? document.CreatedById;
            document.CreatedDate = DateTime.Now;
            document.Status = DocumentStatus.Created;

            return document;
        }
        private TDocument BeforeDeleteDocument(TDocument document)
        {
            document.Status = DocumentStatus.Deleted;
            document.DeletedDate = DateTime.Now;

            document.DeletedById = GetUserId();

            return document;
        }


        public string GetUserId()
        {
            return this._httpContextAccessor.HttpContext?.User?.Identity?.Name;
        }

        public async Task<TDocument> FindWithProjection(FilterDefinition<TDocument> filterDefinition, ProjectionDefinition<TDocument> projection)
        {
            return await _collection.Find(filterDefinition).Project<TDocument>(projection).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TDocument>> FindAllWithProjection(FilterDefinition<TDocument> filterDefinition, ProjectionDefinition<TDocument> projection)
        {
            return await _collection.Find(filterDefinition).Project<TDocument>(projection).ToListAsync();
        }

        public async Task<TDocument> FindWithProjections(FilterDefinition<TDocument> filter, ProjectionDefinition<TDocument> projection)
        {
            return await _collection.Find(filter).Project<TDocument>(projection).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TDocument>> FindAllWithProjections(FilterDefinition<TDocument> filter, ProjectionDefinition<TDocument> projection)
        {
            return await _collection.Find(filter).Project<TDocument>(projection).ToListAsync();
        }
    }
}