using System.IO.Abstractions;

namespace LaunchLocker.Interface
{
    /// <summary>
    /// Holds the parameters passed in and checks their validity
    /// </summary>
    public interface IConfiguration
    {
        IFileInfo TargetFileInfo { get; }

        bool CheckIfValid(string[] args, out string message);
    }
}
