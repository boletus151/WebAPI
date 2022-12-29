using Microsoft.AspNetCore.Mvc;
using Quotes.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI_BaseComponents.Constants;
using WebAPI_BaseComponents.Responses;

namespace Keyvault_WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ExamplesController : Controller
    {
        private readonly IMyAppSettings myAppSettings;

        public ExamplesController(IMyAppSettings myAppSettings)
        {
            this.myAppSettings = myAppSettings;
        }

        /// <summary>
        ///     Get a dummy secret from the keyvault.
        /// </summary>
        /// <returns></returns>
        [HttpGet("show-dummy-secret")]
        [SwaggerResponse(200, Type = typeof(string), Description = ResponseMessages.SuccessMsg)]
        [SwaggerResponse(400, Type = typeof(ErrorResponse), Description = ResponseMessages.BadRequestMsg)]
        public string Get()
        {
            return $"The secret is: {this.myAppSettings.MySecretFromTheKeyvault}";
        }
    }
}
