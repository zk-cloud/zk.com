using CoreCms.Net.Caching;
using CoreCms.Net.Caching.AutoMate.RedisCache;
using CoreCms.Net.Configuration;
using CoreCms.Net.Core;
using CoreCms.Net.Utility.YLQCHelper;
using DotNetty.Buffers;
using DotNetty.Common.Concurrency;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Groups;
using DotNetty.Transport.Channels.Sockets;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCms.Net.Core
{
    public class TcpServerHost : IHostedService
    {

        private readonly IServiceProvider serviceProvider;
        private readonly Caching.IRedisOperationRepository cache;
        private IEventLoopGroup bossGroup;
        private IEventLoopGroup workerGroup;
        private IByteBufferAllocator serverBufferAllocator;

        public TcpServerHost(IServiceProvider serviceProvider, Caching.IRedisOperationRepository cache)
        {
            this.cache = cache;
            this.serviceProvider = serviceProvider;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //IEventLoopGroup eventLoop;
            IEventLoopGroup bossGroup;//主要工作组，设置为2个线程
            IEventLoopGroup workerGroup;//子工作组，推荐设置为内核数*2的线程数
            //eventLoop = new MultithreadEventLoopGroup();
            bossGroup = new MultithreadEventLoopGroup(1);//主线程只会实例化一个
            workerGroup = new MultithreadEventLoopGroup(AppSettingsConstVars.TCPServiceMultithreadNum);//子线程组可以按照自己的需求在构造函数里指定数量
            try
            {
                // 服务器引导程序
                var bootstrap = new ServerBootstrap();
               // bootstrap.Group(eventLoop);
                /*
                *ServerBootstrap是一个引导类，表示实例化的是一个服务端对象
                *声明一个服务端Bootstrap，每个Netty服务端程序，都由ServerBootstrap控制，
                *通过链式的方式组装需要的参数
                */
                //添加工作组，其中内部实现为将子线程组内置到主线程组中进行管理
                bootstrap.Group(bossGroup, workerGroup);
                bootstrap.Channel<TcpServerSocketChannel>();
                bootstrap.ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    pipeline.AddLast(new IdleStateHandler(30, 30, 30));//第一个参数为读，第二个为写，第三个为读写全部
                    pipeline.AddLast(new EchoServerHandler(serviceProvider, cache));
                    // IdleStateHandler 心跳
                    //服务端为读IDLE
                }));
                bootstrap.Option(ChannelOption.SoKeepalive, true)
                    .Option(ChannelOption.ConnectTimeout, new TimeSpan(AppSettingsConstVars.TCPServiceLossTime));
                
                IChannel boundChannel = bootstrap.BindAsync(AppSettingsConstVars.TCPServicePort).Result;
                //Hangfire
                //HangfireDispose.TCPHangfireService();

                //signalR client
                //HubConnection connection = new HubConnectionBuilder()
                //.WithUrl(AppSettingsConstVars.SignalRLine)//连接signalr服务端接线器

                //.WithAutomaticReconnect()//自动重连4次
                //.Build();

                //connection.Closed += async (error) =>
                //{
                //    await Task.Delay(1 * 1000);
                //    await connection.StartAsync();
                //};

                //connection.On<string, string>("ReceiveMessage", (user, message) =>
                //{
                //    var list = message.Split('&');
                //    var session = TCPsocketClientCollection.Getgroupone(list[0]);
                //    IByteBuffer resultbyte = Unpooled.CopiedBuffer(Encoding.Default.GetBytes(list[1]));
                //    session.WriteAndFlushAsync(resultbyte);
                //});


                //Console.ReadLine();


                //控制台输入
                string inputFist = Console.ReadLine();
                while (inputFist != null)
                {
                    IChannelGroup group = TCPsocketClientCollection.Getgroup();
                    IByteBuffer resultbyte = Unpooled.CopiedBuffer(Encoding.Default.GetBytes(inputFist));
                    if (group != null)
                        group.WriteAndFlushAsync(resultbyte);
                    inputFist = Console.ReadLine();

                }

                boundChannel.CloseAsync();



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                //eventLoop.ShutdownGracefullyAsync();
                bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
                workerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            var quietPeriod = TimeSpan.FromSeconds(1);
            var shutdownTimeout = TimeSpan.FromSeconds(3);
            return Task.WhenAll(bossGroup.ShutdownGracefullyAsync(quietPeriod, shutdownTimeout), workerGroup.ShutdownGracefullyAsync(quietPeriod, shutdownTimeout));
        }
    }
}
