using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Spare.NET.Sdk.Models.Payment.Domestic;
using Spare.NET.Sdk.Models.Response;

namespace Spare.NET.Sdk.Client
{
    public interface ISpPaymentClient
    {
        /// <summary>
        /// Create domestic payment
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <param name="signature"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SpCreateDomesticPaymentResponse> CreateDomesticPayment(SpDomesticPaymentRequest paymentRequest, string signature,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get domestic payment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SpSpareSdkResponse<SpDomesticPaymentResponse, object>> GetDomesticPayment(string id,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// List domestic payments
        /// </summary>
        /// <param name="start"></param>
        /// <param name="perPage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SpSpareSdkResponse<IEnumerable<SpDomesticPaymentResponse>, object>> ListDomesticPayments(int start = 0,
            int perPage = 100, CancellationToken cancellationToken = default);
    }
}