using Bookfy.Users.Api.Boundaries;
using Bookfy.Users.Api.Domain;
using Bookfy.Users.Api.Ports;
using Bookfy.Users.Api.src.Adapters;
using MassTransit;

namespace Bookfy.Users.Api.Adapters
{
    public class UserService(
        IConfiguration configuration,
        IUserRepository userRepository) : IUserUseCase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<Result<UserCreated>> Create(CreateUser input, CancellationToken ct)
        {
            var validation = input.Validate();
            if (!validation.Success)
                return validation.To<UserCreated>();

            if (await UserConflicts(input.Email, ct))
                return Result.WithFailure<UserCreated>("user_conflicts", 409);
                
            var user = await _userRepository.Create(
                new User
                {
                    Email = input.Email,
                    Name = input.Name,
                    VerifyEmailToken = $"{input.Email}|{DateTime.UtcNow.Ticks}".Hash(),
                    EmailVerified = false
                }, ct);

            var userCreated = new UserCreated
            {
                Email = user.Email,
                Name = user.Name,
                Id = user.Id,
                CorrelationId = user.Id,
                VerifyEmailToken = user.VerifyEmailToken!,
            };

            return Result.WithSuccess(userCreated, 201);
        }
    private async Task<bool> UserConflicts(string email, CancellationToken ct)
    {
        var user = await _userRepository.First(x => x.Email == email && x.Active, ct);
        return user is not null;
    }

    public Task<Result<EmailVerified>> VerifyEmail(VerifyEmail input, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

}
}