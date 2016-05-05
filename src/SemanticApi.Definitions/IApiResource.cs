using System.Collections.Generic;

namespace SemanticApi.Definitions
{
    public interface IApiResource
    {
        string Id { get; }
        IDictionary<string, IApiLink> _Links { get; }
    }
}