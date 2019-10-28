using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;

namespace LaunchLocker.Tests
{
    [TestClass]
    public class LockBuilderTests : TestBase
    {
        public override void Intialize()
        {
            Configuration.CheckIfValid(new string[] { TestFileName }, out string message);
        }

        [TestMethod]
        public void Build_Should_Work()
        {
            LockBuilder.Build();
            LockBuilder.LaunchLock.Username.Should().NotBeEmpty();
            LockBuilder.LaunchLock.LockTime.Should().BeOnOrBefore(DateTime.Now);
            LockBuilder.LaunchLock.FileName.Should().NotBeNullOrEmpty();
        }

    }
}
