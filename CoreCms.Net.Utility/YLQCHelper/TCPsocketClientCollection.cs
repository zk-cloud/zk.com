using CoreCms.Net.Models;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCms.Net.Utility.YLQCHelper
{
    public class TCPsocketClientCollection
    {
        private static List<Session> _clients = new List<Session>();
        private static IChannelGroup group;
        private static object obj = new object();

        private static int clientsgroupindex = 0;
        private static Dictionary<int, List<string>> dic = new Dictionary<int, List<string>>();
        private static Dictionary<string, IChannel> ChannelDic = new Dictionary<string, IChannel>();


        //获取登录车辆VIN
        public static List<string> GetOnlineVehicle(){
            return ChannelDic.Keys.ToArray().ToList();
        }

        //添加用户通道绑定
        public static void AddChannelDic(string VIN, IChannel channel) {
            ChannelDic.Add(VIN, channel);
        }
        //获取当前用户绑定关系
        public static IChannel GetChannelDic(string VIN)
        {
            ChannelDic.TryGetValue(VIN, out IChannel channel);
            return channel;
        }

        //更新当前用户绑定关系
        public static void UpdateChannelDic(string VIN,IChannel ctx)
        {
            ChannelDic[VIN] = ctx;            
        }

        //更新绑定关系
        public static void UpdateChannelDic()
        {
            foreach (KeyValuePair<string, IChannel> cd in ChannelDic) {
                if (!group.Contains(cd.Value)) {
                    ChannelDic.Remove(cd.Key);
                }
            }
        }

        //删除绑定关系
        public static async Task DelChannelDic(IChannelHandlerContext context)
        {
            AttributeKey<String> key = AttributeKey<String>.ValueOf("VIN");
            if (context.HasAttribute(key))
                ChannelDic.Remove(context.GetAttribute(key).Get());
            await Task.CompletedTask;
        }


        public static void Addgroup(IChannelHandlerContext contex) {

            lock (obj)
            {
                if (group == null)
                {

                    group = new DefaultChannelGroup(contex.Executor);
                    
                }
                
                group.Add(contex.Channel);

            }
            
        }
        public static IChannelGroup Getgroup() {
            return group;
        }

        
        public static IChannel Getgroupone(string token) {
            return group.AsQueryable().Where(p => p.Id.AsLongText()== token).FirstOrDefault();
        }

        //重复登录时判断原连接是否断开
        public static bool IsOnline(string VIN,out string clientid) {
            LogHelper.Debug("TCP服务器进行在线判断：开始");
            IChannel channel= GetChannelDic(VIN);
            if (channel==null) {
                LogHelper.Debug($"TCP服务器进行在线判断：通道为空");
                clientid = null;
                return false;
            }
            clientid = channel.Id.AsLongText();

            if (!channel.Active) {
                LogHelper.Info($"TCP服务器进行在线判断：通道{clientid}不活动！");
                ChannelDic.Remove(VIN);
                channel.CloseAsync();
            }
            return channel.Active;
        }

        public static void Add(Session client)
        {
            //clientsgroupindex++;
            //clientsgroupindex = clientsgroupindex == 2 ? 0 : clientsgroupindex;
            //if (!dic.ContainsKey(clientsgroupindex))
            //{
            //    dic.Add(clientsgroupindex, new List<string> { client.Id });
            //}
            //else {
            //    List<string> value= new List<string>();
            //    if (dic.TryGetValue(clientsgroupindex, out value))
            //    {
            //        value.Add(client.Id);
            //        dic[clientsgroupindex] = value;
            //    }

            //}
            _clients.Add(client);
        }

        public static void Remove(string clientid)
        {

            _clients.Remove(Get(clientid));
        }

        public static List<string> GetGroup(int index)
        {
            List<string> value = new List<string>();
            if (dic.TryGetValue(index, out value))
            {
                return value;

            }
            return null;
        }

        public static Session Get(string clientId)
        {
            var client = _clients.FirstOrDefault(c => c.Id == clientId);

            return client;
        }

        public static async Task clientdel(IChannelHandlerContext contex)
        {
            group.Remove(contex.Channel);
            AttributeKey<String> key = AttributeKey<String>.ValueOf("VIN");
            if (contex.HasAttribute(key))
            {
                string ctxid = contex.GetAttribute(key).Get();
                ChannelDic.TryGetValue(ctxid,out IChannel ctx1);
                if (contex.Channel.Id.AsLongText() == ctx1.Id.AsLongText()) {
                    ChannelDic.Remove(ctxid);
                }
                
            }
            contex.CloseAsync();
            await Task.CompletedTask;
        }

        public static int clientcount()
        {
            return group!=null? group.Count:0;
        }

        public static int sessioncount()
        {
            return _clients.Count;
        }

    }
}
