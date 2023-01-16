using Microsoft.AspNetCore.Mvc;
using Quotes.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI_BaseComponents.Constants;
using WebAPI_BaseComponents.Responses;

namespace Keyvault_WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class TestController : ControllerBase
    {
        private readonly IMyAppSettings myAppSettings;

        public TestController(IMyAppSettings myAppSettings)
        {
            this.myAppSettings = myAppSettings;
        }

        /// <summary>
        ///     Get a dummy secret from the keyvault.
        /// </summary>
        /// <returns></returns>
        [HttpGet("show-dummy-secret")]
        [MapToApiVersion("2.0")]
        [SwaggerResponse(200, Type = typeof(string), Description = ResponseMessages.SuccessMsg)]
        [SwaggerResponse(400, Type = typeof(ErrorResponse), Description = ResponseMessages.BadRequestMsg)]
        public string Get2()
        {
            return $"This is test";
        }
    }
}
