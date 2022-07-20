using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LaunchLocker.Tests;

[ExcludeFromCodeCoverage]
[TestClass]
public class LockFinderTests : TestBase
{
    public override void Intialize()
    {
        Configuration.CheckIfValid(new string[] { string.Empty, TestFileName }, out string message);
    }

    [TestMethod]
    public void DoesLockExist_Should_BeFalse_When_Lock_Absent()
    {
        var (lockExists, _) = LockFinder.DoesLockExist();

        lockExists.Should().BeFalse();
    }

    [TestMethod]
    public void DoesLockExist_Should_BeFalse_When_One_Lock_Present()
    {
        FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.launchlock", "lock");

        var (lockExists, lockInfoCollection) = LockFinder.DoesLockExist();

        lockExists.Should().BeTrue();
        lockInfoCollection.Length.Should().Be(1);
    }

    [TestMethod]
    public void DoesLockExist_Should_BeFalse_When_Two_Locks_Present()
    {
        FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.launchlock", "lock");
        FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.launchlock", "lock");

        var (lockExists, lockInfoCollection) = LockFinder.DoesLockExist();

        lockExists.Should().BeTrue();
        lockInfoCollection.Length.Should().Be(2);
    }

    [TestMethod]
    public void No_Problem_Indicators_When_No_Values_In_Settings()
    {
        FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.conflicted", string.Empty);

        var (pExists, pCollection) = LockFinder.DoesProblemIndicatorExist();

        pExists.Should().BeFalse();
        pCollection.Should().BeEmpty();
    }

    [TestMethod]
    public void No_Problem_Indicators_When_No_Files_Problematic()
    {
        FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}", string.Empty);
        FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}", string.Empty);

        Settings.ProblemIndicators.Add("conflicted");

        var (pExists, pCollection) = LockFinder.DoesProblemIndicatorExist();

        pExists.Should().BeFalse();
        pCollection.Should().BeEmpty();
    }

    [TestMethod]
    public void Problem_Indicator_Found_When_One_Files_Problematic()
    {
        FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}", string.Empty);
        FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.CONFLICTED", string.Empty);

        Settings.ProblemIndicators.Add("conflicted");

        var (pExists, pCollection) = LockFinder.DoesProblemIndicatorExist();

        pExists.Should().BeTrue();
        pCollection.Length.Should().Be(1);
    }

    [TestMethod]
    public void Problem_Indicator_Found_When_Two_Files_Problematic()
    {
        FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.conflicted", string.Empty);
        FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.CONFLICTED", string.Empty);

        Settings.ProblemIndicators.Add("conflicted");

        var (pExists, pCollection) = LockFinder.DoesProblemIndicatorExist();

        pExists.Should().BeTrue();
        pCollection.Length.Should().Be(2);
    }

    [TestMethod]
    public void Problem_Indicator_File_Only_Identified_Once()
    {
        FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}", string.Empty);
        FileSystem.AddFile($"{TestFileName}.{Guid.NewGuid()}.CONFLICTED.bad", string.Empty);

        Settings.ProblemIndicators.Add("conflicted");
        Settings.ProblemIndicators.Add("bad");

        var (pExists, pCollection) = LockFinder.DoesProblemIndicatorExist();

        pExists.Should().BeTrue();
        pCollection.Length.Should().Be(1);
    }
}
