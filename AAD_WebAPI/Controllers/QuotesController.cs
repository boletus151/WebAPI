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
    [HandleResult]
    [HandleException]
    public class QuotesController : Controller
    {
        private readonly IQuotesRepository quotesRepository;
        private readonly IConfiguration configuration;

        /// <summary>
        ///     Initializes a new instance of the <see cref="QuotesController" /> class.
        /// </summary>
        /// <param name="quotesRepo">The quotes repository.</param>
        public QuotesController(IQuotesRepository quotesRepo, IConfiguration configuration)
        {
            this.quotesRepository = quotesRepo;
            this.configuration = configuration;
        }

        /// <summary>
        ///     Creates a new quote.
        /// </summary>
        /// <param name="quote">The quote.</param>
        /// <returns>The identifier of the new quote</returns>
        [HttpPost]
        [Authorize]
        [SwaggerResponse(201, Type = typeof(SuccessResponse), Description = ResponseMessages.Created)]
        public int Post([FromBody] Quote quote)
        {
            if (quote == null) return 0;
            var id = this.quotesRepository.AddQuote(quote);
            return id;
        }

        /// <summary>
        ///     Deletes a quote.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if quote was deleted, false otherwise</returns>
        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerResponse(204, Type = typeof(bool), Description = ResponseMessages.Deleted)]
        [SwaggerResponse(401, Type = typeof(bool), Description = ResponseMessages.UnauthorizedMsg)]
        public bool Delete(int id)
        {
            return this.quotesRepository.Delete(id);
        }

        /// <summary>
        ///     Get a list of quotes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(Quote), Description = ResponseMessages.SuccessMsg)]
        public IEnumerable<Quote> Get()
        {
            return this.quotesRepository.GetQuotes();
        }

        /// <summary>
        ///     Get a quote by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The specific quote</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<Quote>), Description = ResponseMessages.SuccessMsg)]
        public Quote Get(int id)
        {
            return this.quotesRepository.GetQuoteById(id);
        }

        /// <summary>
        /// Updates the specified quote.
        /// </summary>
        /// <param name="quote">The quote.</param>
        /// <returns>The modified quote</returns>
        [HttpPatch]
        [SwaggerResponse(201, Type = typeof(SuccessResponse), Description = ResponseMessages.Created)]
        
        public Quote Update([FromBody] Quote quote)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Get appsettings dummy
        /// </summary>
        /// <returns></returns>
        [HttpGet("configvalues")]
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

            var ret = JsonConvert.SerializeObject(values, Formatting.Indented);
            return ret;
        }
    }
}