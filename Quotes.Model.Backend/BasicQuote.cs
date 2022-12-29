using Quotes.Model.Backend.Enums;
using System;
using System.Text.Json.Serialization;

namespace Quotes.Model.Backend
{
    public class BasicQuote
    {

        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("QuoteType")]
        public int QuoteType { get; set; }

        [JsonPropertyName("QuoteText")]
        public string QuoteText { get; set; }

        [JsonPropertyName("SetAuthor")]
        public string SetAuthor { get; set; }

        [JsonPropertyName("Version")]
        public int Version { get; set; }

        [JsonPropertyName("CreationDate")]
        public DateTime CreationDate { get; set; }

        [JsonPropertyName("LastUpdateDate")]
        public DateTime LastUpdateDate { get; set; }
    }
}
