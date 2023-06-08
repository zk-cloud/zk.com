/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 2023/5/4 8:46:53
 *        Description: 暂无
 ***********************************************************************/

using SqlSugar;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreCms.Net.Model.Entities
{
    /// <summary>
    /// 延龙物流用户
    /// </summary>
    public partial class yl_user
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public yl_user()
        {
        }
		
        /// <summary>
        /// id
        /// </summary>
        [Display(Name = "id")]
		
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 id  { get; set; }
        
		
        /// <summary>
        /// 昵称
        /// </summary>
        [Display(Name = "昵称")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String name  { get; set; }
        
		
        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name = "头像")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String avatar  { get; set; }
        
		
        /// <summary>
        /// 公司
        /// </summary>
        [Display(Name = "公司")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String company  { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        [Display(Name = "公司地址")]



        [StringLength(maximumLength: 255, ErrorMessage = "{0}不能超过{1}字")]

        public System.String address { get; set; }


        /// <summary>
        /// 类型（1.vip/2.个人/3.公司）
        /// </summary>
        [Display(Name = "类型（1.vip/2.个人/3.公司）")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String type  { get; set; }
        
		
        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String realName  { get; set; }
        
		
        /// <summary>
        /// 身份证
        /// </summary>
        [Display(Name = "")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String idCrad  { get; set; }
        
		
        /// <summary>
        /// 月结账号
        /// </summary>
        [Display(Name = "月结账号")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String settlementNum  { get; set; }


        /// <summary>
        /// 微信openid
        /// </summary>
        [Display(Name = "微信openid")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String openid  { get; set; }


        /// <summary>
        /// 微信unionid
        /// </summary>
        [Display(Name = "微信unionid")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String unionid  { get; set; }
        
		
        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String phone  { get; set; }
        
		
        /// <summary>
        /// 验证码
        /// </summary>
        [Display(Name = "验证码")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String code  { get; set; }
        
		
        /// <summary>
        /// 是否注册
        /// </summary>
        [Display(Name = "是否注册")]
		
        
        
        
        
        public System.Boolean? IsRegister  { get; set; }
        
		
        /// <summary>
        /// 是否删除
        /// </summary>
        [Display(Name = "是否删除")]
		
        
        
        
        
        public System.Boolean? IsDelete  { get; set; }
        
		
        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String creator  { get; set; }
        
		
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
		
        
        
        
        
        public System.DateTime? createTime  { get; set; }
        
		
        /// <summary>
        /// 修改人
        /// </summary>
        [Display(Name = "修改人")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String modified  { get; set; }
        
		
        /// <summary>
        /// 修改时间
        /// </summary>
        [Display(Name = "修改时间")]
		
        
        
        
        
        public System.DateTime? modifyTime  { get; set; }
        
		
    }
}
