using System;
using Microsoft.Extensions.DependencyInjection;
using Spare.NET.Sdk.Client;
using Spare.NET.Sdk.Exceptions;

namespace Spare.NET.Sdk
{
    public static class SpDomesticPaymentClientExtension
    {
        /// <summary>
        /// Inject SpPaymentClient
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        /// <exception cref="SpNullReferenceException"></exception>
        public static IServiceCollection AddSpPaymentClient(this IServiceCollection services,
            Action<SpPaymentClientOptions> configuration)
        {
            if (configuration == null)
            {
                throw new SpNullReferenceException(nameof(configuration));
            }

            var options = new SpPaymentClientOptions();
            configuration.Invoke(options);

            if (options.BaseUrl == null)
            {
                throw new SpNullReferenceException(nameof(options.BaseUrl));
            }

            if (string.IsNullOrWhiteSpace(options.AppId))
            {
                throw new SpNullReferenceException(nameof(options.AppId));
            }

            if (string.IsNullOrWhiteSpace(options.ApiKey))
            {
                throw new SpNullReferenceException(nameof(options.ApiKey));
            }

            return services.AddTransient<ISpPaymentClient, SpPaymentClient>(provider => new SpPaymentClient(options));
        }
    }
}