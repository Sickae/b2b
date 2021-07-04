using System.Reflection;
using B2B.DataAccess.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace B2B.Logic.Infrastructure
{
    public class CustomJsonContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProp = base.CreateProperty(member, memberSerialization);
            if (jsonProp.PropertyType.IsGenericType
                || jsonProp.PropertyType.IsClass && jsonProp.PropertyType.Namespace != "System"
                || member.IsDefined(typeof(SkipLogAttribute)))
                jsonProp.Ignored = true;

            return jsonProp;
        }
    }
}
