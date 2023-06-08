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
    [Route("api/[controller]/[action]")]
    [ApiController]
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
        public async Task<WebApiCallBack> CreateOrder(yl_orders entity)
        {
            var jm = new WebApiCallBack();
            //生成订单编号
            entity.number = CommonHelper.GetSerialNumberType((int)GlobalEnumVars.SerialNumberType.订单编号);
            entity.createTime = DateTime.Now;
            entity.isComfirm = false;
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

        #region 确认订单下单
        /// <summary>
        /// 确认订单下单
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiCallBack> AddOrder(string number)
        {
            var jm = new WebApiCallBack();

            var entity = await _yl_ordersServices.QueryByClauseAsync(p => p.number == number);

            entity.isComfirm = true;

            var result = await _yl_ordersServices.UpdateAsync(entity);
            if (result)
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
        /// <param name="userid">用户ID（关联用户表）</param>
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
                var countStart = orderlist.Select(a => a.isComfirm == false).Count();
                if(countStart > 0)
                {
                    jm.code = -1;
                    jm.msg = "订单不可删除";
                    jm.status = false;

                    return jm;
                }
                else
                {
                    var result = await _yl_ordersServices.DeleteAsync(p => p.number == number && p.userid == userid);
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
        /// <param name="userid">用户id(关联用户表)</param>
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

        #region 订单号查询预览订单
        /// <summary>
        /// 订单号查询预览订单
        /// </summary>
        /// <param name="number">订单号</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiCallBack> QueryForDetail(string number)
        {
            var jm = new WebApiCallBack();

            var result = await _yl_ordersServices.QueryListByClauseAsync(p => p.number == number);

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

        #region 确认签收订单
        /// <summary>
        /// 确认签收订单
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiCallBack> OrderConfirm(int id)
        {
            var jm = new WebApiCallBack();

            var result = await _yl_ordersServices.UpdateAsync(p => new yl_orders() { state = 4}, p => p.id == id);

            if (result)
            {
                jm.code = 0;
                jm.msg = "签收成功";
                jm.data = result;
                jm.status = true;
            }
            else
            {
                jm.code = 500;
                jm.msg = "签收失败";
                jm.data = null;
                jm.status = false;
            }

            return jm;
        }
        #endregion

        #region 修改订单状态
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="number">订单号</param>
        /// <param name="state">状态（0-待确认/1-已生成/2-已接单/3-运输中/4-完成）</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiCallBack> EditOrder(string number, string state)
        {
            var jm = new WebApiCallBack();

            var entity = await _yl_ordersServices.QueryByClauseAsync(p => p.number == number);

            entity.state = int.Parse(state);

            var result = await _yl_ordersServices.UpdateAsync(entity);
            if (result)
            {
                jm.code = 0;
                jm.msg = "修改成功";
                jm.status = true;
            }
            else
            {
                jm.code = 500;
                jm.msg = "修改失败";
                jm.status = false;
            }

            return jm;
        }
        #endregion

        #region 查询月账单
        /// <summary>
        /// 查询月账单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiCallBack> QueryAmountForMonth(int id, DateTime datetime)
        {
            var jm = new WebApiCallBack();

            var date = new DateTime(datetime.Year, datetime.Month, 0);
            var result = await _yl_ordersServices.QueryListByClauseAsync(p => p.userid == id && p.state == 4 && p.modifyTime > date && p.modifyTime < date.AddMonths(1));
            double sum = 0;
            foreach(var item in result)
            {
                sum += item.amount;
            }
            jm.code = 0;
            jm.msg = "查询成功";
            jm.data = sum;
            jm.status = true;

            return jm;
        }
        #endregion

        #region 查询年账单
        /// <summary>
        /// 查询年账单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiCallBack> QueryAmountForYear(int id, DateTime datetime)
        {
            var jm = new WebApiCallBack();

            var date = new DateTime(datetime.Year, 0, 0);
            var result = await _yl_ordersServices.QueryListByClauseAsync(p => p.userid == id && p.state == 4 && p.modifyTime > date && p.modifyTime < date.AddYears(1));
            double sum = 0;
            foreach (var item in result)
            {
                sum += item.amount;
            }
            jm.code = 0;
            jm.msg = "查询成功";
            jm.data = sum;
            jm.status = true;

            return jm;
        }
        #endregion
    }
}
