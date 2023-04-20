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
using CoreCms.Net.Caching.Manual;
using CoreCms.Net.Configuration;
using CoreCms.Net.Model.Entities;
using CoreCms.Net.Model.ViewModels.Basics;
using CoreCms.Net.IRepository;
using CoreCms.Net.IRepository.UnitOfWork;
using CoreCms.Net.Model.ViewModels.UI;
using SqlSugar;

namespace CoreCms.Net.Repository
{
    /// <summary>
    /// 车辆更新版本表 接口实现
    /// </summary>
    public class vehicle_versionRepository : BaseRepository<vehicle_version>, Ivehicle_versionRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public vehicle_versionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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
        public new async Task<IPageList<vehicle_version>> QueryPageAsync(Expression<Func<vehicle_version, bool>> predicate,
            Expression<Func<vehicle_version, object>> orderByExpression, OrderByType orderByType, int pageIndex = 1,
            int pageSize = 20, bool blUseNoLock = false)
        {
            RefAsync<int> totalCount = 0;
            List<vehicle_version> page;
            if (blUseNoLock)
            {
                page = await DbClient.Queryable<vehicle_version>()
                .OrderByIF(orderByExpression != null, orderByExpression, orderByType)
                .WhereIF(predicate != null, predicate).Select(p => new vehicle_version
                {
                      id = p.id,
                vehicleTypeId = p.vehicleTypeId,
                batchid = p.batchid,
                Version = p.Version,
                isOfficial = p.isOfficial,
                isDel = p.isDel,
                path = p.path,
                firmwares = p.firmwares,
                fileNum = p.fileNum,
                fileSize = p.fileSize,
                date = p.date,
                desc = p.desc,
                
                }).With(SqlWith.NoLock).ToPageListAsync(pageIndex, pageSize, totalCount);
            }
            else
            {
                page = await DbClient.Queryable<vehicle_version>()
                .OrderByIF(orderByExpression != null, orderByExpression, orderByType)
                .WhereIF(predicate != null, predicate).Select(p => new vehicle_version
                {
                      id = p.id,
                vehicleTypeId = p.vehicleTypeId,
                batchid = p.batchid,
                Version = p.Version,
                isOfficial = p.isOfficial,
                isDel = p.isDel,
                path = p.path,
                firmwares = p.firmwares,
                fileNum = p.fileNum,
                fileSize = p.fileSize,
                date = p.date,
                desc = p.desc,
                
                }).ToPageListAsync(pageIndex, pageSize, totalCount);
            }
            var list = new PageList<vehicle_version>(page, pageIndex, pageSize, totalCount);
            return list;
        }

        #endregion

    }
}
