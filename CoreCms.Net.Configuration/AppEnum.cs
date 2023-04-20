using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCms.Net.Configuration
{
    class AppEnum
    {
    }

    public enum FileType { 
        商城酒水订单=1,
        商城饮料订单=2
    }

    /// <summary>
    /// 车辆动作：1 点火、2 熄火、3 上报故障、 4 ota升级开始、 5 ota升级结束、 6通信
    /// </summary>
    public enum VehicleReport {

        点火 = 1,
        熄火 =2,
        上报故障=3,
        ota升级开始=4,
        ota升级结束=5,
        通信=6    
    }
}
