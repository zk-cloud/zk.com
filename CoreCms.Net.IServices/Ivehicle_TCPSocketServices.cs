/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 
 *        Description: 暂无
 ***********************************************************************/

using CoreCms.Net.Model.Entities;
using CoreCms.Net.Model.FromDate;
using CoreCms.Net.Model.FromDto;
using CoreCms.Net.Model.ViewModels.Basics;
using CoreCms.Net.Model.ViewModels.UI;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreCms.Net.IServices
{
    /// <summary>
    /// 车辆日志表 服务工厂接口
    /// </summary>
    public interface Ivehicle_TCPSocketServices : IBaseServices<vehicle_log>
    {
        #region 重写根据条件查询分页数据
        /// <summary>
        ///     重写根据条件查询分页数据
        /// </summary>
        /// <param name="predicate">判断集合</param>
        /// <param name="orderByType">排序方式</param>
        /// <param name="pageIndex">当前页面索引</param>
        /// <param name="pageSize">分布大小</param>
        /// <param name="orderByExpression"></param>
        /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
        /// <returns></returns>
        new Task<IPageList<vehicle_log>> QueryPageAsync(
            Expression<Func<vehicle_log, bool>> predicate,
            Expression<Func<vehicle_log, object>> orderByExpression, OrderByType orderByType, int pageIndex = 1,
            int pageSize = 20, bool blUseNoLock = false);
        #endregion

        /// <summary>
        /// Socket登录获取令牌
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public WebApiCallBack SocketLogin(FMVehicleLoginDto model);

        /// <summary>
        /// 车辆退出登录
        /// </summary>
        /// <param name="LoginToken"></param>
        /// <returns></returns>
        public WebApiCallBack SocketLogout(string LoginToken);

        /// <summary>
        /// Socket车辆参数上传
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<WebApiCallBack> SocketVehicleParameterUp(FMVehicleParameter dto, vehicle vehicle, string LogToken);

        Task SocketVehicleGPS();

        /// <summary>
        /// TCP服务器车辆登出
        /// </summary>
        /// <param name="msgstr"></param>
        /// <param name="logouttr"></param>
        /// <param name="vin"></param>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public Task<WebApiCallBack> TCPSocketLogout(string msgstr, List<vehicle_terminalconfigure> logouttr, string vin, string clientid);

        /// <summary>
        /// TCP服务器车辆登入
        /// </summary>
        /// <param name="msgstr"></param>
        /// <param name="loginstr"></param>
        /// <param name="vin"></param>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public Task<WebApiCallBack> SocketLoginAsync(string msgstr, List<vehicle_terminalconfigure> loginstr, string vin, string clientid);

        /// <summary>
        /// TCP服务器车辆信息上传
        /// </summary>
        /// <param name="hexstrpart"></param>
        /// <param name="LogToken"></param>
        /// <param name="vin"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public Task<WebApiCallBack> TCPSocketVehicleParameterUp(string hexstrpart, string LogToken, string vin, List<vehicle_terminalconfigure> list);

        /// <summary>
        /// 关闭tcp消息通道后执行操作
        /// </summary>
        /// <param name="LogToken"></param>
        /// <returns></returns>
        public Task TCPSocketRemoveRedis(string LogToken);

        /// <summary>
        /// 车辆参数批量上传
        /// </summary>
        /// <param name="hexstrpartlist"></param>
        /// <returns></returns>
        public Task<WebApiCallBack> TCPSocketVehicleParameterBatchUp(List<string> hexstrpartlist);
    }
}
