using BlazorFluentUI;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CloudDT.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddBlazorFluentUI();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
