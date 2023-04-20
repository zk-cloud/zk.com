using CoreCms.Net.Caching;
using CoreCms.Net.IServices;
using CoreCms.Net.Utility.YLQCHelper;
using DotNetty.Buffers;
using DotNetty.Transport.Channels.Groups;
using System;
using System.Text;

namespace CoreCms.Net.Task
{
    public class AutoSendTCPAllJob
    {

        private readonly Ivehicle_TCPSocketServices _Ivehicle_TCPSocketServices;
        private readonly IRedisOperationRepository _cache;


        public AutoSendTCPAllJob(Ivehicle_TCPSocketServices Ivehicle_TCPSocketServices, IRedisOperationRepository cache) {
            _Ivehicle_TCPSocketServices = Ivehicle_TCPSocketServices;
            _cache = cache;
        }

        public async System.Threading.Tasks.Task Execute() {
            Console.WriteLine($"当前客户端数量：{TCPsocketClientCollection.clientcount()}; SessionList:{TCPsocketClientCollection.sessioncount()}");
            IChannelGroup group = TCPsocketClientCollection.Getgroup();
            IByteBuffer resultbyte = Unpooled.CopiedBuffer(Encoding.Default.GetBytes("YLQC,PING;\r\n"));
            if(group!=null) 
                group.WriteAndFlushAsync(resultbyte);
        }

    }
}
