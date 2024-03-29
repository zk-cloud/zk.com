/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 
 *        Description: 暂无
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreCms.Net.Model.Entities;
using CoreCms.Net.Model.ViewModels.Basics;
using CoreCms.Net.Model.ViewModels.UI;
using SqlSugar;

namespace CoreCms.Net.IServices
{
	/// <summary>
    /// 车辆更新版本表 服务工厂接口
    /// </summary>
    public interface Ivehicle_versionServices : IBaseServices<vehicle_version>
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
        new Task<IPageList<vehicle_version>> QueryPageAsync(
            Expression<Func<vehicle_version, bool>> predicate,
            Expression<Func<vehicle_version, object>> orderByExpression, OrderByType orderByType, int pageIndex = 1,
            int pageSize = 20, bool blUseNoLock = false);
        #endregion

        /// <summary>
        /// 检查更新
        /// </summary>
        /// <param name="vehicleTypeId"></param>
        /// <param name="batchid"></param>
        /// <param name="Vehicle_Version"></param>
        /// <param name="isOfficial"></param>
        /// <returns></returns>
        new Task<vehicle_version> CheckForUpdates(vehicle vmodel, bool isOfficial = true);
    }
}
