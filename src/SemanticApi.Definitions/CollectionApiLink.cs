namespace SemanticApi.Definitions
{
    public class CollectionApiLink : IApiLink
    {
        public string Rel { get; }
        public string Href { get; }


        public CollectionApiLink(string collectionName, string href) {
            Rel = $"collection/{collectionName}";
            Href = href;
        }
    }
}