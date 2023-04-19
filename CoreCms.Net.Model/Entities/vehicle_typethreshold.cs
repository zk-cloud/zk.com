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
    /// 车辆类型阀值
    /// </summary>
    public partial class vehicle_typethreshold
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle_typethreshold()
        {
        }
		
        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = "编号")]
		
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
        /// 车辆类型ID
        /// </summary>
        [Display(Name = "车辆类型ID")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String VehicleTypeID  { get; set; }
        
		
        /// <summary>
        /// 车辆型号名称
        /// </summary>
        [Display(Name = "车辆型号名称")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String VehicleTypeName  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池包过压报警(V)
        /// </summary>
        [Display(Name = "动力蓄电池包过压报警(V)")]
		
        
        
        
        
        public System.Decimal? DLXDCBHightVoltageBJ  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池总电流过流充电(A)
        /// </summary>
        [Display(Name = "动力蓄电池总电流过流充电(A)")]
		
        
        
        
        
        public System.Decimal? DLXDCBHightCurrentInBJ  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池总电流过流放电(A)
        /// </summary>
        [Display(Name = "动力蓄电池总电流过流放电(A)")]
		
        
        
        
        
        public System.Decimal? DLXDCBHightCurrentOutBJ  { get; set; }
        
		
        /// <summary>
        /// 电池高温报警(℃)
        /// </summary>
        [Display(Name = "电池高温报警(℃)")]
		
        
        
        
        
        public System.Decimal? DCGWBJ  { get; set; }
        
		
        /// <summary>
        /// 绝缘报警(Ω/V)
        /// </summary>
        [Display(Name = "绝缘报警(Ω/V)")]
		
        
        
        
        
        public System.Decimal? JYBJ  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池包过压报警(V)
        /// </summary>
        [Display(Name = "动力蓄电池包过压报警(V)")]
		
        
        
        
        
        public System.Decimal? SJBJ_DLXDCBGYBJ  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池总电流过流充电(A)
        /// </summary>
        [Display(Name = "动力蓄电池总电流过流充电(A)")]
		
        
        
        
        
        public System.Decimal? SJBJ_DLXDCBHightCurrentInBJ  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池总电流过流放电(A)
        /// </summary>
        [Display(Name = "动力蓄电池总电流过流放电(A)")]
		
        
        
        
        
        public System.Decimal? SJBJ_DLXDCBHightCurrentOutBJ  { get; set; }
        
		
        /// <summary>
        /// 单体电池过压报警(V)
        /// </summary>
        [Display(Name = "单体电池过压报警(V)")]
		
        
        
        
        
        public System.Decimal? SJBJ_DTJCGYBJ  { get; set; }
        
		
        /// <summary>
        /// 电池高温报警(℃)
        /// </summary>
        [Display(Name = "电池高温报警(℃)")]
		
        
        
        
        
        public System.Decimal? SJBJ_DCGWBJ  { get; set; }
        
		
        /// <summary>
        /// 温度差异报警(℃)
        /// </summary>
        [Display(Name = "温度差异报警(℃)")]
		
        
        
        
        
        public System.Decimal? SJBJ_WDCYBJ  { get; set; }
        
		
        /// <summary>
        /// 绝缘报警(Ω/V)
        /// </summary>
        [Display(Name = "绝缘报警(Ω/V)")]
		
        
        
        
        
        public System.Decimal? SJBJ_JYBJ  { get; set; }
        
		
        /// <summary>
        /// 动力蓄电池包欠压报警(V)
        /// </summary>
        [Display(Name = "动力蓄电池包欠压报警(V)")]
		
        
        
        
        
        public System.Decimal? EJBJ_DLXDCBQYBJ  { get; set; }
        
		
        /// <summary>
        /// 驱动电机电流过高报警(A)
        /// </summary>
        [Display(Name = "驱动电机电流过高报警(A)")]
		
        
        
        
        
        public System.Decimal? EJBJ_QDDJDLGGBJ  { get; set; }
        
		
        /// <summary>
        /// 驱动电机控制器温度报警(℃)
        /// </summary>
        [Display(Name = "驱动电机控制器温度报警(℃)")]
		
        
        
        
        
        public System.Decimal? QDDJKZQWDBJ  { get; set; }
        
		
        /// <summary>
        /// 驱动电机温度报警(℃)
        /// </summary>
        [Display(Name = "驱动电机温度报警(℃)")]
		
        
        
        
        
        public System.Decimal? QDDJWDBJ  { get; set; }
        
		
        /// <summary>
        /// 驱动电机转速过高报警(r/min)
        /// </summary>
        [Display(Name = "驱动电机转速过高报警(r/min)")]
		
        
        
        
        
        public System.Decimal? YIBJ_QDDJZSGGBJ  { get; set; }
        
		
        /// <summary>
        /// SOC低报警(%)
        /// </summary>
        [Display(Name = "SOC低报警(%)")]
		
        
        
        
        
        public System.Decimal? YIBJ_SOCLowBJ  { get; set; }
        
		
        /// <summary>
        /// DC-DC温度报警(℃)
        /// </summary>
        [Display(Name = "DC-DC温度报警(℃)")]
		
        
        
        
        
        public System.Decimal? YJBJ_DCDCWDBJ  { get; set; }
        
		
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
