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
    /// 车辆列表
    /// </summary>
    public partial class vehicle
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle()
        {
        }
		
        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = "编号")]
		
        [SugarColumn(IsPrimaryKey = true)]
        
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String id  { get; set; }
        
		
        /// <summary>
        /// 车辆VIN码
        /// </summary>
        [Display(Name = "车辆VIN码")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String VIN  { get; set; }
        
		
        /// <summary>
        /// 车辆密码
        /// </summary>
        [Display(Name = "车辆密码")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Pwd  { get; set; }
        
		
        /// <summary>
        /// 车牌号
        /// </summary>
        [Display(Name = "车牌号")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String code  { get; set; }
        
		
        /// <summary>
        /// 车辆类型
        /// </summary>
        [Display(Name = "车辆类型")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String vehicletypeId  { get; set; }
        
		
        /// <summary>
        /// 车辆批次
        /// </summary>
        [Display(Name = "车辆批次")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String vehicleBatch  { get; set; }
        
		
        /// <summary>
        /// 车辆当前版本
        /// </summary>
        [Display(Name = "车辆当前版本")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String vehicleVersion  { get; set; }
        
		
        /// <summary>
        /// 所属部门
        /// </summary>
        [Display(Name = "所属部门")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String organizationId  { get; set; }
        
		
        /// <summary>
        /// 最后一次上线时间
        /// </summary>
        [Display(Name = "最后一次上线时间")]
		
        
        
        
        
        public System.DateTime? lastUsingDate  { get; set; }
        
		
        /// <summary>
        /// 是否使用中
        /// </summary>
        [Display(Name = "是否使用中")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Boolean isUsing  { get; set; }
        
		
        /// <summary>
        /// 是否故障
        /// </summary>
        [Display(Name = "是否故障")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Boolean isError  { get; set; }
        
		
        /// <summary>
        /// 故障码
        /// </summary>
        [Display(Name = "故障码")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String ErrorCode  { get; set; }
        
		
        /// <summary>
        /// 交付日期
        /// </summary>
        [Display(Name = "交付日期")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.DateTime deliverydate  { get; set; }
        
		
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
		
        
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String desc  { get; set; }
        
		
        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String createBy  { get; set; }
        
		
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.DateTime createTime  { get; set; }
        
		
        /// <summary>
        /// 修改者
        /// </summary>
        [Display(Name = "修改者")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String updateBy  { get; set; }
        
		
        /// <summary>
        /// 修改时间
        /// </summary>
        [Display(Name = "修改时间")]
		
        
        
        
        
        public System.DateTime? updateTime  { get; set; }
        
		
    }
}
