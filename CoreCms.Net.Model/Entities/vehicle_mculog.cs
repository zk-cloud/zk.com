/***********************************************************************
 *            Project: CoreCms                             
 *         CreateTime: 2022/11/30 16:52:40
 *        Description: 暂无
 ***********************************************************************/

using SqlSugar;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreCms.Net.Model.Entities
{
    [SplitTable(SplitType.Month)]//按月分表 （自带分表支持 年、季、月、周、日）
    [SugarTable("vehicle_mculog_{year}{month}{day}")]//3个变量必须要有，这么设计为了兼容开始按年，后面改成按月、按日
    /// <summary>
    /// 电机记录表
    /// </summary>
    public partial class vehicle_mculog
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle_mculog()
        {
        }
		
        /// <summary>
        /// 自增主键
        /// </summary>
        
        [Display(Name = "自增主键")]
        
		
        [SugarColumn(IsPrimaryKey = true)]
        
        
        
        
        
        public System.Int64 id  { get; set; }
        
		
        /// <summary>
        /// 外部标识
        /// </summary>
        
        [Display(Name = "外部标识")]
        
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String code  { get; set; }
        
		
        /// <summary>
        /// 驱动电机序号
        /// </summary>
        
        [Display(Name = "驱动电机序号")]
        
		
        
                
        public int MCU_Code  { get; set; }
        
		
        /// <summary>
        /// 驱动电机状态
        /// </summary>
        
        [Display(Name = "驱动电机状态")]
        
		
        
        
        
        public int MCU_Sts  { get; set; }
        
		
        /// <summary>
        /// 驱动电机控制器温度
        /// </summary>
        
        [Display(Name = "驱动电机控制器温度")]



        [SugarColumn(Length = 20, DecimalDigits = 6)]
        
        public decimal MCU_KZQWD  { get; set; }
        
		
        /// <summary>
        /// 驱动电机转速
        /// </summary>
        
        [Display(Name = "驱动电机转速")]




        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public decimal MCU_DJZS  { get; set; }
        
		
        /// <summary>
        /// 驱动电机转矩
        /// </summary>
        
        [Display(Name = "驱动电机转矩")]




        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public decimal MCU_DJZJ  { get; set; }
        
		
        /// <summary>
        /// 驱动电机温度
        /// </summary>
        
        [Display(Name = "驱动电机温度")]




        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public decimal MCU_DJWD  { get; set; }
        
		
        /// <summary>
        /// 驱动电机控制器电压
        /// </summary>
        
        [Display(Name = "驱动电机控制器电压")]




        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public decimal MCU_KZQDY  { get; set; }
        
		
        /// <summary>
        /// 驱动电机控制器母线电流
        /// </summary>
        
        [Display(Name = "驱动电机控制器母线电流")]




        [SugarColumn(Length = 20, DecimalDigits = 6)]
        public decimal MCU_KZQMXDL  { get; set; }
        
		
        /// <summary>
        /// 提交时间
        /// </summary>
        
        [Display(Name = "提交时间")]




        [SplitField]


        public System.DateTime date  { get; set; }
        
		
    }
}
