using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Payment.Models.Payment.Domestic;
using Payment.Models.Response;

namespace Payment.Client
{
    public interface ISpPaymentClient
    {
        /// <summary>
        /// Create domestic payment
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="signature"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SpCreateDomesticPaymentResponse> CreateDomesticPayment(SpDomesticPayment payment,string signature,
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