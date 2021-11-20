using System.Diagnostics;
using CloudDT.ContainerAPI.Interfaces;

namespace CloudDT.ContainerAPI.Services;

public class NodeService : ICodeService
{
    private readonly string savePath = "main.js";

    public void Save(string code)
    {
        File.WriteAllText(savePath, code);
    }

    public void Run()
    {
        ProcessStartInfo startInfo = new()
        {
            FileName = "ttyd -o -p 8435",
            Arguments = $"node {savePath}"
        };
    }
}