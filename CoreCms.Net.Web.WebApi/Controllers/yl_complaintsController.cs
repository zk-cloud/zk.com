/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 2023/6/6 17:22:19
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
    public class yl_complaintsController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Iyl_complaintsServices _yl_complaintsServices;

        /// <summary>
        /// 构造函数
        ///</summary>
        public yl_complaintsController(IWebHostEnvironment webHostEnvironment
            ,Iyl_complaintsServices yl_complaintsServices
            )
        {
            _webHostEnvironment = webHostEnvironment;
            _yl_complaintsServices = yl_complaintsServices;
        }

        #region 新增投诉
        // POST: Api/yl_complaints/DoCreate
        /// <summary>
        /// 新增投诉
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("新增投诉")]
        public async Task<WebApiCallBack> Add([FromBody] yl_complaints entity)
        {
            var jm = new WebApiCallBack();
            var result = await _yl_complaintsServices.InsertAsync(entity);
            if (result > 0)
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

        #region 受理投诉
        /// <summary>
        /// 受理投诉
        /// </summary>
        /// <param name="id"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("受理投诉")]
        public async Task<WebApiCallBack> Results(int id, string results)
        {
            var jm = new WebApiCallBack();
            var result = await _yl_complaintsServices.UpdateAsync(p => new yl_complaints() { results = results }, p => p.id == id);
            if (result)
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

        #region 查询历史投诉
        /// <summary>
        /// 查询历史投诉
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        [HttpPost]
        [Description("查询历史投诉")]
        public async Task<WebApiCallBack> QueryForUser(int userid)
        {
            var jm = new WebApiCallBack();
            var result = await _yl_complaintsServices.QueryByClauseAsync(p => p.userId == userid);
            if (result != null)
            {
                jm.msg = "查询成功";
                jm.code = 0;
                jm.data = result;
                jm.status = true;
            }
            else
            {
                jm.msg = "查询失败";
                jm.code = 500;
                jm.status = false;
            }
            return jm;
        }
        #endregion
    }
}
