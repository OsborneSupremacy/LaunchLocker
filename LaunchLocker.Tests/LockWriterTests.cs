/*
namespace LaunchLocker.Tests;

[ExcludeFromCodeCoverage]
[TestClass]
public class LockWriterTests : TestBase
{
    [TestMethod]
    public void Write_Should_Work()
    {
        // arrange
        LockBuilder.Build();
        var expectedFile = LockBuilder.LaunchLock.FileName;

        // act
        LockWriter.Write();

        var fileInfo = FileSystem.FileInfo.FromFileName(expectedFile);
        fileInfo.Exists.Should().BeTrue();

        var lockContents = FileSystem.File.ReadAllText(expectedFile);
        lockContents.Length.Should().BeGreaterThan(0);
    }

}
*/
