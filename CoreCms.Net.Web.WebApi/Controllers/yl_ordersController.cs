/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 2023/5/4 8:46:46
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
using CoreCms.Net.Services;

namespace CoreCms.Net.Web.WebApi.Controllers
{
    /// <summary>
    /// 
    ///</summary>
    [Description("")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [RequiredErrorForAdmin]
    [Authorize]
    public class yl_ordersController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Iyl_ordersServices _yl_ordersServices;

        /// <summary>
        /// 构造函数
        ///</summary>
        public yl_ordersController(IWebHostEnvironment webHostEnvironment
            ,Iyl_ordersServices yl_ordersServices
            )
        {
            _webHostEnvironment = webHostEnvironment;
            _yl_ordersServices = yl_ordersServices;
        }

        #region 新增订单
        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiCallBack> Add(yl_orders entity)
        {
            var jm = new WebApiCallBack();

            var result = await _yl_ordersServices.InsertAsync(entity);

            if (result > 0)
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

        #region 删除订单
        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="number">订单号</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiCallBack> Delete(string number, int userid)
        {
            var jm = new WebApiCallBack();

            var orderlist = _yl_ordersServices.QueryListByClause(p => p.number == number && p.userid == userid);

            if (orderlist == null)
            {
                jm.code = -1;
                jm.msg = "订单不存在";
                jm.status = false;

                return jm;
            }
            else
            {
                var countStart = orderlist.Select(a => a.state == 0).Count();
                var countEnd = orderlist.Select(a => a.state == 4).Count();
                if(countStart != orderlist.Count && countEnd != orderlist.Count)
                {
                    jm.code = -1;
                    jm.msg = "订单不可删除";
                    jm.status = false;

                    return jm;
                }
                else
                {
                    var result = await _yl_ordersServices.DeleteAsync(p => p.id == id && p.userid == userid);
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
            }
        }
        #endregion

        #region 查询用户订单
        /// <summary>
        /// 查询用户订单
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiCallBack> QueryForUser(int userid)
        {
            var jm = new WebApiCallBack();

            var result = await _yl_ordersServices.QueryListByClauseAsync(p => p.userid == userid);

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
                jm.msg = "查询失败";
                jm.data = null;
                jm.status = false;
            }

            return jm;
        }
        #endregion

    }
}
