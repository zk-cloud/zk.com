/***********************************************************************
 *            Project: CoreCms.Net                                     *
 *                Web: https://CoreCms.Net                             *
 *        ProjectName: 核心内容管理系统                                *
 *             Author: 大灰灰                                          *
 *              Email: JianWeie@163.com                                *
 *         CreateTime: 2020-03-15 20:42:29
 *        Description: 暂无
 ***********************************************************************/


using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CoreCms.Net.Filter
{
    public class ApiExplorerIgnores : IActionModelConvention
    {
        /// <summary>
        /// 自带的Controller与swagger3.0冲突，在此排除扫描
        /// </summary>
        /// <param name="action"></param>
        public void Apply(ActionModel action)
        {
            //冲突的Ocelot.Raft.RaftController
            if (action.Controller.ControllerName == ("WxOfficialOAuth") || action.Controller.ControllerName == ("WxOpenOAuth"))
                action.ApiExplorer.IsVisible = false;
            //Ocelot.Cache.OutputCacheController
            if (action.Controller.ControllerName == ("AliPay"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("WeChatPay"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("Advert"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("Agent"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("Article"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("Cart"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("Common"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("Coupon"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("Distribution"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("Form"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("Good"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("Group"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("Notice"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("Order"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("Page"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("Payments"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("PinTuan"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("Service"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("Store"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("User"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("WeChatAppletsMessage"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("yl_orders"))
                action.ApiExplorer.IsVisible = false;

            if (action.Controller.ControllerName == ("yl_address"))
                action.ApiExplorer.IsVisible = false;
        }
    }
}
