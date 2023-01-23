using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using report.application.Consumers;

namespace report.application
{
    public static class BusConfiguratorRegistration
    {
        public static void ConfigureBus(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddMassTransit(x =>
            {
                x.AddConsumer<PersonsConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(configuration["RabbitMQ:baseuri"]), h =>
                    {
                        h.Username(configuration["RabbitMQ:username"]);
                        h.Password(configuration["RabbitMQ:password"]);
                    });
                    config.ReceiveEndpoint(configuration["RabbitMQ:personidqueue"], ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<PersonsConsumer>(provider);
                    });
                }));
            });
            services.AddMassTransitHostedService();
        }
    }
}
