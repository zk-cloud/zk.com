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
    /// 车辆基础类型表
    /// </summary>
    public partial class vehicle_type
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle_type()
        {
        }
		
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
		
        [SugarColumn(IsPrimaryKey = true)]
        
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String id  { get; set; }
        
		
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String name  { get; set; }
        
		
        /// <summary>
        /// 上级底盘ID，无上级为0
        /// </summary>
        [Display(Name = "上级底盘ID，无上级为0")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String ParentID  { get; set; }
        
		
        /// <summary>
        /// 品牌名称
        /// </summary>
        [Display(Name = "品牌名称")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String ZCSBNAME  { get; set; }
        
		
        /// <summary>
        /// 车辆类型
        /// </summary>
        [Display(Name = "车辆类型")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String CARTYPE  { get; set; }
        
		
        /// <summary>
        /// 整车质保期（年/万公里）
        /// </summary>
        [Display(Name = "整车质保期（年/万公里）")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String ZCZBQ  { get; set; }
        
		
        /// <summary>
        /// 准载人数
        /// </summary>
        [Display(Name = "准载人数")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 ZZNUM  { get; set; }
        
		
        /// <summary>
        /// 总质量（kg）
        /// </summary>
        [Display(Name = "总质量（kg）")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Decimal ZZL  { get; set; }
        
		
        /// <summary>
        /// 整备质量（kg）
        /// </summary>
        [Display(Name = "整备质量（kg）")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Decimal ZBZL  { get; set; }
        
		
        /// <summary>
        /// 工况法持续里程(km)
        /// </summary>
        [Display(Name = "工况法持续里程(km)")]
		
        
        
        
        
        public System.Decimal? GKFXSLC  { get; set; }
        
		
        /// <summary>
        /// 匀速法续驶里程(km)
        /// </summary>
        [Display(Name = "匀速法续驶里程(km)")]
		
        
        
        
        
        public System.Decimal? YSFXSLC  { get; set; }
        
		
        /// <summary>
        /// 最高车速(km/h)
        /// </summary>
        [Display(Name = "最高车速(km/h)")]
		
        
        
        
        
        public System.Decimal? MAXCS  { get; set; }
        
		
        /// <summary>
        /// 有能量回收装置
        /// </summary>
        [Display(Name = "有能量回收装置")]
		
        
        
        
        
        public System.Boolean? NLHSZZ  { get; set; }
        
		
        /// <summary>
        /// 充电方式
        /// </summary>
        [Display(Name = "充电方式")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String DDQCCDFS  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池包个数
        /// </summary>
        [Display(Name = "动力蓄电池包个数")]
		
        
        
        
        
        public System.Int32? DLXDCBNUM  { get; set; }
        
		
        /// <summary>
        /// 动力方式
        /// </summary>
        [Display(Name = "动力方式")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String POWERTYPE  { get; set; }
        
		
        /// <summary>
        /// 是否停产
        /// </summary>
        [Display(Name = "是否停产")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Boolean ISSTOP  { get; set; }
        
		
        /// <summary>
        /// 车载充电机生产企业
        /// </summary>
        [Display(Name = "车载充电机生产企业")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String DDQCCZCDJSCQY  { get; set; }
        
		
        /// <summary>
        /// 车载充电机型号
        /// </summary>
        [Display(Name = "车载充电机型号")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String DDQCCZCKJXH  { get; set; }
        
		
        /// <summary>
        /// 车载充电机额定输入电压(V)
        /// </summary>
        [Display(Name = "车载充电机额定输入电压(V)")]
		
        
        
        
        
        public System.Decimal? CZCDJEDSRDY  { get; set; }
        
		
        /// <summary>
        /// 车载充电机额定输入电流(A)
        /// </summary>
        [Display(Name = "车载充电机额定输入电流(A)")]
		
        
        
        
        
        public System.Decimal? CZCDJEDSRDL  { get; set; }
        
		
        /// <summary>
        /// 车载充电机额定输入频率(Hz)
        /// </summary>
        [Display(Name = "车载充电机额定输入频率(Hz)")]
		
        
        
        
        
        public System.Decimal? CZCDJEDSRPL  { get; set; }
        
		
        /// <summary>
        /// 车载充电机输出电压(V)
        /// </summary>
        [Display(Name = "车载充电机输出电压(V)")]
		
        
        
        
        
        public System.Decimal? CZCDJSCDY  { get; set; }
        
		
        /// <summary>
        /// 车载充电机输出电流(A)
        /// </summary>
        [Display(Name = "车载充电机输出电流(A)")]
		
        
        
        
        
        public System.Decimal? CZCDJSCDL  { get; set; }
        
		
        /// <summary>
        /// 车载充电机输出功率(kW)
        /// </summary>
        [Display(Name = "车载充电机输出功率(kW)")]
		
        
        
        
        
        public System.Decimal? CZCDJSCGL  { get; set; }
        
		
        /// <summary>
        /// 驱动电机生产企业
        /// </summary>
        [Display(Name = "驱动电机生产企业")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String QDDJSCQY  { get; set; }
        
		
        /// <summary>
        /// 驱动电机型号
        /// </summary>
        [Display(Name = "驱动电机型号")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String QDDJBH  { get; set; }
        
		
        /// <summary>
        /// 驱动电机控制器生产企业
        /// </summary>
        [Display(Name = "驱动电机控制器生产企业")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String QDDJKZQSCQY  { get; set; }
        
		
        /// <summary>
        /// 驱动电机控制器型号
        /// </summary>
        [Display(Name = "驱动电机控制器型号")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String QDDJKZQXH  { get; set; }
        
		
        /// <summary>
        /// 驱动电机类型
        /// </summary>
        [Display(Name = "驱动电机类型")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String QDDJTYPE  { get; set; }
        
		
        /// <summary>
        /// 驱动电机安装数量
        /// </summary>
        [Display(Name = "驱动电机安装数量")]
		
        
        
        
        
        public System.Int32? QDDJAZNUM  { get; set; }
        
		
        /// <summary>
        /// 驱动电机布置
        /// </summary>
        [Display(Name = "驱动电机布置")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String QDDJBZ  { get; set; }
        
		
        /// <summary>
        /// 驱动电机冷却方式
        /// </summary>
        [Display(Name = "驱动电机冷却方式")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String QDDJLQFS  { get; set; }
        
		
        /// <summary>
        /// 驱动电机额定功率(kW)
        /// </summary>
        [Display(Name = "驱动电机额定功率(kW)")]
		
        
        
        
        
        public System.Decimal? QDDJRatedPower  { get; set; }
        
		
        /// <summary>
        /// 驱动电机额定转速(r/min)
        /// </summary>
        [Display(Name = "驱动电机额定转速(r/min)")]
		
        
        
        
        
        public System.Decimal? QDDJRatedRevolution  { get; set; }
        
		
        /// <summary>
        /// 驱动电机额定转矩(N.m)
        /// </summary>
        [Display(Name = "驱动电机额定转矩(N.m)")]
		
        
        
        
        
        public System.Decimal? QDDJRatedTorque  { get; set; }
        
		
        /// <summary>
        /// 驱动电机峰值功率(kW)
        /// </summary>
        [Display(Name = "驱动电机峰值功率(kW)")]
		
        
        
        
        
        public System.Decimal? QDDJHightPower  { get; set; }
        
		
        /// <summary>
        /// 驱动电机峰值转速(r/min)
        /// </summary>
        [Display(Name = "驱动电机峰值转速(r/min)")]
		
        
        
        
        
        public System.Decimal? QDDJHightRevolution  { get; set; }
        
		
        /// <summary>
        /// 驱动电机峰值转矩(N.m)
        /// </summary>
        [Display(Name = "驱动电机峰值转矩(N.m)")]
		
        
        
        
        
        public System.Decimal? QDDJHightTorque  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池生产企业
        /// </summary>
        [Display(Name = "动力蓄电池生产企业")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String DLXDCSCQY  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池质保期(年/万公里)
        /// </summary>
        [Display(Name = "动力蓄电池质保期(年/万公里)")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String DLXDCZBQ  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池规格
        /// </summary>
        [Display(Name = "动力蓄电池规格")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String DLXDCNorms  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池类型
        /// </summary>
        [Display(Name = "动力蓄电池类型")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String DLXDCType  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池种类
        /// </summary>
        [Display(Name = "动力蓄电池种类")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String DLXDCZL  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池总成型号
        /// </summary>
        [Display(Name = "动力蓄电池总成型号")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String DLXDCZC  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池总成质量(kg)
        /// </summary>
        [Display(Name = "动力蓄电池总成质量(kg)")]
		
        
        
        
        
        public System.Decimal? DLXDCZCZL  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池总成标称容量(Ah)
        /// </summary>
        [Display(Name = "动力蓄电池总成标称容量(Ah)")]
		
        
        
        
        
        public System.Decimal? DLXDCZCBCRL  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池额定能量(kWh)
        /// </summary>
        [Display(Name = "动力蓄电池额定能量(kWh)")]
		
        
        
        
        
        public System.Decimal? DLXDCRatedPower  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池额定电压
        /// </summary>
        [Display(Name = "动力蓄电池额定电压")]
		
        
        
        
        
        public System.Decimal? DLXDCRatedVoltage  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池总成额定输出电流(A)
        /// </summary>
        [Display(Name = "动力蓄电池总成额定输出电流(A)")]
		
        
        
        
        
        public System.Decimal? DLXDCZCRatedCurrent  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池冷却方式
        /// </summary>
        [Display(Name = "动力蓄电池冷却方式")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String DLXDCLQType  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池加热方式
        /// </summary>
        [Display(Name = "动力蓄电池加热方式")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String DLXDCJRType  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池单体型号
        /// </summary>
        [Display(Name = "动力蓄电池单体型号")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String DLXDCDTXH  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池单体标称电压(V)
        /// </summary>
        [Display(Name = "动力蓄电池单体标称电压(V)")]
		
        
        
        
        
        public System.Decimal? DLXDCDTBCDY  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池单体外形
        /// </summary>
        [Display(Name = "动力蓄电池单体外形")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String DLXDCDTWX  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池单体外形尺寸(mm)
        /// </summary>
        [Display(Name = "动力蓄电池单体外形尺寸(mm)")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String DLXDCDTWXCC  { get; set; }
        
		
        /// <summary>
        /// 单体电池总数
        /// </summary>
        [Display(Name = "单体电池总数")]
		
        
        
        
        
        public System.Decimal? DTXDCTotalNum  { get; set; }
        
		
        /// <summary>
        /// 温度探针总数
        /// </summary>
        [Display(Name = "温度探针总数")]
		
        
        
        
        
        public System.Decimal? WDTZTotalNum  { get; set; }
        
		
        /// <summary>
        /// 能量密度
        /// </summary>
        [Display(Name = "能量密度")]
		
        
        
        
        
        public System.Decimal? EnergyDensity  { get; set; }
        
		
        /// <summary>
        /// 整车控制器生产企业
        /// </summary>
        [Display(Name = "整车控制器生产企业")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String ZCKZQSCQY  { get; set; }
        
		
        /// <summary>
        /// 整车控制器型号
        /// </summary>
        [Display(Name = "整车控制器型号")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String ZCKZQXH  { get; set; }
        
		
        /// <summary>
        /// 国家平台车型符合性报告附件上传地址
        /// </summary>
        [Display(Name = "国家平台车型符合性报告附件上传地址")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String BGFJPath  { get; set; }
        
		
        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String createBy  { get; set; }
        
		
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
		
        
        
        
        
        public System.DateTime? createTime  { get; set; }
        
		
        /// <summary>
        /// 修改者
        /// </summary>
        [Display(Name = "修改者")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String updateBy  { get; set; }
        
		
        /// <summary>
        /// 修改时间
        /// </summary>
        [Display(Name = "修改时间")]
		
        
        
        
        
        public System.DateTime? updateTime  { get; set; }
        
		
    }
}
