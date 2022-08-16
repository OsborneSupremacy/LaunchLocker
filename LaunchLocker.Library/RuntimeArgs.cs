using System.IO.Abstractions;

namespace LaunchLocker.Library;

public record RuntimeArgs
{
    /// <summary>
    /// The program that will run the target file. String rather than FileInfo,
    /// since executable may be an alias and not a locatable file.
    /// </summary>
    public IFileInfo TargetProgram { get; init; }

    /// <summary>
    /// The file that will be locked
    /// </summary>
    public IFileInfo TargetFile { get; init; }

    public IList<string> AdditionalArgs { get; init; }

    public RuntimeArgs(
        IFileInfo targetProgram,
        IFileInfo targetFile,
        IList<string> additionalArgs
        )
    {
        TargetProgram = targetProgram ?? throw new ArgumentNullException(nameof(targetProgram));
        TargetFile = targetFile ?? throw new ArgumentNullException(nameof(targetFile));
        AdditionalArgs = additionalArgs ?? throw new ArgumentNullException(nameof(additionalArgs));
    }
}

