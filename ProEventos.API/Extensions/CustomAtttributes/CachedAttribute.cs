using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DadosInCached.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        protected readonly MemoryCache ApiCache = new(new MemoryCacheOptions());
        protected int _expirationTime;
        protected readonly List<string> KeyList = new();

        public CachedAttribute(int expirationTime = 5)
        {
            _expirationTime = expirationTime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string _cachekey = CreateCacheKey(context.HttpContext.Request);

            if (context.HttpContext.Request.Method != "GET")
            {
                CleanCache();
                await next();
                return;
            }

            if (ApiCache.TryGetValue(_cachekey, out IActionResult cachedResult))
            {
                context.Result = cachedResult;
                return;
            }

            var executedContext = await next();
            ArmazenarRespostaEmCache(executedContext);
        }

        private void ArmazenarRespostaEmCache(ActionExecutedContext context)
        {
            if (context.Result is OkObjectResult okResult)
            {
                string cacheKey = CreateCacheKey(context.HttpContext.Request);

                ApiCache.Set(cacheKey, okResult,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(_expirationTime)));

                KeyList.Add(cacheKey);
            }
        }

        protected string CreateCacheKey(HttpRequest request)
        {
            string baseUri = $"{request.Scheme}://{request.Host.Value}";
            string fullPath = $"{request.Path.Value}{request.QueryString.Value}";

            return $"{baseUri}{fullPath}";
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
