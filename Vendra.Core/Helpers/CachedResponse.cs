using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Interfaces;

namespace Vendra.Core.Helpers
{
    public class CachedResponse : Attribute, IAsyncActionFilter
    {
        private readonly int timeToLiveInSeconds;

        public CachedResponse(int timeToLiveInSeconds) 
        {
            this.timeToLiveInSeconds = timeToLiveInSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) 
        {

            var cachedService = context.HttpContext.RequestServices.GetService<IResopnseCachService>();

            var cachedKey = GenerateCachKey(context.HttpContext.Request);

            var cachedResponse = await cachedService.GetCachedRespone(cachedKey);
            
            if(!string.IsNullOrEmpty(cachedResponse)) 
            {
                
                var contentRes = new ContentResult()
                {
                    Content = cachedResponse, 
                    ContentType = "application/json", 
                    StatusCode = 200
                };
                context.Result= contentRes; 
                return;
            }
            
            var executedEndPointContext = await next.Invoke(); 
            if(executedEndPointContext.Result is OkObjectResult objectResult) 
            {
                await cachedService.CachResponseAsync(cachedKey, objectResult.Value, TimeSpan.FromSeconds(timeToLiveInSeconds));
            }



        }

        private string GenerateCachKey(HttpRequest request)
        {
            
            var keyBuilder=new StringBuilder(); 
            keyBuilder.Append($"{request.Path}");
            foreach(var (key, value) in request.Query.OrderBy(x=>x.Key)) 
            {
                keyBuilder.Append($"{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
