using CoreCms.Net.Caching.AutoMate.RedisCache;
using CoreCms.Net.Configuration;
using CoreCms.Net.Model.FromDate;
using CoreCms.Net.Models;
using CoreCms.Net.Utility;
using DotNetty.Buffers;
using DotNetty.Common.Concurrency;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CoreCms.Net.Core
{
    public class TCPAsyncTask : IRunnable
    {
        //通过注入的方式，把缓存操作接口通过构造函数注入
        private readonly IRedisOperationRepository _cache;

        private readonly IServiceProvider _serviceProvider;

        private string path = "";

        private Session _session;
        private FMTCPMessage _message;
        private string _msg;

        public TCPAsyncTask(
            IServiceProvider serviceProvider,
            IRedisOperationRepository cache,
            Session session,
            string msg
            )
        {
            _serviceProvider = serviceProvider;
            _cache = cache;
            _session = session;
            _msg = msg;
        }

        public void Run()
        {
            MessageRoute(_session, _msg);
        }

        private async Task MessageRoute(Session session, string message)
        {
            var resultmsg = "";
            try
            {
                LogHelper.Debug("传入参数:"+ message);
                
                string pd = message + "&" +  session.Id;
                switch (message.Substring(4,2)) {
                    case "01":
                        await _cache.ListLeftPushAsync(RedisMessageQueueKey.TCPSocketGBT32960Login, pd);

                        break;
                    case "02":
                    case "03":
                        //单条处理
                        //await _cache.ListLeftPushAsync(RedisMessageQueueKey.TCPSocketGBT32960ParameterUp, pd);

                        //批量处理
                        await _cache.ListLeftPushAsync(RedisMessageQueueKey.TCPSocketGBT32960ParameterBatchUp, pd);
                        break;
                    case "04":
                        await _cache.ListLeftPushAsync(RedisMessageQueueKey.TCPSocketGBT32960Logout, pd);
                        break;
                    default:

                        break;
                }
            }
            catch (Exception e)
            {
                LogHelper.Error($"TCP处理报错:{e.Message}", e);
            }
            //return resultmsg;
            //本文唯一值得注意的就是这里，下文会阐述
            IByteBuffer resultval = Unpooled.CopiedBuffer(Encoding.Default.GetBytes(resultmsg));
            await session.Channel.WriteAndFlushAsync(resultval);

        }
    }
}
