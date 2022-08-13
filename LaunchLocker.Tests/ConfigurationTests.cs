/*using LaunchLocker.Interface;

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

        var message = string.Empty;

        var IsValid = config.GetTargetClas(Array.Empty<string>())
            .Match(success =>
            {
                return true;
            },
            exception =>
            {
                message = exception.Message;
                return false;
            });

        IsValid.Should().BeFalse();
        message.Should().Contain("At least two command line arguments are required");
    }

    [TestMethod]
    public void CheckIfValid_Should_BeFalse_When_TargetFile_Is_Empty()
    {
        var config = new Library.Configuration(FileSystem);

        var message = string.Empty;

        var IsValid = config.GetTargetClas(new string[] { "", "" })
            .Match(success =>
            {
                return true;
            },
            exception =>
            {
                message = exception.Message;
                return false;
            });

        IsValid.Should().BeFalse();
        message.Should().Contain("The second command line argument should be the file to be launched");
    }

    [TestMethod]
    public void CheckIfValid_Should_BeFalse_When_No_File_Absent()
    {
        var config = new Library.Configuration(FileSystem);

        var message = string.Empty;

        var IsValid = config.GetTargetClas(new string[] { "", @"C:\Test.txt" })
            .Match(success =>
            {
                return true;
            },
            exception =>
            {
                message = exception.Message;
                return false;
            });

        IsValid.Should().BeFalse();
    }

    [TestMethod]
    public void CheckIfValid_Should_BeTrue_When_File_Present()
    {
        var testFile = @"C:\Test.txt";
        FileSystem.AddFile(testFile, new MockFileData("Test"));

        var config = new Library.Configuration(FileSystem);

        var message = string.Empty;

        var IsValid = config.GetTargetClas(new string[] { "", testFile })
            .Match(success =>
            {
                return true;
            },
            exception =>
            {
                message = exception.Message;
                return false;
            });

        IsValid.Should().BeTrue();
    }

    [TestMethod]
    public void CheckIfValid_Should_Identify_CLAs()
    {
        var testFile = @"C:\Test.txt";
        FileSystem.AddFile(testFile, new MockFileData("Test"));

        var config = new Library.Configuration(FileSystem);

        var targetClas = config.GetTargetClas(new string[] { "", testFile, "two", "three", "four" })
            .Match(success =>
            {
                return success;
            },
            exception =>
            {
                return Enumerable.Empty<string>();
            });

        targetClas.Count().Should().Be(3);
        targetClas.Should().Contain("two");
        targetClas.Should().Contain("four");
    }

}
*/
