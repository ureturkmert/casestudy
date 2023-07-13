using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace HPASS.Service.Base.Configuration
{
    public static class WebApplicationBuilderExtentions
    {
        public static WebApplicationBuilder ConfigureDefaultCorsPolicy(this WebApplicationBuilder webApplicationBuilder, string policyName = "CUSTOMCORSPOLICY")
        {

            webApplicationBuilder.Services.AddCors(options =>
            {
                options.AddPolicy(name: policyName,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader();
                                  });
            });

            return webApplicationBuilder;
        }

        public static WebApplicationBuilder ConfigureBackEndControllers(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddControllers();
            return webApplicationBuilder;
        }

        public static WebApplicationBuilder ConfigureForKestrelWebServer(this WebApplicationBuilder webApplicationBuilder, int defaultKestrelServicePort = 5015)
        {

            string kestrelportstring = Environment.GetEnvironmentVariable("KESTREL_SERVICE_PORT");
            if (!string.IsNullOrEmpty(kestrelportstring) && int.TryParse(kestrelportstring, out int parsedport))
            {
                defaultKestrelServicePort = parsedport;
            }

            webApplicationBuilder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(defaultKestrelServicePort, listenOptions =>
                {
                    listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
                });
            });

            return webApplicationBuilder;
        }

        public static WebApplicationBuilder ConfigureDefaultAppSettings(this WebApplicationBuilder webApplicationBuilder)
        {

            webApplicationBuilder.Configuration.SetBasePath(AppContext.BaseDirectory);
            webApplicationBuilder.Configuration.AddJsonFile("appsettings.json", true, true);

            return webApplicationBuilder;
        }

        public static WebApplicationBuilder ConfigureAutomapper(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return webApplicationBuilder;
        }

        public static WebApplicationBuilder ConfigureAutofacDependencyInjection(this WebApplicationBuilder webApplicationBuilder, IModule applyingAutofacModule)
        {

            webApplicationBuilder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            webApplicationBuilder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(applyingAutofacModule));


            return webApplicationBuilder;
        }

        public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder webApplicationBuilder)
        {

            webApplicationBuilder.Host.UseSerilog((context, loggerConfiguration) =>
            {

                loggerConfiguration.Enrich.FromLogContext()
                 .WriteTo.Console(Serilog.Events.LogEventLevel.Debug, "[{Timestamp:HH:mm:ss:ffff} {Level}] {SourceContext}{NewLine}{Message}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Sixteen)
                 .MinimumLevel.Debug()
                 .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                 //.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(context.Configuration["elasticSearchConnectionUri"]))
                 //{
                 //    AutoRegisterTemplate = true,
                 //    IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:dd-MM-yyyy}"
                 //})
                 .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName);


            });

            return webApplicationBuilder;
        }


      

    }
}