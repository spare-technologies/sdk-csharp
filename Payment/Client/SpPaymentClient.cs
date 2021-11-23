using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Payment.Models.Payment.Domestic;
using Payment.Models.Response;

namespace Payment.Client
{
    public sealed class SpPaymentClient : ISpPaymentClient
    {
        private readonly SpPaymentClientOptions _clientOptions;

        public SpPaymentClient(SpPaymentClientOptions clientOptions)
        {
            if (clientOptions.BaseUrl == null)
            {
                throw new NullReferenceException(nameof(clientOptions.BaseUrl));
            }

            if (string.IsNullOrWhiteSpace(clientOptions.AppId))
            {
                throw new NullReferenceException(nameof(clientOptions.AppId));
            }

            if (string.IsNullOrWhiteSpace(clientOptions.ApiKey))
            {
                throw new NullReferenceException(nameof(clientOptions.ApiKey));
            }

            _clientOptions = clientOptions;
        }


        /// <summary>
        /// Create domestic payment
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="signature"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SpCreateDomesticPaymentResponse> CreateDomesticPayment(SpDomesticPayment payment,
            string signature,
            CancellationToken cancellationToken = default)
        {
            using var client = GetClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("x-signature", signature);
            var response = await client.PostAsync(GetUrl(SpEndpoints.CreateDomesticPayment), GetBody(payment),
                cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }

            return new SpCreateDomesticPaymentResponse
            {
                Signature = response.Headers.GetValues("x-signature").FirstOrDefault(),
                PaymentResponse = JsonConvert.DeserializeObject<SpSpareSdkResponse<SpDomesticPaymentResponse, object>>(
                    await response.Content.ReadAsStringAsync(), _clientOptions.SerializerSettings)
            };
        }

        /// <summary>
        /// Get domestic payment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SpSpareSdkResponse<SpDomesticPaymentResponse, object>> GetDomesticPayment(string id,
            CancellationToken cancellationToken = default)
        {
            using var client = GetClient();
            var response =
                await client.GetAsync($"{GetUrl(SpEndpoints.GetDomesticPayment)}?id={id}", cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }


            return JsonConvert.DeserializeObject<SpSpareSdkResponse<SpDomesticPaymentResponse, object>>(
                await response.Content.ReadAsStringAsync(), _clientOptions.SerializerSettings);
        }

        /// <summary>
        /// List domestic payments
        /// </summary>
        /// <param name="start"></param>
        /// <param name="perPage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SpSpareSdkResponse<IEnumerable<SpDomesticPaymentResponse>, object>> ListDomesticPayments(
            int start = 0, int perPage = 100, CancellationToken cancellationToken = default)
        {
            using var client = GetClient();
            var response =
                await client.GetAsync($"{GetUrl(SpEndpoints.ListDomesticPayments)}?start={start}&perPage={perPage}",
                    cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }


            return JsonConvert.DeserializeObject<SpSpareSdkResponse<IEnumerable<SpDomesticPaymentResponse>, object>>(
                await response.Content.ReadAsStringAsync(), _clientOptions.SerializerSettings);
        }

        /// <summary>
        /// Serialize body
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static StringContent GetBody(object o)
            => new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");

        private string GetUrl(SpEndpoints endpoints) => $"{_clientOptions.BaseUrl}{endpoints.Value}";

        /// <summary>
        /// Get client
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        private HttpClient GetClient()
        {
            if (string.IsNullOrWhiteSpace(_clientOptions.AppId))
            {
                throw new NullReferenceException(nameof(_clientOptions.AppId));
            }

            if (string.IsNullOrWhiteSpace(_clientOptions.ApiKey))
            {
                throw new NullReferenceException(nameof(_clientOptions.ApiKey));
            }

            var handler = new HttpClientHandler {SslProtocols = SslProtocols.Tls12};
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.TryAddWithoutValidation("app-id", $"{_clientOptions.AppId}");
            client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", $"{_clientOptions.ApiKey}");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}