// -------------------------------------------------------------------------------------------------------------------
//  <summary>
//     This implementation is a group of the offers of several persons along the network;
//     because of this, it is under Creative Common By License:
//
//     You are free to:
//
//     Share — copy and redistribute the material in any medium or format
//     Adapt — remix, transform, and build upon the material for any purpose, even commercially.
//
//     The licensor cannot revoke these freedoms as long as you follow the license terms.
//
//     Under the following terms:
//
//     Attribution — You must give appropriate credit, provide a link to the license, and indicate if changes were made. You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.
//     No additional restrictions — You may not apply legal terms or technological measures that legally restrict others from doing anything the license permits.
//
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace WebAPI_BaseComponents.Filters
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using WebAPI_BaseComponents.Exceptions;
    using WebAPI_BaseComponents.Responses;

    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            string json;

            context.ExceptionHandled = true;
            context.HttpContext.Response.Clear();

            var httpStatusCode = context.HttpContext.Response.StatusCode.ToString();
            var httpMessage = context.HttpContext.Response.ToString();

            ErrorResponse errorResp;

            if (context.Exception.GetType() == typeof(InvalidBackendCastException))
            {
                var ex = (InvalidBackendCastException)context.Exception;
                errorResp = new ErrorResponse
                {
                    HttpCode = httpStatusCode,
                    HttpMessage = httpMessage,
                    Exception = context.Exception,
                    Message = context.Exception.Message,
                    MoreInformation = new List<MoreInfoError>
                    {
                        new MoreInfoError(nameof(ex.CustomMessage),ex.CustomMessage),
                        new MoreInfoError(nameof(ex.Endpoint),ex.Endpoint),
                        new MoreInfoError(nameof(ex.ServiceResponse),ex.ServiceResponse),
                    }

                };
            }
            else
            {
                string stacktrace = JsonConvert.SerializeObject(context.Exception.StackTrace);
                string innerex = JsonConvert.SerializeObject(context.Exception.InnerException);

                errorResp = new ErrorResponse
                {
                    HttpCode = httpStatusCode,
                    HttpMessage = httpMessage,
                    Exception = context.Exception,
                    Message = context.Exception.Message,
                    MoreInformation = new List<MoreInfoError>
                    {
                        new MoreInfoError(nameof(context.Exception.StackTrace), stacktrace),
                        new MoreInfoError(nameof(context.Exception.InnerException), innerex),
                    }

                };
            }

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            json = JsonConvert.SerializeObject(errorResp);

            context.HttpContext.Response.ContentType = "application/json";
            await context.HttpContext.Response.WriteAsync(json);

            await base.OnExceptionAsync(context);
        }
    }
}
