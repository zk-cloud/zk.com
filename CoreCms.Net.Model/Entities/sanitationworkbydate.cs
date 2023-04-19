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
    /// 环卫工作安排表
    /// </summary>
    public partial class sanitationworkbydate
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public sanitationworkbydate()
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
        /// 三级对象ID
        /// </summary>
        [Display(Name = "三级对象ID")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String threeSanitationId  { get; set; }
        
		
        /// <summary>
        /// 日班次序列
        /// </summary>
        [Display(Name = "日班次序列")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 sequence  { get; set; }
        
		
        /// <summary>
        /// 清扫开始时间
        /// </summary>
        [Display(Name = "清扫开始时间")]
		
        
        
        
        
        public System.DateTime? begindate  { get; set; }
        
		
        /// <summary>
        /// 清扫结束时间
        /// </summary>
        [Display(Name = "清扫结束时间")]
		
        
        
        
        
        public System.DateTime? enddate  { get; set; }
        
		
        /// <summary>
        /// 清扫人员ID
        /// </summary>
        [Display(Name = "清扫人员ID")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String sanitationerId  { get; set; }
        
		
        /// <summary>
        /// 最新更新人员ID
        /// </summary>
        [Display(Name = "最新更新人员ID")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String updateBy  { get; set; }
        
		
        /// <summary>
        /// 最新变更日期
        /// </summary>
        [Display(Name = "最新变更日期")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.DateTime updateTime  { get; set; }
        
		
    }
}
