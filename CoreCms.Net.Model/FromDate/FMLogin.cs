using CoreCms.Net.Model.ViewModels.ylqc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreProject.Net.Models.FromDate
{



    /// <summary>
    ///     用户登录验证实体
    /// </summary>
    public class FMLogin
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    /// <summary>
    /// 时间选择器
    /// </summary>
    public class FMDatetime : FMPage
    { 
        /// <summary>
        /// 编号
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime beginDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endDate { get; set; }

    }

    /// <summary>
    /// 数据库实体
    /// </summary>
    public class DbModel
    {
        public int dbid { get; set; }
        public string dbname { get; set; }
        public string tableName { get; set; }
        public string fileType { get; set; }
    }

    public class FMIntId
    {
        /// <summary>
        ///     序列
        /// </summary>
        [Display(Name = "序列")]
        [Required(ErrorMessage = "请输入要提交的序列参数")]
        public int id { get; set; }

        public object data { get; set; } = null;
    }

    public class FMIntIdByListIntData
    {
        public int id { get; set; }
        public List<int> data { get; set; } = null;
    }


    public class FMArrayIntIds
    {
        public int[] id { get; set; }
        public object data { get; set; } = null;
    }

    public class FMStringId
    {
        /// <summary>
        /// 主键 单据id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 可选扩展业务参数，非必须
        /// </summary>
        public object data { get; set; } = null;
    }

    public class FMArrayStringIds
    {
        public string[] id { get; set; }
        public object data { get; set; } = null;
    }

    public class FMArrayStringPage: FMPage
    {
        public List<VehicleTreeVeiw> str { get; set; }
    }


    /// <summary>
    /// 基础分页dto
    /// </summary>
    public class FMPage
    {
        /// <summary>
        ///     当前页码
        /// </summary>
        public int page { get; set; } = 1;

        /// <summary>
        ///     每页数据量
        /// </summary>
        public int limit { get; set; } = 10;

    }


    public class FMGuidId
    {
        public Guid id { get; set; }
        public object data { get; set; } = null;
    }


    public class FMArrayGuidIds
    {
        public Guid[] id { get; set; }
        public object data { get; set; } = null;
    }

    public class FMlongId
    {
        public long id { get; set; }
        public object data { get; set; } = null;
    }


    public class FMArraylongIds
    {
        public long[] id { get; set; }
        public object data { get; set; } = null;
    }

    public class FMSysDictionary
    {

        /// <summary>
        ///  字典编码code
        /// </summary>
        [Required(ErrorMessage = "请输入数据字典编码")]
        public string code { get; set; }

    }

    #region 通用更新实体============================================================

    /// <summary>
    ///     按照序列进行更新Bool类型数据
    /// </summary>
    public class FMUpdateBoolDataByIntId
    {
        /// <summary>
        ///     序列
        /// </summary>
        [Required(ErrorMessage = "请输入序列")]
        public string id { get; set; }

        /// <summary>
        ///     数据
        /// </summary>
        [Required(ErrorMessage = "请输入相应数据")]
        public bool data { get; set; }
    }

    /// <summary>
    ///     按照序列进行更新String类型数据
    /// </summary>
    public class FMUpdateStringDataByIntId
    {
        /// <summary>
        ///     序列
        /// </summary>
        [Required(ErrorMessage = "请输入序列")]
        public int id { get; set; }

        /// <summary>
        ///     数据
        /// </summary>
        [Required(ErrorMessage = "请输入相应数据")]
        public string data { get; set; }
    }

    /// <summary>
    ///     按照序列进行更新Int类型数据
    /// </summary>
    public class FMUpdateIntegerDataByIntId
    {
        /// <summary>
        ///     序列
        /// </summary>
        [Required(ErrorMessage = "请输入序列")]
        public int id { get; set; }

        /// <summary>
        ///     数据
        /// </summary>
        [Required(ErrorMessage = "请输入相应数据")]
        public int data { get; set; }
    }


    /// <summary>
    ///     按照序列进行更新Decimal类型数据
    /// </summary>
    public class FMUpdateDecimalDataByIntId
    {
        /// <summary>
        ///     序列
        /// </summary>
        [Required(ErrorMessage = "请输入序列")]
        public int id { get; set; }

        /// <summary>
        ///     数据
        /// </summary>
        [Required(ErrorMessage = "请输入相应数据")]
        public decimal data { get; set; }
    }


    /// <summary>
    ///     按照序列进行更新Decimal类型数据
    /// </summary>
    public class FMUpdateArrayIntDataByIntId
    {
        /// <summary>
        ///     序列
        /// </summary>
        [Required(ErrorMessage = "请输入序列")]
        public int id { get; set; }

        /// <summary>
        ///     数据
        /// </summary>
        [Required(ErrorMessage = "请输入相应数据")]
        public List<int> data { get; set; }
    }

    /// <summary>
    ///     按照序列进行更新string数组类型数据
    /// </summary>
    public class FMUpdateArrayStringDataByIntId
    {
        /// <summary>
        ///     序列
        /// </summary>
        [Required(ErrorMessage = "请输入序列")]
        public int id { get; set; }

        /// <summary>
        ///     数据
        /// </summary>
        [Required(ErrorMessage = "请输入相应数据")]
        public List<string> data { get; set; }
    }

    #endregion

}
