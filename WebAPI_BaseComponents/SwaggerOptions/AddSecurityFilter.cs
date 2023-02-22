using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI_BaseComponents.SwaggerOptions
{
    public class AddSecurityFilter : IOperationFilter
    {
        private string exampletext = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI...";

        //public AuthorizationHeaderFilter(string value = "value")
        //{
        //    if (string.IsNullOrEmpty(value))
        //        throw new ArgumentNullException(nameof(value));

        //    this.exampletext = value;
        //}

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //if (operation.Parameters == null)
            //    operation.Parameters = new List<OpenApiParameter>();

            //operation.Parameters.Add(new OpenApiParameter
            //{
            //    Name = "My parameter..",
            //    Description = "My parameter description",
            //    AllowEmptyValue= false,
            //    In = ParameterLocation.Header,
            //    //In = ParameterLocation.Query,
            //    //In = ParameterLocation.Path,
            //    Example = new OpenApiString(this.exampletext),
            //    Required = true // set to false if this is optional
            //});

            operation.Security ??= new List<OpenApiSecurityRequirement>();


            // Get Authorize attribute
            var attributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                                    .Union(context.MethodInfo.GetCustomAttributes(true))
                                    .OfType<AuthorizeAttribute>();
            var authorizeAttributes = attributes
                                    .OfType<AuthorizeAttribute>();

            if (attributes != null && attributes.Count() > 0)
            {
                var attr = attributes.ToList()[0];

                // Add what should be show inside the security section
                var securityInfos = new List<string>
                {
                    $"{nameof(AuthorizeAttribute.Policy)}:{attr.Policy}",
                    $"{nameof(AuthorizeAttribute.Roles)}:{attr.Roles}",
                    $"{nameof(AuthorizeAttribute.AuthenticationSchemes)}:{attr.AuthenticationSchemes}"
                };

                switch (attr.AuthenticationSchemes?.ToLower())
                {
                    case "basic":
                        operation.Security = new List<OpenApiSecurityRequirement>()
                   {
                        new OpenApiSecurityRequirement()
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Id = "basic", // Must fit the defined Id of SecurityDefinition in global configuration
                                        Type = ReferenceType.SecurityScheme,
                                    }
                                },
                                securityInfos
                            }
                        }
                    };
                        break;

                    case "bearer": // = JwtBearerDefaults.AuthenticationScheme
                    default:
                        operation.Security = new List<OpenApiSecurityRequirement>()
                    {
                        new OpenApiSecurityRequirement()
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Id = "bearer", // Must fit the defined Id of SecurityDefinition in global configuration
                                        Type = ReferenceType.SecurityScheme
                                    }
                                },
                                securityInfos
                            }
                        }
                    };
                        break;
                }
            }
            else
            {
                operation.Security.Clear();
            }
        }
    }
}
