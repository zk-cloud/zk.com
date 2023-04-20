using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreProject.Net.Models.FromDate
{
    public class FMSocketModel
    {
        public string Id{ get; set; }
        public WebSocket WebSocket { get; set; }

        public string RoomNo { get; set; }

        public Task SendMessageAsync(string message)
        {
            var msg = Encoding.UTF8.GetBytes(message);
            return WebSocket.SendAsync(new ArraySegment<byte>(msg, 0, msg.Length), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public Task SendFileAsync(string path)
        {
            path = AppDomain.CurrentDomain.BaseDirectory + path;
            //创建d:\file.txt的FileStream对象
            FileStream fstream = new FileStream(path, FileMode.OpenOrCreate);
            byte[] bData = new byte[fstream.Length];
            //设置流当前位置为文件开始位置
            fstream.Seek(0, SeekOrigin.Begin);

            //将文件的内容存到字节数组中（缓存）
            fstream.Read(bData, 0, bData.Length);
            string result = Encoding.UTF8.GetString(bData);
            if (fstream != null)
            {
                //清除此流的缓冲区，使得所有缓冲的数据都写入到文件中
                fstream.Flush();
                fstream.Close();
            }
            return WebSocket.SendAsync(new ArraySegment<byte>(bData, 0, bData.Length), WebSocketMessageType.Binary, true, CancellationToken.None);
        }
    }
}
