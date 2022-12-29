namespace Quotes.Contracts.SettingsClasses
{
    public class AzureAdSettings
    {
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public string Instance { get; set; }
        public string Audience { get; set; }
    }
}
