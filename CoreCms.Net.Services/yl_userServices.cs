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
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreCms.Net.Auth.Policys;
using CoreCms.Net.Configuration;
using CoreCms.Net.IRepository;
using CoreCms.Net.IRepository.UnitOfWork;
using CoreCms.Net.IServices;
using CoreCms.Net.Model.Entities;
using CoreCms.Net.Model.FromBody;
using CoreCms.Net.Model.ViewModels.Basics;
using CoreCms.Net.Model.ViewModels.UI;
using CoreCms.Net.Utility.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SqlSugar;


namespace CoreCms.Net.Services
{
    /// <summary>
    ///  接口实现
    /// </summary>
    public class yl_userServices : BaseServices<yl_user>, Iyl_userServices
    {
        private readonly Iyl_userRepository _dal;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICoreCmsSmsServices _smsServices;
        private readonly ICoreCmsUserWeChatInfoServices _userWeChatInfoServices;
        private readonly PermissionRequirement _permissionRequirement;


        public yl_userServices(
            IUnitOfWork unitOfWork
            , Iyl_userRepository dal
            , ICoreCmsSmsServices smsServices
            , PermissionRequirement permissionRequirement
            , ICoreCmsUserWeChatInfoServices userWeChatInfoServices)
        {
            this._dal = dal;
            base.BaseDal = dal;
            _unitOfWork = unitOfWork;
            _smsServices = smsServices;
            _permissionRequirement = permissionRequirement;
            _userWeChatInfoServices = userWeChatInfoServices;
        }


        #region 手机短信验证码登陆，同时兼有手机短信注册的功能，还有第三方账户绑定的功能

        /// <summary>
        /// 手机短信验证码登陆，同时兼有手机短信注册的功能，还有第三方账户绑定的功能
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <param name="loginType">登录方式(1普通,2短信,3微信小程序拉取手机号)</param>
        /// <param name="platform"></param>
        /// <returns></returns>
        public async Task<WebApiCallBack> SmsLogin(FMWxAccountCreate entity, int loginType = (int)GlobalEnumVars.LoginType.WeChatPhoneNumber, int platform = 1)
        {
            var jm = new WebApiCallBack();
            var userInfo = await _dal.QueryByClauseAsync(p => p.openid == entity.sessionAuthId);
            if (userInfo == null)
            {
                userInfo = new yl_user();
                userInfo.openid = entity.sessionAuthId;
                userInfo.avatar = entity.avatar;
                userInfo.phone = entity.mobile;
                userInfo.IsDelete = false;
                userInfo.IsRegister = true;
                userInfo.createTime = DateTime.Now;
                userInfo.creator = string.Empty;

                var userId = await _dal.InsertAsync(userInfo);
                if (userId == 0)
                {
                    jm.msg = GlobalErrorCodeVars.Code10000;
                    return jm;
                }
                userInfo = await _dal.QueryByIdAsync(userId);
            }
            else
            {
                //如果有这个账号的话，判断一下是不是传密码了，如果传密码了，就是注册，这里就有问题，因为已经注册过
                if (!string.IsNullOrEmpty(entity.password))
                {
                    jm.msg = GlobalErrorCodeVars.Code11019;
                    return jm;
                }
            }

            //判断是否是小程序里的微信登陆，如果是，就给他绑定微信账号
            if (!string.IsNullOrEmpty(entity.sessionAuthId))
                {
                    var updateAsync = await _userWeChatInfoServices.UpdateAsync(p => new CoreCmsUserWeChatInfo() { userId = userInfo.id }, p => p.openid == entity.sessionAuthId);
                    if (updateAsync)
                    {
                        //多个微信可能同时授权一个号码登录。
                        //如果已经存在微信用户(A)数据绑定了手机号码。
                        //使用新微信(B)登录，同时又授权此手机号码绑定。
                        //小程序内微信支付时候，因为登录的微信（B）与拉取手机号码绑定后获取到数据是（A）。
                        //会导致微信数据报错（）
                        await _userWeChatInfoServices.UpdateAsync(p => new CoreCmsUserWeChatInfo() { userId = 0 }, p => p.openid != entity.sessionAuthId && p.userId == userInfo.id);
                    }
                    //如果是别的未绑定微信用户进来，则反向直接关联。
                    var wxUserInfo = await _userWeChatInfoServices.QueryByClauseAsync(p => p.openid == entity.sessionAuthId);
                    if (wxUserInfo != null)
                    {
                        await _dal.UpdateAsync(p => new yl_user() { name = wxUserInfo.nickName }, p => p.id == userInfo.id);
                    }
                }

                if ((bool)userInfo.IsRegister)
                {
                    var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, userInfo.openid),
                        new Claim(JwtRegisteredClaimNames.Jti, userInfo.id.ToString()),
                        new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_permissionRequirement.Expiration.TotalSeconds).ToString()) };
                    //用户标识
                    var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                    identity.AddClaims(claims);
                    jm.status = true;
                    jm.msg = "注册成功";
                    jm.data = JwtToken.BuildJwtToken(claims.ToArray(), _permissionRequirement);
                }
                else
                {
                    jm.msg = GlobalErrorCodeVars.Code11022;
                    return jm;
                }
                return jm;
            
        }
        #endregion

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
        public new async Task<IPageList<yl_user>> QueryPageAsync(Expression<Func<yl_user, bool>> predicate,
            Expression<Func<yl_user, object>> orderByExpression, OrderByType orderByType, int pageIndex = 1,
            int pageSize = 20, bool blUseNoLock = false)
        {
            return await _dal.QueryPageAsync(predicate, orderByExpression, orderByType, pageIndex, pageSize, blUseNoLock);
        }
        #endregion

    }
}
