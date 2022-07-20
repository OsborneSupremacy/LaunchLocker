

namespace LaunchLocker.Tests;

[TestClass]
[ExcludeFromCodeCoverage]
public class UnlockerTests : TestBase
{
    public override void Intialize()
    {
        Configuration.CheckIfValid(new string[] { string.Empty, TestFileName }, out string message);

    }

    [TestMethod]
    public void RemoveLock_Should_Remove_Current_Lock()
    {
        // create valid lock that shouldn't get removed
        var validLock = $"{TestFileName}.{Guid.NewGuid()}.launchlock";
        FileSystem.AddFile(validLock, new Mocks().GetLaunchLockJson(validLock));

        // create current lock that should get removed
        LockBuilder.Build();
        LockWriter.Write();

        GetCurrentLauchLocks();

        Unlocker.RemoveLock();

        GetCurrentLauchLocks().Count().Should().Be(1);

    }

    [TestMethod]
    public void RemoveObsoleteLocks_Should_Remove_Invalid_Lock()
    {
        // create valid lock that shouldn't get removed
        var validLock = $"{TestFileName}.{Guid.NewGuid()}.launchlock";
        FileSystem.AddFile(validLock, new Mocks().GetLaunchLockJson(validLock));

        // create invalid lock that should be removed
        var invalidLock = $"{TestFileName}.{Guid.NewGuid()}.launchlock";
        FileSystem.AddFile(invalidLock, "lock");

        GetCurrentLauchLocks();

        Unlocker.RemoveObsoleteLocks();

        LockFinder.DoesLockExist();

        GetCurrentLauchLocks().Count().Should().Be(1);
    }

    [TestMethod]
    public void RemoveObsoleteLocks_Should_Remove_Old_User_Lock()
    {
        // create valid lock that shouldn't get removed
        var validLock = $"{TestFileName}.{Guid.NewGuid()}.launchlock";
        FileSystem.AddFile(validLock, new Mocks().GetLaunchLockJson(validLock));

        // create old lock that should be removed
        var invalidLock = $"{TestFileName}.{Guid.NewGuid()}.launchlock";
        FileSystem.AddFile(invalidLock, new Mocks().GetLaunchLockJson(validLock, true));

        GetCurrentLauchLocks();

        Unlocker.RemoveObsoleteLocks();

        LockFinder.DoesLockExist();

        GetCurrentLauchLocks().Count().Should().Be(1);
    }

    protected IEnumerable<Interface.ILaunchLock> GetCurrentLauchLocks()
    {
        var (_, lockInfoCollection) = LockFinder.DoesLockExist();
        LockReader.Read(lockInfoCollection);
        return LockReader.LaunchLocks;
    }

}
