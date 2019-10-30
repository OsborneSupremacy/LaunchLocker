﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Linq;
using System;

namespace LaunchLocker.Tests
{
    [TestClass]
    public class LaunchLockProcessTests : TestBase
    {
        public override void Intialize()
        {

        }

        [TestMethod]
        public void Execute_Should_Not_Launch_File_When_Locks_Present()
        {
            FileSystem.AddFile(TestFileName, new Bogus.Faker().Lorem.Paragraphs(3));
            FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.launchlock", "lock");

            LaunchLockProcess.Execute(new string[] { TestFileName });
            Communicator.Messages.Should().Contain("File is locked and should not be opened.");
        }

        [TestMethod]
        public void Execute_Should_Launch_File_When_Only_Bad_Lock_Present()
        {
            FileSystem.AddFile(TestFileName, new Bogus.Faker().Lorem.Paragraphs(3));
            FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.launchlock", "lock");

            LaunchLockProcess.Execute(new string[] { TestFileName });

            Communicator.Messages.Should().Contain("Launching file.");
        }

        [TestMethod]
        public void Execute_Should_Launch_File_When_Only_User_Lock_Present()
        {
            FileSystem.AddFile(TestFileName, new Bogus.Faker().Lorem.Paragraphs(3));
            FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.launchlock", "lock"); // need to make this a user lock

            LaunchLockProcess.Execute(new string[] { TestFileName });

            Communicator.Messages.Should().Contain("Launching file.");
        }

        [TestMethod]
        public void Execute_Should_Launch_File_When_No_Locks_Present()
        {
            FileSystem.AddFile(TestFileName, new Bogus.Faker().Lorem.Paragraphs(3));

            LaunchLockProcess.Execute(new string[] { TestFileName });
            Communicator.Messages.Should().Contain("Launching file.");
        }

        [TestMethod]
        public void Execute_Should_Delete_Lock_When_File_Is_Close()
        {
            FileSystem.AddFile(TestFileName, new Bogus.Faker().Lorem.Paragraphs(3));

            LaunchLockProcess.Execute(new string[] { TestFileName  });

            LockFinder.DoesLockExist().Should().BeFalse();
        }

    }
}