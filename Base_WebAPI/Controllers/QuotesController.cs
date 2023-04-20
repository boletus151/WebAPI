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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quotes.Contracts;
using Quotes.Model.UI;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI_BaseComponents.Constants;

namespace Base_WebAPI.Controllers
{
    [Route("api/quotes")]
    [ApiController]
    [ApiVersion("1.0")]
    public class QuotesController : Controller
    {
        protected readonly IQuotesRepository quotesRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="QuotesController" /> class.
        /// </summary>
        /// <param name="quotesRepo">The quotes repository.</param>
        public QuotesController(IQuotesRepository quotesRepo)
        {
            this.quotesRepository = quotesRepo;
        }

        /// <summary>
        ///     Creates a new quote.
        /// </summary>
        /// <param name="quote">The quote.</param>
        /// <returns>The identifier of the new quote</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [SwaggerResponse(201, Type = typeof(string), Description = ResponseMessages.SuccessMsg)]
        [SwaggerResponse(400, Description = ResponseMessages.BadRequestMsg)]
        [SwaggerResponse(401, Description = ResponseMessages.UnauthorizedMsg)]
        [SwaggerResponse(500, Description = ResponseMessages.InternalError)]
        public int Create(Quote quote)
        {
            var id = this.quotesRepository.AddQuote(quote);
            return id;
        }

        /// <summary>
        ///     Deletes a quote.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if quote was deleted, false otherwise</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [SwaggerResponse(204, Type = typeof(bool), Description = ResponseMessages.Deleted)]
        [SwaggerResponse(400, Description = ResponseMessages.BadRequestMsg)]
        [SwaggerResponse(401, Description = ResponseMessages.UnauthorizedMsg)]
        [SwaggerResponse(404, Description = ResponseMessages.NotFoundMsg)]
        [SwaggerResponse(500, Description = ResponseMessages.InternalError)]
        public bool DeleteById(int id)
        {
            return this.quotesRepository.Delete(id);
        }

        /// <summary>
        ///     Get a list of quotes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(Quote), Description = ResponseMessages.SuccessMsg)]
        [SwaggerResponse(400, Description = ResponseMessages.BadRequestMsg)]
        [SwaggerResponse(401, Description = ResponseMessages.UnauthorizedMsg)]
        [SwaggerResponse(404, Description = ResponseMessages.NotFoundMsg)]
        [SwaggerResponse(500, Description = ResponseMessages.InternalError)]
        public IEnumerable<Quote> GetAll()
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
        [SwaggerResponse(400, Description = ResponseMessages.BadRequestMsg)]
        [SwaggerResponse(401, Description = ResponseMessages.UnauthorizedMsg)]
        [SwaggerResponse(404, Description = ResponseMessages.NotFoundMsg)]
        [SwaggerResponse(500, Description = ResponseMessages.InternalError)]
        public virtual Quote GetById(int id)
        {
            return this.quotesRepository.GetQuoteById(id);
        }
    }
    
    //[Route("api/quotes")]
    //[ApiController]
    //[ApiVersion("2.0")]
    //public class QuotesV2Controller : QuotesController
    //{

    //    /// <summary>
    //    ///     Initializes a new instance of the <see cref="QuotesController" /> class.
    //    /// </summary>
    //    /// <param name="quotesRepo">The quotes repository.</param>
    //    public QuotesV2Controller(IQuotesRepository quotesRepo): base(quotesRepo)
    //    {
    //    }

    //    [HttpGet("{id}")]
    //    public override Quote Get(int id)
    //    {
    //        return new Quote();
    //    }
    //}
}