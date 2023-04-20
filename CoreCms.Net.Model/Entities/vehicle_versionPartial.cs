using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCms.Net.Model.Entities
{
    public partial class vehicle_version
    {
        /// <summary>
        /// 是否需要更新
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool isUpdate { get; set; }

        /// <summary>
        /// 车辆批次集合
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<string> bitchList{ get; set; }

        /// <summary>
        /// 车辆固件集合
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<string> firmwareList { get; set; }
    }
}
