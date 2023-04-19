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
    /// 车辆定位
    /// </summary>
    public partial class vehicle_gpslocation
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle_gpslocation()
        {
        }
		
        /// <summary>
        /// 自增主键
        /// </summary>
        [Display(Name = "自增主键")]
		
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        
        [Required(ErrorMessage = "请输入{0}")]
        
        
        
        public System.Int32 vgid  { get; set; }
        
		
        /// <summary>
        /// 车辆VIN
        /// </summary>
        [Display(Name = "车辆VIN")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String VIN  { get; set; }
        
		
        /// <summary>
        /// 角度
        /// </summary>
        [Display(Name = "角度")]
		
        
        
        
        
        public System.Decimal? Radius  { get; set; }
        
		
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
        
		
        /// <summary>
        /// 速度（km/h）
        /// </summary>
        [Display(Name = "速度（km/h）")]
		
        
        public double? Speed { get; set; }
        
        
        
		
        /// <summary>
        /// 上传时间
        /// </summary>
        [Display(Name = "上传时间")]
        
        
        public System.DateTime? GPSDate  { get; set; }
        
		
        /// <summary>
        /// 定位类型
        /// </summary>
        [Display(Name = "定位类型")]
		
        
        
        
        
        public System.Int32? LOCType  { get; set; }
        
		
        /// <summary>
        /// 城市代码
        /// </summary>
        [Display(Name = "城市代码")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String CityCode  { get; set; }
        
		
        /// <summary>
        /// 省
        /// </summary>
        [Display(Name = "省")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Province  { get; set; }
        
		
        /// <summary>
        /// 市
        /// </summary>
        [Display(Name = "市")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String City  { get; set; }
        
		
        /// <summary>
        /// 城区/乡、镇
        /// </summary>
        [Display(Name = "城区/乡、镇")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Town  { get; set; }
        
		
        /// <summary>
        /// 街道
        /// </summary>
        [Display(Name = "街道")]
		
        
        
        [StringLength(maximumLength:100,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Street  { get; set; }
        
		
        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
		
        
        
        [StringLength(maximumLength:100,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Address  { get; set; }
        
		
        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
		
        
        
        [StringLength(maximumLength:100,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String Remark  { get; set; }
        
		
    }
}
