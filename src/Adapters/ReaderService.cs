using Bookfy.Users.Api.Boundaries;
using Bookfy.Users.Api.Ports;

namespace Bookfy.Users.Api.Adapters
{
    public class ReaderService : IReaderUseCase
    {
        public Task<Result> Create(CreateReader input, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

    }
}