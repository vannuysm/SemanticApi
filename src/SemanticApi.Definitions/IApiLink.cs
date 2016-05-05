namespace SemanticApi.Definitions
{
    public interface IApiLink
    {
        string Rel { get; }
        string Href { get; }
    }
}