using Serilog.Core;
using Serilog.Events;

namespace ResidentAPI.CustomEnrichers
{
    public class HttpRequestAndCorrelationContextEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpRequestAndCorrelationContextEnricher(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            // Get HttpContext properties here
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                // Add properties to the log event based on HttpContext
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("RequestMethod", httpContext.Request.Method));
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("RequestPath", httpContext.Request.Path));
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserAgent", httpContext.Request.Headers["User-Agent"]));

                //Let us say we get correlationid passed in via request header, let us see how we can pull and populate that
                if(httpContext.Request.Headers.TryGetValue("App-Correlation-Id", out var appCorrelationId))
                {
                    logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CorrelationId", appCorrelationId));
                }
            }           

        }
    }
}
