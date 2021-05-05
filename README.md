# sdk-csharp

### Usage

1- Download nuget package

```xml
<PackageReference Include="" Version="1.0.0" />
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
        public TestController(ISpPaymentClient client)
        {
            _client = client;
        }
        
        // GET
        public IActionResult Index()
        {
            var payment = await client.CreateDomesticPayment(new SpDomesticPayment
            {
                Amount = 10,
                Description = "Payment from csharp sdk test",
                FailUrl = new Uri("https://example.com"),
                SuccessUrl = new Uri("https://example.com")
            });
        }
    }
```