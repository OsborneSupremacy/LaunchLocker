using LaunchLocker.Interface;
using LaunchLocker.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentAssertions;

namespace LaunchLocker.Tests
{
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

            var lauchLock = JsonOperations.Deserialize(FileName, input);

            lauchLock.IsValid.Should().Be(true);
            lauchLock.FileName.Should().Be(FileName);
            lauchLock.LockTime.Should().BeCloseTo(lockTime);
            lauchLock.Username.Should().Be(userName);
        }

        [TestMethod]
        public void Deserialize_Should_Work_For_Invalid_Input()
        {

            var lauchLock = JsonOperations.Deserialize(FileName, "lock");

            lauchLock.IsValid.Should().Be(false);
        }

    }
}
