using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CoreCms.Net.Model.TCPSocket
{
    public class Enum
    {
    }
    /// <summary>
    ///     枚举实体
    /// </summary>
    public class EnumEntity
    {
        /// <summary>
        ///     枚举的描述
        /// </summary>
        public string description { set; get; }

        /// <summary>
        ///     枚举名称
        /// </summary>
        public string title { set; get; }

        /// <summary>
        ///     枚举对象的值
        /// </summary>
        public int value { set; get; }
    }

    /// <summary>
    /// 终端控制器传输协议类别
    /// </summary>
    public enum TransmissionAgreement
    {
        /// <summary>
        /// mcu与vcu使用
        /// </summary>
        [Description("mcu与vcu使用")]
        LSB = 1,
        /// <summary>
        /// bms与dc使用
        /// </summary>
        [Description("bms与dc使用")]
        MSB = 2
    }
}
