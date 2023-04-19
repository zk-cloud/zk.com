/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 
 *        Description: 暂无
 ***********************************************************************/

using SqlSugar;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreCms.Net.Model.Entities
{
    /// <summary>
    /// 车辆参数采集列表
    /// </summary>
    public partial class vehicle_parameterlist
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle_parameterlist()
        {
        }
		
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
		
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 id  { get; set; }
        
		
        /// <summary>
        /// 车架号
        /// </summary>
        [Display(Name = "车架号")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String VIN  { get; set; }
        
		
        /// <summary>
        /// 上传记录id
        /// </summary>
        [Display(Name = "上传记录id")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 Logid  { get; set; }
        
		
        /// <summary>
        ///  电池编码信息帧序号Battery Code number of frame VIN
        /// </summary>
        [Display(Name = " 电池编码信息帧序号Battery Code number of frame VIN")]
		
        
        
        
        
        public System.Decimal? B2V_SN_FrameNo  { get; set; }
        
		
        /// <summary>
        ///  电池编码长度Length of battery code
        /// </summary>
        [Display(Name = " 电池编码长度Length of battery code")]
		
        
        
        
        
        public System.Decimal? B2V_SN_SysCodeLength  { get; set; }
        
		
        /// <summary>
        ///  电池编码(SN)字符1,7,13,19(ASCII)Battery Code(SN) Character 1,7,13,19(ASCII)
        /// </summary>
        [Display(Name = " 电池编码(SN)字符1,7,13,19(ASCII)Battery Code(SN) Character 1,7,13,19(ASCII)")]
		
        
        
        
        
        public System.Decimal? B2V_SN_SysCode_B1_7_13_19  { get; set; }
        
		
        /// <summary>
        ///  电池编码(SN)字符2,8,14,20(ASCII)Battery Code(SN) Character 2,8,14,20(ASCII)
        /// </summary>
        [Display(Name = " 电池编码(SN)字符2,8,14,20(ASCII)Battery Code(SN) Character 2,8,14,20(ASCII)")]
		
        
        
        
        
        public System.Decimal? B2V_SN_SysCode_B2_8_14_20  { get; set; }
        
		
        /// <summary>
        ///  电池编码(SN)字符3,9,15,21(ASCII)Battery Code(SN) Character 3,9,15,21(ASCII)
        /// </summary>
        [Display(Name = " 电池编码(SN)字符3,9,15,21(ASCII)Battery Code(SN) Character 3,9,15,21(ASCII)")]
		
        
        
        
        
        public System.Decimal? B2V_SN_SysCode_B3_9_15_21  { get; set; }
        
		
        /// <summary>
        ///  电池编码(SN)字符4,10,16,22(ASCII)Battery Code(SN) Character4,10,16,22(ASCII)
        /// </summary>
        [Display(Name = " 电池编码(SN)字符4,10,16,22(ASCII)Battery Code(SN) Character4,10,16,22(ASCII)")]
		
        
        
        
        
        public System.Decimal? B2V_SN_SysCode_B4_10_16_22  { get; set; }
        
		
        /// <summary>
        ///  电池编码(SN)字符5,11,17,23(ASCII)Battery Code(SN) Character 5,11,17,23(ASCII)
        /// </summary>
        [Display(Name = " 电池编码(SN)字符5,11,17,23(ASCII)Battery Code(SN) Character 5,11,17,23(ASCII)")]
		
        
        
        
        
        public System.Decimal? B2V_SN_SysCode_B5_11_17_23  { get; set; }
        
		
        /// <summary>
        ///  电池编码(SN)字符6,12,18,24(ASCII)Battery Code(SN) Character 6,12,18,24(ASCII)
        /// </summary>
        [Display(Name = " 电池编码(SN)字符6,12,18,24(ASCII)Battery Code(SN) Character 6,12,18,24(ASCII)")]
		
        
        
        
        
        public System.Decimal? B2V_SN_SysCode_B6_12_18_24  { get; set; }
        
		
        /// <summary>
        ///  PACK中电芯温度点(探针)的总数目Sum of Temperature Probe in PACK
        /// </summary>
        [Display(Name = " PACK中电芯温度点(探针)的总数目Sum of Temperature Probe in PACK")]
		
        
        
        
        
        public System.Decimal? B2V_BI2_PackTotTempNum  { get; set; }
        
		
        /// <summary>
        ///  可充电储能系统数目（固定为1）
        /// </summary>
        [Display(Name = " 可充电储能系统数目（固定为1）")]
		
        
        
        
        
        public System.Decimal? B2V_BI2_BattSysNum  { get; set; }
        
		
        /// <summary>
        ///  可充电储能系统序号（固定为1）
        /// </summary>
        [Display(Name = " 可充电储能系统序号（固定为1）")]
		
        
        
        
        
        public System.Decimal? B2V_BI2_BattSysSeqNum  { get; set; }
        
		
        /// <summary>
        ///  本帧起始单体电压序号（从1开始）Cell serial number of this Frame(Started from 1)
        /// </summary>
        [Display(Name = " 本帧起始单体电压序号（从1开始）Cell serial number of this Frame(Started from 1)")]
		
        
        
        
        
        public System.Decimal? B2V_V_FrameNo  { get; set; }
        
		
        /// <summary>
        ///  本帧第1号电池电压（最后一帧发不满，则发送0xFFFF）Voltage of No.1 cell  in this frame (Send 0xFFFF if thevoltage value is invail);单位：V
        /// </summary>
        [Display(Name = " 本帧第1号电池电压（最后一帧发不满，则发送0xFFFF）Voltage of No.1 cell  in this frame (Send 0xFFFF if thevoltage value is invail);单位：V")]
		
        
        
        
        
        public System.Decimal? B2V_V_Cell_VoltN1  { get; set; }
        
		
        /// <summary>
        ///  本帧第2号电池电压（最后一帧发不满，则发送0xFFFF）Voltage of No.2 cell  in this frame (Send 0xFFFF if thevoltage value is invail);单位：V
        /// </summary>
        [Display(Name = " 本帧第2号电池电压（最后一帧发不满，则发送0xFFFF）Voltage of No.2 cell  in this frame (Send 0xFFFF if thevoltage value is invail);单位：V")]
		
        
        
        
        
        public System.Decimal? B2V_V_Cell_VoltN2  { get; set; }
        
		
        /// <summary>
        ///  本帧第3号电池电压（最后一帧发不满，则发送0xFFFF）Voltage of No.3 cell  in this frame (Send 0xFFFF if thevoltage value is invail);单位：V
        /// </summary>
        [Display(Name = " 本帧第3号电池电压（最后一帧发不满，则发送0xFFFF）Voltage of No.3 cell  in this frame (Send 0xFFFF if thevoltage value is invail);单位：V")]
		
        
        
        
        
        public System.Decimal? B2V_V_Cell_VoltN3  { get; set; }
        
		
        /// <summary>
        ///  本帧起始单体温度序号（从1开始）Temperature probe serial number of this Frame(Startedfrom 1)
        /// </summary>
        [Display(Name = " 本帧起始单体温度序号（从1开始）Temperature probe serial number of this Frame(Startedfrom 1)")]
		
        
        
        
        
        public System.Decimal? B2V_T_FrameNo  { get; set; }
        
		
        /// <summary>
        ///  本帧第1号电池温度（最后一帧发不满，则发送0xFF）Temperature of No.1 probe  in this frame (Send 0xFF ifthe temperature value is invail);单位：℃
        /// </summary>
        [Display(Name = " 本帧第1号电池温度（最后一帧发不满，则发送0xFF）Temperature of No.1 probe  in this frame (Send 0xFF ifthe temperature value is invail);单位：℃")]
		
        
        
        
        
        public System.Decimal? B2V_T_Cell_TempN1  { get; set; }
        
		
        /// <summary>
        ///  本帧第2号电池温度（最后一帧发不满，则发送0xFF）Temperature of No.2 probe  in this frame (Send 0xFF ifthe temperature value is invail);单位：℃
        /// </summary>
        [Display(Name = " 本帧第2号电池温度（最后一帧发不满，则发送0xFF）Temperature of No.2 probe  in this frame (Send 0xFF ifthe temperature value is invail);单位：℃")]
		
        
        
        
        
        public System.Decimal? B2V_T_Cell_TempN2  { get; set; }
        
		
        /// <summary>
        ///  本帧第3号电池温度（最后一帧发不满，则发送0xFF）Temperature of No.3 probe  in this frame (Send 0xFF ifthe temperature value is invail);单位：℃
        /// </summary>
        [Display(Name = " 本帧第3号电池温度（最后一帧发不满，则发送0xFF）Temperature of No.3 probe  in this frame (Send 0xFF ifthe temperature value is invail);单位：℃")]
		
        
        
        
        
        public System.Decimal? B2V_T_Cell_TempN3  { get; set; }
        
		
        /// <summary>
        ///  本帧第4号电池温度（最后一帧发不满，则发送0xFF）Temperature of No.4 probe  in this frame (Send 0xFF ifthe temperature value is invail);单位：℃
        /// </summary>
        [Display(Name = " 本帧第4号电池温度（最后一帧发不满，则发送0xFF）Temperature of No.4 probe  in this frame (Send 0xFF ifthe temperature value is invail);单位：℃")]
		
        
        
        
        
        public System.Decimal? B2V_T_Cell_TempN4  { get; set; }
        
		
        /// <summary>
        ///  本帧第5号电池温度（最后一帧发不满，则发送0xFF）Temperature of No.5 probe  in this frame (Send 0xFF ifthe temperature value is invail);单位：℃
        /// </summary>
        [Display(Name = " 本帧第5号电池温度（最后一帧发不满，则发送0xFF）Temperature of No.5 probe  in this frame (Send 0xFF ifthe temperature value is invail);单位：℃")]
		
        
        
        
        
        public System.Decimal? B2V_T_Cell_TempN5  { get; set; }
        
		
        /// <summary>
        ///  本帧第6号电池温度（最后一帧发不满，则发送0xFF）Temperature of No.6 probe  in this frame (Send 0xFF ifthe temperature value is invail);单位：℃
        /// </summary>
        [Display(Name = " 本帧第6号电池温度（最后一帧发不满，则发送0xFF）Temperature of No.6 probe  in this frame (Send 0xFF ifthe temperature value is invail);单位：℃")]
		
        
        
        
        
        public System.Decimal? B2V_T_Cell_TempN6  { get; set; }
        
		
        /// <summary>
        ///  电芯温差异常报警Excessive temperature differentials alarm
        /// </summary>
        [Display(Name = " 电芯温差异常报警Excessive temperature differentials alarm")]
		
        
        
        
        
        public System.Decimal? B2V_Fult1_DelTemp  { get; set; }
        
		
        /// <summary>
        ///  电芯温度过高报警Excess temperature alarm
        /// </summary>
        [Display(Name = " 电芯温度过高报警Excess temperature alarm")]
		
        
        
        
        
        public System.Decimal? B2V_Fult1_OverTemp  { get; set; }
        
		
        /// <summary>
        ///  PACK过压报警（车载储能装置过压）
        /// </summary>
        [Display(Name = " PACK过压报警（车载储能装置过压）")]
		
        
        
        
        
        public System.Decimal? B2V_Fult1_PackOverHVolt  { get; set; }
        
		
        /// <summary>
        ///  PACK欠压报警（车载储能装置欠压）
        /// </summary>
        [Display(Name = " PACK欠压报警（车载储能装置欠压）")]
		
        
        
        
        
        public System.Decimal? B2V_Fult1_PackLowHVolt  { get; set; }
        
		
        /// <summary>
        ///  SOC过低报警
        /// </summary>
        [Display(Name = " SOC过低报警")]
		
        
        
        
        
        public System.Decimal? B2V_Fult1_LowSOC  { get; set; }
        
		
        /// <summary>
        ///  单体电压过高报警
        /// </summary>
        [Display(Name = " 单体电压过高报警")]
		
        
        
        
        
        public System.Decimal? B2V_Fult1_OverUcell  { get; set; }
        
		
        /// <summary>
        ///  单体电压欠压报警
        /// </summary>
        [Display(Name = " 单体电压欠压报警")]
		
        
        
        
        
        public System.Decimal? B2V_Fult1_LowUcell  { get; set; }
        
		
        /// <summary>
        ///  绝缘报警Insulation resistance too low alarm
        /// </summary>
        [Display(Name = " 绝缘报警Insulation resistance too low alarm")]
		
        
        
        
        
        public System.Decimal? B2V_Fult1_LowInsRes  { get; set; }
        
		
        /// <summary>
        ///  电池一致性差报警Cell high consistency alarm
        /// </summary>
        [Display(Name = " 电池一致性差报警Cell high consistency alarm")]
		
        
        
        
        
        public System.Decimal? B2V_Fult1_UcellUniformity  { get; set; }
        
		
        /// <summary>
        ///  车载储能装置过充报警Energy system overcharged alarm
        /// </summary>
        [Display(Name = " 车载储能装置过充报警Energy system overcharged alarm")]
		
        
        
        
        
        public System.Decimal? B2V_Fult1_OverChg  { get; set; }
        
		
        /// <summary>
        ///  SOC过高报警
        /// </summary>
        [Display(Name = " SOC过高报警")]
		
        
        
        
        
        public System.Decimal? B2V_Fult1_OverSOC  { get; set; }
        
		
        /// <summary>
        ///  SOC跳变报警
        /// </summary>
        [Display(Name = " SOC跳变报警")]
		
        
        
        
        
        public System.Decimal? B2V_Fult1_SOCChangeFast  { get; set; }
        
		
        /// <summary>
        ///  可充电储能系统不匹配报警BMS not matched alarm
        /// </summary>
        [Display(Name = " 可充电储能系统不匹配报警BMS not matched alarm")]
		
        
        
        
        
        public System.Decimal? B2V_Fult1_BatSysNotMatch  { get; set; }
        
		
        /// <summary>
        ///  高压互锁报警HVIL alarm
        /// </summary>
        [Display(Name = " 高压互锁报警HVIL alarm")]
		
        
        
        
        
        public System.Decimal? B2V_Fult1_HVILFault  { get; set; }
        
		
        /// <summary>
        ///  GBT32960.3中规定的故障数目(当前时刻发生的)Sum alarm specified in GBT32960.3(Current)
        /// </summary>
        [Display(Name = " GBT32960.3中规定的故障数目(当前时刻发生的)Sum alarm specified in GBT32960.3(Current)")]
		
        
        
        
        
        public System.Decimal? B2V_Fult1_FaultNum_32960  { get; set; }
        
		
        /// <summary>
        ///  BMS当前发生的故障总数目（包括GB32960中规定的故障数目）Sum fault of BMS(Include GBT32960 fault)
        /// </summary>
        [Display(Name = " BMS当前发生的故障总数目（包括GB32960中规定的故障数目）Sum fault of BMS(Include GBT32960 fault)")]
		
        
        
        
        
        public System.Decimal? B2V_Fult2_FaultNum  { get; set; }
        
		
        /// <summary>
        ///  BMS当前充电模式BMS charge mode
        /// </summary>
        [Display(Name = " BMS当前充电模式BMS charge mode")]
		
        
        
        
        
        public System.Decimal? B2V_ST1_ChrgMode  { get; set; }
        
		
        /// <summary>
        ///  充电状态BMS charge status
        /// </summary>
        [Display(Name = " 充电状态BMS charge status")]
		
        
        
        
        
        public System.Decimal? B2V_ST1_ChrgStatus  { get; set; }
        
		
        /// <summary>
        ///  电池包SOC;单位：%
        /// </summary>
        [Display(Name = " 电池包SOC;单位：%")]
		
        
        
        
        
        public System.Decimal? B2V_ST2_SOC  { get; set; }
        
		
        /// <summary>
        ///  电池包总电流，充电为负值，放电为正值（总电流）;单位：A
        /// </summary>
        [Display(Name = " 电池包总电流，充电为负值，放电为正值（总电流）;单位：A")]
		
        
        
        
        
        public System.Decimal? B2V_ST2_PackCurrent  { get; set; }
        
		
        /// <summary>
        ///  电池包内侧电压（总电压）;单位：V
        /// </summary>
        [Display(Name = " 电池包内侧电压（总电压）;单位：V")]
		
        
        
        
        
        public System.Decimal? B2V_ST2_PackInsideVolt  { get; set; }
        
		
        /// <summary>
        ///  故障码Fault code
        /// </summary>
        [Display(Name = " 故障码Fault code")]
		
        
        
        
        
        public System.Decimal? B2V_ST2_FaultCode  { get; set; }
        
		
        /// <summary>
        ///  当前最高故障等级Highest fault level
        /// </summary>
        [Display(Name = " 当前最高故障等级Highest fault level")]
		
        
        
        
        
        public System.Decimal? B2V_ST2_FaultLevel  { get; set; }
        
		
        /// <summary>
        ///  BMS系统绝缘值（绝缘电阻）;单位：kohm
        /// </summary>
        [Display(Name = " BMS系统绝缘值（绝缘电阻）;单位：kohm")]
		
        
        
        
        
        public System.Decimal? B2V_ST3_SysInsRes  { get; set; }
        
		
        /// <summary>
        ///  电芯温度最大值（最高温度值）;单位：℃
        /// </summary>
        [Display(Name = " 电芯温度最大值（最高温度值）;单位：℃")]
		
        
        
        
        
        public System.Decimal? B2V_ST4_Max_Temp  { get; set; }
        
		
        /// <summary>
        ///  电芯温度最小值（最低温度值）;单位：℃
        /// </summary>
        [Display(Name = " 电芯温度最小值（最低温度值）;单位：℃")]
		
        
        
        
        
        public System.Decimal? B2V_ST4_Min_Temp  { get; set; }
        
		
        /// <summary>
        ///  最高温度探针所在的CSC编号（最高温度探针单体代号）
        /// </summary>
        [Display(Name = " 最高温度探针所在的CSC编号（最高温度探针单体代号）")]
		
        
        
        
        
        public System.Decimal? B2V_ST4_MaxTemp_CSCNo  { get; set; }
        
		
        /// <summary>
        ///  最高温度探针在系统中的位置（最高温度子系统号）
        /// </summary>
        [Display(Name = " 最高温度探针在系统中的位置（最高温度子系统号）")]
		
        
        
        
        
        public System.Decimal? B2V_ST4_MaxTemp_Position  { get; set; }
        
		
        /// <summary>
        ///  最低温度探针所在的CSC编号（最低温度探针单体代号）
        /// </summary>
        [Display(Name = " 最低温度探针所在的CSC编号（最低温度探针单体代号）")]
		
        
        
        
        
        public System.Decimal? B2V_ST4_MinTemp_CSCNo  { get; set; }
        
		
        /// <summary>
        ///  最低温度探针在系统中的位置（最高温度子系统号）
        /// </summary>
        [Display(Name = " 最低温度探针在系统中的位置（最高温度子系统号）")]
		
        
        
        
        
        public System.Decimal? B2V_ST4_MinTemp_Position  { get; set; }
        
		
        /// <summary>
        ///  电芯电压最大值（单体电压最高值）;单位：v
        /// </summary>
        [Display(Name = " 电芯电压最大值（单体电压最高值）;单位：v")]
		
        
        
        
        
        public System.Decimal? B2V_ST5_Max_Ucell  { get; set; }
        
		
        /// <summary>
        ///  电芯电压最小值（单体电压最低值）;单位：v
        /// </summary>
        [Display(Name = " 电芯电压最小值（单体电压最低值）;单位：v")]
		
        
        
        
        
        public System.Decimal? B2V_ST5_Min_Ucell  { get; set; }
        
		
        /// <summary>
        ///  最高电压电芯在的CSC编号（最高电压单体代号）
        /// </summary>
        [Display(Name = " 最高电压电芯在的CSC编号（最高电压单体代号）")]
		
        
        
        
        
        public System.Decimal? B2V_ST6_MaxUcell_CSCNo  { get; set; }
        
		
        /// <summary>
        ///  最高电压电芯在系统中的位置（最高电压单体子系统号）
        /// </summary>
        [Display(Name = " 最高电压电芯在系统中的位置（最高电压单体子系统号）")]
		
        
        
        
        
        public System.Decimal? B2V_ST6_MaxUcell_Position  { get; set; }
        
		
        /// <summary>
        ///  最低电压电芯在的CSC编号（最低电压单体代号）
        /// </summary>
        [Display(Name = " 最低电压电芯在的CSC编号（最低电压单体代号）")]
		
        
        
        
        
        public System.Decimal? B2V_ST6_MinUcell_CSCNo  { get; set; }
        
		
        /// <summary>
        ///  最低电压电芯在系统中的位置（最低电压单体子系统号）
        /// </summary>
        [Display(Name = " 最低电压电芯在系统中的位置（最低电压单体子系统号）")]
		
        
        
        
        
        public System.Decimal? B2V_ST6_MinUcell_Position  { get; set; }
        
		
        /// <summary>
        /// 故障报警低字节
        /// </summary>
        [Display(Name = "故障报警低字节")]
		
        
        
        
        
        public System.Decimal? DC_Alarm1  { get; set; }
        
		
        /// <summary>
        /// 故障报警高字节
        /// </summary>
        [Display(Name = "故障报警高字节")]
		
        
        
        
        
        public System.Decimal? DC_Alarm2  { get; set; }
        
		
        /// <summary>
        /// 工作状态
        /// </summary>
        [Display(Name = "工作状态")]
		
        
        
        
        
        public System.Decimal? DC_OnOFF  { get; set; }
        
		
        /// <summary>
        /// 内部温度;单位：℃
        /// </summary>
        [Display(Name = "内部温度;单位：℃")]
		
        
        
        
        
        public System.Decimal? DC_Temp  { get; set; }
        
		
        /// <summary>
        /// 车速;单位：KPH
        /// </summary>
        [Display(Name = "车速;单位：KPH")]
		
        
        
        
        
        public System.Decimal? MCU_VehSpd  { get; set; }
        
		
        /// <summary>
        /// 油门踏板有效信号
        /// </summary>
        [Display(Name = "油门踏板有效信号")]
		
        
        
        
        
        public System.Decimal? MCU_AccPdlPosV  { get; set; }
        
		
        /// <summary>
        /// 刹车踏板状态
        /// </summary>
        [Display(Name = "刹车踏板状态")]
		
        
        
        
        
        public System.Decimal? MCU_BrkPdlSts  { get; set; }
        
		
        /// <summary>
        /// MCU信号提示当前油门踩压位置;单位：%
        /// </summary>
        [Display(Name = "MCU信号提示当前油门踩压位置;单位：%")]
		
        
        
        
        
        public System.Decimal? MCU_AccPdlPos  { get; set; }
        
		
        /// <summary>
        /// 车辆运行状态
        /// </summary>
        [Display(Name = "车辆运行状态")]
		
        
        
        
        
        public System.Decimal? MCU_VehicleSts  { get; set; }
        
		
        /// <summary>
        /// MCU故障等级
        /// </summary>
        [Display(Name = "MCU故障等级")]
		
        
        
        
        
        public System.Decimal? MCU_FltRsvrd  { get; set; }
        
		
        /// <summary>
        /// 车辆工作状态
        /// </summary>
        [Display(Name = "车辆工作状态")]
		
        
        
        
        
        public System.Decimal? MCU_WorkingSts  { get; set; }
        
		
        /// <summary>
        /// MCU故障总数
        /// </summary>
        [Display(Name = "MCU故障总数")]
		
        
        
        
        
        public System.Decimal? MCU_ErrNumberRsvrd  { get; set; }
        
		
        /// <summary>
        /// 电机转速;单位：rpm
        /// </summary>
        [Display(Name = "电机转速;单位：rpm")]
		
        
        
        
        
        public System.Decimal? MCU_MtrSpd  { get; set; }
        
		
        /// <summary>
        /// 电机转速有效位
        /// </summary>
        [Display(Name = "电机转速有效位")]
		
        
        
        
        
        public System.Decimal? MCU_MtrSpdV  { get; set; }
        
		
        /// <summary>
        /// 电机控制器温度;单位：℃
        /// </summary>
        [Display(Name = "电机控制器温度;单位：℃")]
		
        
        
        
        
        public System.Decimal? MCU_MtrCntrlTmp  { get; set; }
        
		
        /// <summary>
        /// 电机温度;单位：℃
        /// </summary>
        [Display(Name = "电机温度;单位：℃")]
		
        
        
        
        
        public System.Decimal? MCU_MtrTmp  { get; set; }
        
		
        /// <summary>
        /// 电机运行模式
        /// </summary>
        [Display(Name = "电机运行模式")]
		
        
        
        
        
        public System.Decimal? MCU_MtrModSts  { get; set; }
        
		
        /// <summary>
        /// 电机实际扭矩;单位：N.m
        /// </summary>
        [Display(Name = "电机实际扭矩;单位：N.m")]
		
        
        
        
        
        public System.Decimal? MCU_MtrDlvrdTrq  { get; set; }
        
		
        /// <summary>
        /// 电机系统故障状态
        /// </summary>
        [Display(Name = "电机系统故障状态")]
		
        
        
        
        
        public System.Decimal? MCU_MtrSysFltSts  { get; set; }
        
		
        /// <summary>
        /// 电机直流母线端电压;单位：V
        /// </summary>
        [Display(Name = "电机直流母线端电压;单位：V")]
		
        
        
        
        
        public System.Decimal? MCU_MtrInverRatVol  { get; set; }
        
		
        /// <summary>
        /// 电机控制器母线端电流;单位：A
        /// </summary>
        [Display(Name = "电机控制器母线端电流;单位：A")]
		
        
        
        
        
        public System.Decimal? MCU_MtrCntrlRatCrnt  { get; set; }
        
		
        /// <summary>
        /// 电机控制器温度报警
        /// </summary>
        [Display(Name = "电机控制器温度报警")]
		
        
        
        
        
        public System.Decimal? MCU_MtrCntrlTmpWarn  { get; set; }
        
		
        /// <summary>
        /// 电机温度报警
        /// </summary>
        [Display(Name = "电机温度报警")]
		
        
        
        
        
        public System.Decimal? MCU_MtrTmpWarn  { get; set; }
        
		
        /// <summary>
        /// 电机序列号
        /// </summary>
        [Display(Name = "电机序列号")]
		
        
        
        
        
        public System.Decimal? MCU_MtrSerialNmb  { get; set; }
        
		
        /// <summary>
        /// 电机故障代码
        /// </summary>
        [Display(Name = "电机故障代码")]
		
        
        
        
        
        public System.Decimal? MCU_MtrErrCod  { get; set; }
        
		
        /// <summary>
        /// 驱动电机个数
        /// </summary>
        [Display(Name = "驱动电机个数")]
		
        
        
        
        
        public System.Decimal? MCU_DriveMotorNumber  { get; set; }
        
		
        /// <summary>
        /// 驱动电机故障总数
        /// </summary>
        [Display(Name = "驱动电机故障总数")]
		
        
        
        
        
        public System.Decimal? MCU_DriveMotorErrorNumber  { get; set; }
        
		
        /// <summary>
        /// VCU档位信息
        /// </summary>
        [Display(Name = "VCU档位信息")]
		
        
        
        
        
        public System.Decimal? Vcu_GearPosn  { get; set; }
        
		
        /// <summary>
        /// 加速踏板开度;单位：%
        /// </summary>
        [Display(Name = "加速踏板开度;单位：%")]
		
        
        
        
        
        public System.Decimal? PedalDepth  { get; set; }
        
		
        /// <summary>
        /// 制动状态
        /// </summary>
        [Display(Name = "制动状态")]
		
        
        
        
        
        public System.Decimal? BreakStsType  { get; set; }
        
		
        /// <summary>
        /// 累计里程;单位：Km
        /// </summary>
        [Display(Name = "累计里程;单位：Km")]
		
        
        
        
        
        public System.Decimal? mileage  { get; set; }
        
		
        /// <summary>
        /// 制动系统报警标志位
        /// </summary>
        [Display(Name = "制动系统报警标志位")]
		
        
        
        
        
        public System.Decimal? BreakSts  { get; set; }
        
		
        /// <summary>
        /// 提交日期
        /// </summary>
        [Display(Name = "提交日期")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.DateTime date  { get; set; }
        
		
    }
}
