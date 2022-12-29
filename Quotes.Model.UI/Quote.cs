using System.Text.Json.Serialization;

namespace Quotes.Model.UI
{
    public class Quote : BasicQuote
    {
        public Quote()
        {
            this.Author = "Refranero";
        }

        [JsonPropertyName("Author")]
        public string Author { get; set; }

        [JsonPropertyName("Meaning")]
        public string  Meaning { get; set; }
    }
}
