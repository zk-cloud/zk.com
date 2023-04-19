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
    /// 车辆故障处置表
    /// </summary>
    public partial class vehicle_troubleshooting
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle_troubleshooting()
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
        /// 故障等级
        /// </summary>
        [Display(Name = "故障等级")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 level  { get; set; }
        
		
        /// <summary>
        /// 故障类型； 1：可充电储能装置故障，2：驱动电机故障，3发动机故障
        /// </summary>
        [Display(Name = "故障类型； 1：可充电储能装置故障，2：驱动电机故障，3发动机故障")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 type  { get; set; }
        
		
        /// <summary>
        /// 故障代码
        /// </summary>
        [Display(Name = "故障代码")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String warningcode  { get; set; }
        
		
        /// <summary>
        /// 故障开始时间
        /// </summary>
        [Display(Name = "故障开始时间")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.DateTime begindate  { get; set; }
        
		
        /// <summary>
        /// 故障更新时间
        /// </summary>
        [Display(Name = "故障更新时间")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.DateTime update  { get; set; }
        
		
        /// <summary>
        /// 车辆故障发送定位
        /// </summary>
        [Display(Name = "车辆故障发送定位")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Decimal warninglat  { get; set; }
        
		
        /// <summary>
        /// 车辆故障发送定位
        /// </summary>
        [Display(Name = "车辆故障发送定位")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Decimal warninglng  { get; set; }
        
		
        /// <summary>
        /// 关联车辆车架号
        /// </summary>
        [Display(Name = "关联车辆车架号")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:255,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String vin  { get; set; }
        
		
        /// <summary>
        /// 故障已处理
        /// </summary>
        [Display(Name = "故障已处理")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Boolean state  { get; set; }
        
		
    }
}
