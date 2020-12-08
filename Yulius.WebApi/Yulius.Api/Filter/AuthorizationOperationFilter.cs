using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Yulius.Api.Filter
{
    /// <summary>
    /// AuthorizationHeaderParameterOperationFilter para introducir JWT en dialogo Swagger
    /// </summary>
    public class AuthorizationOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            var security = false;
            var requiredScopes = context.MethodInfo.GetCustomAttributes(true);
            foreach (var item in requiredScopes)
            {
                if (item.ToString() == "Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute")
                {
                    security = true;
                    return;
                }
            }  
            
            if(!security)
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "access token",
                    Required = false,
                    Schema = new OpenApiSchema
                    {
                        Type = "String",
                        Default = new OpenApiString("Bearer ")
                    }
                });
            }

        }
    }
}
