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
    /// 微信授权交互
    /// </summary>
    public partial class wechataccesstoken
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public wechataccesstoken()
        {
        }
		
        /// <summary>
        /// 序列
        /// </summary>
        [Display(Name = "序列")]
		
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 id  { get; set; }
        
		
        /// <summary>
        /// 类型1小程序2公众号
        /// </summary>
        [Display(Name = "类型1小程序2公众号")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 appType  { get; set; }
        
		
        /// <summary>
        /// 微信appId
        /// </summary>
        [Display(Name = "微信appId")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String appId  { get; set; }
        
		
        /// <summary>
        /// 微信accessToken
        /// </summary>
        [Display(Name = "微信accessToken")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String accessToken  { get; set; }
        
		
        /// <summary>
        /// 截止时间
        /// </summary>
        [Display(Name = "截止时间")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int64 expireTimestamp  { get; set; }
        
		
        /// <summary>
        /// 更新时间
        /// </summary>
        [Display(Name = "更新时间")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int64 updateTimestamp  { get; set; }
        
		
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int64 createTimestamp  { get; set; }
        
		
    }
}
