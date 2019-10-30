using LaunchLocker.Interface;
using System;

namespace LaunchLocker.Library
{
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
    }
}
