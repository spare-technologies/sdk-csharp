# sdk-csharp

### Usage

1- Download nuget package

```xml
<PackageReference Include="Spare.NET.Sdk" Version="1.0.0" />
``` 

2- In the ConfigureServices method of Startup.cs, register the SpPaymentClient..

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSpPaymentClient(options =>
    {
        options.ApiKey = "Your API key";
        options.AppId = "Your app id";
        options.BaseUrl = new Uri("https://payment.tryspare.com");
    });
}
```

3- Inject the service on the desired controller..

```csharp
public class TestController : Controller
    {
        private readonly ISpPaymentClient _client_;

        // Business ECC private key
        private readonly PrivateKey = "Your ecc private key";

        // Spare ECC public key
        private readonly ServerPublicKey = "Server ecc public key";

        public TestController(ISpPaymentClient client)
        {
            _client = client;
        }
        
        // GET
        public async Task<IActionResult> Index()
        {
            // Initialize payment
             var payment = new SpDomesticPayment
            {
                Amount = 10m,
                Description = "Payment description"
            };

            // Sign the payment
            var signature = SpEccSignatureManager.Sign(payment, PrivateKey);

            // Create payment
            var createPayment =  await _paymentClient.CreateDomesticPayment(payment,signature);

           // To verify signature of the created payment 
           if(SpEccSignatureManager.Verify(createPayment.PaymentResponse, createPayment.Signature, ServerPublicKey)){
               // signature verified
           }
        }
    }
```