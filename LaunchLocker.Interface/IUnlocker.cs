using System;
using System.Collections.Generic;
using System.Text;

namespace LaunchLocker.Interface
{
    public interface IUnlocker
    {
        void RemoveLock();

        void RemoveObsoleteLocks();

    }
}
