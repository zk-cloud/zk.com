﻿/***********************************************************************
 *            Project: CoreCms.Net                                     *
 *                Web: https://CoreCms.Net                             *
 *        ProjectName: 核心内容管理系统                                *
 *             Author: 大灰灰                                          *
 *              Email: JianWeie@163.com                                *
 *         CreateTime: 2020-09-06 23:39:37
 *        Description: 暂无
 ***********************************************************************/


using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace CoreCms.Net.Utility.Hub
{
    public class ChatHub : Hub<IChatClient>
    {
        //继承IUserIdProvider用于告诉SignalR连接用谁来绑定用户id
        public class ChatHubGetUserId : IUserIdProvider
        {
            string IUserIdProvider.GetUserId(HubConnectionContext connection)
            {
                //获取当前登录用户id
                //string userid = connection.User.Identity.Name; 
                string userid = connection.User.FindFirst(p => p.Type == JwtRegisteredClaimNames.Jti).Value;
                return userid;
            }
        }

        /// <summary>
        /// 向指定群组发送信息
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="message">信息内容</param>  
        /// <returns></returns>
        public async Task SendMessageToGroupAsync(string groupName, string message)
        {
            await Clients.Group(groupName).ReceiveMessage(message);
        }

        /// <summary>
        /// 加入指定组
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns></returns>
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// 退出指定组
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns></returns>
        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// 向指定成员发送信息
        /// </summary>
        /// <param name="user">成员名</param>
        /// <param name="message">信息内容</param>
        /// <returns></returns>
        public async Task SendPrivateMessage(string user, string message)
        {
            await Clients.User(user).ReceiveMessage(message);
        }

        /// <summary>
        /// 当连接建立时运行
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            //TODO..
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// 当链接断开时运行
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(System.Exception ex)
        {
            //TODO..
            return base.OnDisconnectedAsync(ex);
        }


        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }

        //定于一个通讯管道，用来管理我们和客户端的连接
        //1、客户端调用 GetLatestCount，就像订阅
        public async Task GetLatestCount(string random)
        {
            //2、服务端主动向客户端发送数据，名字千万不能错
            var dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            await Clients.All.ReceiveUpdate(dt);
            //3、客户端再通过 ReceiveUpdate ，来接收

        }
    }
}
