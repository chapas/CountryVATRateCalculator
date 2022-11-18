using CountryVATCalculator.Constants;
using CountryVATCalculator.Services;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;

namespace CountryVATCalculator.Middleware
{
    /// <summary>
    /// Logging http header information for context
    /// </summary>
    public class HeaderLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public HeaderLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IHttpHeaderAccessor httpHeaderAccessor)
        {
            using (LogContext.PushProperty(HttpHeaderConstants.CorrelationId, httpHeaderAccessor.CorrelationId))
            {
                await _next(httpContext);
            }
        }
    }
}