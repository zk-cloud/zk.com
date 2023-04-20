/***********************************************************************
 *            Project: CoreCms.Net                                     *
 *                Web: https://CoreCms.Net                             *
 *        ProjectName: 核心内容管理系统                                *
 *             Author: 大灰灰                                          *
 *              Email: JianWeie@163.com                                *
 *           Versions: 1.0                                             *
 *         CreateTime: 2020-02-01 17:48:52
 *          NameSpace: CoreCms.Net.Framework.Utility.Extensions
 *           FileName: ConvertExtensions
 *   ClassDescription:
 ***********************************************************************/


using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CoreCms.Net.Utility.Extensions
{
    /// <summary>
    /// 扩展数据转换
    /// </summary>
    public static class ObjectExtensions
    {
        private static BindingFlags _bindingFlags { get; }
            = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase;
        /// <summary>
        /// 数据转换为int类型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static int ObjectToInt(this object thisValue)
        {
            int result = 0;
            if (thisValue == null)
                return 0;
            return thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out result) ? result : result;
        }

        /// <summary>
        /// 数据转换为int类型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static int ObjectToInt(this object thisValue, int errorValue)
        {
            int result = 0;
            return thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out result) ? result : errorValue;
        }

        /// <summary>
        /// 数据转换为Double类型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static double ObjectToDouble(this object thisValue)
        {
            double result = 0.0;
            return thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out result) ? result : 0.0;
        }

        /// <summary>
        /// 数据转换为Double类型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static double ObjectToDouble(this object thisValue, double errorValue)
        {
            double result = 0.0;
            return thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out result) ? result : errorValue;
        }

        /// <summary>
        /// 数据转换为Float类型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static float ObjectToFloat(this object thisValue)
        {
            float result = 0;
            return thisValue != null && thisValue != DBNull.Value && float.TryParse(thisValue.ToString(), out result) ? result : 0;
        }

        /// <summary>
        /// 数据转换为Float类型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static float ObjectToFloat(this object thisValue, float errorValue)
        {
            float result = 0;
            return thisValue != null && thisValue != DBNull.Value && float.TryParse(thisValue.ToString(), out result) ? result : errorValue;
        }

        /// <summary>
        /// 数据转换为String类型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static string ObjectToString(this object thisValue)
        {
            return thisValue != null ? thisValue.ToString().Trim() : "";
        }

        /// <summary>
        /// 数据转换为String类型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static string ObjectToString(this object thisValue, string errorValue)
        {
            return thisValue != null ? thisValue.ToString().Trim() : errorValue;
        }

        /// <summary>
        /// 数据转换为Decimal类型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static Decimal ObjectToDecimal(this object thisValue)
        {
            Decimal result = new Decimal();
            return thisValue != null && thisValue != DBNull.Value && Decimal.TryParse(thisValue.ToString(), out result) ? result : Decimal.Zero;
        }

        /// <summary>
        /// 数据转换为Decimal类型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static Decimal ObjectToDecimal(this object thisValue, Decimal errorValue)
        {
            Decimal result = new Decimal();
            return thisValue != null && thisValue != DBNull.Value && Decimal.TryParse(thisValue.ToString(), out result) ? result : errorValue;
        }

        /// <summary>
        /// 数据转换为DateTime类型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static DateTime ObjectToDate(this object thisValue)
        {
            DateTime result = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out result))
                result = Convert.ToDateTime(thisValue);
            return result;
        }

        /// <summary>
        /// 数据转换为DateTime类型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static DateTime ObjectToDate(this object thisValue, DateTime errorValue)
        {
            DateTime result = DateTime.MinValue;
            return thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out result) ? result : errorValue;
        }

        /// <summary>
        /// 数据转换为bool类型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool ObjectToBool(this object thisValue)
        {
            bool result = false;
            return thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out result) ? result : result;
        }

        /// <summary>
        /// 是否拥有某属性
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public static bool ContainsProperty(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName, _bindingFlags) != null;
        }
        /// <summary>
        /// 获取属性类型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public static string GetPropertyType(this object obj, string propertyName)
        {
            var p = obj.GetType().GetProperty(propertyName, _bindingFlags);
            if (p != null)
            {
                return p.PropertyType.Name;
            }
            return null;
        }
        /// <summary>
        /// 获取某属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName, _bindingFlags).GetValue(obj);
        }

        /// <summary>
        /// 设置某属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            obj.GetType().GetProperty(propertyName, _bindingFlags).SetValue(obj, value);
        }

        /// <summary>
        /// 是否拥有某字段
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="fieldName">字段名</param>
        /// <returns></returns>
        public static bool ContainsField(this object obj, string fieldName)
        {
            return obj.GetType().GetField(fieldName, _bindingFlags) != null;
        }

        /// <summary>
        /// 获取某字段值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="fieldName">字段名</param>
        /// <returns></returns>
        public static object GetGetFieldValue(this object obj, string fieldName)
        {
            return obj.GetType().GetField(fieldName, _bindingFlags).GetValue(obj);
        }

        /// <summary>
        /// 设置某字段值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void SetFieldValue(this object obj, string fieldName, object value)
        {
            obj.GetType().GetField(fieldName, _bindingFlags).SetValue(obj, value);
        }


        /// <summary>
        /// 深复制
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static T DeepClone<T>(this T obj) where T : class
        {
            if (obj == null)
                return null;

            return obj.ToJson().ToObject<T>();
        }

        #region 空值判断

        public static bool IsNull<T>(this T obj) where T : class
        {
            return obj == null;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        #endregion


        #region 对象转换为字典,对象转换为数组

        /// <summary>
        /// 对象转换为字典
        /// </summary>
        /// <param name="obj">待转化的对象</param>
        /// <param name="isIgnoreNull">是否忽略NULL 这里我不需要转化NULL的值，正常使用可以不穿参数 默认全转换</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObjectToMap(this object obj, bool isIgnoreNull = false)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();

            Type t = obj.GetType(); // 获取对象对应的类， 对应的类型

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance); // 获取当前type公共属性

            foreach (PropertyInfo p in pi)
            {
                MethodInfo m = p.GetGetMethod();

                if (m != null && m.IsPublic)
                {
                    // 进行判NULL处理 
                    if (m.Invoke(obj, new object[] { }) != null || !isIgnoreNull)
                    {
                        map.Add(p.Name, m.Invoke(obj, new object[] { })); // 向字典添加元素
                    }
                }
            }
            return map;
        }


        #endregion


    }
}
