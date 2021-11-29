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

        // File.Copy("Env/demo.csproj.templete", $"{Configurator.EnvPath}/Dotnet/demo.csproj", true);
    }
}