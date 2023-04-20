/***********************************************************************
 *            Project: CoreCms                             
 *         CreateTime: 2022/11/30 16:52:56
 *        Description: 暂无
 ***********************************************************************/

using SqlSugar;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreCms.Net.Model.Entities
{
    [SplitTable(SplitType.Month)]//按月分表 （自带分表支持 年、季、月、周、日）
    [SugarTable("vehicle_batterydylog_{year}{month}{day}")]//3个变量必须要有，这么设计为了兼容开始按年，后面改成按月、按日
    /// <summary>
    /// 信息上传电池系统记录表
    /// </summary>
    public partial class vehicle_batterydylog
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle_batterydylog()
        {
        }
		
        /// <summary>
        /// 自增主键
        /// </summary>
        
        [Display(Name = "自增主键")]
        
		
        [SugarColumn(IsPrimaryKey = true)]
        
        
        
        
        
        public System.Int64 id  { get; set; }
        
		
        /// <summary>
        /// 外部关联编码
        /// </summary>
        
        [Display(Name = "外部关联编码")]
        
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String code  { get; set; }
        
		
        /// <summary>
        /// 可充电储能子系统号
        /// </summary>
        
        [Display(Name = "可充电储能子系统号")]
        
		
        
        
        
        
        public System.Int32? BatteryCode  { get; set; }
        
		
        /// <summary>
        /// 可储能装置电压
        /// </summary>
        
        [Display(Name = "可储能装置电压")]





        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public System.Decimal? BatteryDY  { get; set; }
        
		
        /// <summary>
        /// 可储能装置电流
        /// </summary>
        
        [Display(Name = "可储能装置电流")]




        [SugarColumn(Length = 20, DecimalDigits = 6)]

        public System.Decimal? BatteryDL  { get; set; }
        
		
        /// <summary>
        /// 单体电池总数
        /// </summary>
        
        [Display(Name = "单体电池总数")]
        
		
        
        
        
        
        public int BatteryNum  { get; set; }
        
		
        /// <summary>
        /// 本帧起始电池序号
        /// </summary>
        
        [Display(Name = "本帧起始电池序号")]
        
		
        
        
        
        
        public int BatteryMonomerCode  { get; set; }
        
		
        /// <summary>
        /// 本帧单体电池总数
        /// </summary>
        
        [Display(Name = "本帧单体电池总数")]
        
		
        
        
        
        
        public int BatteryMonomerNum  { get; set; }
        
		
        /// <summary>
        /// 单体电池电压
        /// </summary>
        
        [Display(Name = "单体电池电压")]





        [SugarColumn(ColumnDataType = "longtext")]
        public System.String BatteryMonomerDY  { get; set; }
        
		
        /// <summary>
        /// 提交时间
        /// </summary>
        
        [Display(Name = "提交时间")]



        [SplitField]



        public System.DateTime date  { get; set; }
        
		
    }
}
