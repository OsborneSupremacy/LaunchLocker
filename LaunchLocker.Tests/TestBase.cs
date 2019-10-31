using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.IO.Abstractions.TestingHelpers;

namespace LaunchLocker.Tests
{
    [ExcludeFromCodeCoverage]
    public abstract class TestBase
    {
        protected MockFileSystem FileSystem = null;
        protected Library.Configuration Configuration = null;
        protected Library.LockFinder LockFinder = null;
        protected Library.LockReader LockReader = null;
        protected Library.LockBuilder LockBuilder = null;
        protected Library.LockWriter LockWriter = null;
        protected Library.JsonOperations JsonOperations = null;
        protected Library.LaunchLockProcess LaunchLockProcess = null;
        protected ListCommunicator Communicator = null;
        protected Library.Unlocker Unlocker = null;

        protected string TestDirectoryName = string.Empty;
        protected string TestFileName = string.Empty;

        [TestInitialize]
        public void InitializeBase()
        {
            FileSystem = new MockFileSystem();
            TestDirectoryName = @"C:\ProgramFiles";
            TestFileName = $@"{TestDirectoryName}\text.txt";

            FileSystem.AddDirectory(TestDirectoryName);

            JsonOperations = new Library.JsonOperations();
            Configuration = new Library.Configuration(FileSystem);
            LockFinder = new Library.LockFinder(FileSystem, Configuration);
            LockReader = new Library.LockReader(FileSystem, LockFinder, JsonOperations);
            LockBuilder = new Library.LockBuilder(Configuration);
            LockWriter = new Library.LockWriter(FileSystem, JsonOperations, LockBuilder);
            Communicator = new ListCommunicator();
            Unlocker = new Library.Unlocker(FileSystem, LockReader, LockBuilder);
            LaunchLockProcess = 
                new Library.LaunchLockProcess(
                    Configuration, 
                    LockFinder, 
                    LockReader, 
                    LockBuilder, 
                    LockWriter, 
                    Communicator, 
                    FileSystem, 
                    Unlocker);

            Intialize();

        }

        public abstract void Intialize();
    }
}
