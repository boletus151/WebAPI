using Quotes.Contracts;
using Quotes.Model.UI;
using Quotes.Model.UI.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Quotes.Implementations.Mocks
{
    public class QuotesRepositoryMockData : IQuotesRepository
    {
        private List<Quote> quotesList;

        public QuotesRepositoryMockData()
        {
            this.quotesList = GetAllDummyQuotes();
        }

        public int AddQuote(Quote newQuote)
        {
            newQuote.Id = quotesList.Any() ? quotesList.Max(e => e.Id) + 1 : 1;
            quotesList.Add(newQuote);

            return newQuote.Id;
        }

        public bool Delete(int id)
        {
            var q = GetQuoteById(id);
            quotesList.Remove(q);
            return true;
        }

        public Quote GetQuoteById(int id)
        {
            return quotesList.FirstOrDefault(elem => elem.Id == id);
        }

        public IEnumerable<Quote> GetQuotes()
        {
            return quotesList;
        }

        private static List<Quote> GetAllDummyQuotes()
        {
            return new List<Quote>
            {
                new Quote { Id = 1, QuoteType =  QuoteTypes.Saying, Author = "Refranero", QuoteText = "Burro grande ande o no ande" },
                new Quote { Id = 2, QuoteType =  QuoteTypes.Saying, Author = "Refranero", QuoteText = "Cada mochuelo a su olivo" },
                new Quote { Id = 3, QuoteType =  QuoteTypes.Saying, Author = "Refranero", QuoteText = "Y vuelta la burra al trigo" },
                new Quote { Id = 4, QuoteType =  QuoteTypes.Saying, Author = "Refranero", QuoteText = "2 que duermen en el mismo colchón se vuelven de la misma condición" }
            };
        }
    }
}