/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 
 *        Description: 暂无
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreCms.Net.Auth.Policys;
using CoreCms.Net.Caching.AutoMate.RedisCache;
using CoreCms.Net.Configuration;
using CoreCms.Net.Core.Attribute;
using CoreCms.Net.IRepository;
using CoreCms.Net.IRepository.UnitOfWork;
using CoreCms.Net.IServices;
using CoreCms.Net.Model.Entities;
using CoreCms.Net.Model.FromDto;
using CoreCms.Net.Model.ViewModels.Basics;
using CoreCms.Net.Model.ViewModels.UI;
using CoreCms.Net.Utility;
using CoreCms.Net.Utility.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using SqlSugar;


namespace CoreCms.Net.Services
{
    /// <summary>
    /// 车辆列表 接口实现
    /// </summary>
    public class vehicleServices : BaseServices<vehicle>, IvehicleServices
    {
        private readonly PermissionRequirement _permissionRequirement;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRedisOperationRepository _redisOperationRepository;

        private readonly IvehicleRepository _dal;
        private readonly IUnitOfWork _unitOfWork;

        public vehicleServices(
            IUnitOfWork unitOfWork
            , IvehicleRepository dal
            , PermissionRequirement permissionRequirement,
            IHttpContextAccessor httpContextAccessor,
            IRedisOperationRepository redisOperationRepository
            )
        {
            this._dal = dal;
            base.BaseDal = dal;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _permissionRequirement = permissionRequirement;
            _redisOperationRepository = redisOperationRepository;
        }


        #region 实现重写增删改查操作==========================================================
        /// <summary>
        /// 重写异步查询方法
        /// </summary>
        /// <param name="objId">id</param>
        /// <returns></returns>
        public new async Task<vehicle> QueryByIdAsync(object objId, bool blUseNoLock = false)
        {
            return await _dal.QueryByIdAsync(objId);
        }

        /// <summary>
        /// 重写异步插入方法
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public new async Task<int> InsertAsync(vehicle entity)
        {
            InitEntity(entity);
            return await _dal.InsertAsync(entity);
        }

        /// <summary>
        /// 重写异步更新方法方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public new async Task<bool> UpdateAsync(vehicle entity)
        {
            InitUpdateEntity(entity);
            return await base.UpdateAsync(entity);
        }

        /// <summary>
        /// 重写异步更新方法方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public new async Task<bool> UpdateRangeAsync(List<vehicle> entity)
        {
            return await _dal.UpdateAsync(entity);
        }

        /// <summary>
        /// 重写删除指定ID的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new async Task<bool> DeleteByIdAsync(object id)
        {
            return await base.DeleteByIdAsync(id);
        }

        /// <summary>
        /// 重写删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public new async Task<bool> DeleteByIdsAsync(int[] ids)
        {
            return await _dal.DeleteByIdsAsync(ids);
        }

        public async Task<List<vehicle>> QueryListByClauseAsync(Expression<Func<vehicle, bool>> predicate, Expression<Func<vehicle, object>> orderByPredicate, OrderByType orderByType, bool blUseNoLock = false)
        {
            var db = SqlSugarHelper.Db;
            return blUseNoLock ?
                await db.Queryable<vehicle>().OrderByIF(orderByPredicate != null, orderByPredicate, orderByType)
                .WhereIF(predicate != null, predicate).With(SqlWith.NoLock).ToListAsync()
                : await db.Queryable<vehicle>().OrderByIF(orderByPredicate != null, orderByPredicate, orderByType)
                    .WhereIF(predicate != null, predicate).ToListAsync();
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
        public new async Task<IPageList<vehicle>> QueryPageAsync(Expression<Func<vehicle, bool>> predicate,
            Expression<Func<vehicle, object>> orderByExpression, OrderByType orderByType, int pageIndex = 1,
            int pageSize = 20, bool blUseNoLock = false)
        {
            return await _dal.QueryPageAsync(predicate, orderByExpression, orderByType, pageIndex, pageSize, blUseNoLock);
        }
        #endregion


        #region 车辆获取token
        [VehicleReport(VehicleReport.点火)]
        public async Task<WebApiCallBack> Login(FMVehicleLoginDto model)
        {
            var jm = new WebApiCallBack();

            if (string.IsNullOrEmpty(model.VIN) || string.IsNullOrEmpty(model.Pwd))
            {
                jm.msg = "VIN码或密码不能为空";
                return jm;
            }

            model.Pwd = CommonHelper.Md5For32(model.Pwd);

            var user = await _dal.QueryByClauseAsync(p => p.VIN == model.VIN && p.Pwd == model.Pwd);
            if (user != null)
            {
                if (!user.isUsing)
                {
                    jm.status = false;
                    jm.msg = "车辆已移除，如有问题请联系管理员！";
                    return jm;
                }

                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.GivenName, user.code),
                    new Claim(ClaimTypes.Name, user.VIN),
                        new Claim(JwtRegisteredClaimNames.Jti, user.id.ToString()),
                        new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_permissionRequirement.Expiration.TotalSeconds).ToString()),
                        new Claim(ClaimTypes.Role,"Vehicle"),
                        new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(60 * 60 * 24).ToString()),
                        new Claim("orgid",user.organizationId)



                };


                //用户标识
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);

                var token = JwtToken.BuildJwtToken(claims.ToArray(), _permissionRequirement);

                Newtonsoft.Json.Linq.JObject jobject = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(Newtonsoft.Json.JsonConvert.SerializeObject(token));
                var jwtData = new
                {
                    success = jobject["success"],
                    token = "Bearer " + jobject["token"],
                    expires_in = jobject["expires_in"],
                };
                jm.code = 0;
                jm.status = true;
                jm.msg = "认证成功";
                jm.data = jwtData;



                return jm;
            }
            else
            {
                jm.code = 401;
                jm.msg = "账户密码错误";
                return jm;
            }
        }

        [VehicleReport(VehicleReport.点火)]
        public WebApiCallBack SocketLogin(FMVehicleLoginDto model)
        {
            var jm = new WebApiCallBack();

            if (string.IsNullOrEmpty(model.VIN) || string.IsNullOrEmpty(model.Pwd))
            {
                jm.msg = "VIN码或密码不能为空";
                return jm;
            }

            model.Pwd = CommonHelper.Md5For32(model.Pwd);

            var user = _dal.QueryByClause(p => p.VIN == model.VIN && p.Pwd == model.Pwd);
            if (user != null)
            {
                if (!user.isUsing)
                {
                    jm.code = 401;
                    jm.status = false;
                    jm.msg = "车辆已移除，如有问题请联系管理员！";
                    return jm;
                }

                //用户标识

                var token = model.clientId;
                _redisOperationRepository.Set(token, user, TimeSpan.FromMinutes(60 * 24));
                var jwtData = new
                {
                    success = true,
                    token = token
                };
                jm.code = 0;
                jm.status = true;
                jm.msg = "认证成功";
                jm.data = jwtData;



                return jm;
            }
            else
            {
                jm.code = 401;
                jm.msg = "账户密码错误";
                return jm;
            }
        }

        #endregion

    }
}
