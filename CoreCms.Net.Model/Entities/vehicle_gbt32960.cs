/***********************************************************************
 *            Project: CoreCms                             
 *         CreateTime: 2022/11/30 16:34:34
 *        Description: 暂无
 ***********************************************************************/

using SqlSugar;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreCms.Net.Model.Entities
{
    [SplitTable(SplitType.Month)]//按月分表 （自带分表支持 年、季、月、周、日）
    [SugarTable("vehicle_gbt32960_{year}{month}{day}")]//3个变量必须要有，这么设计为了兼容开始按年，后面改成按月、按日
    /// <summary>
    /// 
    /// </summary>
    public partial class vehicle_gbt32960
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle_gbt32960()
        {
        }
		
        /// <summary>
        /// 主键
        /// </summary>
        
        [Display(Name = "主键")]
        
		
        [SugarColumn(IsPrimaryKey = true)]
        
        
        
        
        
        public System.Int64 id  { get; set; }
        
		
        /// <summary>
        /// 车架号
        /// </summary>
        
        [Display(Name = "车架号")]
        
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String VIN  { get; set; }
        
		
        /// <summary>
        /// 车辆状态
        /// </summary>
        
        [Display(Name = "车辆状态")]
        
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String VehicleSts  { get; set; }
        
		
        /// <summary>
        /// 充电状态
        /// </summary>
        
        [Display(Name = "充电状态")]
        
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String VehicleCharge  { get; set; }
        
		
        /// <summary>
        /// 行驶模式
        /// </summary>
        
        [Display(Name = "行驶模式")]
        
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Drivingmode  { get; set; }
        
		
        /// <summary>
        /// 速度
        /// </summary>
        
        [Display(Name = "速度")]




        [SugarColumn(Length = 20, DecimalDigits = 6)]

        public System.Decimal? VehicleSpeed  { get; set; }
        
		
        /// <summary>
        /// 车辆行驶里程
        /// </summary>
        
        [Display(Name = "车辆行驶里程")]





        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public System.Decimal? VehicleMileage  { get; set; }
        
		
        /// <summary>
        /// 车辆电压
        /// </summary>
        
        [Display(Name = "车辆电压")]





        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public System.Decimal? VehicleDY  { get; set; }
        
		
        /// <summary>
        /// 车辆总电流
        /// </summary>
        
        [Display(Name = "车辆总电流")]





        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public System.Decimal? VehicleDL  { get; set; }
        
		
        /// <summary>
        /// 电池当前容量
        /// </summary>
        
        [Display(Name = "电池当前容量")]





        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public System.Decimal? VehicleSOC  { get; set; }
        
		
        /// <summary>
        /// DC_DC状态
        /// </summary>
        
        [Display(Name = "DC_DC状态")]
        
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String VehicleDC_DC  { get; set; }
        
		
        /// <summary>
        /// 车辆挡位
        /// </summary>
        
        [Display(Name = "车辆挡位")]
        
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String VehicleGear  { get; set; }
        
		
        /// <summary>
        /// 车辆电阻
        /// </summary>
        
        [Display(Name = "车辆电阻")]





        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public System.Decimal? Vehicle  { get; set; }
        
		
        /// <summary>
        /// 加速踏板行程
        /// </summary>
        
        [Display(Name = "加速踏板行程")]





        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public System.Decimal? VehicleJSTB  { get; set; }
        
		
        /// <summary>
        /// 制动踏板行程
        /// </summary>
        
        [Display(Name = "制动踏板行程")]





        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public System.Decimal? VehicleZDTB  { get; set; }
        
		
        /// <summary>
        /// 驱动电机个数
        /// </summary>
        
        [Display(Name = "驱动电机个数")]





        public int MCU_NUM  { get; set; }
        
		
        /// <summary>
        /// 驱动电机序号
        /// </summary>
        
        [Display(Name = "驱动电机序号")]
        
		
        
        
        [StringLength(maximumLength:100,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String MCU_TableCode  { get; set; }
        
		
        /// <summary>
        /// GPS定位状态
        /// </summary>
        
        [Display(Name = "GPS定位状态")]
        
		
        
        
        [StringLength(maximumLength:100,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String GPS_Sts  { get; set; }
        
		
        /// <summary>
        /// GPS经度
        /// </summary>
        
        [Display(Name = "GPS经度")]





        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public System.Decimal? GPS_lng  { get; set; }
        
		
        /// <summary>
        /// GPS纬度
        /// </summary>
        
        [Display(Name = "GPS纬度")]





        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public System.Decimal? GPS_lat  { get; set; }
        
		
        /// <summary>
        /// 最高电压电池子系统编码
        /// </summary>
        
        [Display(Name = "最高电压电池子系统编码")]
        
		
        
                
        public int Peak_HightDYXTCode  { get; set; }
        
		
        /// <summary>
        /// 最高电压电池单体编码
        /// </summary>
        
        [Display(Name = "最高电压电池单体编码")]
        
		
        
                
        public int Peak_HightDYDTCode  { get; set; }
        
		
        /// <summary>
        /// 电池单体电压最高压
        /// </summary>
        
        [Display(Name = "电池单体电压最高压")]





        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public System.Decimal? Peak_HightDYVal  { get; set; }
        
		
        /// <summary>
        /// 最低电压电池子系统编码
        /// </summary>
        
        [Display(Name = "最低电压电池子系统编码")]
        
		
        
               
        public int Peak_LowDYXTCode  { get; set; }
        
		
        /// <summary>
        /// 最低电压电池单体编码
        /// </summary>
        
        [Display(Name = "最低电压电池单体编码")]
        
		
        
                
        public int Peak_LowDYDTCode  { get; set; }
        
		
        /// <summary>
        /// 电池单体电压最低压
        /// </summary>
        
        [Display(Name = "电池单体电压最低压")]





        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public System.Decimal? Peak_LowDYVal  { get; set; }
        
		
        /// <summary>
        /// 最高温度子系统号
        /// </summary>
        
        [Display(Name = "最高温度子系统号")]
        
		
        
        public int Peak_HightZXTCode  { get; set; }
        
		
        /// <summary>
        /// 最高温度探针号
        /// </summary>
        
        [Display(Name = "最高温度探针号")]
        
		
        
               
        public int Peak_HightTZCode  { get; set; }
        
		
        /// <summary>
        /// 最高温度值
        /// </summary>
        
        [Display(Name = "最高温度值")]





        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public System.Decimal? Peak_HightWDVal  { get; set; }
        
		
        /// <summary>
        /// 最低温度子系统号
        /// </summary>
        
        [Display(Name = "最低温度子系统号")]
        
		
        
               
        public int Peak_LowZXTCode  { get; set; }
        
		
        /// <summary>
        /// 最低温度探针号
        /// </summary>
        
        [Display(Name = "最低温度探针号")]
        
		
        
               
        public int Peak_LowTZCode  { get; set; }
        
		
        /// <summary>
        /// 最低温度值
        /// </summary>
        
        [Display(Name = "最低温度值")]





        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public System.Decimal? Peak_LowWDVal  { get; set; }
        
		
        /// <summary>
        /// 最高报警等级
        /// </summary>
        
        [Display(Name = "最高报警等级")]
        
		
        
        
        
        
        public int Report_Lev  { get; set; }
        
		
        /// <summary>
        /// 通用报警标志
        /// </summary>
        
        [Display(Name = "通用报警标志")]
        
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Report_Sts  { get; set; }
        
		
        /// <summary>
        /// 可充电储能装置故障总数
        /// </summary>
        
        [Display(Name = "可充电储能装置故障总数")]
        
		
        
        
        
        
        public int Report_CNGZNum  { get; set; }
        
		
        /// <summary>
        /// 可充电储能装置故障
        /// </summary>
        
        [Display(Name = "可充电储能装置故障")]
        
		
        
        
        [StringLength(maximumLength:250,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Report_CNGZ  { get; set; }
        
		
        /// <summary>
        /// 驱动电机故障总数
        /// </summary>
        
        [Display(Name = "驱动电机故障总数")]
        
		
        
        
        
        
        public int Report_QDDJGZNum  { get; set; }
        
		
        /// <summary>
        /// 驱动电机故障
        /// </summary>
        
        [Display(Name = "驱动电机故障")]
        
		
        
        
        [StringLength(maximumLength:250,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Report_QDDJGZ  { get; set; }
        
		
        /// <summary>
        /// 发动机故障总数
        /// </summary>
        
        [Display(Name = "发动机故障总数")]
        
		
        
        
        
        
        public int Report_FDJGZNum  { get; set; }
        
		
        /// <summary>
        /// 发动机故障
        /// </summary>
        
        [Display(Name = "发动机故障")]
        
		
        
        
        [StringLength(maximumLength:250,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Report_FDJGZ  { get; set; }
        
		
        /// <summary>
        /// 其它故障总数
        /// </summary>
        
        [Display(Name = "其它故障总数")]
        
		
        
        
        
        
        public int Report_QTGZNum  { get; set; }
        
		
        /// <summary>
        /// 其它故障
        /// </summary>
        
        [Display(Name = "其它故障")]
        
		
        
        
        [StringLength(maximumLength:250,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Report_QTGZ  { get; set; }
        
		
        /// <summary>
        /// 电池装置电压数据采集个数
        /// </summary>
        
        [Display(Name = "电池装置电压数据采集个数")]
        
		
        
        
        
        
        public System.Int32? Betty_DYNun  { get; set; }
        
		
        /// <summary>
        /// 电池电压记录表外键
        /// </summary>
        
        [Display(Name = "电池电压记录表外键")]
        
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Betty_DYCode  { get; set; }
        
		
        /// <summary>
        /// 电池装置温度数据采集个数
        /// </summary>
        
        [Display(Name = "电池装置温度数据采集个数")]
        
		
        
        
        
        
        public System.Int32? Betty_WDNum  { get; set; }
        
		
        /// <summary>
        /// 电池温度记录表外键
        /// </summary>
        
        [Display(Name = "电池温度记录表外键")]
        
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Betty_WDCode  { get; set; }
        
		
        /// <summary>
        /// 提交日期
        /// </summary>
        
        [Display(Name = "提交日期")]
        
		
        [SplitField]



        public System.DateTime date  { get; set; }
        
		
    }
}
