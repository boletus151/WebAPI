using Quotes.Contracts;
using Quotes.Contracts.SettingsClasses;

namespace Keyvault_WebAPI.Settings
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

            MySecretFromTheKeyvault = _configuration["DummySecret"];
        }

        public AzureAdSettings? AzureAdCredentials { get; }

        public int MinutesTimeout { get; }

        /// <summary>
        /// This value will be extracted from the keyvault.
        /// For testing purposes we are going to use it from an endpoint
        /// </summary>
        public string? MySecretFromTheKeyvault { get; }
    }
}