namespace SemanticApi.Definitions
{
    public class SelfApiLink : IApiLink
    {
        public string Rel { get; } = "self";
        public string Href { get; }

        public SelfApiLink(string href) {
            Href = href;
        }
    }
}