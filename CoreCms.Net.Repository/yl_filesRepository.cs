/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 2023/6/6 17:22:48
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
    public class yl_filesRepository : BaseRepository<yl_files>, Iyl_filesRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public yl_filesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
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
        public new async Task<IPageList<yl_files>> QueryPageAsync(Expression<Func<yl_files, bool>> predicate,
            Expression<Func<yl_files, object>> orderByExpression, OrderByType orderByType, int pageIndex = 1,
            int pageSize = 20, bool blUseNoLock = false)
        {
            RefAsync<int> totalCount = 0;
            List<yl_files> page;
            if (blUseNoLock)
            {
                page = await DbClient.Queryable<yl_files>()
                .OrderByIF(orderByExpression != null, orderByExpression, orderByType)
                .WhereIF(predicate != null, predicate).Select(p => new yl_files
                {
                      id = p.id,
                Name = p.Name,
                Path = p.Path,
                creator = p.creator,
                createTime = p.createTime,
                modified = p.modified,
                modifyTime = p.modifyTime,
                
                }).With(SqlWith.NoLock).ToPageListAsync(pageIndex, pageSize, totalCount);
            }
            else
            {
                page = await DbClient.Queryable<yl_files>()
                .OrderByIF(orderByExpression != null, orderByExpression, orderByType)
                .WhereIF(predicate != null, predicate).Select(p => new yl_files
                {
                      id = p.id,
                Name = p.Name,
                Path = p.Path,
                creator = p.creator,
                createTime = p.createTime,
                modified = p.modified,
                modifyTime = p.modifyTime,
                
                }).ToPageListAsync(pageIndex, pageSize, totalCount);
            }
            var list = new PageList<yl_files>(page, pageIndex, pageSize, totalCount);
            return list;
        }

        #endregion

    }
}
