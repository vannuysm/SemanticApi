namespace SemanticApi.Definitions
{
    public class ResourceApiLink : IApiLink
    {
        public string Id { get; }
        public string Rel { get; }
        public string Href { get; }


        public ResourceApiLink(string resourceName, string href, string id) {
            Rel = $"resource/{resourceName}";
            Href = href;
            Id = id;
        }
    }
}