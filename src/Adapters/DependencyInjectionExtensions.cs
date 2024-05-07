using Bookfy.Users.Api.Ports;
using Bookfy.Users.Api.Adapters;
using MassTransit;
using Bookfy.Users.Api.src.Adapters;
using MongoDB.Driver;
using Bookfy.Users.Api.src.Ports;

namespace Bookfy.Users.Api.Adapters;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        => services

            .AddScoped<IUserUseCase, UserService>()
            .AddScoped<IReaderUseCase, ReaderService>()
            .AddScoped<IEmailUseCase, EmailService>()
            .AddScoped<IUserRepository, UserMongoDb>()
            .AddScoped<IEmailSender, EmailSender>()

            .AddLogging()

            .Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)))
            .Configure<EmailSenderSettings>(configuration.GetSection(nameof(EmailSenderSettings)))
            .Configure<EmailServiceSettings>(configuration.GetSection(nameof(EmailServiceSettings)))

            .AddSingleton<IMongoClient>(new MongoClient(
                configuration
                    .GetSection(nameof(MongoDbSettings))
                    .GetValue<string>("ConnectionString")))

            .AddRabbitMq(configuration);


    private static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
                    {
                        var settings = configuration.GetRequiredSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>()
                            ?? throw new NullReferenceException(nameof(RabbitMqSettings));

                        x.AddSagaStateMachine<UserCreationSaga, UserCreationSagaState>(cfg =>
                            {
                                cfg.UseCircuitBreaker();
                                cfg.UseMessageRetry(opts =>
                                {
                                    opts.Interval(5, 5000);
                                    opts.Handle<Exception>();
                                });
                            }).InMemoryRepository();

                        x.SetKebabCaseEndpointNameFormatter();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host(settings.Host, settings.VirtualHost ?? "/", h =>
                            {
                                h.Username(settings.Username);
                                h.Password(settings.Password);
                            });
                            cfg.ConfigureEndpoints(context);
                        });



                    });

        return services;
    }
}