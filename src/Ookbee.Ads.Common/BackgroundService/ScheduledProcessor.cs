using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NCrontab;
using Ookbee.Ads.Common;
using Ookbee.Common.BackgroundService;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Common
{
    public abstract class ScheduledProcessor : ScopedProcessor
    {
        private CrontabSchedule CrontabSchedule { get; set; }
        public DateTime NextRun { get; set; }

        public ScheduledProcessor(
            ILogger logger,
            IServiceScopeFactory serviceScopeFactory) : base(logger, serviceScopeFactory)
        {

        }

        protected virtual bool IsExecuteOnServerRestart => false;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CrontabSchedule = CrontabSchedule.Parse(Schedule);
            NextRun = IsExecuteOnServerRestart
                ? MechineDateTime.Now.DateTime
                : CrontabSchedule.GetNextOccurrence(MechineDateTime.Now.DateTime);
            do
            {
                if (MechineDateTime.Now.DateTime >= NextRun)
                {
                    await Process(stoppingToken);
                }
                NextRun = CrontabSchedule.GetNextOccurrence(MechineDateTime.Now.DateTime);
                var delay = NextRun - MechineDateTime.Now.DateTime;
                await Task.Delay(delay, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        protected abstract string Schedule { get; }
    }
}
