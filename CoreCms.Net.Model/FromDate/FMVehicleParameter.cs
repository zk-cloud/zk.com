using System;
using System.Collections.Generic;
using System.Text;

namespace CoreProject.Net.Models.FromDate
{
    /// <summary>
    /// 车辆参数上传标准
    /// </summary>
    public class FMVehicleParameter
    {
        /// <summary>
        /// 当前车辆是否正常
        /// </summary>
        public bool isNormal { get; set; }
        /// <summary>
        /// 车辆上传参数
        /// </summary>
        public Dictionary<string, object> Parameter { get;set;}
        /// <summary>
        /// 当前定位经度
        /// </summary>
        public decimal Longtitude{ get; set; }
        /// <summary>
        /// 当前定位纬度
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// 真实记录时间
        /// </summary>
        public DateTime date { get; set; }
    }
}
