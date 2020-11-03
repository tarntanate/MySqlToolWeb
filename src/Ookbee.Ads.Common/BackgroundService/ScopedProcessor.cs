using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ookbee.Ads.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Common.BackgroundService
{
    public abstract class ScopedProcessor : BackgroundService
    {
        public string ServiceName => $"[{GetType().Name}]";
        private readonly IServiceScopeFactory ServiceScopeFactory;
        protected ILogger Logger { get; }

        public ScopedProcessor(
            ILogger logger,
            IServiceScopeFactory serviceScopeFactory)
            : base()
        {
            Logger = logger;
            ServiceScopeFactory = serviceScopeFactory;
        }

        protected override async Task Process(CancellationToken stoppingToken)
        {
            try
            {
                using (var scope = ServiceScopeFactory.CreateScope())
                {
                    Logger.LogInformation($"{ServiceName}: Start executing work at {MechineDateTime.Now}");
                    await ProcessInScope(scope.ServiceProvider, stoppingToken);
                    Logger.LogInformation($"{ServiceName}: Finished executing work at {MechineDateTime.Now}");
                }
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, GetType().Name);
                throw;
            }
        }

        protected abstract Task ProcessInScope(IServiceProvider serviceProvider, CancellationToken stoppingToken);
    }
}
