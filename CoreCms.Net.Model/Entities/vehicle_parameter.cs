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
    /// 车辆信息上报报文表
    /// </summary>
    public partial class vehicle_parameter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle_parameter()
        {
        }
		
        /// <summary>
        /// 自增主键
        /// </summary>
        [Display(Name = "自增主键")]
		
        [SugarColumn(IsPrimaryKey = true)]
        
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 vpid  { get; set; }
        
		
        /// <summary>
        /// 车辆VIN码
        /// </summary>
        [Display(Name = "车辆VIN码")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String VIN  { get; set; }
        
		
        /// <summary>
        /// 键
        /// </summary>
        [Display(Name = "键")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String vahicle_key  { get; set; }
        
		
        /// <summary>
        /// 值
        /// </summary>
        [Display(Name = "值")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String vahicle_value  { get; set; }
        
		
        /// <summary>
        /// 上报时间
        /// </summary>
        [Display(Name = "上报时间")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.DateTime date  { get; set; }
        
		
        /// <summary>
        /// 车辆日志表通信类型id
        /// </summary>
        [Display(Name = "车辆日志表通信类型id")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 LogId  { get; set; }
        
		
    }
}
