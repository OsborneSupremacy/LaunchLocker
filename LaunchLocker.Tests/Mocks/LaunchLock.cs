using LaunchLocker.Library;
using Bogus;
using LaunchLocker.Interface;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;

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
