using Fiap.TesteTecnico.ClassManager.WebApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiRoute = builder.Configuration.GetSection("ApiRoute").Value
    ?? builder.HostEnvironment.BaseAddress;

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiRoute) });
builder.Services.AddMudServices();

await builder.Build().RunAsync();
