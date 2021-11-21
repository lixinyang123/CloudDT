using CloudDT.ContainerAPI.Models;

namespace CloudDT.ContainerAPI.Services;

public class PythonService
{
    private static string savePath = $"{Configurator.EnvPath}/Python/main.py";

    public PythonService Save(string code)
    {
        File.WriteAllText(savePath, code);
        return this;
    }

    public void Run()
    {
        string command = $"python {savePath}";
        TTYD.Run(command);
    }
}