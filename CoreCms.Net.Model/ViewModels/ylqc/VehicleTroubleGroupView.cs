using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCms.Net.Model.ViewModels.ylqc
{
    /// <summary>
    /// 车辆故障分类视图
    /// </summary>
    public class VehicleTroubleGroupView
    {

        /// <summary>
        /// 故障名
        /// </summary>
        public string TroubleName { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string VehicleCode { get; set; }

        /// <summary>
        /// 故障合计数
        /// </summary>
        public int TroubleNum { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string Errorcode { get; set; }

        /// <summary>
        /// 车架号
        /// </summary>
        public string Vin { get; set; }
    }
}
