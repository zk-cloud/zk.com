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
    /// 车辆报警处理措施表
    /// </summary>
    public partial class vehicle_warnmanage
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle_warnmanage()
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
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
		
        [Required(ErrorMessage = "请输入{0}")]
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String name  { get; set; }
        
		
        /// <summary>
        /// 车辆类型ID
        /// </summary>
        [Display(Name = "车辆类型ID")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String VehicleTypeID  { get; set; }
        
		
        /// <summary>
        /// 车辆型号名称
        /// </summary>
        [Display(Name = "车辆型号名称")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String VehicleTypeName  { get; set; }
        
		
        /// <summary>
        /// 突发事故处置措施
        /// </summary>
        [Display(Name = "突发事故处置措施")]
		
        
        
        
        
        public System.String TFSGCZCS  { get; set; }
        
		
        /// <summary>
        /// 三级报警处置措施
        /// </summary>
        [Display(Name = "三级报警处置措施")]
		
        
        
        
        
        public System.String SJBJ_Manage  { get; set; }
        
		
        /// <summary>
        /// 二级报警处置措施
        /// </summary>
        [Display(Name = "二级报警处置措施")]
		
        
        
        
        
        public System.String EJBJ_Manage  { get; set; }
        
		
        /// <summary>
        /// 一级报警处置措施
        /// </summary>
        [Display(Name = "一级报警处置措施")]
		
        
        
        
        
        public System.String YJBJ_Manage  { get; set; }
        
		
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
		
        
        
        
        
        public System.String Desc  { get; set; }
        
		
        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
		
        
        
        [StringLength(maximumLength:50,ErrorMessage = "{0}不能超过{1}字")]
        
        public System.String createBy  { get; set; }
        
		
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
		
        
        
        
        
        public System.DateTime? createTime  { get; set; }
        
		
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
