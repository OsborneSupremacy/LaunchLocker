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
