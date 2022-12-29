using Keyvault_WebAPI.Extensions;

namespace Keyvault_WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            webBuilder
                .AddKeyVault()
                .UseStartup<Startup>()
            );
    }
}