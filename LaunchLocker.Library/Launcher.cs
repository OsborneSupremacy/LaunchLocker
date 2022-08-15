using System.Diagnostics;
using System.Text;

namespace LaunchLocker.Library;

public class Launcher : ILauncher
{
    public RuntimeArgs _runtimeArgs { get; set; }

    public Launcher(RuntimeArgs runtimeArgs)
    {
        _runtimeArgs = runtimeArgs ?? throw new System.ArgumentException(null, nameof(runtimeArgs));
    }

    public async Task RunAsync()
    {
        var args = new StringBuilder();
        args.Append(_runtimeArgs.TargetFile);

        foreach (var cla in _runtimeArgs.AdditionalArgs)
            args.Append($" {cla}");

        var process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = _runtimeArgs.TargetProgram,
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
