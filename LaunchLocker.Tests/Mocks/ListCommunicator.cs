using LaunchLocker.Interface;
using System.Collections.Generic;
using System.Diagnostics;

namespace LaunchLocker.Tests
{
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
    }
}
