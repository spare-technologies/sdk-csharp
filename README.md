# sdk-csharp

![Nuget](https://img.shields.io/nuget/v/Spare.NET.Sdk)
![Test and analyse workflow](https://github.com/spare-technologies/sdk-csharp/actions/workflows/dev_build_and_analyse.yml/badge.svg)
![Build and deploy workflow](https://github.com/spare-technologies/sdk-csharp/actions/workflows/master_build_and_deploy.yml/badge.svg)

### Usage

#### I- Download nuget package

```xml
<PackageReference Include="Spare.NET.Sdk" Version="1.3.0" />
``` 

#### II- To Generate ECC key pair

```csharp
 class Program
{
    static void Main(string[] args)
    {
        var eccKeyPair = SpCrypto.GenerateKeyPair();
            
        Console.WriteLine($"Private key \n{eccKeyPair.PrivateKey}");
        Console.WriteLine("\n\n");
        Console.WriteLine($"Public key \n{eccKeyPair.PublicKey}");
    }
}
```

#### II- To create a domestic payment

```csharp
public class TestController : Controller
    {
        private readonly ISpPaymentClient _client_;

        // Business ECC private key
        private readonly PrivateKey = "Your ecc private key";

        // Spare ECC public key
        private readonly ServerPublicKey = "Server ecc public key";

        public TestController()
        {
            var options = new SpPaymentClientOptions
            {
                ApiKey = "Your API key",
                AppId = "Your app id",
                BaseUrl = new Uri("https://payment.tryspare.com");
            };
            
            _client = new SpPaymentClient(options);
        }
        
        // GET
        public async Task<IActionResult> Index()
        {
            // Initialize payment
             var payment = new SpDomesticPaymentRequest
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