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
                logging.ClearProviders(); //�Ƴ��Ѿ�ע���������־�������
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace); //������С����־����
            })
            .UseLog4Net();
    }
}
