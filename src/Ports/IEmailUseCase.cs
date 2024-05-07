using Bookfy.Users.Api.Boundaries;

namespace Bookfy.Users.Api.Ports;

public interface IEmailUseCase
{
    Result<VerifyEmailSent> SendVerifyEmail(SendVerifyEmail input, CancellationToken ct);
}