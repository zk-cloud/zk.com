/***********************************************************************
 *            Project: XNS.Net                                     *
 *                Web: https://XNS.Net                             *
 *        ProjectName: 核心内容管理系统                                *
 *             Author: 大灰灰                                          *
 *              Email: JianWeie@163.com                                *
 *         CreateTime: 2020-03-21 22:32:23
 *        Description: 暂无
 ***********************************************************************/


using SqlSugar;
using System.Collections.Generic;
using System.Linq;


namespace CoreCms.Net.Utility.Helper
{
    public static class IdHelper
    {

        public static long WorkerId { get; }

        //
        // 摘要:
        //     获取String型雪花Id
        public static string GetId() { return SnowFlakeSingle.Instance.getID().ToString(); }
        public static string GetId(string prefix) { return prefix + SnowFlakeSingle.Instance.getID().ToString(); }
        //
        // 摘要:
        //     获取long型雪花Id
        public static long GetLongId() { return SnowFlakeSingle.instance.getID(); }
    }

}
