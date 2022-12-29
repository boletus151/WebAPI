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
    using WebAPI_BaseComponents.Responses;

    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            string json;

            context.ExceptionHandled = true;
            context.HttpContext.Response.Clear();

            if (context.Exception.GetType() == typeof(ArgumentException))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                json = JsonConvert.SerializeObject(new ErrorResponse
                {
                    HttpCode = ((int)HttpStatusCode.BadRequest).ToString(),
                    HttpMessage = Constants.ResponseMessages.BadRequestMsg,
                    MoreInformation = new List<MoreInfoError>
                    {
                        new MoreInfoError
                        {
                            Campo = "Excepción", Valor = context.Exception.Message
                        }
                    }
                });
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                json = JsonConvert.SerializeObject(new ErrorResponse
                {
                    HttpCode = ((int)HttpStatusCode.BadRequest).ToString(),
                    HttpMessage = Constants.ResponseMessages.InternalError,
                    MoreInformation = new List<MoreInfoError>
                    {
                        new MoreInfoError
                        {
                            Campo = Constants.ResponseMessages.ExceptionMsg, Valor = context.Exception.Message
                        }
                    }
                });
            }

            await context.HttpContext.Response.WriteAsync(json);
            context.HttpContext.Response.ContentType = "application/json";

            await base.OnExceptionAsync(context);
        }
    }
}
