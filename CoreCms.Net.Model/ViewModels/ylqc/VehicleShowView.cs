using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCms.Net.Model.ViewModels.ylqc
{
    public class VehicleShowView
    {
        /// <summary>
        /// 车辆数量
        /// </summary>
        public int vehiclenum { get; set; }

        /// <summary>
        /// 车辆使用数量
        /// </summary>
        public int vehicleusenum { get; set; }

        /// <summary>
        /// 总里程数
        /// </summary>
        public decimal MileageTotal { get; set; }

        /// <summary>
        /// 今日里程数
        /// </summary>
        public decimal MileageDay { get; set; }

        /// <summary>
        /// 今日能耗
        /// </summary>
        public decimal EnergyTotal { get; set; }

        /// <summary>
        /// 百公里能耗
        /// </summary>
        public decimal EnergyConsumption { get; set; }
    }

    public class VehicleDtoView
    {
        /// <summary>
        /// VIN
        /// </summary>
        public string vin { get; set; }


        /// <summary>
        /// 总里程数
        /// </summary>
        public decimal MileageTotal { get; set; }

        /// <summary>
        /// 今日里程数
        /// </summary>
        public decimal MileageDay { get; set; }

        /// <summary>
        /// 今日能耗
        /// </summary>
        public decimal EnergyTotal { get; set; }

    }

    public class VehicleTypeGroupView
    {
        public string typename { get; set; }

        public int vtnum { get; set; }
    }
}
