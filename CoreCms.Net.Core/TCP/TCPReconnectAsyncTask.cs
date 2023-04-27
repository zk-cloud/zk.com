using CoreCms.Net.Caching;
using CoreCms.Net.Caching.AutoMate.RedisCache;
using CoreCms.Net.Configuration;
using CoreCms.Net.Model.Entities;
using CoreCms.Net.Model.FromDate;
using CoreCms.Net.Models;
using CoreCms.Net.Utility;
using CoreCms.Net.Utility.Extensions;
using CoreCms.Net.Utility.YLQCHelper;
using DotNetty.Common.Concurrency;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreCms.Net.Core
{
    public class TCPReconnectAsyncTask : IRunnable
    {
        //通过注入的方式，把缓存操作接口通过构造函数注入
        private readonly IRedisOperationRepository _cache;

        private readonly IServiceProvider _serviceProvider;

        private string path = "";

        private Session _session;
        private FMTCPMessage _message;
        private IChannelHandlerContext _ctx;

        public TCPReconnectAsyncTask(
            IServiceProvider serviceProvider,
            IRedisOperationRepository cache,

            //Session session
            IChannelHandlerContext ChannelHandlerContext
            )
        {
            _serviceProvider = serviceProvider;
            _cache = cache;
            _ctx = ChannelHandlerContext;
        }

        public void Run()
        {
            MessageRoute(_ctx);
        }

        private async Task MessageRoute(IChannelHandlerContext ctx)
        {
            try
            {
                LogHelper.Info("重连机制开始！");
                AttributeKey<String> key = AttributeKey<String>.ValueOf("VIN");
                var cannelcode = ctx.Channel.Id.AsLongText();
                var vehicle1 = new vehicle();
                await _cache.LockActionAsync($"Select{cannelcode}",async()=> { vehicle1 = await _cache.GetAsync<vehicle>(cannelcode); });
                LogHelper.Info($"重连机制车辆信息:{JsonConvert.SerializeObject(vehicle1)}");
                string VIN = ctx.GetAttribute(key).Get();
                string vinkey = VIN + "_loss";
                long lenght = await _cache.ListLengthAsync(vinkey);
                if (lenght > 0)
                {
                    string lastmsg = await _cache.ListLeftPopAsync(vinkey);
                    string lastcode = lastmsg.Split('&')[1];
                    IChannel chanel = TCPsocketClientCollection.Getgroupone(lastcode);
                    //添加绑定
                    TCPsocketClientCollection.UpdateChannelDic(VIN, chanel);

                    // 双向绑定
                    // channel -> userId
                    chanel.GetAttribute(key).Set(VIN);
                    LogHelper.Info($"重连机制原通道号：{cannelcode}    新通道号:{lastcode}");
                    await _cache.Set(lastcode, vehicle1, TimeSpan.FromMinutes(60 * 24));
                    //await _cache.ListLeftPushAsync(RedisMessageQueueKey.TCPSocketLoss, ctx.Channel.Id.AsLongText());
                    LogHelper.Info("重连机制完成！");
                    List<string> gbtlist = new List<string> { lastmsg };
                    //await _cache.ListLeftPushAsync(RedisMessageQueueKey.TCPSocketGBT32960ParameterBatchUp, lastmsg);
                    string result = null;
                    while (!(result = await _cache.ListRightPopAsync(vinkey)).IsNullOrEmpty())
                    {
                        if (result.Split('&')[1] == lastcode)
                        {
                            gbtlist.Add(result);
                        }
                    }
                    await _cache.ListRightPushAsync(RedisMessageQueueKey.TCPSocketGBT32960ParameterBatchUp, gbtlist);
                    //if (lenght == 1)
                    //{
                    //    await _cache.ListLeftPushAsync(RedisMessageQueueKey.TCPSocketGBT32960ParameterBatchUp, lastmsg);
                    //}
                    //else
                    //{
                    //    await _cache.ListLeftPushAsync(RedisMessageQueueKey.TCPSocketGBT32960ParameterBatchUp, lastmsg);
                    //    for (long i = 0; i < lenght - 1; i++)
                    //    {
                    //        string result = await _cache.ListRightPopAsync(vinkey);
                    //        if (result.Split('&')[1] == lastcode)
                    //        {
                    //            await _cache.ListLeftPushAsync(RedisMessageQueueKey.TCPSocketGBT32960ParameterBatchUp, result);
                    //        }
                    //    }
                    //}





                }
                else {
                    LogHelper.Info("重连机制:无重连管道！");
                }
            }
            catch (Exception e) {
                LogHelper.Error("重连机制出错！",e);
            }

            //await Task.WhenAll(TCPsocketClientCollection.clientdel(ctx), _cache.ListLeftPushAsync(RedisMessageQueueKey.TCPSocketLoss, cannelcode));

        }
    }
}
