namespace LaunchLocker.Library;

public class RuntimeArgsBuilder
{
    public Result<RuntimeArgs> Build(IFileSystem filesystem, string[] args)
    {
        if (args.Length < 2)
            return new Result<RuntimeArgs>(new ArgumentException("At least two command line arguments are required."));

        return GetTarget(filesystem, args[1])
            .Match
            (
                targetFile =>
                {
                    return new RuntimeArgs(
                        args[0].Trim(),
                        targetFile,
                        args.Skip(2).ToList()  // skip first (target program) and second (target file)
                    );
                },
                exception =>
                {
                    return new Result<RuntimeArgs>(new AggregateException("The second command line argument should be the file to be locked. " + exception.ToString()));
                }
            );
    }

    protected static Result<IFileInfo> GetTarget(IFileSystem filesystem, string fullPath)
    {
        if (string.IsNullOrEmpty(fullPath))
            return new Result<IFileInfo>(new ArgumentException("Argument not provided"));

        IFileInfo targetFileInfo = filesystem.FileInfo.FromFileName(fullPath);

        // explorer.exe is special in that it doesn't exist as a real file, but can be run.
        // there are probably other programs like this. Whitelist them in appsettings.eventually
        // or find a better way to handle them.
        if (targetFileInfo.Name.Equals("explorer.exe", StringComparison.OrdinalIgnoreCase))
            return new Result<IFileInfo>(targetFileInfo);

        if (!targetFileInfo.Exists)
            return new Result<IFileInfo>(new System.IO.FileNotFoundException(fullPath));

        return new Result<IFileInfo>(targetFileInfo);
    }
}
