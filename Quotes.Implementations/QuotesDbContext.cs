namespace Quotes.Implementations
{
    using Microsoft.EntityFrameworkCore;
    using Quotes.Model.Backend;

    public class QuotesDbContext :DbContext
    {
        public QuotesDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Quote> Quotes { get; set; }
    }
}
