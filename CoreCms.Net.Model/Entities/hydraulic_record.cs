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
    /// 液压触发记录表
    /// </summary>
    public partial class hydraulic_record
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public hydraulic_record()
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
        /// 设备编码
        /// </summary>
        [Display(Name = "设备编码")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String code  { get; set; }
        
		
        /// <summary>
        /// 油温
        /// </summary>
        [Display(Name = "油温")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Decimal oil_temperature  { get; set; }
        
		
        /// <summary>
        /// 油压
        /// </summary>
        [Display(Name = "油压")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Decimal oil_pressure  { get; set; }
        
		
        /// <summary>
        /// 设备状态
        /// </summary>
        [Display(Name = "设备状态")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 state  { get; set; }
        
		
        /// <summary>
        /// 定位经度
        /// </summary>
        [Display(Name = "定位经度")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Decimal lat  { get; set; }
        
		
        /// <summary>
        /// 定位维度
        /// </summary>
        [Display(Name = "定位维度")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Decimal lng  { get; set; }
        
		
        /// <summary>
        /// 充电状态
        /// </summary>
        [Display(Name = "充电状态")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 charge_state  { get; set; }
        
		
        /// <summary>
        /// 风扇状态
        /// </summary>
        [Display(Name = "风扇状态")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 fan_state  { get; set; }
        
		
        /// <summary>
        /// 电池状态
        /// </summary>
        [Display(Name = "电池状态")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 battery_state  { get; set; }
        
		
        /// <summary>
        /// 液压状态
        /// </summary>
        [Display(Name = "液压状态")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 hydraulic_state  { get; set; }
        
		
        /// <summary>
        /// 高压状态
        /// </summary>
        [Display(Name = "高压状态")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 highpressure_state  { get; set; }
        
		
        /// <summary>
        /// 推头状态
        /// </summary>
        [Display(Name = "推头状态")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 push_state  { get; set; }
        
		
        /// <summary>
        /// 推头开始时间
        /// </summary>
        [Display(Name = "推头开始时间")]
		
        
        
        
        
        public System.DateTime? push_bgdate  { get; set; }
        
		
        /// <summary>
        /// 推头开始油压
        /// </summary>
        [Display(Name = "推头开始油压")]
		
        
        
        
        
        public System.Decimal? push_bgpressure  { get; set; }
        
		
        /// <summary>
        /// 推头中间态时间
        /// </summary>
        [Display(Name = "推头中间态时间")]
		
        
        
        
        
        public System.DateTime? push_middate  { get; set; }
        
		
        /// <summary>
        /// 推头中间态油压
        /// </summary>
        [Display(Name = "推头中间态油压")]
		
        
        
        
        
        public System.Decimal? push_midpressure  { get; set; }
        
		
        /// <summary>
        /// 推头终止时间
        /// </summary>
        [Display(Name = "推头终止时间")]
		
        
        
        
        
        public System.DateTime? push_enddate  { get; set; }
        
		
        /// <summary>
        /// 推头终止油压
        /// </summary>
        [Display(Name = "推头终止油压")]
		
        
        
        
        
        public System.Decimal? push_endpressure  { get; set; }
        
		
        /// <summary>
        /// 推头回落时间
        /// </summary>
        [Display(Name = "推头回落时间")]
		
        
        
        
        
        public System.DateTime? push_redate  { get; set; }
        
		
        /// <summary>
        /// 推头回落油压
        /// </summary>
        [Display(Name = "推头回落油压")]
		
        
        
        
        
        public System.Decimal? push_repressure  { get; set; }
        
		
        /// <summary>
        /// 提交时间
        /// </summary>
        [Display(Name = "提交时间")]
		
        
        
        
        
        public System.DateTime? date  { get; set; }
        
		
    }
}
