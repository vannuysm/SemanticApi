using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SemanticApi.Mvc
{
    public class ExpandAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context) {
            var request = context.HttpContext?.Request;
            var expansions = new List<string>();
            
            if (request != null && request.Query.ContainsKey("expand")) {
                var values = request.Query["expand"][0];
                expansions.AddRange(values.Split(','));
            }
                
            context.ActionArguments["expansions"] = new ReadOnlyCollection<string>(expansions);
            base.OnActionExecuting(context);
        }
    }
}