using System;
using System.Collections.Generic;
using LaunchLocker.Interface;

namespace LaunchLocker.Library;

public class ConsoleCommunicator : ICommunicator
{
    public void Exit()
    {
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
        Environment.Exit(-1);
    }

    public void Write(string message) =>
        Console.Write(message);

    public void WriteSentence(string message) =>
        Console.WriteLine(message);

    public void WriteLockInfo(IEnumerable<ILaunchLock> launchLocks)
    {
        Console.WriteLine("Lock info:");
        foreach (var launchLock in launchLocks)
            Console.WriteLine($"User: {launchLock.Username}, Time: {launchLock.LockTime}");
    }
}
