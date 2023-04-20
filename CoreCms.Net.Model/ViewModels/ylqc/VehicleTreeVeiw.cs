using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCms.Net.Model.ViewModels.ylqc
{
    /// <summary>
    /// 车辆选择树视图
    /// </summary>
    public class VehicleTreeVeiw
    {
        /// <summary>
        /// 当前树ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public string parentId { get; set; }
        /// <summary>
        /// 树名字
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 树类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 子树
        /// </summary>
        public List<VehicleTreeVeiw> children { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int state { get; set; }

    }
}
