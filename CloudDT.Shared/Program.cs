using BlazorFluentUI;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CloudDT.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddBlazorFluentUI();
builder.Services.AddScoped(sp => new HttpClient 
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
