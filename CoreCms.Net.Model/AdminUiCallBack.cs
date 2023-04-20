using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCms.Net.Models
{
    /// <summary>
    ///     LayUIAdmin后端UI回调Json实体
    /// </summary>
    public partial class AdminUiCallBack
    {
        /// <summary>
        ///     状态码(ok = 0, error = 1, timeout = 401)
        /// </summary>
        public int code { get; set; } = 1;

        /// <summary>
        ///     可选。信息内容。
        /// </summary>
        public string msg { get; set; } = "空数据返回";

        public object data { get; set; }

        public object otherData { get; set; }

        public int count { get; set; }
    }

    public partial class AdminUiCallBack
    {
        public static AdminUiCallBack Success()
        {
            return Create(0, "ok", null);
        }
        public static AdminUiCallBack Success(string msg)
        {
            return Create(0, msg, null);
        }
        public static AdminUiCallBack Success(object data)
        {
            return Create(0, null, data);
        }
        public static AdminUiCallBack Success(string message, object data)
        {
            return Create(0, message, data);
        }
        public static AdminUiCallBack Failed(string message)
        {
            return Create(1, message);
        }
        public static AdminUiCallBack Failed(int code)
        {
            return Create(code, code.ToString());
        }
        public static AdminUiCallBack Failed(string message, object data)
        {
            return Create(1, message, data);
        }
        public static AdminUiCallBack Failed(int code, string message)
        {
            return Create(code, message);
        }
        public static AdminUiCallBack Failed(int code, string message, object data)
        {
            return Create(code, message, data);
        }
        public static AdminUiCallBack Create(object data)
        {
            return Create(0, null, data);
        }

        public static AdminUiCallBack Create(int code)
        {
            return Create(code, null, null);
        }

        public static AdminUiCallBack Create(int code, string message)
        {
            return Create(code, message, null);
        }

        public static AdminUiCallBack Create(int code, object data)
        {
            return Create(code, null, data);
        }
        public static AdminUiCallBack Create(string message, object data)
        {
            return Create(0, message, data, null);
        }
        public static AdminUiCallBack Create(int code, string message, object data, object otherData = null)
        {
            return new AdminUiCallBack
            {
                code = code,
                msg = string.IsNullOrEmpty(message) ? code.ToString() : message,
                data = data,
                otherData = otherData
            };
        }
    }

    public partial class WebApiCallBack
    {
        /// <summary>
        ///     请求接口返回说明
        /// </summary>
        public string methodDescription { get; set; }


        /// <summary>
        ///     提交数据
        /// </summary>
        public object otherData { get; set; } = null;

        /// <summary>
        ///     状态码
        /// </summary>
        public bool status { get; set; } = false;

        /// <summary>
        ///     信息说明。
        /// </summary>
        public string msg { get; set; } = "接口响应成功";

        /// <summary>
        ///     返回数据
        /// </summary>
        public object data { get; set; }

        /// <summary>
        ///     返回编码
        /// </summary>
        public int code { get; set; } = 0;
    }

    public partial class WebApiCallBack
    {
        public static WebApiCallBack Success()
        {
            return Create(0, "接口响应成功", null);
        }
        public static WebApiCallBack Success(string msg)
        {
            return Create(0, msg, null);
        }
        public static WebApiCallBack Success(object data)
        {
            return Create(0, null, data);
        }
        public static WebApiCallBack Success(string message, object data)
        {
            return Create(0, message, data);
        }
        public static WebApiCallBack Failed(string message)
        {
            return Create(1, message);
        }
        public static WebApiCallBack Failed(int code)
        {
            return Create(code, code.ToString());
        }
        public static WebApiCallBack Failed(string message, object data)
        {
            return Create(1, message, data);
        }
        public static WebApiCallBack Failed(int code, string message)
        {
            return Create(code, message);
        }
        public static WebApiCallBack Failed(int code, string message, object data)
        {
            return Create(code, message, data);
        }
        public static WebApiCallBack Create(object data)
        {
            return Create(0, null, data);
        }

        public static WebApiCallBack Create(int code)
        {
            return Create(code, null, null);
        }

        public static WebApiCallBack Create(int code, string message)
        {
            return Create(code, message, null);
        }

        public static WebApiCallBack Create(int code, object data)
        {
            return Create(code, null, data);
        }
        public static WebApiCallBack Create(string message, object data)
        {
            return Create(0, message, data, null);
        }
        public static WebApiCallBack Create(int code, string message, object data, object otherData = null)
        {
            return new WebApiCallBack
            {
                code = code,
                msg = string.IsNullOrEmpty(message) ? code.ToString() : message,
                data = data,
                otherData = otherData,
                status = code == 0
            };
        }
    }

    /// <summary>
    ///     layUIAdmin左侧导航实体
    /// </summary>
    public class AdminUiMenu
    {
        /// <summary>
        ///     标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        ///     英文标识符
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        ///     跳转地址(如home/homepage1)
        /// </summary>
        public string jump { get; set; }

        /// <summary>
        ///     是否展开当前列表
        /// </summary>
        public bool spread { get; set; } = false;

        /// <summary>
        ///     子类(防止json循环使用object类型)
        /// </summary>
        public object list { get; set; }
    }

    public class responseJson {
        public bool success { get; set; }
        public string token { get;set; }
        public double expires_in { get; set; }
        public string token_type { get; set; }
    }

    public class ResponseBase { 
        public bool Success { get; set; }

        public int code { get; set; }
        public object data { get; set; }
        public string ErrorMsg { get; set; }
    }

    public class LoginDto { 
        public string User { get; set; }

        public string Pwd { get; set; }
    }



}
