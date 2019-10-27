using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text;

namespace LaunchLocker.Interface
{
    public interface IConfiguration
    {
        IFileInfo TargetFileInfo { get; }

        bool CheckIfValid(string[] args, out string message);
    }
}
