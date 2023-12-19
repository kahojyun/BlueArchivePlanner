using BlueArchivePlanner;
using BlueArchivePlanner.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<SchaleDb>();
builder.Services.AddScoped<UserPreferences>();
builder.Services.AddScoped<Calculator>();
builder.Services.AddFluentUIComponents();
builder.Services.AddLocalStorageServices();

await builder.Build().RunAsync();
