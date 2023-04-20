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
    ///  接口实现
    /// </summary>
    public class vehicle_gbt32960Repository : BaseRepository<vehicle_gbt32960>, Ivehicle_gbt32960Repository
    {
        private readonly IUnitOfWork _unitOfWork;
        public vehicle_gbt32960Repository(IUnitOfWork unitOfWork) : base(unitOfWork)
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
        public new async Task<IPageList<vehicle_gbt32960>> QueryPageAsync(Expression<Func<vehicle_gbt32960, bool>> predicate,
            Expression<Func<vehicle_gbt32960, object>> orderByExpression, OrderByType orderByType, int pageIndex = 1,
            int pageSize = 20, bool blUseNoLock = false)
        {
            RefAsync<int> totalCount = 0;
            List<vehicle_gbt32960> page;
            if (blUseNoLock)
            {
                page = await DbClient.Queryable<vehicle_gbt32960>()
                .OrderByIF(orderByExpression != null, orderByExpression, orderByType)
                .WhereIF(predicate != null, predicate).Select(p => new vehicle_gbt32960
                {
                      id = p.id,
                VIN = p.VIN,
                VehicleSts = p.VehicleSts,
                VehicleCharge = p.VehicleCharge,
                Drivingmode = p.Drivingmode,
                VehicleSpeed = p.VehicleSpeed,
                VehicleMileage = p.VehicleMileage,
                VehicleDY = p.VehicleDY,
                VehicleDL = p.VehicleDL,
                VehicleSOC = p.VehicleSOC,
                VehicleDC_DC = p.VehicleDC_DC,
                VehicleGear = p.VehicleGear,
                Vehicle = p.Vehicle,
                VehicleJSTB = p.VehicleJSTB,
                VehicleZDTB = p.VehicleZDTB,
                MCU_NUM = p.MCU_NUM,
                MCU_TableCode = p.MCU_TableCode,
                GPS_Sts = p.GPS_Sts,
                GPS_lng = p.GPS_lng,
                GPS_lat = p.GPS_lat,
                Peak_HightDYXTCode = p.Peak_HightDYXTCode,
                Peak_HightDYDTCode = p.Peak_HightDYDTCode,
                Peak_HightDYVal = p.Peak_HightDYVal,
                Peak_LowDYXTCode = p.Peak_LowDYXTCode,
                Peak_LowDYDTCode = p.Peak_LowDYDTCode,
                Peak_LowDYVal = p.Peak_LowDYVal,
                Peak_HightZXTCode = p.Peak_HightZXTCode,
                Peak_HightTZCode = p.Peak_HightTZCode,
                Peak_HightWDVal = p.Peak_HightWDVal,
                Peak_LowZXTCode = p.Peak_LowZXTCode,
                Peak_LowTZCode = p.Peak_LowTZCode,
                Peak_LowWDVal = p.Peak_LowWDVal,
                Report_Lev = p.Report_Lev,
                Report_Sts = p.Report_Sts,
                Report_CNGZNum = p.Report_CNGZNum,
                Report_CNGZ = p.Report_CNGZ,
                Report_QDDJGZNum = p.Report_QDDJGZNum,
                Report_QDDJGZ = p.Report_QDDJGZ,
                Report_FDJGZNum = p.Report_FDJGZNum,
                Report_FDJGZ = p.Report_FDJGZ,
                Report_QTGZNum = p.Report_QTGZNum,
                Report_QTGZ = p.Report_QTGZ,
                Betty_DYNun = p.Betty_DYNun,
                Betty_DYCode = p.Betty_DYCode,
                Betty_WDNum = p.Betty_WDNum,
                Betty_WDCode = p.Betty_WDCode,
                date = p.date,
                
                }).With(SqlWith.NoLock).ToPageListAsync(pageIndex, pageSize, totalCount);
            }
            else
            {
                page = await DbClient.Queryable<vehicle_gbt32960>()
                .OrderByIF(orderByExpression != null, orderByExpression, orderByType)
                .WhereIF(predicate != null, predicate).Select(p => new vehicle_gbt32960
                {
                      id = p.id,
                VIN = p.VIN,
                VehicleSts = p.VehicleSts,
                VehicleCharge = p.VehicleCharge,
                Drivingmode = p.Drivingmode,
                VehicleSpeed = p.VehicleSpeed,
                VehicleMileage = p.VehicleMileage,
                VehicleDY = p.VehicleDY,
                VehicleDL = p.VehicleDL,
                VehicleSOC = p.VehicleSOC,
                VehicleDC_DC = p.VehicleDC_DC,
                VehicleGear = p.VehicleGear,
                Vehicle = p.Vehicle,
                VehicleJSTB = p.VehicleJSTB,
                VehicleZDTB = p.VehicleZDTB,
                MCU_NUM = p.MCU_NUM,
                MCU_TableCode = p.MCU_TableCode,
                GPS_Sts = p.GPS_Sts,
                GPS_lng = p.GPS_lng,
                GPS_lat = p.GPS_lat,
                Peak_HightDYXTCode = p.Peak_HightDYXTCode,
                Peak_HightDYDTCode = p.Peak_HightDYDTCode,
                Peak_HightDYVal = p.Peak_HightDYVal,
                Peak_LowDYXTCode = p.Peak_LowDYXTCode,
                Peak_LowDYDTCode = p.Peak_LowDYDTCode,
                Peak_LowDYVal = p.Peak_LowDYVal,
                Peak_HightZXTCode = p.Peak_HightZXTCode,
                Peak_HightTZCode = p.Peak_HightTZCode,
                Peak_HightWDVal = p.Peak_HightWDVal,
                Peak_LowZXTCode = p.Peak_LowZXTCode,
                Peak_LowTZCode = p.Peak_LowTZCode,
                Peak_LowWDVal = p.Peak_LowWDVal,
                Report_Lev = p.Report_Lev,
                Report_Sts = p.Report_Sts,
                Report_CNGZNum = p.Report_CNGZNum,
                Report_CNGZ = p.Report_CNGZ,
                Report_QDDJGZNum = p.Report_QDDJGZNum,
                Report_QDDJGZ = p.Report_QDDJGZ,
                Report_FDJGZNum = p.Report_FDJGZNum,
                Report_FDJGZ = p.Report_FDJGZ,
                Report_QTGZNum = p.Report_QTGZNum,
                Report_QTGZ = p.Report_QTGZ,
                Betty_DYNun = p.Betty_DYNun,
                Betty_DYCode = p.Betty_DYCode,
                Betty_WDNum = p.Betty_WDNum,
                Betty_WDCode = p.Betty_WDCode,
                date = p.date,
                
                }).ToPageListAsync(pageIndex, pageSize, totalCount);
            }
            var list = new PageList<vehicle_gbt32960>(page, pageIndex, pageSize, totalCount);
            return list;
        }

        #endregion

    }
}
