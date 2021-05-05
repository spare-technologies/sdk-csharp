using System.Collections.Generic;
using System.Threading.Tasks;
using Payment.Models.Payment.Domestic;

namespace Payment.Client
{
    public interface ISpPaymentClient
    {
        /// <summary>
        /// Create domestic payment
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        Task<SpDomesticPaymentResponse> CreateDomesticPayment(SpDomesticPayment payment);

        /// <summary>
        /// Get domestic payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SpDomesticPaymentResponse> GetDomesticPayment(string id);

        /// <summary>
        /// List domestic payments
        /// </summary>
        /// <param name="start"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        Task<IEnumerable<SpDomesticPaymentResponse>> ListDomesticPayments(int start = 0, int perPage = 100);
    }
}