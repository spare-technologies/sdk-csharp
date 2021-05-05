namespace Payment.Client
{
    public sealed class SpEndpoints
    {
        public string Value { get; }

        private SpEndpoints(string value)
        {
            Value = value;
        }

        public static readonly SpEndpoints CreateDomesticPayment = new SpEndpoints("api/v1.0/payments/domestic/Create");
        public static readonly SpEndpoints GetDomesticPayment = new SpEndpoints("api/v1.0/payments/domestic/Get");
        public static readonly SpEndpoints ListDomesticPayments = new SpEndpoints("api/v1.0/payments/domestic/List");
    }
}