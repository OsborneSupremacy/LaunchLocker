﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;

namespace LaunchLocker.Tests
{
    [TestClass]
    public class LockFinderTests : TestBase
    {
        public override void Intialize()
        {
            Configuration.CheckIfValid(new string[] { TestFileName }, out string message);
        }

        [TestMethod]
        public void DoesLockExist_Should_BeFalse_When_Lock_Absent()
        {          
            LockFinder.DoesLockExist().Should().BeFalse();         
        }

        [TestMethod]
        public void DoesLockExist_Should_BeFalse_When_One_Lock_Present()
        {
            FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.launchlock", "lock");

            LockFinder.DoesLockExist().Should().BeTrue();
            LockFinder.LockInfoCollection.Length.Should().Be(1);
        }

        [TestMethod]
        public void DoesLockExist_Should_BeFalse_When_Two_Locks_Present()
        {
            FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.launchlock", "lock");
            FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.launchlock", "lock");

            LockFinder.DoesLockExist().Should().BeTrue();
            LockFinder.LockInfoCollection.Length.Should().Be(2);
        }

    }
}
