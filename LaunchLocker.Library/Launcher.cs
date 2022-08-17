namespace LaunchLocker.Library;

public class Launcher : ILauncher
{
    private readonly IRuntimeArgs _runtimeArgs;

    private readonly ILaunchProcess _launchProcess;

    public Launcher(
        IRuntimeArgs runtimeArgs,
        ILaunchProcess launchProcess
        )
    {
        _runtimeArgs = runtimeArgs ?? throw new ArgumentNullException(nameof(runtimeArgs));
        _launchProcess = launchProcess ?? throw new ArgumentNullException(nameof(launchProcess));
    }

    public async Task RunAsync() =>
        await _launchProcess
            .RunAsync
            (
                _runtimeArgs.TargetProgram,
                _runtimeArgs.TargetFile,
                _runtimeArgs.AdditionalArgs
            );
}
