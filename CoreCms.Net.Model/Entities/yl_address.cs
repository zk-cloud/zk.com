/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 2023/5/4 8:46:26
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
    public partial class yl_address
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public yl_address()
        {
        }
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "id")]
		
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 id  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "用户id")]
		
        
        
        
        
        public System.Int32? userid  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "地址")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String address  { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "经度")]



        public double lat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "纬度")]



        public double lng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "联系人")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String name  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "联系电话")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String phone  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "创建人")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String creator  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "创建时间")]
		
        
        
        
        
        public System.DateTime? createTime  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "修改人")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String modified  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "修改时间")]
		
        
        
        
        
        public System.DateTime? modifyTime  { get; set; }
        
		
    }
}
