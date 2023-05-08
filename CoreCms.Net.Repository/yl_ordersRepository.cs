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
    public class yl_ordersRepository : BaseRepository<yl_orders>, Iyl_ordersRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public yl_ordersRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
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
        public new async Task<IPageList<yl_orders>> QueryPageAsync(Expression<Func<yl_orders, bool>> predicate,
            Expression<Func<yl_orders, object>> orderByExpression, OrderByType orderByType, int pageIndex = 1,
            int pageSize = 20, bool blUseNoLock = false)
        {
            RefAsync<int> totalCount = 0;
            List<yl_orders> page;
            if (blUseNoLock)
            {
                page = await DbClient.Queryable<yl_orders>()
                .OrderByIF(orderByExpression != null, orderByExpression, orderByType)
                .WhereIF(predicate != null, predicate).Select(p => new yl_orders
                {
                      id = p.id,
                number = p.number,
                orderType = p.orderType,
                userid = p.userid,
                driverid = p.driverid,
                sneder = p.sneder,
                sendAddress = p.sendAddress,
                snedPhone = p.snedPhone,
                recipient = p.recipient,
                receAddress = p.receAddress,
                recePhone = p.recePhone,
                carType = p.carType,
                pay = p.pay,
                amount = p.amount,
                settlementNum = p.settlementNum,
                remark = p.remark,
                itemName = p.itemName,
                itemType = p.itemType,
                itemWeight = p.itemWeight,
                itemVolume = p.itemVolume,
                itemNum = p.itemNum,
                itemPictures = p.itemPictures,
                deliveryTime = p.deliveryTime,
                isDelete = p.isDelete,
                isComfirm = p.isComfirm,
                state = p.state,
                createor = p.createor,
                createTime = p.createTime,
                modified = p.modified,
                modifyTime = p.modifyTime,
                
                }).With(SqlWith.NoLock).ToPageListAsync(pageIndex, pageSize, totalCount);
            }
            else
            {
                page = await DbClient.Queryable<yl_orders>()
                .OrderByIF(orderByExpression != null, orderByExpression, orderByType)
                .WhereIF(predicate != null, predicate).Select(p => new yl_orders
                {
                      id = p.id,
                number = p.number,
                orderType = p.orderType,
                userid = p.userid,
                driverid = p.driverid,
                sneder = p.sneder,
                sendAddress = p.sendAddress,
                snedPhone = p.snedPhone,
                recipient = p.recipient,
                receAddress = p.receAddress,
                recePhone = p.recePhone,
                carType = p.carType,
                pay = p.pay,
                amount = p.amount,
                settlementNum = p.settlementNum,
                remark = p.remark,
                itemName = p.itemName,
                itemType = p.itemType,
                itemWeight = p.itemWeight,
                itemVolume = p.itemVolume,
                itemNum = p.itemNum,
                itemPictures = p.itemPictures,
                deliveryTime = p.deliveryTime,
                isDelete = p.isDelete,
                isComfirm = p.isComfirm,
                state = p.state,
                createor = p.createor,
                createTime = p.createTime,
                modified = p.modified,
                modifyTime = p.modifyTime,
                
                }).ToPageListAsync(pageIndex, pageSize, totalCount);
            }
            var list = new PageList<yl_orders>(page, pageIndex, pageSize, totalCount);
            return list;
        }

        #endregion

    }
}
