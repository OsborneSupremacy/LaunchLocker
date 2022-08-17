using System.Diagnostics;
using System.Text;

namespace LaunchLocker.Library;

public class LaunchProcess : ILaunchProcess
{
    public async Task<ProcessStartInfo> RunAsync(
        IFileInfo targetProgram,
        IFileInfo targetFile,
        IList<string> additionalArgs
    )
    {
        var args = new StringBuilder();
        args.Append(targetFile.FullName);

        foreach (var cla in additionalArgs)
            args.Append($" {cla}");

        var startInfo = new ProcessStartInfo()
        {
            FileName = targetProgram.FullName,
            UseShellExecute = true,
            Arguments = args.ToString()
        };

        var process = new Process()
        {
            StartInfo = startInfo
        };

        await Task.Run(() =>
        {
            process.Start();
            process.WaitForExit();
        });

        return startInfo;
    }
}
