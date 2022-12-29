using Quotes.Model.UI.Enums;
using System.Text.Json.Serialization;

namespace Quotes.Model.UI
{
    public class BasicQuote
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("QuoteType")]
        public QuoteTypes QuoteType { get; set; }

        [JsonPropertyName("QuoteText")]
        public string QuoteText { get; set; }
    }
}
