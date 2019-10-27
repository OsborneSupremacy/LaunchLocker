using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

namespace LaunchLocker.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
        MockFileSystem FileSystem = null;

        [TestInitialize]
        public void Initialize()
        {
            FileSystem = new MockFileSystem();
        }

        [TestMethod]
        public void CheckIfValid_Should_BeFalse_When_No_Args()
        {
            var config = new Library.Configuration(FileSystem, new string[] { });

            var IsValid = config.CheckIfValid(out string message);

            IsValid.Should().BeFalse();
            message.Should().Contain("At least one command line argument is required");
        }

        [TestMethod]
        public void CheckIfValid_Should_BeFalse_When_TargetFile_Is_Empty()
        {
            var config = new Library.Configuration(FileSystem, new string[] { "" });

            var IsValid = config.CheckIfValid(out string message);

            IsValid.Should().BeFalse();
            message.Should().Contain("The first command line argument should be the file to be lauched");
        }

        [TestMethod]
        public void CheckIfValid_Should_BeFalse_When_No_File_Absent()
        {
            var config = new Library.Configuration(FileSystem, new string[] { @"C:\Test.txt" });

            var IsValid = config.CheckIfValid(out string message);

            IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void CheckIfValid_Should_BeTrue_When_File_Present()
        {
            var testFile = @"C:\Test.txt";
            FileSystem.AddFile(testFile, new MockFileData("Test"));

            var config = new Library.Configuration(FileSystem, new string[] { testFile });

            var IsValid = config.CheckIfValid(out string message);

            IsValid.Should().BeTrue();
        }
    }
}
