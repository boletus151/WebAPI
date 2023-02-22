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
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Linq;
    using System.Net;
    using WebAPI_BaseComponents.Constants;
    using WebAPI_BaseComponents.Responses;

    /// <summary>
    ///     Clase que permite personalizar la información del error 404
    /// </summary>
    public class HandleResultAttribute : ResultFilterAttribute
    {
        /// <summary>
        ///     Método que personaliza el error
        /// </summary>
        /// <param name="context">Contexto del error</param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var resultType = context.Result?.GetType();
            if (resultType == typeof(BadRequestResult) || resultType == typeof(BadRequestObjectResult))
            {
                context.Result = new ObjectResult(new ErrorResponse
                {
                    HttpCode = ((int)HttpStatusCode.BadRequest).ToString(),
                    HttpMessage = ResponseMessages.BadRequestMsg,
                    MoreInformation = context.ModelState.Select(e => new MoreInfoError(e.Key, e.Value?.Errors?.FirstOrDefault()?.ErrorMessage))
                })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            base.OnResultExecuting(context);
        }
    }
}
