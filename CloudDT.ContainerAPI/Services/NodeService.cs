using CloudDT.ContainerAPI.Models;

namespace CloudDT.ContainerAPI.Services;

public class NodeService
{
    private static string savePath = $"{Configurator.EnvPath}/Node/main.js";

    public NodeService Save(string code)
    {
        File.WriteAllText(savePath, code);
        return this;
    }

    public void Run()
    {
        string command = $"node {savePath}";
        TTYD.Run(command);
    }
}