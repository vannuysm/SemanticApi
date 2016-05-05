using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SemanticApi.Mvc
{
    public class ExpandAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context) {
            var request = context.HttpContext.Request;
            List<string> expansions;
            
            if (request.Query.ContainsKey("expand")) {
                var values = request.Query["expand"][0];
                expansions = values.Split(',').ToList();
            }
            else {
                expansions = new List<string>();
            }
                
            context.ActionArguments.Add("expansions", new ReadOnlyCollection<string>(expansions));
            base.OnActionExecuting(context);
        }
        
        private ReadOnlyCollection<string> ProcessExpansions() {
            
            
            return new ReadOnlyCollection<string>(new List<string>());
        }
    }
}