using CoreCms.Net.Model.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCms.Net.Model.Entities
{
    public partial class vehicle
    {

        /// <summary>
        /// 直接上级
        /// </summary>
        [SugarColumn(IsIgnore = true)]

        public string parentId { get; set; }

        /// <summary>
        /// 所有上级
        /// </summary>
        [SugarColumn(IsIgnore = true)]

        public List<string> parentIdList { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [SugarColumn(IsIgnore = true)]

        public string title { get; set; }

        /// <summary>
        /// 车型信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]

        public vehicle_type vehtype { get; set; }

        /// <summary>
        /// 车辆状态
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int state { get; set; }

        /// <summary>
        /// 车辆定位列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<string> vehiclegbtlist { get; set; }

        /// <summary>
        /// 车辆区间行驶里程
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public decimal mileage { get; set; }


    }
}
