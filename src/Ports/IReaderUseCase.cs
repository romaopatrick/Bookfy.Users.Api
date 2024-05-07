using Bookfy.Users.Api.Boundaries;

namespace Bookfy.Users.Api.Ports;
public interface IReaderUseCase
{
    Task<Result> Create(CreateReader input, CancellationToken ct);
}