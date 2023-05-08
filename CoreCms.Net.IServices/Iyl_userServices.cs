/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 2023/5/4 8:46:53
 *        Description: 暂无
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreCms.Net.Configuration;
using CoreCms.Net.Model.Entities;
using CoreCms.Net.Model.FromBody;
using CoreCms.Net.Model.ViewModels.Basics;
using CoreCms.Net.Model.ViewModels.UI;
using SqlSugar;

namespace CoreCms.Net.IServices
{
	/// <summary>
    ///  服务工厂接口
    /// </summary>
    public interface Iyl_userServices : IBaseServices<yl_user>
    {

        /// <summary>
        ///     手机短信验证码登陆，同时兼有手机短信注册的功能，还有第三方账户绑定的功能
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <param name="loginType">登录方式(1普通,2短信,3微信小程序拉取手机号)</param>
        /// <param name="platform"></param>
        /// <returns></returns>
        Task<WebApiCallBack> SmsLogin(FMWxAccountCreate entity,
            int loginType = (int)GlobalEnumVars.LoginType.WeChatPhoneNumber, int platform = 1);

        #region 重写根据条件查询分页数据
        /// <summary>
        ///     重写根据条件查询分页数据
        /// </summary>
        /// <param name="predicate">判断集合</param>
        /// <param name="orderByType">排序方式</param>
        /// <param name="pageIndex">当前页面索引</param>
        /// <param name="pageSize">分布大小</param>
        /// <param name="orderByExpression"></param>
        /// <param name="blUseNoLock">是否使用WITH(NOLOCK)</param>
        /// <returns></returns>
        new Task<IPageList<yl_user>> QueryPageAsync(
            Expression<Func<yl_user, bool>> predicate,
            Expression<Func<yl_user, object>> orderByExpression, OrderByType orderByType, int pageIndex = 1,
            int pageSize = 20, bool blUseNoLock = false);
        #endregion
    }
}
