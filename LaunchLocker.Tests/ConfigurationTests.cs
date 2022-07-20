namespace LaunchLocker.Tests;

[ExcludeFromCodeCoverage]
[TestClass]
public class ConfigurationTests
{
    MockFileSystem FileSystem = null;

    [TestInitialize]
    public void Initialize()
    {
        FileSystem = new MockFileSystem();
    }

    [TestMethod]
    public void CheckIfValid_Should_BeFalse_When_No_Args()
    {
        var config = new Library.Configuration(FileSystem);

        var IsValid = config.CheckIfValid(new string[] { }, out string message);

        IsValid.Should().BeFalse();
        message.Should().Contain("At least two command line arguments are required");
    }

    [TestMethod]
    public void CheckIfValid_Should_BeFalse_When_TargetFile_Is_Empty()
    {
        var config = new Library.Configuration(FileSystem);

        var IsValid = config.CheckIfValid(new string[] { "", "" }, out string message);

        IsValid.Should().BeFalse();
        message.Should().Contain("The second command line argument should be the file to be launched");
    }

    [TestMethod]
    public void CheckIfValid_Should_BeFalse_When_No_File_Absent()
    {
        var config = new Library.Configuration(FileSystem);

        var IsValid = config.CheckIfValid(new string[] { "", @"C:\Test.txt" }, out string message);

        IsValid.Should().BeFalse();
    }

    [TestMethod]
    public void CheckIfValid_Should_BeTrue_When_File_Present()
    {
        var testFile = @"C:\Test.txt";
        FileSystem.AddFile(testFile, new MockFileData("Test"));

        var config = new Library.Configuration(FileSystem);

        var IsValid = config.CheckIfValid(new string[] { "", testFile }, out string message);

        IsValid.Should().BeTrue();
    }

    [TestMethod]
    public void CheckIfValid_Should_Identify_CLAs()
    {
        var testFile = @"C:\Test.txt";
        FileSystem.AddFile(testFile, new MockFileData("Test"));

        var config = new Library.Configuration(FileSystem);

        config.CheckIfValid(new string[] { "", testFile, "two", "three", "four" }, out string message);

        config.TargetClas.Count().Should().Be(3);
        config.TargetClas.Should().Contain("two");
        config.TargetClas.Should().Contain("four");
    }

}
