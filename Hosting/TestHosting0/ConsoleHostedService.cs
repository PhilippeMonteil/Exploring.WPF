
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace TestHosting0
{

    internal sealed class ConsoleHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;

        public ConsoleHostedService(
            ILogger<ConsoleHostedService> logger,
            IHostApplicationLifetime appLifetime)
        {
            _logger = logger;
            _appLifetime = appLifetime;
        }

        void log(string Txt, [CallerMemberName] string? Member = null)
        {
            _logger.LogInformation($"[{Thread.CurrentThread.ManagedThreadId}] >>> {GetType().Name}.{Member}{Member} '{Txt}'");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            log($"StartAsync with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

            _appLifetime.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        log("Task.Run(-)");

                        // Simulate real work is being done
                        await Task.Delay(1000);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Unhandled exception!");
                    }
                    finally
                    {
                        log("Task.Run(+)");
                        // Stop the application once the work is done
                        _appLifetime.StopApplication();
                        log("Task.Run(++)");
                    }
                });
            });

            _appLifetime.ApplicationStopping.Register(() =>
            {
                log($"ApplicationStopping");
            });

            _appLifetime.ApplicationStopped.Register(() =>
            {
                log($"ApplicationStopped");
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            log($"ConsoleHostedService.StopAsync");
            return Task.CompletedTask;
        }
    }

}
