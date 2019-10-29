using LaunchLocker.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using LaunchLocker.Interface;
using Newtonsoft.Json;

namespace LaunchLocker.Tests
{
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
            JsonConvert.SerializeObject(GetLaunchLock(fileName), new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

    }
}
