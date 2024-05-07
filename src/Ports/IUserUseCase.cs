using Bookfy.Users.Api.Boundaries;

namespace Bookfy.Users.Api.Ports;

public interface IUserUseCase
{
    Task<Result<UserCreated>> Create(CreateUser input, CancellationToken ct);
    Task<Result<EmailVerified>> VerifyEmail(VerifyEmail input, CancellationToken ct);

}