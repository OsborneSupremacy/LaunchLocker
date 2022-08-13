using CliWrap;

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
        await Cli.Wrap(_runtimeArgs.TargetFileInfo.FullName)
            .WithArguments(_runtimeArgs.AdditionalArgs, false)
            .ExecuteAsync();
        return;
    }

}
