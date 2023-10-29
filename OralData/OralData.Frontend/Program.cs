using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OralData.Frontend;
using OralData.Frontend.Auth;
using OralData.Frontend.Repositories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5203/") });
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddSweetAlert2();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderTest>();


await builder.Build().RunAsync();
