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
        public ILaunchLock GetLaunchLock(string fileName, bool useCurrentUser = false) =>
            new LaunchLock()
            {
                FileName = fileName,
                IsValid = true,
                Username = 
                    useCurrentUser 
                    ? System.Security.Principal.WindowsIdentity.GetCurrent().Name
                    : new Faker().Internet.UserName(),
                LockTime = new Bogus.DataSets.Date().Past(1)
            };

        public string GetLaunchLockJson(string fileName, bool useCurrentUser = false) =>
            JsonSerializer.Serialize(GetLaunchLock(fileName, useCurrentUser));

    }
}
