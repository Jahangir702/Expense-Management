using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web.Blazor.Client;
using Web.Blazor.Client.HttpServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5008/") });
builder.Services.AddScoped<ExpenseCategoryHttpService>();
builder.Services.AddScoped<ExpenseHttpService>();

await builder.Build().RunAsync();
