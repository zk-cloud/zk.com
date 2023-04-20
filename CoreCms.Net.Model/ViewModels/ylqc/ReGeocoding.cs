using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCms.Net.Model.ViewModels.ylqc
{
    /// <summary>
    /// （周边poi数组）
    /// </summary>
    public class pois
    {
        /// <summary>
        /// 		地址信息
        /// </summary>
        public string addr { get; set; }

        /// <summary>
        /// 	和当前坐标点的方向
        /// </summary>
        public string direction { get; set; }

        /// <summary>
        /// 	离坐标点距离
        /// </summary>
        public int distance { get; set; }

        /// <summary>
        /// 	poi名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 	poi类型，如’美食;中餐厅’。tag与poiType字段均为poi类型，建议使用tag字段，信息更详细。poi详细类别
        /// </summary>
        public string tag { get; set; }

        /// <summary>
        /// 	邮编
        /// </summary>
        public int zip { get; set; }


    }
    //经纬度坐标类
    public class location
    {
        /// <summary>
        ///  纬度值 float
        /// </summary>
        public float lat { get; set; }

        /// <summary>
        ///  经度值 float
        /// </summary>
        public float lng { get; set; }
    }

    public class addressComponent
    {
        /// <summary>
        /// 	国家
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 	国家编码 int
        /// </summary>
        public int country_code { get; set; }
        /// <summary>
        /// 	国家英文缩写（三位） 	string
        /// </summary>
        public string country_code_iso { get; set; }
        /// <summary>
        /// 	国家英文缩写（两位）
        /// </summary>
        public string country_code_iso2 { get; set; }
        /// <summary>
        /// 省名
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 	城市名
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 	城市所在级别（仅国外有参考意义。国外行政区划与中国有差异，城市对应的层级不一定为『city』。country、province、city、district、town分别对应0-4级，若city_level=3，则district层级为该国家的city层级） 	int
        /// </summary>
        public int city_level { get; set; }
        /// <summary>
        /// 	区县名
        /// </summary>
        public string district { get; set; }
        /// <summary>
        /// 乡镇名，需设置extensions_town=true时才会返回 string
        /// </summary>
        public string town { get; set; }
        /// <summary>
        /// 	乡镇id string
        /// </summary>
        public string town_code { get; set; }

        /// <summary>
        /// 	街道名（行政区划中的街道层级）
        /// </summary>
        public string street { get; set; }
        /// <summary>
        /// 	街道门牌号
        /// </summary>
        public string street_number { get; set; }
        /// <summary>
        /// 	行政区划代码，前往下载
        /// </summary>
        public int adcode { get; set; }
        /// <summary>
        /// 	相对当前坐标点的方向，当有门牌号的时候返回数据
        /// </summary>
        public string direction { get; set; }
        /// <summary>
        /// 	相对当前坐标点的距离，当有门牌号的时候返回数据
        /// </summary>
        public string distance { get; set; }
    }

    public class ReGeocoding
    {
        /// <summary>
        /// 返回结果状态值， 成功返回0，其他值请查看下方返回码状态表。 	int
        /// </summary>
        public int status { get; set; }

        public ReGeocodResult result { get; set; }

    }

    /// <summary>
    /// 坐标转换
    /// </summary>
    public class GpsToBmapPoint
    {
        /// <summary>
        /// 本次API访问状态，如果成功返回0，如果失败返回其他数字 
        /// </summary>
        public int status { get; set; }

        public List<BmapPoint> result { get; set; }
    }

    public class BmapPoint
    {
        /// <summary>
        /// 经度
        /// </summary>
        public decimal x { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public decimal y { get; set; }
    }

    public class ReGeocodResult
    {


        public location location { get; set; }

        /// <summary>
        /// 	结构化地址信息 string
        /// </summary>
        public string formatted_address { get; set; }
        /// <summary>
        /// 	坐标所在商圈信息，如 "人民大学,中关村,苏州街"。最多返回3个。 	string
        /// </summary>
        public string business { get; set; }
        public addressComponent addressComponent { get; set; }
        /// <summary>
        /// 周边poi数组
        /// </summary>
        public List<pois> pois { get; set; }



    }
}
