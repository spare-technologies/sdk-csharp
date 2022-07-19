using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Spare.NET.Security.Serialization
{
    public sealed class SpOrderedContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Json key ordering contract
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override System.Collections.Generic.IList<JsonProperty> CreateProperties(System.Type type,
            MemberSerialization memberSerialization)
        {
            return base.CreateProperties(type, memberSerialization).OrderBy(p => p.PropertyName).ToList();
        }
    }
}