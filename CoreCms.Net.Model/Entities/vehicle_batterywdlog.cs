/***********************************************************************
 *            Project: CoreCms                             
 *         CreateTime: 2022/11/30 16:53:03
 *        Description: 暂无
 ***********************************************************************/

using SqlSugar;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreCms.Net.Model.Entities
{
    [SplitTable(SplitType.Month)]//按月分表 （自带分表支持 年、季、月、周、日）
    [SugarTable("vehicle_batterywdlog_{year}{month}{day}")]//3个变量必须要有，这么设计为了兼容开始按年，后面改成按月、按日
    /// <summary>
    /// 电池温度记录列表
    /// </summary>
    public partial class vehicle_batterywdlog
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public vehicle_batterywdlog()
        {
        }

        /// <summary>
        /// 自增主键
        /// </summary>

        [Display(Name = "自增主键")]


        [SugarColumn(IsPrimaryKey = true)]





        public long id { get; set; }


        /// <summary>
        /// 外部关联
        /// </summary>

        [Display(Name = "外部关联")]


        [Required(ErrorMessage = "请输入{0}")]

        [StringLength(maximumLength: 50, ErrorMessage = "{0}不能超过{1}字")]

        public string code { get; set; }


        /// <summary>
        /// 电池子系统号
        /// </summary>

        [Display(Name = "电池子系统号")]






        public int? BatteryCode { get; set; }


        /// <summary>
        /// 电池温度探针数
        /// </summary>

        [Display(Name = "电池温度探针数")]






        public int? BatteryWDTZNum { get; set; }


        /// <summary>
        /// 电池温度列表
        /// </summary>

        [Display(Name = "电池温度列表")]




        [SugarColumn(ColumnDataType = "longtext")]

        public string BatteryWDList { get; set; }


        /// <summary>
        /// 提交时间
        /// </summary>

        [Display(Name = "提交时间")]


        [SplitField]




        public System.DateTime date { get; set; }


    }
}
