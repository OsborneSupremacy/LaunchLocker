using System.IO.Abstractions;

namespace LaunchLocker.Interface
{
    /// <summary>
    /// Holds the parameters passed into and checks their validity
    /// </summary>
    public interface IConfiguration
    {
        IFileInfo TargetFileInfo { get; }

        bool CheckIfValid(string[] args, out string message);
    }
}
