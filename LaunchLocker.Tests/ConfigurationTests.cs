using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace LaunchLocker.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void CheckIfValid_Should_BeFalse_When_No_Args()
        {
            var config = new Library.Configuration(new string[] { });

            var IsValid = config.CheckIfValid(out string message);

            IsValid.Should().BeFalse();
        }
    }
}
