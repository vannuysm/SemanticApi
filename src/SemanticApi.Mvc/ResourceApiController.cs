using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SemanticApi.Definitions;

namespace SemanticApi.Mvc
{
    public class ResourceApiController : ControllerBase
    {
        [NonAction]
        public IActionResult FilteredResult<TModel, TResource>(
            IEnumerable<string> acceptableFilterCriteria,
            Func<List<Expression<Func<TModel, bool>>>, IEnumerable<TResource>> callback
        ) where TResource : IApiResource {
            var criteria = Request.Query.Where(q => acceptableFilterCriteria.Contains(q.Key)).ToList();
            
            if (!criteria.Any()) {
                var formattedCriteriaMembers = $"'{string.Join("', '", acceptableFilterCriteria)}'";
                return BadRequest($"You must specify a filter criteria. Valid filter criteria: [{formattedCriteriaMembers}]");
            }
            
            if (criteria.Any(c => c.Value.Count > 1)) {
                return BadRequest($"Each filter criteria must only appear once in the query string");
            }
    
            var filters = new List<Expression<Func<TModel, bool>>>();
            
            foreach (var item in criteria) {
                var expression = GetExpression<TModel>(item);
                filters.Add(expression);
            }
            
            return Ok(callback(filters));
        }
        
        private Expression<Func<T, bool>> GetExpression<T>(KeyValuePair<string, StringValues> item) {
            var typeParameter = Expression.Parameter(typeof(T));
            Expression property = typeParameter;
            
            var propertySegments = PropertySegments(item.Key);
            foreach (var segment in propertySegments) {
                property = Expression.Property(property, segment);
            }
            
            var values = item.Value.First().Split(',');
            var firstValue = Expression.Constant(values.First());
            var filter = Expression.Equal(property, firstValue);
            
            foreach (var otherValue in values.Skip(1)) {
                filter = Expression.OrElse(filter, Expression.Equal(property, Expression.Constant(otherValue)));
            }

            return Expression.Lambda<Func<T, bool>>(filter, typeParameter);
        }
        
        private static IEnumerable<string> PropertySegments(string value) {
            var segments = value.Split('.');
            var output = new List<string>();
            
            foreach (var segment in segments) {
                output.Add($"{char.ToUpper(segment.First())}{segment.Substring(1)}");
            }
            
            return output;
        }
    }
}