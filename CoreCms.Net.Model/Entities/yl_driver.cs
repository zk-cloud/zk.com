/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 2023/5/4 8:46:37
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
    public partial class yl_driver
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public yl_driver()
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
        [Display(Name = "姓名")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String name  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "头像")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String avatar  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "车型")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String carType  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "车牌")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String licensePlate  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "真实姓名")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String realName  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "身份证")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String idCard  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "驾照")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String licence  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "微信openid")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String openid  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "微信unionid")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String unionid  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "手机号")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String phone  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "验证码")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String code  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "是否注册")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public bool IsRegister  { get; set; }
        
		
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "是否删除")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public bool IsDelete  { get; set; }
        
		
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
