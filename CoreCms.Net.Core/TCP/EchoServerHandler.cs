using Castle.Core.Internal;
using CoreCms.Net.Caching.AutoMate.RedisCache;
using CoreCms.Net.Configuration;
using CoreCms.Net.IServices;
using CoreCms.Net.Model.Entities;
using CoreCms.Net.Model.FromDate;
using CoreCms.Net.Model.FromDto;
using CoreCms.Net.Models;
using CoreCms.Net.Utility;
using CoreCms.Net.Utility.Extensions;
using CoreCms.Net.Utility.YLQCHelper;
using DotNetty.Buffers;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCms.Net.Core
{
    /// <summary>
    /// 因为服务器只需要响应传入的消息，所以只需要实现ChannelHandlerAdapter就可以了
    /// </summary>
    public class EchoServerHandler : ChannelHandlerAdapter
    {
        //通过注入的方式，把缓存操作接口通过构造函数注入
        private readonly IRedisOperationRepository _cache;

        private readonly IServiceProvider _serviceProvider;

        private string path = "";


        private static readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public EchoServerHandler(
            IServiceProvider serviceProvider,
            IRedisOperationRepository cache)
        {
            _serviceProvider = serviceProvider;
            _cache = cache;
        }


        public async Task UpdateCannelUP(IChannelHandlerContext ctx) {
            
        }

        public override void ChannelActive(IChannelHandlerContext contex)
        {
            TCPsocketClientCollection.Addgroup(contex);
        }

        /// <summary>
        /// 每个传入消息都会调用
        /// 处理传入的消息需要复写这个方法
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="msg"></param>
        public override void ChannelRead(IChannelHandlerContext ctx, object msg)
        {
            IByteBuffer request = msg as IByteBuffer;

            IByteBuffer request1 = msg as IByteBuffer;



             //var channelId = ctx.Channel.Id.AsShortText();
            string hexstr = "";
            int length = request.ReadableBytes;

            //for (int i = 0; i < length; i++)
            //{
            //    hexstr += request.GetByte(i);
            //}
            hexstr =ByteBufferUtil.HexDump(request);

            string str = request1.ReadString(length, Encoding.UTF8).Trim();
            
        
        //Ivehicle_logServices vl = _serviceProvider.GetService<Ivehicle_logServices>();
        //    object obj = new
        //    {
        //        title = "TCP接收参数",
        //        MSGHex = hexstr,
        //        MSGstr = str,
        //        Datetime = DateTime.Now
        //    };
            //LogHelper.Info(obj.ToJson()); 
            Session session = new Session { Channel = ctx.Channel };
            _lock.EnterWriteLock();
            //TCPsocketClientCollection.Add(session);
            _lock.ExitWriteLock();
            if (str.IndexOf('{') < 0)
            {
                

                // 2 . 获取通道对应的事件循环
                IEventLoop eventLoop = ctx.Channel.EventLoop;
                // 3 . 在 Runnable 中用户自定义耗时操作, 异步执行该操作, 该操作不能阻塞在此处执行
                TCPAsyncTask tcpat = new TCPAsyncTask(_serviceProvider, _cache, session, str);
                eventLoop.Execute(tcpat);
            }
        
            else
            {
                //本文唯一值得注意的就是这里，下文会阐述
                //IByteBuffer result = Unpooled.CopiedBuffer(Encoding.UTF8.GetBytes(obj.ToJson()));
               // LogHelper.Debug("传入数据："+ str);
                ctx.WriteAsync(request);
            }
        }


        //重写客户端断开事件
        public override void HandlerRemoved(IChannelHandlerContext ctx)
        {
            LogHelper.Info($"触发TCP断开事件，关闭连接！");
            //var session= TCPsocketClientCollection.Get(ctx.Channel.Id.AsShortText());
            // 2 . 获取通道对应的事件循环
            IEventLoop eventLoop = ctx.Channel.EventLoop;
            // 3 . 在 Runnable 中用户自定义耗时操作, 异步执行该操作, 该操作不能阻塞在此处执行
            //TCPRemoveAsyncTask tcpat = new TCPRemoveAsyncTask(_serviceProvider, _cache, session);
            TCPRemoveAsyncTask tcpat = new TCPRemoveAsyncTask(_serviceProvider, _cache, ctx);
            eventLoop.Execute(tcpat);
            //_lock.EnterWriteLock();
            //TCPsocketClientCollection.Remove(ctx.Channel.Id.AsShortText());
            //_lock.ExitWriteLock();
            base.HandlerRemoved(ctx);

        }

        /// <summary>
        /// 心跳包超时重写
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="evt"></param>
        public override void UserEventTriggered(IChannelHandlerContext ctx, object evt)
        {
            //Console.WriteLine("已经15秒未收到客户端的消息了！");
            LogHelper.Info($"连接{ctx.Channel.RemoteAddress}超过30秒无读写，关闭连接！");
            
            if (evt is IdleStateEvent eventState)
            {
                // 2 . 获取通道对应的事件循环
                IEventLoop eventLoop = ctx.Channel.EventLoop;
                // 3 . 在 Runnable 中用户自定义耗时操作, 异步执行该操作, 该操作不能阻塞在此处执行
                //TCPRemoveAsyncTask tcpat = new TCPRemoveAsyncTask(_serviceProvider, _cache, session);
                TCPReconnectAsyncTask tcpat = new TCPReconnectAsyncTask(_serviceProvider, _cache, ctx);
                eventLoop.Execute(tcpat);
                HandlerRemoved(ctx);
            }
            else
            {
                base.UserEventTriggered(ctx, evt);
            }
        }


        private void HandleArray(byte[] array, int offset, int length)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 批量读取中的最后一条消息已经读取完成
        /// </summary>
        /// <param name="context"></param>
        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            context.Flush();
        }
        /// <summary>
        /// 发生异常
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception exception)
        {
            Console.WriteLine(exception);
            //ctx.CloseAsync();

            LogHelper.Info("客户端:" + ctx.Channel.RemoteAddress + "主动关闭连接", exception);
            // 2 . 获取通道对应的事件循环
            IEventLoop eventLoop = ctx.Channel.EventLoop;
            // 3 . 在 Runnable 中用户自定义耗时操作, 异步执行该操作, 该操作不能阻塞在此处执行
            //TCPRemoveAsyncTask tcpat = new TCPRemoveAsyncTask(_serviceProvider, _cache, session);
            TCPReconnectAsyncTask tcpat = new TCPReconnectAsyncTask(_serviceProvider, _cache, ctx);
            eventLoop.Execute(tcpat);
        }

        private async Task MessageRoute(Session session, FMTCPMessage message)
        {
            var resultmsg = "";
            Ivehicle_SocketServices _IvehicleServices = _serviceProvider.GetService<Ivehicle_SocketServices>();
            try
            {
                switch (message.action)
                {
                    case "Login":
                        FMVehicleLoginDto dto = new FMVehicleLoginDto
                        {
                            VIN = message.VIN,
                            Pwd = message.PWD,
                            clientId = message.SendClientId
                        };
                        var result = _IvehicleServices.SocketLogin(dto);
                        resultmsg = result.ToJson();
                        break;
                    case "Logout":
                        var outresult = new WebApiCallBack();
                        if (message.LoginToken != message.SendClientId)
                        {
                            outresult = WebApiCallBack.Failed(403, "授权失败，通道码错误！");
                        }
                        else
                        {
                            var vehicleresult = _cache.Get(message.LoginToken).Result;
                            if (vehicleresult.IsNullOrEmpty())
                            {
                                outresult = WebApiCallBack.Failed(403, "请先登录!");
                            }
                            else
                            {

                                outresult = WebApiCallBack.Success(_IvehicleServices.SocketLogout(message.LoginToken));
                            }
                        }
                        resultmsg = outresult.ToJson();
                        break;
                    case "ParamerUp":
                        WebApiCallBack upresult = new WebApiCallBack();
                        //FMVehicleParameter updto = SerializeExtensions.ToObject<FMVehicleParameter>(message.ParamerData);
                        if (message.LoginToken != message.SendClientId)
                        {
                            upresult = WebApiCallBack.Failed(403, "授权失败，通道码错误！");
                        }
                        else
                        {
                            var vehicleresult = await _cache.Get(message.LoginToken);
                            if (vehicleresult.IsNullOrEmpty())
                            {
                                upresult = WebApiCallBack.Failed(403, "请先登录!");
                            }
                            else
                            {

                                string pd = message.ParamerData + "&&" + vehicleresult + "&&" + message.LoginToken;
                                await _cache.ListLeftPushAsync(RedisMessageQueueKey.TCPSocketVehicleParameterUp, pd);
                                upresult = WebApiCallBack.Success("数据已上传！");
                            }
                        }
                        resultmsg = upresult.ToJson();
                        break;
                    case "GPSInfoUp":
                        WebApiCallBack Gpsresult = new WebApiCallBack();
                        //FMVehicleParameter updto = SerializeExtensions.ToObject<FMVehicleParameter>(message.ParamerData);
                        if (message.LoginToken != message.SendClientId)
                        {
                            Gpsresult = WebApiCallBack.Failed(403, "授权失败，通道码错误！");
                        }
                        else
                        {
                            var vehicleresult = await _cache.Get(message.LoginToken);
                            if (vehicleresult.IsNullOrEmpty())
                            {
                                Gpsresult = WebApiCallBack.Failed(403, "请先登录!");
                            }
                            else
                            {

                                string pd = message.ParamerData + "&&" + vehicleresult + "&&" + message.LoginToken;
                                await _cache.ListLeftPushAsync(RedisMessageQueueKey.SocketVehicleParameterUp, pd);
                                Gpsresult = WebApiCallBack.Success("数据已上传！");
                            }
                        }
                        resultmsg = Gpsresult.ToJson();
                        break;
                    case "CheckForUpdates":
                        Ivehicle_versionServices vv = _serviceProvider.GetService<Ivehicle_versionServices>();
                        WebApiCallBack Updateresult = new WebApiCallBack();
                        //FMVehicleParameter updto = SerializeExtensions.ToObject<FMVehicleParameter>(message.ParamerData);
                        if (message.LoginToken != message.SendClientId)
                        {
                            Updateresult = WebApiCallBack.Failed(403, "授权失败，通道码错误！");
                        }
                        else
                        {
                            var vehicleresult = _cache.Get(message.LoginToken).Result;
                            if (vehicleresult.IsNullOrEmpty())
                            {
                                Updateresult = WebApiCallBack.Failed(403, "请先登录!");
                            }
                            else
                            {

                                vehicle model = JsonConvert.DeserializeObject<vehicle>(vehicleresult);
                                var vmodel = vv.CheckForUpdates(model).Result;
                                Updateresult = WebApiCallBack.Success(vmodel.isUpdate ? $"车辆控制器存在新版本，大小为{vmodel.fileSize}KB，是否进行更新？" : "当前版本为最新，无需更新！");
                                path = vmodel.path;
                            }
                        }
                        resultmsg = Updateresult.ToJson();
                        break;
                    //case "EditionUpdate":

                    //    if (message.LoginToken != message.SendClientId)
                    //    {
                    //        Updateresult = WebApiCallBack.Failed(403, "授权失败，通道码错误！");
                    //    }
                    //    else
                    //    {
                    //        var vehicleresult = _cache.Get(message.LoginToken).Result;
                    //        if (vehicleresult.IsNullOrEmpty())
                    //        {
                    //            Updateresult = WebApiCallBack.Failed(403, "请先登录!");
                    //        }
                    //        else
                    //        {
                    //            if (!path.IsNullOrEmpty())
                    //                client.SendFileAsync(path);
                    //        }
                    //    }
                    //    break;
                    default:
                        break;
                }
            }
            catch (Exception e) {
                LogHelper.Error($"TCP处理报错:{e.Message}",e);
            }
            IByteBuffer resultval = Unpooled.CopiedBuffer(Encoding.Default.GetBytes(resultmsg));
            await session.Channel.WriteAndFlushAsync(resultval);
        }
    }
}
