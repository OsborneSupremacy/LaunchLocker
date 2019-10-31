using LaunchLocker.Interface;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace LaunchLocker.Tests
{
    [ExcludeFromCodeCoverage]
    public class ListCommunicator : ICommunicator
    {
        public List<string> Messages { get; set; }

        public ListCommunicator()
        {
            Messages = new List<string>();
        }

        public void Exit()
        {
            foreach (var message in Messages)
                Debug.WriteLine(message);
        }

        public void Write(string message)
        {
            Messages.Add(message);
        }

        public void WriteSentence(string message)
        {
            Messages.Add(message);
        }

        public void WriteLockInfo(IEnumerable<ILaunchLock> launchLocks)
        {
            Messages.Add("Lock info:");
            foreach (var launchLock in launchLocks)
                Messages.Add($"User: {launchLock.Username}, Time: {launchLock.LockTime.ToString()}");
        }
    }
}
