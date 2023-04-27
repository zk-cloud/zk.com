using Autofac.Extensions.DependencyInjection;
using CoreCms.Net.Core;
using CoreCms.Net.WebSocket;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoreCms.Net.TCPSocket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseUrls($"http://*:1010");
                })
            .ConfigureServices(services =>
            {

                services.AddHostedService<TcpServerHost>();
            })
            .ConfigureLogging(logging => {
                logging.ClearProviders(); //移除已经注册的其他日志处理程序
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace); //设置最小的日志级别
            })
            .UseLog4Net();
    }
}
