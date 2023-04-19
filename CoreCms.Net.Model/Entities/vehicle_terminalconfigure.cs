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
    /// 
    /// </summary>
    public partial class vehicle_terminalconfigure
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle_terminalconfigure()
        {
        }
		
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
		
        [SugarColumn(IsPrimaryKey = true)]
        
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 id  { get; set; }
        
		
        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String MsgID  { get; set; }
        
		
        /// <summary>
        /// 属性名
        /// </summary>
        [Display(Name = "属性名")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String SignalName  { get; set; }
        
		
        /// <summary>
        /// 中文属性名
        /// </summary>
        [Display(Name = "中文属性名")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String SignalDescription  { get; set; }
        
		
        /// <summary>
        /// 开始bit字节
        /// </summary>
        [Display(Name = "开始bit字节")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 StartBit  { get; set; }
        
		
        /// <summary>
        /// bit字节长度
        /// </summary>
        [Display(Name = "bit字节长度")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 BitLength  { get; set; }
        
		
        /// <summary>
        /// 十进制乘积
        /// </summary>
        [Display(Name = "十进制乘积")]
		
        
        
        
        
        public System.Decimal? Resolution  { get; set; }
        
		
        /// <summary>
        /// 十进制偏移量
        /// </summary>
        [Display(Name = "十进制偏移量")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Decimal Offset  { get; set; }
        
		
        /// <summary>
        /// 最小值
        /// </summary>
        [Display(Name = "最小值")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Decimal MIN  { get; set; }
        
		
        /// <summary>
        /// 最大值
        /// </summary>
        [Display(Name = "最大值")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Decimal MAX  { get; set; }
        
		
        /// <summary>
        /// 单位
        /// </summary>
        [Display(Name = "单位")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Unit  { get; set; }
        
		
        /// <summary>
        /// 协议类型：1：LSB，2：MSB
        /// </summary>
        [Display(Name = "协议类型：1：LSB，2：MSB")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 Type  { get; set; }
        
		
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Desc  { get; set; }
        
		
    }
}
