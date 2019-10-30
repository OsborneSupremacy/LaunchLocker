using System;
using System.Collections.Generic;
using System.Text;

namespace LaunchLocker.Interface
{
    public interface ILaunchLockProcess
    {
        void Execute(string[] args);
    }
}
