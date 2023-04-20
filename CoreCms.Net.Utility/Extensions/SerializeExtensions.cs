/***********************************************************************
 *            Project: CoreCms.Net                                     *
 *                Web: https://CoreCms.Net                             *
 *        ProjectName: 核心内容管理系统                                *
 *             Author: 大灰灰                                          *
 *              Email: JianWeie@163.com                                *
 *           Versions: 1.0                                             *
 *         CreateTime: 2020-02-02 1:03:21
 *          NameSpace: CoreCms.Net.Framework.Utility.Extensions
 *           FileName: SerializeExtensions
 *   ClassDescription: 
 ***********************************************************************/


using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CoreCms.Net.Utility.Extensions
{
    /// <summary>
    /// 字符串序列化类(用于redis数据传递保持编码格式统一)
    /// </summary>
    public static class SerializeExtensions
    {
        static SerializeExtensions()
        {
            JsonConvert.DefaultSettings = () => DefaultJsonSetting;
        }
        public static JsonSerializerSettings DefaultJsonSetting = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver(),
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DateFormatString = "yyyy-MM-dd HH:mm:ss.fff"
        };

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static byte[] Serialize(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);

            return Encoding.UTF8.GetBytes(jsonString);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEntity Deserialize<TEntity>(byte[] value)
        {
            if (value == null)
            {
                return default(TEntity);
            }
            var jsonString = Encoding.UTF8.GetString(value);
            return JsonConvert.DeserializeObject<TEntity>(jsonString);
        }

        /// <summary>
        /// 将对象序列化成Json字符串
        /// </summary>
        /// <param name="obj">需要序列化的对象</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, DefaultJsonSetting);
        }

        /// <summary>
        /// 将Json字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="jsonStr">Json字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        /// <summary>
        /// 将Json字符串反序列化为对象
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public static object ToObject(this string jsonStr, Type type)
        {
            return JsonConvert.DeserializeObject(jsonStr, type);
        }
    }
}
