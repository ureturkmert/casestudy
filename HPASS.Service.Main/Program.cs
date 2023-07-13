using HPASS.Service.Base.Configuration;
using HPASS.Service.IocBuilder;
using HPASS.Service.Main.BackgroundServices;

const string applyingCorsPolicyName = "CUSTOMCORSPOLICY";

WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);

webApplicationBuilder.ConfigureBackEndControllers();
webApplicationBuilder.ConfigureDefaultCorsPolicy(applyingCorsPolicyName);
webApplicationBuilder.ConfigureForKestrelWebServer(5015);
webApplicationBuilder.ConfigureDefaultAppSettings();
webApplicationBuilder.ConfigureAutomapper();
webApplicationBuilder.ConfigureAutofacDependencyInjection(new DependencyInjectionModule());
webApplicationBuilder.ConfigureSerilog();

webApplicationBuilder.Services.AddHostedService<AppointmentRemienderService>();

WebApplication webApplication = webApplicationBuilder.Build();
webApplication.UseCors(applyingCorsPolicyName);
webApplication.MapControllers();
webApplication.Run();
