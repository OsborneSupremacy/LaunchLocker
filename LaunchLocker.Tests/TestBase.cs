/*
namespace LaunchLocker.Tests;

[ExcludeFromCodeCoverage]
public abstract class TestBase
{
    protected MockFileSystem FileSystem = null;
    protected Library.LockFinder LockFinder = null;
    protected Library.LockReader LockReader = null;
    protected Library.LockBuilder LockBuilder = null;
    protected Library.LockWriter LockWriter = null;
    protected Library.JsonOperations JsonOperations = null;
    protected Library.LaunchLockProcess LaunchLockProcess = null;
    protected ListCommunicator Communicator = null;
    protected Library.Unlocker Unlocker = null;
    protected Launcher Launcher = null;
    protected Library.Settings Settings = null;

    protected string TestDirectoryName = string.Empty;
    protected string TestFileName = string.Empty;

    [TestInitialize]
    public void InitializeBase()
    {
        FileSystem = new MockFileSystem();
        TestDirectoryName = @"C:\ProgramFiles";
        TestFileName = $@"{TestDirectoryName}\text.txt";

        var runtimeArgs = new RuntimeArgs(new MockFileSystem(), null);

        FileSystem.AddDirectory(TestDirectoryName);

        Settings = new Library.Settings();

        JsonOperations = new Library.JsonOperations();
        LockFinder = new Library.LockFinder(FileSystem, runtimeArgs, Settings);
        LockReader = new Library.LockReader(FileSystem, JsonOperations);
        LockBuilder = new Library.LockBuilder(runtimeArgs);
        LockWriter = new Library.LockWriter(FileSystem, JsonOperations, LockBuilder);
        Communicator = new ListCommunicator();
        Unlocker = new Library.Unlocker(FileSystem, LockReader, LockBuilder);
        Launcher = new Launcher();

        LaunchLockProcess =
            new Library.LaunchLockProcess(
                LockFinder,
                LockReader,
                LockBuilder,
                LockWriter,
                Communicator,
                Unlocker,
                Launcher
                );
    }
}
*/
