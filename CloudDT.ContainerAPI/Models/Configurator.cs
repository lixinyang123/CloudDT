namespace CloudDT.ContainerAPI.Models;

public static class Configurator
{
    public static string EnvPath { get => "/home/CloudDT/Env"; }

    public static void InitEnv()
    {
        Directory.CreateDirectory(Configurator.EnvPath);
        Directory.CreateDirectory($"{Configurator.EnvPath}/Node");
        Directory.CreateDirectory($"{Configurator.EnvPath}/Dotnet");
        Directory.CreateDirectory($"{Configurator.EnvPath}/Python");

        File.AppendAllText($"{Configurator.EnvPath}/Dotnet/demo.csproj", "<Project Sdk=\"Microsoft.NET.Sdk\">");
        File.AppendAllText($"{Configurator.EnvPath}/Dotnet/demo.csproj", "<PropertyGroup>");
        File.AppendAllText($"{Configurator.EnvPath}/Dotnet/demo.csproj", "<OutputType>Exe</OutputType>");
        File.AppendAllText($"{Configurator.EnvPath}/Dotnet/demo.csproj", "<TargetFramework>net6.0</TargetFramework>");
        File.AppendAllText($"{Configurator.EnvPath}/Dotnet/demo.csproj", "<ImplicitUsings>enable</ImplicitUsings>");
        File.AppendAllText($"{Configurator.EnvPath}/Dotnet/demo.csproj", "<Nullable>enable</Nullable>");
        File.AppendAllText($"{Configurator.EnvPath}/Dotnet/demo.csproj", "</PropertyGroup>");
        File.AppendAllText($"{Configurator.EnvPath}/Dotnet/demo.csproj", "</Project>");
    }
}