namespace LaunchLocker.Library;

public class RuntimeArgsBuilder
{
    public Result<RuntimeArgs> Build(IFileSystem filesystem, string[] args)
    {
        if (args.Length < 2)
            return new Result<RuntimeArgs>(new ArgumentException("At least two command line arguments are required."));

        return GetProgramToBeLaunched(filesystem, args);
    }

    protected static Result<RuntimeArgs> GetProgramToBeLaunched(IFileSystem filesystem, string[] args) =>
        GetTarget(filesystem, args[0])
            .Match
            (
                targetFile =>
                {
                    return GetFileToBeLocked(filesystem, args);
                },
                exception =>
                {
                    return new Result<RuntimeArgs>(new AggregateException("The first command line argument should be the program to be launched. " + exception.ToString()));
                }
            );

    protected static Result<RuntimeArgs> GetFileToBeLocked(IFileSystem filesystem, string[] args) =>
        GetTarget(filesystem, args[1])
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

    protected static Result<IFileInfo> GetTarget(IFileSystem filesystem, string fullPath)
    {
        if (string.IsNullOrEmpty(fullPath))
            return new Result<IFileInfo>(new ArgumentException("Argument not provided"));

        IFileInfo targetFileInfo = filesystem.FileInfo.FromFileName(fullPath);

        if (!targetFileInfo.Exists)
            return new Result<IFileInfo>(new System.IO.FileNotFoundException(fullPath));

        return new Result<IFileInfo>(targetFileInfo);
    }
}
