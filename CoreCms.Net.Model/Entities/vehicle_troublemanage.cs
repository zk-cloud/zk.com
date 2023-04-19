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
    /// 车辆故障处置附表
    /// </summary>
    public partial class vehicle_troublemanage
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle_troublemanage()
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
        /// 关联报警故障表
        /// </summary>
        [Display(Name = "关联报警故障表")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 troubleid  { get; set; }
        
		
        /// <summary>
        /// 故障详情描述
        /// </summary>
        [Display(Name = "故障详情描述")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String troubledesc  { get; set; }
        
		
        /// <summary>
        /// 处置类型：1、忽略；2、远程处置；3、现场处置；4、回厂返修
        /// </summary>
        [Display(Name = "处置类型：1、忽略；2、远程处置；3、现场处置；4、回厂返修")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 managetype  { get; set; }
        
		
        /// <summary>
        /// 处置过程描述
        /// </summary>
        [Display(Name = "处置过程描述")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String managedesc  { get; set; }
        
		
        /// <summary>
        /// 处置时间
        /// </summary>
        [Display(Name = "处置时间")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.DateTime date  { get; set; }
        
		
        /// <summary>
        /// 处置人员ID
        /// </summary>
        [Display(Name = "处置人员ID")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String managerid  { get; set; }
        
		
    }
}
