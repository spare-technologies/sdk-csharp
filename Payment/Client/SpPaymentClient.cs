using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
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
        /// <returns></returns>
        public async Task<SpareSdkResponse<SpDomesticPaymentResponse, object>> CreateDomesticPayment(
            SpDomesticPayment payment)
        {
            using var client = GetClient();
            var response = await client.PostAsync(GetUrl(SpEndpoints.CreateDomesticPayment), GetBody(payment));
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }


            return JsonConvert.DeserializeObject<SpareSdkResponse<SpDomesticPaymentResponse, object>>(
                await response.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// Get domestic payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SpareSdkResponse<SpDomesticPaymentResponse, object>> GetDomesticPayment(string id)
        {
            using var client = GetClient();
            var response = await client.GetAsync($"{GetUrl(SpEndpoints.GetDomesticPayment)}?id={id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }


            return JsonConvert.DeserializeObject<SpareSdkResponse<SpDomesticPaymentResponse, object>>(
                await response.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// List domestic payments
        /// </summary>
        /// <param name="start"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<SpareSdkResponse<IEnumerable<SpDomesticPaymentResponse>, object>> ListDomesticPayments(
            int start = 0, int perPage = 100)
        {
            using var client = GetClient();
            var response =
                await client.GetAsync($"{GetUrl(SpEndpoints.ListDomesticPayments)}?start={start}&perPage={perPage}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }


            return JsonConvert.DeserializeObject<SpareSdkResponse<IEnumerable<SpDomesticPaymentResponse>, object>>(
                await response.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented
                });
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