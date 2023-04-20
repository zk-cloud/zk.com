using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Threading.Tasks;

namespace CoreCms.Net.Models
{
    public class Session : ISession
    {
        public IChannel Channel { get; set; }
        public string Id => Channel?.Id.AsLongText();

        public void Dispose()
        {
            Channel.DisconnectAsync();
            Channel = null;
        }

        public Task Send(IByteBuffer data) => Channel.WriteAsync(data);
        public Task SendAndFlush(IByteBuffer data) => Channel.WriteAndFlushAsync(data);
    }
    public interface ISession : IDisposable
    {
        public string Id { get; }
        Task Send(IByteBuffer data);
        Task SendAndFlush(IByteBuffer data);
        public enum SessionStatus
        {
            Offline
        }
    }
}
