using Microsoft.Extensions.DependencyInjection;
using Payment.Client;

namespace Payment
{
    public static class SpDomesticPaymentClientExtension
    {
        public static IServiceCollection AddSpPaymentClient(this IServiceCollection services) =>
            services.AddTransient<ISpPaymentClient, SpPaymentClient>();
    }
}