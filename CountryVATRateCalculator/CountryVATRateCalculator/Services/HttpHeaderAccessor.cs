using CountryVATCalculator.Constants;
using Microsoft.AspNetCore.Http;
using System;

namespace CountryVATCalculator.Services
{
    public interface IHttpHeaderAccessor
    {
        Guid CorrelationId { get; }
    }

    public class HttpHeaderAccessor : IHttpHeaderAccessor
    {
        private readonly IHttpContextAccessor contextAccessor;

        public HttpHeaderAccessor(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public Guid CorrelationId => GetCorrelationId(contextAccessor.HttpContext);

        protected Guid GetCorrelationId(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue(HttpHeaderConstants.CorrelationId, out var idText))
            {
                if (Guid.TryParse(idText, out Guid correlationId))
                {
                    return correlationId;
                }

                throw new NotImplementedException("Gained header information but not parsed successfully");
            }

            return Guid.Empty;
        }
    }
}
