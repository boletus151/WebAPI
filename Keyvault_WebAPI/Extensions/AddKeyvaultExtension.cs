using Azure.Core;
using Azure.Identity;
using Quotes.Contracts.SettingsClasses;

namespace Keyvault_WebAPI.Extensions
{
    public static class AddKeyvaultExtension
    {
        public static IWebHostBuilder AddKeyVault(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
            {
                var configuration = configurationBuilder.Build();
                var keyvaultName = configuration["KeyvaultName"];
                var keyVaultUri = new Uri($"https://{keyvaultName}.vault.azure.net/");

                TokenCredential credential;

                if (webHostBuilderContext.HostingEnvironment.IsDevelopment())
                {
                    var tenantId = configuration["AzureAd:TenantId"];
                    credential = new DefaultAzureCredential(
                        new DefaultAzureCredentialOptions
                        {
                            ExcludeVisualStudioCodeCredential = true,
                            ExcludeInteractiveBrowserCredential = true,
                            ExcludeSharedTokenCacheCredential = true,
                            ExcludeAzureCliCredential = true,
                            ExcludeEnvironmentCredential = true,
                            ExcludeManagedIdentityCredential = true,
                            ExcludeVisualStudioCredential = false,
                            VisualStudioTenantId = tenantId,
                            VisualStudioCodeTenantId = tenantId,
                            SharedTokenCacheTenantId = tenantId,
                        });
                }
                else
                {
                    credential = new ManagedIdentityCredential();
                }

                configurationBuilder.AddAzureKeyVault(keyVaultUri, credential);
            });

            return webHostBuilder;
        }
    }
}
