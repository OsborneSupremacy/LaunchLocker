using System.IO.Abstractions;

namespace LaunchLocker.Library;

public record RuntimeArgs
{
    public IFileInfo TargetFileInfo { get; private set; }

    public IList<string> AdditionalArgs { get; private set; }

    public RuntimeArgs(IFileSystem filesystem, string[] args)
    {
        if (args.Length < 2)
            throw new ArgumentException("At least two command line arguments are required.");

        string targetFileName = args[1];

        if (string.IsNullOrEmpty(targetFileName))
            throw new ArgumentException("The second command line argument should be the file to be launched.");

        TargetFileInfo = filesystem.FileInfo.FromFileName(targetFileName);

        if (!TargetFileInfo.Exists)
            throw new ArgumentException($"File `{TargetFileInfo.FullName}` not found");

        // handle additional CLAs
        List<string> additionalArgs = new();
        if (args.Length > 2)
            additionalArgs.AddRange(args.Skip(2)); // skip first (this EXE) and second (target file)

        AdditionalArgs = additionalArgs;
    }
}

