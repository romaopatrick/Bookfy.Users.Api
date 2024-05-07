using System.Linq.Expressions;
using Bookfy.Users.Api.Domain;
using Bookfy.Users.Api.Ports;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Bookfy.Users.Api.src.Adapters
{
    public class UserMongoDb(IMongoClient mongoClient, IOptions<MongoDbSettings> dbSettings) : IUserRepository
    {
        private readonly IMongoCollection<User> _collection =
            mongoClient
                .GetDatabase(dbSettings.Value.Database)
                .GetCollection<User>(nameof(User));

        public Task<long> Count(Expression<Func<User, bool>> filter, CancellationToken ct)
            => _collection.CountDocumentsAsync(filter, null, ct);


        public async Task<User> Create(User user, CancellationToken ct)
        {
            user.Id = Guid.NewGuid();
            user.Active = true;
            user.CreatedAt = DateTime.UtcNow;

            await _collection
                .InsertOneAsync(user,
                    cancellationToken: ct);

            return user;
        }

        public Task Delete(Guid id, CancellationToken ct)
            => _collection.DeleteOneAsync(x => x.Id == id, ct);

        public Task<User> First(Expression<Func<User, bool>> predicate, CancellationToken ct)
            => _collection.Find(predicate).FirstOrDefaultAsync(ct);

        public async Task Inactivate(Guid id, CancellationToken ct) =>
            await _collection.UpdateOneAsync(
                Builders<User>.Filter.Eq(x => x.Id, id),
                Builders<User>.Update.Set(x => x.Active, false), cancellationToken: ct);


        public async Task<User> Update(User user, CancellationToken ct)
        {
            user.UpdatedAt = DateTime.UtcNow;
            await _collection
                .ReplaceOneAsync(
                    x => x.Id == user.Id,
                    user,
                    cancellationToken: ct);

            return user;
        }

    }
}