using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Abstractions.TestingHelpers;

namespace LaunchLocker.Tests
{
    public abstract class TestBase
    {
        protected MockFileSystem FileSystem = null;
        protected Library.Configuration Configuration = null;
        protected Library.LockFinder LockFinder = null;
        protected Library.LockBuilder LockBuilder = null;
        protected Library.LockWriter LockWriter = null;
        protected Library.JsonSerializer JsonSerializer = null;

        protected string TestDirectoryName = string.Empty;
        protected string TestFileName = string.Empty;

        [TestInitialize]
        public void InitializeBase()
        {
            FileSystem = new MockFileSystem();
            TestDirectoryName = @"C:\ProgramFiles";
            TestFileName = $@"{TestDirectoryName}\text.txt";

            FileSystem.AddDirectory(TestDirectoryName);

            JsonSerializer = new Library.JsonSerializer();
            Configuration = new Library.Configuration(FileSystem);
            LockFinder = new Library.LockFinder(FileSystem, Configuration);
            LockBuilder = new Library.LockBuilder(Configuration);
            LockWriter = new Library.LockWriter(FileSystem, JsonSerializer, LockBuilder);

            Intialize();

        }

        public abstract void Intialize();
    }
}
