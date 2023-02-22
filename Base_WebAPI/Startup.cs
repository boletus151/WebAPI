using Base_WebAPI.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quotes.Contracts;
using Quotes.Implementations.Mocks;
using System.Diagnostics;
using WebAPI_BaseComponents.Filters;
using WebAPI_BaseComponents.SwaggerOptions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AAD_WebAPI
{
    public class Startup
    {
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

            //add authentication support(bearer token validation).
            // configuration values are pulled from "AzureAD" section of app settings config.
            // this uses Microsoft Identity platform(AAD v2.0).
            services.AddMicrosoftIdentityWebApiAuthentication(Configuration, "AzureAd", subscribeToJwtBearerMiddlewareDiagnosticsEvents: true);
            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RequireSignedTokens = false,
                    ValidateIssuerSigningKey = false,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    RequireExpirationTime = false
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Debug.WriteLine("TOKEN VALIDATION: OnChallenge");
                        Trace.WriteLine("TOKEN VALIDATION: OnChallenge");
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Debug.WriteLine("TOKEN VALIDATION: OnChallenge");
                        Trace.WriteLine("TOKEN VALIDATION: OnChallenge");
                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        Debug.WriteLine("TOKEN VALIDATION: OnForbidden");
                        Trace.WriteLine("TOKEN VALIDATION: OnForbidden");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Debug.WriteLine("TOKEN VALIDATION: The token is valid");
                        Trace.WriteLine("TOKEN VALIDATION: The token is valid");
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddApiVersioning(op =>
            {
                op.DefaultApiVersion = new ApiVersion(1, 0);
                op.ApiVersionReader = new HeaderApiVersionReader() { HeaderNames = { "api-version" } };
                op.AssumeDefaultVersionWhenUnspecified = true;
                op.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(op =>
            {
                //op.GroupNameFormat = "'v'VV";
                op.GroupNameFormat = "VV";
                //op.SubstituteApiVersionInUrl= true;
            });

            // as IApiVersionDescriptionProvider cannot be injected,
            // a new class should be created for this purpose
            services.ConfigureOptions<ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                var securityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Name = "Bearer Authorization",
                    Description = "Bearer Authorization",
                    In = ParameterLocation.Header,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                    Reference = new OpenApiReference()
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                // Format: Id, security scheme
                options.AddSecurityDefinition("Bearer", securityScheme);
                options.OperationFilter<AddSecurityFilter>();
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(new HandleExceptionAttribute());
            });

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
                // Enable middleware to serve generated Swagger as a JSON endpoint.

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
                // specifying the Swagger JSON endpoint.
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    // if not versioned
                    //options.SwaggerEndpoint("/swagger/v1/swagger.json", "Example WebApi");

                    // if versioned
                    foreach (var item in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        var url = $"{item.GroupName}/swagger.json";
                        options.SwaggerEndpoint(url, item.GroupName.ToString());
                    }
                });
            }
            else
            {
                app.UseHsts();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
                // specifying the Swagger JSON endpoint.
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    // customize swagger ui entry point
                    options.RoutePrefix = string.Empty;

                    // if not versioned
                    //options.SwaggerEndpoint("/swagger/v1/swagger.json", "Example WebApi");

                    // if versioned
                    foreach (var item in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        var url = $"{item.GroupName}/swagger.json";
                        options.SwaggerEndpoint(url, item.GroupName.ToString());
                    }
                });
            }                       

            app.UseCors("default");
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //.RequireAuthorization(); // adds auth to ALL endpoints of the API
            });
        }

        private static void RegisterOwnServices(ref IServiceCollection services)
        {
            services.AddScoped<IMyAppSettings, MyAppSettings>();
            //services.AddScoped<IQuotesRepository, QuotesRepository>();
            services.AddSingleton<IQuotesRepository, QuotesRepositoryMockData>();
        }
    }
}