using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace LaunchLocker.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class LockReaderTests : TestBase
    {
        public override void Intialize()
        {
            Configuration.CheckIfValid(new string[] { string.Empty, TestFileName }, out string message);
        }

        [TestMethod]
        public void Read_Should_Work_When_One_Lock_Present()
        {
            FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.launchlock", "lock");

            var (_, lockInfoCollection) = LockFinder.DoesLockExist();

            LockReader.Read(lockInfoCollection);

            LockReader.LaunchLocks.Count().Should().Be(1);
        }

        [TestMethod]
        public void Read_Should_Work_When_Two_Locks_Present()
        {
            var fileName1 = $"{TestFileName}.{Guid.NewGuid()}.launchlock";
            var fileName2 = $"{TestFileName}.{Guid.NewGuid()}.launchlock";

            FileSystem.AddFile(fileName1, new Mocks().GetLaunchLockJson(fileName1));
            FileSystem.AddFile(fileName2, new Mocks().GetLaunchLockJson(fileName2));

            var (_, lockInfoCollection) = LockFinder.DoesLockExist();

            LockReader.Read(lockInfoCollection);

            LockReader.LaunchLocks.Count().Should().Be(2);
        }
    }
}
