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
    /// 车辆日志表
    /// </summary>
    public partial class vehicle_log
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle_log()
        {
        }
		
        /// <summary>
        /// 自增主键
        /// </summary>
        [Display(Name = "自增主键")]
		
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 vlid  { get; set; }
        
		
        /// <summary>
        /// 车辆VIN码
        /// </summary>
        [Display(Name = "车辆VIN码")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String VIN  { get; set; }
        
		
        /// <summary>
        /// 是否正常
        /// </summary>
        [Display(Name = "是否正常")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Boolean isNormal  { get; set; }
        
		
        /// <summary>
        /// 错误码
        /// </summary>
        [Display(Name = "错误码")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String ErrorCode  { get; set; }
        
		
        /// <summary>
        /// 错误信息
        /// </summary>
        [Display(Name = "错误信息")]
		
        
        
        
        
        public System.String ErrorMsg  { get; set; }
        
		
        /// <summary>
        /// 车辆动作：1 点火、2 熄火、3 上报故障、 4 ota升级开始、 5 ota升级结束、 6通信
        /// </summary>
        [Display(Name = "车辆动作：1 点火、2 熄火、3 上报故障、 4 ota升级开始、 5 ota升级结束、 6通信")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 Action  { get; set; }
        
		
        /// <summary>
        /// 通信报文
        /// </summary>
        [Display(Name = "通信报文")]
		
        
        
        
        
        public System.String Msg  { get; set; }
        
		
        /// <summary>
        /// 上报时间
        /// </summary>
        [Display(Name = "上报时间")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.DateTime date  { get; set; }
        
		
        /// <summary>
        /// 经度
        /// </summary>
        [Display(Name = "经度")]
		
        
        
        
        
        public System.Decimal? Longtitude  { get; set; }
        
		
        /// <summary>
        /// 纬度
        /// </summary>
        [Display(Name = "纬度")]
		
        
        
        
        
        public System.Decimal? Latitude  { get; set; }
        
		
        /// <summary>
        /// 坐标
        /// </summary>
        [Display(Name = "坐标")]
		
        
        
        [StringLength(maximumLength:100,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Point  { get; set; }
        
		
    }
}
