using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCms.Net.Model.Entities
{
    public partial class vehicle_gbt32960
    {


        /// <summary>
        /// 驱动电机列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]

        public List<vehicle_mculog> MCUList { get; set; }

        /// <summary>
        /// 电池电压
        /// </summary>
        [SugarColumn(IsIgnore = true)]

        public vehicle_batterydylog BatterDY { get; set; }

        /// <summary>
        /// 电池温度
        /// </summary>
        [SugarColumn(IsIgnore = true)]

        public vehicle_batterywdlog BatterWD { get; set; }


        /// <summary>
        /// 停留时间（秒）
        /// </summary>
        [SugarColumn(IsIgnore = true)]

        public double StopTotal { get; set; } = 0;

        /// <summary>
        /// 停留时间描述（时分秒）
        /// </summary>
        [SugarColumn(IsIgnore = true)]

        public string StopTotalDesc { get; set; }=null;

        /// <summary>
        /// 是否是停留点
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool isStop { get; set; } = false;

        /// <summary>
        /// 结束停留时间
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public DateTime? StopEnd { get; set; }

        /// <summary>
        /// 逆地理编码地址
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string address { get; set; }

        /// <summary>
        /// 当前行驶里程
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public decimal Mileage { get; set; }

        /// <summary>
        /// 查询结果索引
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int index { get; set; }
    }
}
