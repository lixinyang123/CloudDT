using CloudDT.ContainerAPI.Models;

namespace CloudDT.ContainerAPI.Services;

public class CSharpService
{
    private static string savePath = $"{Configurator.EnvPath}/Dotnet/Program.cs";
    private static string projPath = $"{Configurator.EnvPath}/Dotnet/demo.csproj";

    public CSharpService Save(string code)
    {
        File.WriteAllText(savePath, code);
        return this;
    }

    public void Run()
    {
        string command = $"dotnet run --project {projPath}";
        TTYD.Run(command);
    }
}