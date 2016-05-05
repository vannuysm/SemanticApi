using System.Collections.Generic;

namespace SemanticApi.Definitions
{
    public class BasicApiResource : IApiResource
    {
        public string Id { get; }
        public IDictionary<string, IApiLink> _Links { get; }

        public BasicApiResource(IApiResource resource) {
            Id = resource.Id;
            _Links = resource._Links;
        }
    }
}