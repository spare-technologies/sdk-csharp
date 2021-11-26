using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Spare.NET.Security.Serialization;

namespace Spare.NET.Security
{
    public static class SpExtensions
    {
        public static string ToJsonString(this object o) => JsonConvert.SerializeObject(
            o, new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Culture = CultureInfo.CurrentCulture,
                ContractResolver = new SpOrderedContractResolver(),
                Converters = new List<JsonConverter> {new SpDecimalToStringJsonConverter(), new StringEnumConverter()}
            });

        public static byte[] GetJsonBytes(this object o) => Encoding.UTF8.GetBytes(o.ToJsonString());
    }
}