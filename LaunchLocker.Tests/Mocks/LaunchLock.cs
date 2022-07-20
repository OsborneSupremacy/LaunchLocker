using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text.Json;
using Bogus;
using LaunchLocker.Interface;
using LaunchLocker.Library;

namespace LaunchLocker.Tests;

[ExcludeFromCodeCoverage]
public class Mocks
{
    public ILaunchLock GetLaunchLock(string fileName, bool useCurrentUser = false)
    {
        var username =
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
            System.Security.Principal.WindowsIdentity.GetCurrent().Name : "Non-windows user";

        return new LaunchLock()
        {
            FileName = fileName,
            IsValid = true,
            Username =
                useCurrentUser
                ? username
                : new Faker().Internet.UserName(),
            LockTime = new Bogus.DataSets.Date().Past(1)
        };
    }

    public string GetLaunchLockJson(string fileName, bool useCurrentUser = false) =>
        JsonSerializer.Serialize(GetLaunchLock(fileName, useCurrentUser));

}
