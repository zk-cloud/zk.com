using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCms.Net.Model.FromDto
{
    public class FMVehicleLoginDto
    {
        /// <summary>
        /// 车辆VIN码
        /// </summary>
        public string VIN { get; set; }
        /// <summary>
        /// 车辆密码
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 车辆连接ID（socket使用）
        /// </summary>
        public string clientId { get; set; }
    }
}
