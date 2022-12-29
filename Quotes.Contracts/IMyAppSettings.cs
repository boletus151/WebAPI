using Quotes.Contracts.SettingsClasses;
using System.Threading;

namespace Quotes.Contracts
{
    /// <summary>
    /// Configure the settings for the webapi
    /// Mapping an entire object literal to a POCO (a simple .NET class with properties) is useful for aggregating related properties.
    /// </summary>
    /// <remarks>
    /// By default, the user secrets configuration source is registered after the JSON configuration sources. 
    /// Therefore, user secrets keys take precedence over keys in appsettings.json and appsettings.{Environment}.json.
    /// </remarks>
    public interface IMyAppSettings
    {
        AzureAdSettings AzureAdCredentials { get; }

        int MinutesTimeout { get; }

        string MySecretFromTheKeyvault { get; }
    }
}
