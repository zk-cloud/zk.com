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
    /// 车辆更新版本表
    /// </summary>
    public partial class vehicle_version
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle_version()
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
        /// 车辆类型ID
        /// </summary>
        [Display(Name = "车辆类型ID")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String vehicleTypeId  { get; set; }
        
		
        /// <summary>
        /// 车辆批次ID（‘，’分隔）
        /// </summary>
        [Display(Name = "车辆批次ID（‘，’分隔）")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String batchid  { get; set; }
        
		
        /// <summary>
        /// 版本号
        /// </summary>
        [Display(Name = "版本号")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Version  { get; set; }
        
		
        /// <summary>
        /// 是否是正式版
        /// </summary>
        [Display(Name = "是否是正式版")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Boolean isOfficial  { get; set; }
        
		
        /// <summary>
        /// 是否删除版本
        /// </summary>
        [Display(Name = "是否删除版本")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Boolean isDel  { get; set; }
        
		
        /// <summary>
        /// 文件下载地址
        /// </summary>
        [Display(Name = "文件下载地址")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String path  { get; set; }
        
		
        /// <summary>
        /// 固件类型集合（‘，’分隔）
        /// </summary>
        [Display(Name = "固件类型集合（‘，’分隔）")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String firmwares  { get; set; }
        
		
        /// <summary>
        /// 文件数量
        /// </summary>
        [Display(Name = "文件数量")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 fileNum  { get; set; }
        
		
        /// <summary>
        /// 文件大小（单位kb）
        /// </summary>
        [Display(Name = "文件大小（单位kb）")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        public double fileSize { get; set; }
        
		
        /// <summary>
        /// 更新日期
        /// </summary>
        [Display(Name = "更新日期")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.DateTime date  { get; set; }
        
		
        /// <summary>
        /// 更新内容
        /// </summary>
        [Display(Name = "更新内容")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.String desc  { get; set; }
        
		
    }
}
