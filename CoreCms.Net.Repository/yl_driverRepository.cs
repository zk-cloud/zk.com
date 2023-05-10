/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 2023/5/4 8:46:37
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
    public class yl_driverRepository : BaseRepository<yl_driver>, Iyl_driverRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public yl_driverRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
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
        public new async Task<IPageList<yl_driver>> QueryPageAsync(Expression<Func<yl_driver, bool>> predicate,
            Expression<Func<yl_driver, object>> orderByExpression, OrderByType orderByType, int pageIndex = 1,
            int pageSize = 20, bool blUseNoLock = false)
        {
            RefAsync<int> totalCount = 0;
            List<yl_driver> page;
            if (blUseNoLock)
            {
                page = await DbClient.Queryable<yl_driver>()
                .OrderByIF(orderByExpression != null, orderByExpression, orderByType)
                .WhereIF(predicate != null, predicate).Select(p => new yl_driver
                {
                      id = p.id,
                name = p.name,
                avatar = p.avatar,
                carType = p.carType,
                licensePlate = p.licensePlate,
                realName = p.realName,
                idCard = p.idCard,
                licence = p.licence,
                openid = p.openid,
                unionid = p.unionid,
                phone = p.phone,
                code = p.code,
                IsRegister = p.IsRegister,
                IsDelete = p.IsDelete,
                creator = p.creator,
                createTime = p.createTime,
                modified = p.modified,
                modifyTime = p.modifyTime,
                
                }).With(SqlWith.NoLock).ToPageListAsync(pageIndex, pageSize, totalCount);
            }
            else
            {
                page = await DbClient.Queryable<yl_driver>()
                .OrderByIF(orderByExpression != null, orderByExpression, orderByType)
                .WhereIF(predicate != null, predicate).Select(p => new yl_driver
                {
                      id = p.id,
                name = p.name,
                avatar = p.avatar,
                carType = p.carType,
                licensePlate = p.licensePlate,
                realName = p.realName,
                idCard = p.idCard,
                licence = p.licence,
                openid = p.openid,
                unionid = p.unionid,
                phone = p.phone,
                code = p.code,
                IsRegister = p.IsRegister,
                IsDelete = p.IsDelete,
                creator = p.creator,
                createTime = p.createTime,
                modified = p.modified,
                modifyTime = p.modifyTime,
                
                }).ToPageListAsync(pageIndex, pageSize, totalCount);
            }
            var list = new PageList<yl_driver>(page, pageIndex, pageSize, totalCount);
            return list;
        }

        #endregion

    }
}
