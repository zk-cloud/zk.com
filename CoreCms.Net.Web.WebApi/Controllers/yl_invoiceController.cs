/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 2023/6/6 17:22:32
 *        Description: 暂无
 ***********************************************************************/


using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreCms.Net.Configuration;
using CoreCms.Net.Model.Entities;
using CoreCms.Net.Model.Entities.Expression;
using CoreCms.Net.Model.FromBody;
using CoreCms.Net.Model.ViewModels.UI;
using CoreCms.Net.Filter;
using CoreCms.Net.Loging;
using CoreCms.Net.IServices;
using CoreCms.Net.Utility.Helper;
using CoreCms.Net.Utility.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using SqlSugar;

namespace CoreCms.Net.Web.Admin.Controllers
{
    /// <summary>
    /// 
    ///</summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class yl_invoiceController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Iyl_invoiceServices _yl_invoiceServices;

        /// <summary>
        /// 构造函数
        ///</summary>
        public yl_invoiceController(IWebHostEnvironment webHostEnvironment
            ,Iyl_invoiceServices yl_invoiceServices
            )
        {
            _webHostEnvironment = webHostEnvironment;
            _yl_invoiceServices = yl_invoiceServices;
        }

        #region 提交开票
        /// <summary>
        /// 提交开票
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("提交开票")]
        public async Task<WebApiCallBack> AddInvoice([FromBody]yl_invoice entity)
        {
            var jm = new WebApiCallBack();
            var result = await _yl_invoiceServices.InsertAsync(entity);
            if(result > 0)
            {
                jm.msg = "提交成功";
                jm.code = 0;
                jm.status = true;
            }
            else
            {
                jm.msg = "提交失败";
                jm.code = 500;
                jm.status = false;
            }
            return jm;
        }
        #endregion

    }
}
