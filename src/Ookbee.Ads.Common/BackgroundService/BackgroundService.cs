using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Common.BackgroundService
{
    public abstract class BackgroundService : IHostedService
    {
        private Task ExecutingTask;
        private readonly CancellationTokenSource StoppingCts = new CancellationTokenSource();

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            ExecutingTask = ExecuteAsync(StoppingCts.Token);
            if (ExecutingTask.IsCompleted)
                return ExecutingTask;
            return Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            if (ExecutingTask == null)
            {
                return;
            }
            try
            {
                StoppingCts.Cancel();
            }
            finally
            {
                await Task.WhenAny(ExecutingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        protected virtual async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                await Process(stoppingToken);
                await Task.Delay(5000, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        protected abstract Task Process(CancellationToken stoppingToken);
    }
}
