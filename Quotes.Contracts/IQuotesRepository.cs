using Quotes.Model.UI;
using System.Collections.Generic;

namespace Quotes.Contracts
{
    public interface IQuotesRepository
    {
        int AddQuote(Quote newQuote);

        bool Delete(int id);

        Quote GetQuoteById(int id);

        IEnumerable<Quote> GetQuotes();
    }
}
