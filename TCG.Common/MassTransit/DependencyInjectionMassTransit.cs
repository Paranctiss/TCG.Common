using System.Reflection;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TCG.CatalogService.Persitence.MongoSettings;
using TCG.Common.MassTransit.Messages;
using TCG.Common.Settings;

namespace TCG.Common.MassTransit;

public static class DependencyInjectionMassTransit
{
    public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection serviceCollection, Assembly assembly)
    {
        //Config masstransit to rabbitmq
        serviceCollection.AddMassTransit(configure =>
        {
            configure.AddConsumers(assembly);
            configure.UsingRabbitMq((context, configurator) =>
            {
                var config = context.GetService<IConfiguration>();
                //On récupère le nom de la table Catalog
                ////On recupère la config de seeting json pour rabbitMQ
                var rabbitMQSettings = config.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                configurator.Host(rabbitMQSettings.Host);
                //Defnir comment les queues sont crées dans rabbit
                configurator.ReceiveEndpoint(rabbitMQSettings.QueueName, e =>
                {
                    e.ConfigureConsumers(context);
                });
            });
        });
        //Start rabbitmq bus pour exanges
        serviceCollection.AddMassTransitHostedService();
        return serviceCollection;
    }
}