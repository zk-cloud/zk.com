using System;
using System.Collections.Generic;
using System.Text;

namespace CoreProject.Net.Models.FromDate
{
    public class FMErrorCode
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 错误说明
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 当前定位经度
        /// </summary>
        public decimal Longtitude { get; set; }
        /// <summary>
        /// 当前定位纬度
        /// </summary>
        public decimal Latitude { get; set; }
    }
}
