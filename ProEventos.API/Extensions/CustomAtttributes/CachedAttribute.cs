using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ProEventos.API.Controllers.Base;

namespace DadosInCached.CustomAttribute
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        protected readonly MemoryCache ApiCache = new(new MemoryCacheOptions());
        protected int _timespan;
        protected readonly List<string> KeyList = new();

        public CachedAttribute(int timespan = 40)
        {
            _timespan = timespan;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is not BaseApiController baseApiController)
            {
                await next();
                return;
            }

            string _cachekey = context.HttpContext.Request.Headers["Referer"].ToString();

            if (IsNotCacheable(context))
            {
                await next();
                return;
            }

            if (string.IsNullOrEmpty(_cachekey) || !ApiCache.TryGetValue(_cachekey, out _))
                _cachekey = CreateCacheKey(context.HttpContext.Request, baseApiController);
            

            if (ApiCache.TryGetValue(_cachekey, out Tuple<DateTime, IActionResult> cachedResult))
            {
                if (DateTime.UtcNow < cachedResult.Item1)
                {
                    context.Result = cachedResult.Item2;
                    return;
                }
                else
                {
                    ApiCache.Remove(_cachekey);
                }
            }

            var executedContext = await next();

            ArmazenarRespostaEmCache(executedContext, baseApiController);
        }

        private void ArmazenarRespostaEmCache(ActionExecutedContext executedContext, BaseApiController baseApiController)
        {
            string _cachekey = CreateCacheKey(executedContext.HttpContext.Request, baseApiController);
            if (executedContext.Result is OkObjectResult okResult)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(_timespan));

                var resultToCache = new Tuple<DateTime, IActionResult>(DateTime.UtcNow.AddSeconds(_timespan), okResult);

                ApiCache.Set(_cachekey, resultToCache, cacheEntryOptions);
                KeyList.Add(_cachekey);
            }
        }

        protected string CreateCacheKey(HttpRequest request, BaseApiController baseApiController)
        {
            var requestPath = request.Path;
            var acceptHeader = request.Headers["Accept"].ToString();
            var requestContent = baseApiController.ReadRequestBody()?.ToString() ?? string.Empty;

            return $"{requestPath}:{acceptHeader}:{requestContent}";
        }

        protected bool IsNotCacheable(ActionExecutingContext context)
        {
            var requestMethod = context.HttpContext.Request.Method;

            if (requestMethod != "POST" && requestMethod != "GET")
            {
                CleanCache();
                return true;
            }

            return false;
        }

        protected void CleanCache()
        {
            foreach (var key in KeyList)
            {
                ApiCache.Remove(key);
            }

            KeyList.Clear();
        }
    }
}
