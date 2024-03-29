﻿using System;
using System.Threading;
using System.Threading.Tasks;
using LaunchLocker.Interface;
using Microsoft.Extensions.Hosting;

namespace LaunchLocker.UI;

public class ConsoleHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _applicationLifetime;

    private readonly ILaunchLockProcess _launchLockProcess;

    private readonly ICommunicator _communicator;

    public ConsoleHostedService(
        IHostApplicationLifetime applicationLifetime,
        ILaunchLockProcess launchLockProcess,
        ICommunicator communicator
    )
    {
        _applicationLifetime = applicationLifetime;
        _launchLockProcess = launchLockProcess;
        _communicator = communicator;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var args = Environment.GetCommandLineArgs();

        _applicationLifetime.ApplicationStarted.Register(async () =>
        {
            try
            {
                await Task.Run(() =>
                {
                    _launchLockProcess.Execute(Environment.GetCommandLineArgs());
                });
            }
            catch (Exception ex)
            {
                _communicator.WriteSentence(ex.ToString());
            }
            finally
            {
                _applicationLifetime.StopApplication();
            }
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
