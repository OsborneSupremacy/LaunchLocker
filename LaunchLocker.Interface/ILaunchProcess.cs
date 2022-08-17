using System.Diagnostics;
using System.IO.Abstractions;

namespace LaunchLocker.Interface;

public interface ILaunchProcess
{
    public Task<ProcessStartInfo> RunAsync(
       IFileInfo targetProgram,
       IFileInfo targetFile,
       IList<string> additionalArgs
    );
}
