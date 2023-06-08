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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreCms.Net.Configuration;
using CoreCms.Net.Model.Entities;
using CoreCms.Net.Model.Entities.Expression;
using CoreCms.Net.Model.FromBody;
using CoreCms.Net.Filter;
using CoreCms.Net.Loging;
using CoreCms.Net.IServices;
using CoreCms.Net.Utility.Helper;
using CoreCms.Net.Utility.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using SqlSugar;
using CoreCms.Net.Auth.Policys;
using CoreCms.Net.Caching.AccressToken;
using CoreCms.Net.Services;
using CoreCms.Net.WeChat.Service.Enums;
using CoreCms.Net.WeChat.Service.Options;
using CoreCms.Net.WeChat.Service.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SKIT.FlurlHttpClient.Wechat.Api.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading;
using CoreCms.Net.WeChat.Service.HttpClients;
using NLog;
using Nito.AsyncEx;
using Microsoft.Extensions.Options;
using SKIT.FlurlHttpClient.Wechat.Api;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using CoreCms.Net.Model.ViewModels.UI;
using CoreCms.Net.WeChat.Service.Models;
using CoreCms.Net.Utility;
using NLog.Layouts;
using Newtonsoft.Json;
using AutoMapper;
using CoreCms.Net.Model.ViewModels.DTO;
using CoreCms.Net.Auth.HttpContextUser;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace CoreCms.Net.Web.WebApi.Controllers
{
    /// <summary>
    /// 
    ///</summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class yl_userController : ControllerBase
    {
        private readonly IHttpContextUser _user;
        private readonly Iyl_userServices _yl_userServices;
        private readonly ICoreCmsUserWeChatInfoServices _userWeChatInfoServices;
        private readonly PermissionRequirement _permissionRequirement;
        private readonly ICoreCmsBillPaymentsServices _billPaymentsServices;
        private readonly IWeChatApiHttpClientFactory _weChatApiHttpClientFactory;
        private readonly WeChatOptions _weChatOptions;

        private readonly AsyncLock _mutex = new AsyncLock();

        /// <summary>
        /// 构造函数
        ///</summary>
        public yl_userController(IHttpContextUser user
            , Iyl_userServices yl_userServices
            , ICoreCmsUserWeChatInfoServices userWeChatInfoServices
            , PermissionRequirement permissionRequirement
            , IWeChatApiHttpClientFactory weChatApiHttpClientFactory
            , IOptions<WeChatOptions> weChatOptions
            , ICoreCmsBillPaymentsServices billPaymentsServices
            )
        {
            _user = user;
            _yl_userServices = yl_userServices;
            _userWeChatInfoServices = userWeChatInfoServices;
            _permissionRequirement = permissionRequirement;
            _billPaymentsServices = billPaymentsServices;
            _weChatApiHttpClientFactory = weChatApiHttpClientFactory;
            _weChatOptions = weChatOptions.Value;
        }

        #region wx.login登陆成功之后发送的请求=========================================================

        /// <summary>
        /// wx.login登陆成功之后发送的请求
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiCallBack> OnLogin([FromBody] FMWxPost entity)
        {
            var jm = new WebApiCallBack();

            var client = _weChatApiHttpClientFactory.CreateWxOpenClient();
            var accessToken = WeChatCacheAccessTokenHelper.GetWxOpenAccessToken();
            var request = new SnsJsCode2SessionRequest();
            request.JsCode = entity.code;
            request.AccessToken = accessToken;

            var response = await client.ExecuteSnsJsCode2SessionAsync(request, HttpContext.RequestAborted);
            if (response.ErrorCode == (int)WeChatReturnCode.ReturnCode.请求成功)
            {
                using (await _mutex.LockAsync())
                {
                    var userInfo = await _userWeChatInfoServices.QueryByClauseAsync(p => p.openid == response.OpenId);
                    if (userInfo == null)
                    {
                        userInfo = new CoreCmsUserWeChatInfo();
                        userInfo.openid = response.OpenId;
                        userInfo.type = (int)GlobalEnumVars.UserAccountTypes.微信小程序;
                        userInfo.sessionKey = response.SessionKey;
                        userInfo.gender = 1;
                        userInfo.createTime = DateTime.Now;

                        await _userWeChatInfoServices.InsertAsync(userInfo);
                    }
                    else
                    {
                        if (userInfo.sessionKey != response.SessionKey)
                        {
                            await _userWeChatInfoServices.UpdateAsync(
                                p => new CoreCmsUserWeChatInfo() { sessionKey = response.SessionKey, updateTime = DateTime.Now },
                                p => p.openid == userInfo.openid);
                        }
                    }

                    if (userInfo is { userId: > 0 })
                    {
                        var user = await _yl_userServices.QueryByClauseAsync(p => p.id == userInfo.userId);
                        if (user != null)
                        {
                            var claims = new List<Claim> {
                                new Claim(ClaimTypes.Name, user.openid),
                                new Claim(JwtRegisteredClaimNames.Jti, user.id.ToString()),
                                new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_permissionRequirement.Expiration.TotalSeconds).ToString(CultureInfo.InvariantCulture)) };

                            //用户标识
                            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                            identity.AddClaims(claims);
                            jm.status = true;
                            jm.data = new
                            {
                                auth = JwtToken.BuildJwtToken(claims.ToArray(), _permissionRequirement),
                                user
                            };
                            jm.otherData = response.OpenId;

                            return jm;
                        }
                    }
                }

                //注意：生产环境下SessionKey属于敏感信息，不能进行传输！
                //return new JsonResult(new { success = true, msg = "OK", sessionAuthId = sessionBag.Key, sessionKey = sessionBag.SessionKey, data = jsonResult, sessionBag = sessionBag });
                jm.status = true;
                jm.data = response.OpenId;
                jm.otherData = response.OpenId;
                //jm.methodDescription = JsonConvert.SerializeObject(sessionBag);
                jm.msg = "OK";
            }
            else
            {
                jm.msg = response.ErrorMessage;
            }

            return jm;
        }
        #endregion

        #region 微信核验数据并获取用户详细资料=====================================================
        /// <summary>
        /// 核验数据并获取用户详细资料
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiCallBack> DecodeEncryptedData([FromBody] FMWxLoginDecodeEncryptedData entity)
        {
            var jm = new WebApiCallBack();

            var userInfo = await _userWeChatInfoServices.QueryByClauseAsync(p => p.openid == entity.sessionAuthId);
            if (userInfo == null)
            {
                jm.status = false;
                jm.msg = "用户信息获取失败";
                return jm;
            }
            var decodedEntity = EncryptHelper.DecodeUserInfoBySessionId(userInfo.sessionKey, entity.encryptedData, entity.iv);
            var token = string.Empty;
            var userWxId = entity.sessionAuthId;
            //检验水印
            if (decodedEntity != null)
            {
                var checkWatermark = decodedEntity.CheckWatermark(_weChatOptions.WxOpenAppId);
                jm.status = checkWatermark;

                //保存用户信息（可选）
                if (checkWatermark && decodedEntity is { } decodedUserInfo)
                {
                    //更新数据库讯息
                    userInfo.gender = decodedUserInfo.gender;
                    userInfo.city = decodedUserInfo.city;
                    userInfo.avatar = decodedUserInfo.avatarUrl;
                    userInfo.country = decodedUserInfo.country;
                    userInfo.nickName = decodedUserInfo.nickName;
                    userInfo.province = decodedUserInfo.province;
                    userInfo.unionId = decodedUserInfo.unionId;
                    userInfo.updateTime = DateTime.Now;

                    await _userWeChatInfoServices.UpdateAsync(userInfo);

                    if (userInfo.userId > 0)
                    {
                        var user = await _yl_userServices.QueryByClauseAsync(p => p.id == userInfo.userId);
                        if (user != null)
                        {
                            var claims = new List<Claim> {
                                new Claim(ClaimTypes.Name, user.realName),
                                new Claim(JwtRegisteredClaimNames.Jti, user.id.ToString()),
                                new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_permissionRequirement.Expiration.TotalSeconds).ToString(CultureInfo.InvariantCulture)) };

                            //用户标识
                            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                            identity.AddClaims(claims);
                            jm.status = true;
                            jm.data = JwtToken.BuildJwtToken(claims.ToArray(), _permissionRequirement);

                            //更新手机号码标识
                            if (!string.IsNullOrEmpty(userInfo.mobile))
                            {
                                await _userWeChatInfoServices.UpdateAsync(p => new CoreCmsUserWeChatInfo() { mobile = user.phone }, p => p.openid == userInfo.openid);
                            }

                            return jm;
                        }
                    }
                }
            }
            jm.data = new
            {
                token,
                sessionAuthId = userWxId
            };
            return jm;
        }
        #endregion

        #region 微信小程序授权拉取手机号码

        /// <summary>
        /// 微信小程序授权拉取手机号码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebApiCallBack> DecryptPhoneNumber([FromBody] FMWxLoginDecryptPhoneNumber entity)
        {
            var jm = new WebApiCallBack();

            var userInfo = await _userWeChatInfoServices.QueryByClauseAsync(p => p.openid == entity.sessionAuthId);
            if (userInfo == null)
            {
                jm.status = false;
                jm.msg = "用户信息获取失败";
                return jm;
            }
            DecodedPhoneNumber phoneNumber;
            try
            {
                phoneNumber = EncryptHelper.DecryptPhoneNumber(userInfo.sessionKey, entity.encryptedData, entity.iv);
            }
            catch (Exception ex)
            {
                jm.status = false;
                jm.code = 500;
                NLogUtil.WriteAll(LogLevel.Error, LogType.Web, "小程序接口", "微信小程序授权拉取手机号码", ex);
                return jm;
            }

            var data = new FMWxAccountCreate
            {
                mobile = phoneNumber.phoneNumber,
                invitecode = entity.invitecode,
                sessionAuthId = entity.sessionAuthId
            };

            jm = await _yl_userServices.SmsLogin(data);

            return jm;
        }


        #endregion

        #region 注销登录
        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public WebApiCallBack LogOut()
        {
            var jm = new WebApiCallBack
            {
                status = true,
                data = new
                {
                    token = "", //直接前端删除token-无为而治
                }
            };

            return jm;
        }
        #endregion

        #region 编辑用户
        /// <summary>
        /// 编辑用户资料
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<WebApiCallBack> Edit([FromBody]yl_user user)
        {
            var jm = new WebApiCallBack();

            var result = await _yl_userServices.UpdateAsync(user);
            if (result)
            {
                jm.msg = "编辑修改成功";
                jm.code = 0;
                jm.status = true;
            }
            else
            {
                jm.msg = "编辑修改失败";
                jm.code = 500;
                jm.status = false;
            }
            return jm;
        }
        #endregion

        #region 用户认证
        /// <summary>
        /// 用户认证
        /// </summary>
        /// <param name="company">公司名称</param>
        /// <param name="address">公司地址</param>
        /// <param name="realname">真实姓名</param>
        /// <param name="phone">电话号码</param>
        /// <param name="IDcard">身份证号</param>
        /// <param name="openid">微信openid</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<WebApiCallBack> EditUser(string company, string address, string realname, string phone, string IDcard, string openid)
        {
            var jm = new WebApiCallBack();

            var result = await _yl_userServices.UpdateAsync(p => new yl_user(){ 
                company = company,
                address = address,
                realName = realname,
                phone = phone,
                idCrad = IDcard
            } ,p => p.openid == openid);
            if (result)
            {
                jm.msg = "编辑修改成功";
                jm.code = 0;
                jm.status = true;
            }
            else
            {
                jm.msg = "编辑修改失败";
                jm.code = 500;
                jm.status = false;
            }
            return jm;
        }
        #endregion

        #region 支付

        /// <summary>
        /// 支付
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<WebApiCallBack> Pay([FromBody] PayPost entity)
        {
            var jm = new WebApiCallBack();

            if (string.IsNullOrEmpty(entity.ids))
            {
                jm.code = 13100;
                jm.msg = GlobalErrorCodeVars.Code13100;
            }
            else if (string.IsNullOrEmpty(entity.payment_code))
            {
                jm.code = 10055;
                jm.msg = GlobalErrorCodeVars.Code10055;
            }
            //生成支付单,并发起支付
            jm = await _billPaymentsServices.Pay(entity.ids, entity.payment_code, _user.ID, (int)GlobalEnumVars.BillPaymentsType.ServiceOrder,
                entity.@params);

            return jm;
        }

        #endregion
    }
}
