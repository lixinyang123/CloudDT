using System.Diagnostics;

namespace CloudDT.ContainerAPI.Models;

public static class TTYD
{
    private static Process? process;

    public static void StartShell()
    {
        new Process()
        {
            StartInfo = new()
            {
                FileName = "ttyd",
                Arguments = "bash"
            }
        }.Start();
    }

    public static void Run(string command)
    {
        if(process is not null)
            process.Close();

        process = new()
        {
            StartInfo = new()
            {
                FileName = "ttyd",
                Arguments = $"-o -p 8435 {command}"
            }
        };

        process.Start();
    }
}