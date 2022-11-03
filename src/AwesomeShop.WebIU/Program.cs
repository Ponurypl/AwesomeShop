using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using OnboardingIntegrationExample.AwesomeShop.Api.Client;
using OnboardingIntegrationExample.AwesomeShop.WebIU;
using OnboardingIntegrationExample.AwesomeShop.WebIU.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var services = builder.Services;
services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
services.AddScoped(sp =>
                   {
                       var client = new ApiClient("https://localhost:7024", sp.GetRequiredService<HttpClient>());
                       client.SetToken(sp.GetRequiredService<IdentityService>().Token);
                       return client;
                   });
services.AddMudServices(config =>
                        {
                            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
                        });
services.AddSingleton<IdentityService>();

await builder.Build().RunAsync();
