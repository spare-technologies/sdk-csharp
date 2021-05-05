using System;

namespace Payment.Client
{
    public class SpPaymentClientOptions
    {
        public Uri BaseUrl { get; set; }

        public string AppId { get; set; }

        public string ApiKey { get; set; }
    }
}