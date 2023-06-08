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
    ///  接口实现
    /// </summary>
    public class yl_complaintsRepository : BaseRepository<yl_complaints>, Iyl_complaintsRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public yl_complaintsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
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
        public new async Task<IPageList<yl_complaints>> QueryPageAsync(Expression<Func<yl_complaints, bool>> predicate,
            Expression<Func<yl_complaints, object>> orderByExpression, OrderByType orderByType, int pageIndex = 1,
            int pageSize = 20, bool blUseNoLock = false)
        {
            RefAsync<int> totalCount = 0;
            List<yl_complaints> page;
            if (blUseNoLock)
            {
                page = await DbClient.Queryable<yl_complaints>()
                .OrderByIF(orderByExpression != null, orderByExpression, orderByType)
                .WhereIF(predicate != null, predicate).Select(p => new yl_complaints
                {
                      id = p.id,
                userId = p.userId,
                userName = p.userName,
                driverId = p.driverId,
                driverName = p.driverName,
                reason = p.reason,
                details = p.details,
                annexes = p.annexes,
                results = p.results,
                createor = p.createor,
                createTime = p.createTime,
                modified = p.modified,
                modifyTime = p.modifyTime,
                
                }).With(SqlWith.NoLock).ToPageListAsync(pageIndex, pageSize, totalCount);
            }
            else
            {
                page = await DbClient.Queryable<yl_complaints>()
                .OrderByIF(orderByExpression != null, orderByExpression, orderByType)
                .WhereIF(predicate != null, predicate).Select(p => new yl_complaints
                {
                      id = p.id,
                userId = p.userId,
                userName = p.userName,
                driverId = p.driverId,
                driverName = p.driverName,
                reason = p.reason,
                details = p.details,
                annexes = p.annexes,
                results = p.results,
                createor = p.createor,
                createTime = p.createTime,
                modified = p.modified,
                modifyTime = p.modifyTime,
                
                }).ToPageListAsync(pageIndex, pageSize, totalCount);
            }
            var list = new PageList<yl_complaints>(page, pageIndex, pageSize, totalCount);
            return list;
        }

        #endregion

    }
}