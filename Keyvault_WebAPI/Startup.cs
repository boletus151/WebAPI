using Keyvault_WebAPI.Configurations;
using Keyvault_WebAPI.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Quotes.Contracts;
using Quotes.Implementations.Mocks;

namespace Keyvault_WebAPI
{
    public class Startup
    {
        public const string ApiTitle = "Azure Keyvault Example WebApi";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register Contracts and Implementations
            // Register automapper classes
            RegisterOwnServices(ref services);
            services.AddControllers();

            services.AddApiVersioning(op =>
            {
                op.DefaultApiVersion = new ApiVersion(1, 0);
                op.ApiVersionReader = new HeaderApiVersionReader() { HeaderNames = { "api-version" } };
                op.AssumeDefaultVersionWhenUnspecified = true;
                op.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(op =>
            {
                op.GroupNameFormat = "'v'VV";
                op.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen();

            // as IApiVersionDescriptionProvider cannot be injected,
            // a new class should be create
            // Tutorial: https://referbruv.com/blog/integrating-aspnet-core-api-versions-with-swagger-ui/
            services.ConfigureOptions<ConfigureSwaggerOptions>();

            // add wide-open CORS.
            // this should be changed before deploying to production
            // to ensure that only your accepted origins can call the API.
            services.AddCors(o => o.AddPolicy("default", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    foreach (var item in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        var url = $"{item.GroupName}/swagger.json";
                        c.SwaggerEndpoint(url, item.GroupName.ToString());
                    }
                });
            }
            else
            {
                app.UseHsts();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = string.Empty;

                    foreach (var item in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        var url = $"swagger/{item.GroupName}/swagger.json";
                        c.SwaggerEndpoint(url, item.GroupName.ToString());
                    }
                });
            }
            app.UseCors("default");
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //.RequireAuthorization(); // adds auth to ALL endpoints of the API
            });
        }

        private static void RegisterOwnServices(ref IServiceCollection services)
        {
            services.AddScoped<IMyAppSettings, MyAppSettings>();
            services.AddScoped<IQuotesRepository, QuotesRepositoryMockData>();

            //services.AddMemoryCache();
            //services.AddHttpContextAccessor();
        }
    }
}