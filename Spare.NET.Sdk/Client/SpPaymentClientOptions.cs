using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Spare.NET.Security.Serialization;

namespace Spare.NET.Sdk.Client
{
    public class SpPaymentClientOptions
    {
        public Uri BaseUrl { get; set; }

        public string AppId { get; set; }

        public string ApiKey { get; set; }

        public IWebProxy Proxy { get; set; } = null;

        public JsonSerializerSettings SerializerSettings { get; set; } = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Culture = CultureInfo.CurrentCulture,
            ContractResolver = new SpOrderedContractResolver(),
            Converters = new List<JsonConverter> {new SpDecimalToStringJsonConverter(), new StringEnumConverter()}
        };
    }
}