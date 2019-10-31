﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;

namespace LaunchLocker.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class LockWriterTests : TestBase
    {
        public override void Intialize()
        {
            Configuration.CheckIfValid(new string[] { TestFileName }, out string message);
            LockBuilder.Build();
        }

        [TestMethod]
        public void Write_Should_Work()
        {
            var expectedFile = LockBuilder.LaunchLock.FileName;

            LockWriter.Write();

            var fileInfo = FileSystem.FileInfo.FromFileName(expectedFile);
            fileInfo.Exists.Should().BeTrue();

            var lockContents = FileSystem.File.ReadAllText(expectedFile);
            lockContents.Length.Should().BeGreaterThan(0);
        }

    }
}
