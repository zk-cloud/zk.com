using System;
using System.Collections.Generic;
using System.Text;

namespace CoreProject.Net.Models.FromDate
{
    public class FMTCPMessage
    {
        public string SendClientId { get; set; }



        public string msg { get; set; }

        public string nick { get; set; }

        /// <summary>
        /// 车辆行为
        /// </summary>
        public string action { get; set; }

        /// <summary>
        /// 车辆参数
        /// </summary>
        public string ParamerData { get; set; }

        /// <summary>
        /// 车辆编码
        /// </summary>
        public string VIN { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PWD { get; set; }

        /// <summary>
        /// 车辆登录Token
        /// </summary>
        public string LoginToken { get; set; }

        /// <summary>
        /// 车辆定位信息
        /// </summary>
        public string GPSInfo { get; set; }

        /// <summary>
        /// 车辆更新下载包请求地址
        /// </summary>
        public string VersionPath { get; set; }
    }
}
