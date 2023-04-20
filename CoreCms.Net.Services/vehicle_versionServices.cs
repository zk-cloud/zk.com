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
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreCms.Net.Configuration;
using CoreCms.Net.IRepository;
using CoreCms.Net.IRepository.UnitOfWork;
using CoreCms.Net.IServices;
using CoreCms.Net.Model.Entities;
using CoreCms.Net.Model.ViewModels.Basics;
using CoreCms.Net.Model.ViewModels.UI;
using CoreCms.Net.Utility;
using SqlSugar;


namespace CoreCms.Net.Services
{
    /// <summary>
    /// 车辆更新版本表 接口实现
    /// </summary>
    public class vehicle_versionServices : BaseServices<vehicle_version>, Ivehicle_versionServices
    {
        private readonly Ivehicle_versionRepository _dal;
        private readonly IUnitOfWork _unitOfWork;

        public vehicle_versionServices(IUnitOfWork unitOfWork, Ivehicle_versionRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
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
            return await _dal.QueryPageAsync(predicate, orderByExpression, orderByType, pageIndex, pageSize, blUseNoLock);
        }
        #endregion

        #region 车辆固件版本更新判断

        /// <summary>
        /// 检查更新
        /// </summary>
        /// <param name="vehicleTypeId"></param>
        /// <param name="batchid"></param>
        /// <param name="Vehicle_Version"></param>
        /// <param name="isOfficial"></param>
        /// <returns></returns>
        public async Task<vehicle_version> CheckForUpdates(vehicle vmodel, bool isOfficial = true)
        {
            using (var db = SqlSugarHelper.Db)
            {
                List<vehicle_version> newVersionList = await db.Queryable<vehicle_version>().Where(p => p.vehicleTypeId == vmodel.vehicletypeId && p.isOfficial == isOfficial)
                    .Mapper(p =>
                    {
                        p.bitchList = p.batchid.Split(',').ToList();
                        p.firmwareList = p.firmwares.Split(',').ToList();
                    })
                    .OrderByDescending(p => p.date)
                    .ToListAsync();

                Version oldv = new Version(vmodel.vehicleVersion);
                var model = newVersionList.Find(p => p.bitchList.Contains(vmodel.vehicleBatch));
                Version newv = new Version(model.Version);

                if (oldv < newv)
                {
                    model.isUpdate = true;
                }
                return model;
            }
        }
        #endregion
    }
}
