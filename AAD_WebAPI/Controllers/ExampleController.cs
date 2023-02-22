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

namespace AAD_WebAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Quotes.Contracts;
    using Quotes.Model.UI;
    using Swashbuckle.AspNetCore.Annotations;
    using System;
    using System.Collections.Generic;
    using WebAPI_BaseComponents.Constants;
    using WebAPI_BaseComponents.Filters;
    using WebAPI_BaseComponents.Responses;

    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ExampleController : Controller
    {
        private readonly IConfiguration configuration;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExampleController" /> class.
        /// </summary>
        /// <param name="quotesRepo">The quotes repository.</param>
        public ExampleController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        ///     Get appsettings dummy
        /// </summary>
        /// <returns></returns>
        [HttpGet("configvalues")]
        [Authorize]
        [SwaggerResponse(200, Type = typeof(string), Description = ResponseMessages.SuccessMsg)]
        [SwaggerResponse(400, Type = typeof(ErrorResponse), Description = ResponseMessages.BadRequestMsg)]
        public string ConfigValues()
        {
            var values = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("clientId", configuration["AzureAd:ClientId"]),
                new KeyValuePair<string, string>("tenantId", configuration["AzureAd:TenantId"]),
                new KeyValuePair<string, string>("audience", configuration["AzureAd:Audience"]),
                new KeyValuePair<string, string>("instance", configuration["AzureAd:Instance"])
            };

            // only for testing purposes
            var ret = JsonConvert.SerializeObject(values, Formatting.Indented);
            return ret;
        }
    }
}