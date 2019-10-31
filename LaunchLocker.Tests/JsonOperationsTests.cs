using FluentAssertions;
using LaunchLocker.Interface;
using LaunchLocker.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace LaunchLocker.Tests
{
    [ExcludeFromCodeCoverage]

    [TestClass]
    public class JsonOperationsTests
    {
        protected IJsonOperations JsonOperations { get; set; }

        protected string FileName { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            JsonOperations = new JsonOperations();
            FileName = @"C:\ProgramFiles\Test.txt";
        }

        [TestMethod]
        public void Deserialize_Should_Work_For_Valid_Input()
        {
            var lockTime = DateTime.Now;
            var userName = new Bogus.Faker().Internet.UserName();

            var originalLaunchLock = new LaunchLock()
            {
                FileName = FileName,
                Username = userName,
                LockTime = lockTime,
                IsValid = true
            };

            var input = JsonOperations.Serialize(originalLaunchLock);

            var launchLock = JsonOperations.Deserialize(FileName, input);

            launchLock.IsValid.Should().Be(true);
            launchLock.FileName.Should().Be(FileName);
            launchLock.LockTime.Should().BeCloseTo(lockTime);
            launchLock.Username.Should().Be(userName);
        }

        [TestMethod]
        public void Deserialize_Should_Work_For_Invalid_Input()
        {

            var launchLock = JsonOperations.Deserialize(FileName, "lock");

            launchLock.IsValid.Should().Be(false);
        }

    }
}
