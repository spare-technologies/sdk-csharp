using Newtonsoft.Json;

namespace Payment.Models.Response
{
    public class SpSpareSdkResponse<T,TV>
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("meta")]
        public TV Meta { get; set; }
    }
}