using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCms.Net.Model.FromDate
{
    public class FMGPSLocation
    {

        /// <summary>
        /// 角度
        /// </summary>
        public System.Decimal? Radius { get; set; }


        /// <summary>
        /// 经度
        /// </summary>
        public System.Decimal? Longtitude { get; set; }


        /// <summary>
        /// 纬度
        /// </summary>
        public System.Decimal? Latitude { get; set; }


        ///// <summary>
        ///// 坐标
        ///// </summary>
        //public System.String Point { get; set; }


        /// <summary>
        /// 速度（km/h）
        /// </summary>
        public System.Double? Speed { get; set; }


        /// <summary>
        /// 上传时间
        /// </summary>
        public System.DateTime? GPSDate { get; set; }


        /// <summary>
        /// 定位类型
        /// </summary>
        public System.Int32? LOCType { get; set; }


        /// <summary>
        /// 城市代码
        /// </summary>
        public System.String CityCode { get; set; }


        /// <summary>
        /// 省
        /// </summary>
        public System.String Province { get; set; }


        /// <summary>
        /// 市
        /// </summary>
        public System.String City { get; set; }


        /// <summary>
        /// 城区/乡、镇
        /// </summary>
        public System.String Town { get; set; }


        /// <summary>
        /// 街道
        /// </summary>
        public System.String Street { get; set; }


        /// <summary>
        /// 地址
        /// </summary>
        public System.String Address { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        public System.String Remark { get; set; }

        /// <summary>
        /// gps采集时间
        /// </summary>
        public DateTime date { get; set; }
    }
}
