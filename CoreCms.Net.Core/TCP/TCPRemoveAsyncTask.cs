using CoreCms.Net.Caching;
using CoreCms.Net.Caching.AutoMate.RedisCache;
using CoreCms.Net.Configuration;
using CoreCms.Net.Model.FromDate;
using CoreCms.Net.Utility.YLQCHelper;
using DotNetty.Common.Concurrency;
using DotNetty.Transport.Channels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCms.Net.Core
{
    public class TCPRemoveAsyncTask : IRunnable
    {
        //通过注入的方式，把缓存操作接口通过构造函数注入
        private readonly IRedisOperationRepository _cache;

        private readonly IServiceProvider _serviceProvider;

        private readonly IChannelHandlerContext _ChannelHandlerContext;

        private string path = "";

        //private Session _session;
        private string _clientid;
        private FMTCPMessage _message;
        private static readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public TCPRemoveAsyncTask(
            IServiceProvider serviceProvider,
            IRedisOperationRepository cache,

            //Session session
            IChannelHandlerContext ChannelHandlerContext 
            )
        {
            _serviceProvider = serviceProvider;
            _cache = cache;
            // _session = session;
            _ChannelHandlerContext = ChannelHandlerContext;
            _clientid = ChannelHandlerContext.Channel.Id.AsLongText();
        }

        public void Run()
        {
           // MessageRoute(_session);
            MessageRoute(_clientid);
        }

        //private async Task MessageRoute(Session session)
        //{
        //    await _cache.ListLeftPushAsync(RedisMessageQueueKey.TCPSocketLoss, session.Id);
        //}
        private async Task MessageRoute(string clientid)
        {
            //await _cache.ListLeftPushAsync(RedisMessageQueueKey.TCPSocketLoss, session.Id);
            await Task.Delay(2000);
            await Task.WhenAll(TCPsocketClientCollection.clientdel(_ChannelHandlerContext), _cache.ListLeftPushAsync(RedisMessageQueueKey.TCPSocketLoss, clientid));
        }
    }
}
