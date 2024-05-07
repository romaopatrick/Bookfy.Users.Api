using Bookfy.Users.Api.Boundaries;
using Bookfy.Users.Api.Ports;
using MassTransit;

namespace Bookfy.Users.Api.Adapters;

public class UserCreationSagaState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string? CurrentState { get; set; }
}
public class UserCreationSaga : MassTransitStateMachine<UserCreationSagaState>
{
    public required State Running { get; set; }
    public required Event<UserCreated> UserCreated { get; set; }
    public required Event<SendVerifyEmail> SendVerifyEmail { get; set; }
    public required Event<EmailVerified> EmailVerified { get; set; }
    public required Event<CreateReader> CreateReader { get; set; }


    public UserCreationSaga()
    {
        InstanceState(x => x.CurrentState);
        Initially(
            When(UserCreated)
                .TransitionTo(Running)            
                .Publish(msg => new SendVerifyEmail
                {
                    Email = msg.Message.Email,
                    Name = msg.Message.Name,
                    CorrelationId = msg.Message.CorrelationId,
                    VerifyEmailToken = msg.Message.VerifyEmailToken,
                }))
            ;

        DuringAny(
            When(SendVerifyEmail)
                .ThenAsync(async context =>
                {
                    var emailUseCase = context.GetServiceOrCreateInstance<IEmailUseCase>();
                    var result = emailUseCase.SendVerifyEmail(context.Message, context.CancellationToken);
                    if (!result.Success)
                        throw new ApplicationException(result.Message);

                    await context.Publish(result.Data!, context.CancellationToken);
                }),

            When(EmailVerified)
                .Publish(msg => new CreateReader
                {
                    CorrelationId = msg.Message.CorrelationId,
                    Email = msg.Message.Email,
                    Username = msg.Message.Email,
                    Name = msg.Message.Name,
                }),

            When(CreateReader)
                .ThenAsync(async context =>
                {
                    var readerUseCase = context.GetServiceOrCreateInstance<IReaderUseCase>();
                    var result = await readerUseCase.Create(context.Message, context.CancellationToken);
                    if (!result.Success)
                        throw new ApplicationException(result.Message);
                }).Finalize()
        );
        SetCompletedWhenFinalized();
    }


}