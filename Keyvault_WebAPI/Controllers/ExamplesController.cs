using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quotes.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI_BaseComponents.Constants;
using WebAPI_BaseComponents.Responses;

namespace Keyvault_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class Examples1Controller : Controller
    {
        private readonly IMyAppSettings myAppSettings;

        public Examples1Controller(IMyAppSettings myAppSettings)
        {
            this.myAppSettings = myAppSettings;
        }

        /// <summary>
        ///     Get a dummy secret from the keyvault.
        /// </summary>
        /// <returns></returns>
        [HttpGet("show-dummy-secret")]
        [SwaggerResponse(200, Type = typeof(string), Description = ResponseMessages.SuccessMsg)]
        public string Get1()
        {
            return JsonConvert.SerializeObject($"The secret is: {this.myAppSettings.MySecretFromTheKeyvault}");
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class Examples2Controller : Controller
    {
        private readonly IMyAppSettings myAppSettings;

        public Examples2Controller(IMyAppSettings myAppSettings)
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
        public string Get2()
        {
            return JsonConvert.SerializeObject($"The version 2 does not retrive the secret anymore");
        }

        /// <summary>
        ///     Get a dummy secret from the keyvault.
        /// </summary>
        /// <returns></returns>
        [HttpGet("new-endpoint")]
        [MapToApiVersion("2.0")]
        [SwaggerResponse(200, Type = typeof(string), Description = ResponseMessages.SuccessMsg)]
        public string GetNew()
        {
            return JsonConvert.SerializeObject($"This is a new endpoint only available at V2");
        }
    }
}
