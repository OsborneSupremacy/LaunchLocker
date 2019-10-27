using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System;

namespace LaunchLocker.Tests
{
    [TestClass]
    public class LockReaderTests
    {
        MockFileSystem FileSystem = null;
        Library.Configuration Configuration = null;
        Library.LockReader LockReader = null;
        string TestDirectoryName = string.Empty;
        string TestFileName = string.Empty;

        [TestInitialize]
        public void Initialize()
        {
            FileSystem = new MockFileSystem();
            TestDirectoryName = @"C:\ProgramFiles";
            TestFileName = $@"{TestDirectoryName}\text.txt";

            FileSystem.AddDirectory(TestDirectoryName);

            Configuration = new Library.Configuration(FileSystem);
            Configuration.CheckIfValid(new string[] { TestFileName  }, out string message);

            LockReader = new Library.LockReader(FileSystem, Configuration);        
        }

        [TestMethod]
        public void DoesLockExist_Should_BeFalse_When_Lock_Absent()
        {          
            LockReader.DoesLockExist().Should().BeFalse();         
        }

        [TestMethod]
        public void DoesLockExist_Should_BeFalse_When_One_Lock_Present()
        {
            FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.launchlock", "lock");

            LockReader.DoesLockExist().Should().BeTrue();
            LockReader.LockInfoCollection.Length.Should().Be(1);
        }

        [TestMethod]
        public void DoesLockExist_Should_BeFalse_When_Two_Locks_Present()
        {
            FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.launchlock", "lock");
            FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.launchlock", "lock");

            LockReader.DoesLockExist().Should().BeTrue();
            LockReader.LockInfoCollection.Length.Should().Be(2);
        }

    }
}
