using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quotes.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI_BaseComponents.Constants;
using WebAPI_BaseComponents.Responses;

namespace Keyvault_WebAPI.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet("test")]
        [MapToApiVersion("2.0")]
        [SwaggerResponse(200, Type = typeof(string), Description = ResponseMessages.SuccessMsg)]
        public string Get2()
        {
            return JsonConvert.SerializeObject("This is a test for v2.0", Formatting.Indented);
        }
    }
}
