using System.IO.Abstractions;

namespace LaunchLocker.Interface;

public interface IRuntimeArgs
{
    public IFileInfo TargetProgram { get; init; }

    public IFileInfo TargetFile { get; init; }

    public IList<string> AdditionalArgs { get; init; }
}
