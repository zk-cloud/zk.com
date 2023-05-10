/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 2023/5/4 8:46:26
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

namespace CoreCms.Net.Web.WebApi.Controllers
{
    /// <summary>
    /// 
    ///</summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class yl_addressController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Iyl_addressServices _yl_addressServices;

        /// <summary>
        /// 构造函数
        ///</summary>
        public yl_addressController(IWebHostEnvironment webHostEnvironment
            ,Iyl_addressServices yl_addressServices
            )
        {
            _webHostEnvironment = webHostEnvironment;
            _yl_addressServices = yl_addressServices;
        }

        #region 添加地址
        /// <summary>
        /// 添加地址
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiCallBack> Add(yl_address entity)
        {
            var jm = new WebApiCallBack();

            var result = await _yl_addressServices.InsertAsync(entity);

            if(result > 0)
            {
                jm.code = 0;
                jm.msg = "添加成功";
                jm.status = true;
            }
            else
            {
                jm.code = 500;
                jm.msg = "添加失败";
                jm.status = false;
            }
            
            return jm;
        }
        #endregion

        #region 删除地址
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiCallBack> Delete(int id)
        {
            var jm = new WebApiCallBack();

            var result = await _yl_addressServices.DeleteAsync(p => p.id == id);

            if (result)
            {
                jm.code = 0;
                jm.msg = "删除成功";
                jm.status = true;
            }
            else
            {
                jm.code = 500;
                jm.msg = "删除失败";
                jm.status = false;
            }

            return jm;
        }
        #endregion

        #region 查询收藏地址
        /// <summary>
        /// 查询收藏地址
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiCallBack> QueryForUser(int userid)
        {
            var jm = new WebApiCallBack();

            var result = await _yl_addressServices.QueryListByClauseAsync(p => p.userid == userid);

            if (result.Count > 0)
            {
                jm.code = 0;
                jm.msg = "查询成功";
                jm.data = result;
                jm.status = true;
            }
            else
            {
                jm.code = 500;
                jm.msg = "暂无收藏地址";
                jm.data= null;
                jm.status = false;
            }

            return jm;
        }
        #endregion
    }
}
