using System.Diagnostics;
using System.Text;

namespace LaunchLocker.Library;

public class Launcher : ILauncher
{
    public IRuntimeArgs _runtimeArgs { get; set; }

    public Launcher(IRuntimeArgs runtimeArgs)
    {
        _runtimeArgs = runtimeArgs ?? throw new ArgumentException(null, nameof(runtimeArgs));
    }

    public async Task RunAsync()
    {
        var args = new StringBuilder();
        args.Append(_runtimeArgs.TargetFile.FullName);

        foreach (var cla in _runtimeArgs.AdditionalArgs)
            args.Append($" {cla}");

        var process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = _runtimeArgs.TargetProgram.FullName,
                UseShellExecute = true,
                Arguments = args.ToString()
            }
        };

        await Task.Run(() =>
        {
            process.Start();
            process.WaitForExit();
        });

    }

}
