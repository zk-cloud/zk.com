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
    public partial class vehicle_loginlog
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle_loginlog()
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
        /// 当日登录流水号
        /// </summary>
        [Display(Name = "当日登录流水号")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 Serialnumber  { get; set; }
        
		
        /// <summary>
        /// 车架号
        /// </summary>
        [Display(Name = "车架号")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String VIN  { get; set; }
        
		
        /// <summary>
        /// SIM卡号
        /// </summary>
        [Display(Name = "SIM卡号")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String ICCID  { get; set; }
        
		
        /// <summary>
        /// 可充电储能子系统数
        /// </summary>
        [Display(Name = "可充电储能子系统数")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 KcdNum  { get; set; }
        
		
        /// <summary>
        /// 可充电编码长度
        /// </summary>
        [Display(Name = "可充电编码长度")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String KcdList  { get; set; }
        
		
        /// <summary>
        /// 登录时间
        /// </summary>
        [Display(Name = "登录时间")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.DateTime logindate  { get; set; }
        
		
        /// <summary>
        /// 登出时间
        /// </summary>
        [Display(Name = "登出时间")]
		
        
        
        
        
        public System.DateTime? loginoutdate  { get; set; }
        
		
    }
}
