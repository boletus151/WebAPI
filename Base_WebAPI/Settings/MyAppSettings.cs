using Quotes.Contracts;
using Quotes.Contracts.SettingsClasses;

namespace Base_WebAPI.Settings
{
    /// <summary>
    /// Configure the settings for the webapi
    /// Mapping an entire object literal to a POCO (a simple .NET class with properties) is useful for aggregating related properties.
    /// </summary>
    /// <remarks>
    /// By default, the user secrets configuration source is registered after the JSON configuration sources.
    /// Therefore, user secrets keys take precedence over keys in appsettings.json and appsettings.{Environment}.json.
    /// </remarks>
    public class MyAppSettings : IMyAppSettings
    {
        private readonly IConfiguration _configuration;

        public MyAppSettings(IConfiguration configuration)
        {
            _configuration = configuration;

            AzureAdCredentials = _configuration.GetSection("AzureAd").Get<AzureAdSettings>();
        }

        public AzureAdSettings? AzureAdCredentials { get; }

        public int MinutesTimeout { get; }

        // Not used in this AAD_WebAPI
        public string? MySecretFromTheKeyvault { get; }
    }
}