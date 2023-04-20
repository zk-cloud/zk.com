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
    /// 车辆参数采集列表 接口实现
    /// </summary>
    public class vehicle_parameterlistRepository : BaseRepository<vehicle_parameterlist>, Ivehicle_parameterlistRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public vehicle_parameterlistRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
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
        public new async Task<IPageList<vehicle_parameterlist>> QueryPageAsync(Expression<Func<vehicle_parameterlist, bool>> predicate,
            Expression<Func<vehicle_parameterlist, object>> orderByExpression, OrderByType orderByType, int pageIndex = 1,
            int pageSize = 20, bool blUseNoLock = false)
        {
            RefAsync<int> totalCount = 0;
            List<vehicle_parameterlist> page;
            if (blUseNoLock)
            {
                page = await DbClient.Queryable<vehicle_parameterlist>()
                .OrderByIF(orderByExpression != null, orderByExpression, orderByType)
                .WhereIF(predicate != null, predicate).Select(p => new vehicle_parameterlist
                {
                      id = p.id,
                VIN = p.VIN,
                Logid = p.Logid,
                B2V_SN_FrameNo = p.B2V_SN_FrameNo,
                B2V_SN_SysCodeLength = p.B2V_SN_SysCodeLength,
                B2V_SN_SysCode_B1_7_13_19 = p.B2V_SN_SysCode_B1_7_13_19,
                B2V_SN_SysCode_B2_8_14_20 = p.B2V_SN_SysCode_B2_8_14_20,
                B2V_SN_SysCode_B3_9_15_21 = p.B2V_SN_SysCode_B3_9_15_21,
                B2V_SN_SysCode_B4_10_16_22 = p.B2V_SN_SysCode_B4_10_16_22,
                B2V_SN_SysCode_B5_11_17_23 = p.B2V_SN_SysCode_B5_11_17_23,
                B2V_SN_SysCode_B6_12_18_24 = p.B2V_SN_SysCode_B6_12_18_24,
                B2V_BI2_PackTotTempNum = p.B2V_BI2_PackTotTempNum,
                B2V_BI2_BattSysNum = p.B2V_BI2_BattSysNum,
                B2V_BI2_BattSysSeqNum = p.B2V_BI2_BattSysSeqNum,
                B2V_V_FrameNo = p.B2V_V_FrameNo,
                B2V_V_Cell_VoltN1 = p.B2V_V_Cell_VoltN1,
                B2V_V_Cell_VoltN2 = p.B2V_V_Cell_VoltN2,
                B2V_V_Cell_VoltN3 = p.B2V_V_Cell_VoltN3,
                B2V_T_FrameNo = p.B2V_T_FrameNo,
                B2V_T_Cell_TempN1 = p.B2V_T_Cell_TempN1,
                B2V_T_Cell_TempN2 = p.B2V_T_Cell_TempN2,
                B2V_T_Cell_TempN3 = p.B2V_T_Cell_TempN3,
                B2V_T_Cell_TempN4 = p.B2V_T_Cell_TempN4,
                B2V_T_Cell_TempN5 = p.B2V_T_Cell_TempN5,
                B2V_T_Cell_TempN6 = p.B2V_T_Cell_TempN6,
                B2V_Fult1_DelTemp = p.B2V_Fult1_DelTemp,
                B2V_Fult1_OverTemp = p.B2V_Fult1_OverTemp,
                B2V_Fult1_PackOverHVolt = p.B2V_Fult1_PackOverHVolt,
                B2V_Fult1_PackLowHVolt = p.B2V_Fult1_PackLowHVolt,
                B2V_Fult1_LowSOC = p.B2V_Fult1_LowSOC,
                B2V_Fult1_OverUcell = p.B2V_Fult1_OverUcell,
                B2V_Fult1_LowUcell = p.B2V_Fult1_LowUcell,
                B2V_Fult1_LowInsRes = p.B2V_Fult1_LowInsRes,
                B2V_Fult1_UcellUniformity = p.B2V_Fult1_UcellUniformity,
                B2V_Fult1_OverChg = p.B2V_Fult1_OverChg,
                B2V_Fult1_OverSOC = p.B2V_Fult1_OverSOC,
                B2V_Fult1_SOCChangeFast = p.B2V_Fult1_SOCChangeFast,
                B2V_Fult1_BatSysNotMatch = p.B2V_Fult1_BatSysNotMatch,
                B2V_Fult1_HVILFault = p.B2V_Fult1_HVILFault,
                B2V_Fult1_FaultNum_32960 = p.B2V_Fult1_FaultNum_32960,
                B2V_Fult2_FaultNum = p.B2V_Fult2_FaultNum,
                B2V_ST1_ChrgMode = p.B2V_ST1_ChrgMode,
                B2V_ST1_ChrgStatus = p.B2V_ST1_ChrgStatus,
                B2V_ST2_SOC = p.B2V_ST2_SOC,
                B2V_ST2_PackCurrent = p.B2V_ST2_PackCurrent,
                B2V_ST2_PackInsideVolt = p.B2V_ST2_PackInsideVolt,
                B2V_ST2_FaultCode = p.B2V_ST2_FaultCode,
                B2V_ST2_FaultLevel = p.B2V_ST2_FaultLevel,
                B2V_ST3_SysInsRes = p.B2V_ST3_SysInsRes,
                B2V_ST4_Max_Temp = p.B2V_ST4_Max_Temp,
                B2V_ST4_Min_Temp = p.B2V_ST4_Min_Temp,
                B2V_ST4_MaxTemp_CSCNo = p.B2V_ST4_MaxTemp_CSCNo,
                B2V_ST4_MaxTemp_Position = p.B2V_ST4_MaxTemp_Position,
                B2V_ST4_MinTemp_CSCNo = p.B2V_ST4_MinTemp_CSCNo,
                B2V_ST4_MinTemp_Position = p.B2V_ST4_MinTemp_Position,
                B2V_ST5_Max_Ucell = p.B2V_ST5_Max_Ucell,
                B2V_ST5_Min_Ucell = p.B2V_ST5_Min_Ucell,
                B2V_ST6_MaxUcell_CSCNo = p.B2V_ST6_MaxUcell_CSCNo,
                B2V_ST6_MaxUcell_Position = p.B2V_ST6_MaxUcell_Position,
                B2V_ST6_MinUcell_CSCNo = p.B2V_ST6_MinUcell_CSCNo,
                B2V_ST6_MinUcell_Position = p.B2V_ST6_MinUcell_Position,
                DC_Alarm1 = p.DC_Alarm1,
                DC_Alarm2 = p.DC_Alarm2,
                DC_OnOFF = p.DC_OnOFF,
                DC_Temp = p.DC_Temp,
                MCU_VehSpd = p.MCU_VehSpd,
                MCU_AccPdlPosV = p.MCU_AccPdlPosV,
                MCU_BrkPdlSts = p.MCU_BrkPdlSts,
                MCU_AccPdlPos = p.MCU_AccPdlPos,
                MCU_VehicleSts = p.MCU_VehicleSts,
                MCU_FltRsvrd = p.MCU_FltRsvrd,
                MCU_WorkingSts = p.MCU_WorkingSts,
                MCU_ErrNumberRsvrd = p.MCU_ErrNumberRsvrd,
                MCU_MtrSpd = p.MCU_MtrSpd,
                MCU_MtrSpdV = p.MCU_MtrSpdV,
                MCU_MtrCntrlTmp = p.MCU_MtrCntrlTmp,
                MCU_MtrTmp = p.MCU_MtrTmp,
                MCU_MtrModSts = p.MCU_MtrModSts,
                MCU_MtrDlvrdTrq = p.MCU_MtrDlvrdTrq,
                MCU_MtrSysFltSts = p.MCU_MtrSysFltSts,
                MCU_MtrInverRatVol = p.MCU_MtrInverRatVol,
                MCU_MtrCntrlRatCrnt = p.MCU_MtrCntrlRatCrnt,
                MCU_MtrCntrlTmpWarn = p.MCU_MtrCntrlTmpWarn,
                MCU_MtrTmpWarn = p.MCU_MtrTmpWarn,
                MCU_MtrSerialNmb = p.MCU_MtrSerialNmb,
                MCU_MtrErrCod = p.MCU_MtrErrCod,
                MCU_DriveMotorNumber = p.MCU_DriveMotorNumber,
                MCU_DriveMotorErrorNumber = p.MCU_DriveMotorErrorNumber,
                Vcu_GearPosn = p.Vcu_GearPosn,
                PedalDepth = p.PedalDepth,
                BreakStsType = p.BreakStsType,
                mileage = p.mileage,
                BreakSts = p.BreakSts,
                date = p.date,
                
                }).With(SqlWith.NoLock).ToPageListAsync(pageIndex, pageSize, totalCount);
            }
            else
            {
                page = await DbClient.Queryable<vehicle_parameterlist>()
                .OrderByIF(orderByExpression != null, orderByExpression, orderByType)
                .WhereIF(predicate != null, predicate).Select(p => new vehicle_parameterlist
                {
                      id = p.id,
                VIN = p.VIN,
                Logid = p.Logid,
                B2V_SN_FrameNo = p.B2V_SN_FrameNo,
                B2V_SN_SysCodeLength = p.B2V_SN_SysCodeLength,
                B2V_SN_SysCode_B1_7_13_19 = p.B2V_SN_SysCode_B1_7_13_19,
                B2V_SN_SysCode_B2_8_14_20 = p.B2V_SN_SysCode_B2_8_14_20,
                B2V_SN_SysCode_B3_9_15_21 = p.B2V_SN_SysCode_B3_9_15_21,
                B2V_SN_SysCode_B4_10_16_22 = p.B2V_SN_SysCode_B4_10_16_22,
                B2V_SN_SysCode_B5_11_17_23 = p.B2V_SN_SysCode_B5_11_17_23,
                B2V_SN_SysCode_B6_12_18_24 = p.B2V_SN_SysCode_B6_12_18_24,
                B2V_BI2_PackTotTempNum = p.B2V_BI2_PackTotTempNum,
                B2V_BI2_BattSysNum = p.B2V_BI2_BattSysNum,
                B2V_BI2_BattSysSeqNum = p.B2V_BI2_BattSysSeqNum,
                B2V_V_FrameNo = p.B2V_V_FrameNo,
                B2V_V_Cell_VoltN1 = p.B2V_V_Cell_VoltN1,
                B2V_V_Cell_VoltN2 = p.B2V_V_Cell_VoltN2,
                B2V_V_Cell_VoltN3 = p.B2V_V_Cell_VoltN3,
                B2V_T_FrameNo = p.B2V_T_FrameNo,
                B2V_T_Cell_TempN1 = p.B2V_T_Cell_TempN1,
                B2V_T_Cell_TempN2 = p.B2V_T_Cell_TempN2,
                B2V_T_Cell_TempN3 = p.B2V_T_Cell_TempN3,
                B2V_T_Cell_TempN4 = p.B2V_T_Cell_TempN4,
                B2V_T_Cell_TempN5 = p.B2V_T_Cell_TempN5,
                B2V_T_Cell_TempN6 = p.B2V_T_Cell_TempN6,
                B2V_Fult1_DelTemp = p.B2V_Fult1_DelTemp,
                B2V_Fult1_OverTemp = p.B2V_Fult1_OverTemp,
                B2V_Fult1_PackOverHVolt = p.B2V_Fult1_PackOverHVolt,
                B2V_Fult1_PackLowHVolt = p.B2V_Fult1_PackLowHVolt,
                B2V_Fult1_LowSOC = p.B2V_Fult1_LowSOC,
                B2V_Fult1_OverUcell = p.B2V_Fult1_OverUcell,
                B2V_Fult1_LowUcell = p.B2V_Fult1_LowUcell,
                B2V_Fult1_LowInsRes = p.B2V_Fult1_LowInsRes,
                B2V_Fult1_UcellUniformity = p.B2V_Fult1_UcellUniformity,
                B2V_Fult1_OverChg = p.B2V_Fult1_OverChg,
                B2V_Fult1_OverSOC = p.B2V_Fult1_OverSOC,
                B2V_Fult1_SOCChangeFast = p.B2V_Fult1_SOCChangeFast,
                B2V_Fult1_BatSysNotMatch = p.B2V_Fult1_BatSysNotMatch,
                B2V_Fult1_HVILFault = p.B2V_Fult1_HVILFault,
                B2V_Fult1_FaultNum_32960 = p.B2V_Fult1_FaultNum_32960,
                B2V_Fult2_FaultNum = p.B2V_Fult2_FaultNum,
                B2V_ST1_ChrgMode = p.B2V_ST1_ChrgMode,
                B2V_ST1_ChrgStatus = p.B2V_ST1_ChrgStatus,
                B2V_ST2_SOC = p.B2V_ST2_SOC,
                B2V_ST2_PackCurrent = p.B2V_ST2_PackCurrent,
                B2V_ST2_PackInsideVolt = p.B2V_ST2_PackInsideVolt,
                B2V_ST2_FaultCode = p.B2V_ST2_FaultCode,
                B2V_ST2_FaultLevel = p.B2V_ST2_FaultLevel,
                B2V_ST3_SysInsRes = p.B2V_ST3_SysInsRes,
                B2V_ST4_Max_Temp = p.B2V_ST4_Max_Temp,
                B2V_ST4_Min_Temp = p.B2V_ST4_Min_Temp,
                B2V_ST4_MaxTemp_CSCNo = p.B2V_ST4_MaxTemp_CSCNo,
                B2V_ST4_MaxTemp_Position = p.B2V_ST4_MaxTemp_Position,
                B2V_ST4_MinTemp_CSCNo = p.B2V_ST4_MinTemp_CSCNo,
                B2V_ST4_MinTemp_Position = p.B2V_ST4_MinTemp_Position,
                B2V_ST5_Max_Ucell = p.B2V_ST5_Max_Ucell,
                B2V_ST5_Min_Ucell = p.B2V_ST5_Min_Ucell,
                B2V_ST6_MaxUcell_CSCNo = p.B2V_ST6_MaxUcell_CSCNo,
                B2V_ST6_MaxUcell_Position = p.B2V_ST6_MaxUcell_Position,
                B2V_ST6_MinUcell_CSCNo = p.B2V_ST6_MinUcell_CSCNo,
                B2V_ST6_MinUcell_Position = p.B2V_ST6_MinUcell_Position,
                DC_Alarm1 = p.DC_Alarm1,
                DC_Alarm2 = p.DC_Alarm2,
                DC_OnOFF = p.DC_OnOFF,
                DC_Temp = p.DC_Temp,
                MCU_VehSpd = p.MCU_VehSpd,
                MCU_AccPdlPosV = p.MCU_AccPdlPosV,
                MCU_BrkPdlSts = p.MCU_BrkPdlSts,
                MCU_AccPdlPos = p.MCU_AccPdlPos,
                MCU_VehicleSts = p.MCU_VehicleSts,
                MCU_FltRsvrd = p.MCU_FltRsvrd,
                MCU_WorkingSts = p.MCU_WorkingSts,
                MCU_ErrNumberRsvrd = p.MCU_ErrNumberRsvrd,
                MCU_MtrSpd = p.MCU_MtrSpd,
                MCU_MtrSpdV = p.MCU_MtrSpdV,
                MCU_MtrCntrlTmp = p.MCU_MtrCntrlTmp,
                MCU_MtrTmp = p.MCU_MtrTmp,
                MCU_MtrModSts = p.MCU_MtrModSts,
                MCU_MtrDlvrdTrq = p.MCU_MtrDlvrdTrq,
                MCU_MtrSysFltSts = p.MCU_MtrSysFltSts,
                MCU_MtrInverRatVol = p.MCU_MtrInverRatVol,
                MCU_MtrCntrlRatCrnt = p.MCU_MtrCntrlRatCrnt,
                MCU_MtrCntrlTmpWarn = p.MCU_MtrCntrlTmpWarn,
                MCU_MtrTmpWarn = p.MCU_MtrTmpWarn,
                MCU_MtrSerialNmb = p.MCU_MtrSerialNmb,
                MCU_MtrErrCod = p.MCU_MtrErrCod,
                MCU_DriveMotorNumber = p.MCU_DriveMotorNumber,
                MCU_DriveMotorErrorNumber = p.MCU_DriveMotorErrorNumber,
                Vcu_GearPosn = p.Vcu_GearPosn,
                PedalDepth = p.PedalDepth,
                BreakStsType = p.BreakStsType,
                mileage = p.mileage,
                BreakSts = p.BreakSts,
                date = p.date,
                
                }).ToPageListAsync(pageIndex, pageSize, totalCount);
            }
            var list = new PageList<vehicle_parameterlist>(page, pageIndex, pageSize, totalCount);
            return list;
        }

        #endregion

    }
}
