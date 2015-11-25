using MassTransit;
using System;
using System.Configuration;

namespace ServiceBus.Config
{
    public sealed class BusConfig
    {

        public static IBusControl ForRabbitMq(EndpointConfiguration[] endpointConfigs)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(GetHostAddress(), h =>
                {
                    h.Username(ConfigurationManager.AppSettings["RabbitMQUsername"]);
                    h.Password(ConfigurationManager.AppSettings["RabbitMQPassword"]);
                });

                foreach (var config in endpointConfigs)
                {
                    cfg.ReceiveEndpoint(host, config.QueueName, ecfg =>
                    {
                        config.Configuration(ecfg);
                    });
                }
            });
        }

        private static Uri GetHostAddress()
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = "rabbitmq",
                Host = ConfigurationManager.AppSettings["RabbitMQHost"]
            };

            return uriBuilder.Uri;
        }
    }
}
