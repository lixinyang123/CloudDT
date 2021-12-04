using CloudDT.ContainerAPI.Models;
using CloudDT.ContainerAPI.Services;

Configurator.InitEnv();
TTYD.StartShell();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<NodeService>();
builder.Services.AddSingleton<CSharpService>();
builder.Services.AddSingleton<PythonService>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute();

app.Run();
