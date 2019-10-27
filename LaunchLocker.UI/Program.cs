using System;

namespace LaunchLocker.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Library.Configuration(args);
            if (!config.CheckIfValid(out string message))
                ExitWithMessage(message);

            var lockReader = new Library.LockReader(config.TargetFileInfo);
            if(lockReader.DoesLockExist())
                ExitWithMessage("lock exists");

            ExitWithMessage("no issues");
        }

        public static void ExitWithMessage(string Message)
        {
            Console.WriteLine(Message);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(-1);
        }
    }
}
