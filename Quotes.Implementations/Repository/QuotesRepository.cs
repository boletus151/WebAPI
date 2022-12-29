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

namespace Quotes.Implementations.Repository
{
    using System.Collections.Generic;
    using System.Linq;

    using Quotes.Contracts;
    using Quotes.Model.UI;

    public class QuotesRepository : IQuotesRepository
    {
        private readonly QuotesDbContext dbContext;

        public QuotesRepository(QuotesDbContext context)
        {
            this.dbContext = context;
        }

        public int AddQuote(Quote newQuote)
        {
            var result = this.dbContext.Add(newQuote);
            this.dbContext.SaveChanges();

            return result.Entity.Id;
        }

        public bool Delete(int id)
        {
            var q = this.GetQuoteById(id);
            if(q == null)
            {
                return false;
            }

            this.dbContext.Remove(q);
            this.dbContext.SaveChanges(true);
            return true;
        }

        public Quote GetQuoteById(int id)
        {
            //return this.dbContext.Quotes.FirstOrDefault(q => q.Id == id);
            return default;
        }

        public IEnumerable<Quote> GetQuotes()
        {
            //return this.dbContext.Quotes;
            return default;
        }
    }
}