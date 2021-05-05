using System;
using Microsoft.Extensions.DependencyInjection;
using Payment.Client;

namespace Payment
{
    public static class SpDomesticPaymentClientExtension
    {
        public static IServiceCollection AddSpPaymentClient(this IServiceCollection services,
            Action<SpPaymentClientOptions> configuration)
        {
            if (configuration == null)
            {
                throw new NullReferenceException(nameof(configuration));
            }

            var options = new SpPaymentClientOptions();
            configuration.Invoke(options);

            if (options.BaseUrl == null)
            {
                throw new NullReferenceException(nameof(options.BaseUrl));
            }

            if (string.IsNullOrWhiteSpace(options.AppId))
            {
                throw new NullReferenceException(nameof(options.AppId));
            }

            if (string.IsNullOrWhiteSpace(options.ApiKey))
            {
                throw new NullReferenceException(nameof(options.ApiKey));
            }

            return services.AddTransient<ISpPaymentClient, SpPaymentClient>(provider => new SpPaymentClient(options));
        }
    }
}