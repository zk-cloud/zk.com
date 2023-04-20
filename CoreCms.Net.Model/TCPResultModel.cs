using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCms.Net.Models
{
    public class TCPResultModel
    {
        /// <summary>
        /// 返回值
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 返回登录令牌
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 下达指令
        /// </summary>
        public string Instructions { get; set; }
    }
}
