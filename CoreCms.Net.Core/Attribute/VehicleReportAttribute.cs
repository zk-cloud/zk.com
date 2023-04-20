using CoreCms.Net.Configuration;
using CoreCms.Net.Core.Attrbute;
using CoreCms.Net.IServices;
using CoreCms.Net.Model.Entities;
using CoreCms.Net.Models;
using CoreCms.Net.Utility.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCms.Net.Core.Attribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class VehicleReportAttribute : System.Attribute
    {
        private VehicleReport _ActType;

        protected string _VIN { get; }

        //protected bool _isNormal { get; }
        //protected string _ErrorCode { get; }
        //protected string _ErrorMsg { get; }
        public VehicleReportAttribute(VehicleReport ActType
            //, bool isNormal, string ErrorCode, string ErrorMsg, string VIN
            )
        {
            _ActType = ActType;
            //_isNormal = isNormal;
            //_ErrorCode = ErrorCode;
            //_ErrorMsg = ErrorMsg;
            //_VIN = VIN;
        }

        public async Task After(IAOPContext context)
        {
            //Ibill_feeitemServices Ibill_feeitemServices = context.ServiceProvider.GetService<Ibill_feeitemServices>();
            Ivehicle_logServices _Ivehicle_logServices = context.ServiceProvider.GetService<Ivehicle_logServices>();
            var obj = context.Arguments[0];//获取第一个参数
            var returnobj = (WebApiCallBack)context.ReturnValue;
            vehicle_log vlog = new vehicle_log
            {
                VIN = obj.GetPropertyValue("VIN")?.ToString(),
                isNormal = returnobj.code == 0,
                ErrorCode = returnobj.code == 0 ? null : "WL_" + returnobj.code,
                ErrorMsg = returnobj.code == 0 ? null : returnobj.msg,
                Action = (int)_ActType,
                date = DateTime.Now
            };
            await _Ivehicle_logServices.InsertAsync(vlog);
            await Task.CompletedTask;
        }
    }
}
