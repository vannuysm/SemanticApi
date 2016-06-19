using Newtonsoft.Json.Serialization;

namespace SemanticApi.Mvc
{
    public class SemanticApiCamelCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            if (propertyName.StartsWith("_")) {
                var camelCase = char.ToLowerInvariant(propertyName[1]).ToString();
                return $"_{camelCase}{propertyName.Substring(2)}";
            }

            return base.ResolvePropertyName(propertyName);
        }
    }
}