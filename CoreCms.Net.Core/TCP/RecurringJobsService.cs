using CoreCms.Net.Utility;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCms.Net.Core
{
    public class RecurringJobsService : BackgroundService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                //HangfireDispose.TCPHangfireService();
            }
            catch (Exception e)
            {
                LogHelper.Error(e.Message, e);
            }

            return Task.CompletedTask;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
