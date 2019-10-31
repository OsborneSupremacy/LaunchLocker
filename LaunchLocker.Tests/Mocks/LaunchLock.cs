using Bogus;
using LaunchLocker.Interface;
using LaunchLocker.Library;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace LaunchLocker.Tests
{
    [ExcludeFromCodeCoverage]
    public class Mocks
    {
        public ILaunchLock GetLaunchLock(string fileName) =>
            new LaunchLock()
            {
                FileName = fileName,
                IsValid = true,
                Username = new Faker().Internet.UserName(),
                LockTime = new Bogus.DataSets.Date().Past(1)
            };

        public string GetLaunchLockJson(string fileName) =>
            JsonSerializer.Serialize(GetLaunchLock(fileName));

    }
}
