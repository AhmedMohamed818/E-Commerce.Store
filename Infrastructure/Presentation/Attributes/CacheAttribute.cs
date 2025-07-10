using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    public class CacheAttribute (int durationInSec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;

            var cachKey = GenerateCacheKey(context.HttpContext.Request); 

            var result = await cacheService.GetCasheVakueAsync(cachKey);
            {
                if (string.IsNullOrEmpty(result))
                {
                    context.Result = new ContentResult() 
                    { 
                        Content = result, 
                        StatusCode = StatusCodes.Status404NotFound, 
                        ContentType = "application/json" 
                    };
                    return;
                   
                }
                // Execute the action if cache is not found ( End point is not cached)
               var contextResult =  await next.Invoke();
                if (contextResult.Result is OkObjectResult okObject && okObject.Value != null)
                {
                    // Cache the result
                    await cacheService.SetCasheValueAsync(cachKey, okObject.Value, TimeSpan.FromMinutes(durationInSec));
                }
            }
        }


        private string GenerateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);
            foreach (var query in request.Query.OrderBy(q => q.Key))
            {
                key.Append($" | {query.Key} - {query.Value}");
            }
            return key.ToString();
           
        }





















    }
}
