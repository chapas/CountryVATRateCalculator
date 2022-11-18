using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CountryVATCalculator.Filters
{
    public class HeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(
                new OpenApiParameter
                {
                    Name = Constants.HttpHeaderConstants.CorrelationId,
                    In = ParameterLocation.Header,
                    Description = "An ID that can be used to trace the request across multiple application boundaries",
                    Required = true,
                    Schema = new OpenApiSchema()
                    {
                        //Type = "String",
                        //Format = "uuid", // https://swagger.io/docs/specification/data-models/data-types/
                        Default = new OpenApiString(Guid.NewGuid().ToString())
                    }                    
                });
        }
    }
}