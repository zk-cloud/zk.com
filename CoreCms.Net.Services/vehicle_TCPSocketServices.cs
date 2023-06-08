/***********************************************************************
 *            Project: CoreCms
 *        ProjectName: 核心内容管理系统                                
 *                Web: https://www.corecms.net                      
 *             Author: 大灰灰                                          
 *              Email: jianweie@163.com                                
 *         CreateTime: 
 *        Description: 暂无
 ***********************************************************************/

using AutoMapper;
using CoreCms.Net.Caching.AutoMate.RedisCache;
using CoreCms.Net.Configuration;
using CoreCms.Net.Core.Attribute;
using CoreCms.Net.IRepository;
using CoreCms.Net.IRepository.UnitOfWork;
using CoreCms.Net.IServices;
using CoreCms.Net.Model.Entities;
using CoreCms.Net.Model.FromDate;
using CoreCms.Net.Model.FromDto;
using CoreCms.Net.Model.TCPSocket;
using CoreCms.Net.Model.ViewModels.Basics;
using CoreCms.Net.Model.ViewModels.UI;
using CoreCms.Net.Utility;
using CoreCms.Net.Utility.Extensions;
using CoreCms.Net.Utility.Helper;
using CoreCms.Net.Utility.Hub;
using CoreCms.Net.Utility.YLQCHelper;
using DotNetty.Buffers;
using DotNetty.Common.Utilities;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCms.Net.Services
{
    /// <summary>
    /// 车辆日志表 接口实现
    /// </summary>
    public class vehicle_TCPSocketServices : BaseServices<vehicle_log>, Ivehicle_TCPSocketServices
    {
        private readonly Ivehicle_TCPSocketRepository _dal;
        private readonly IvehicleRepository _ivehicleRepository;
        private readonly Ivehicle_loginlogRepository _ivehicle_loginlogRepository;
        private readonly Ivehicle_logRepository _ivehicle_logRepository;
        private readonly ISysOrganizationRepository _isysOrganizationRepository;
        private readonly Ivehicle_terminalconfigureRepository _ivehicle_terminalconfigureRepository;
        private readonly Ivehicle_parameterlistRepository _ivehicle_parameterlistRepository;
        private readonly Ivehicle_gbt32960Repository _ivehicle_gbt32960Repository;
        private readonly Ivehicle_mculogRepository _ivehicle_mculogRepository;
        private readonly Ivehicle_batterydylogRepository _ivehicle_batterydylogRepository;
        private readonly Ivehicle_batterywdlogRepository _ivehicle_batterywdlogRepository;
        private readonly Ivehicle_troubleshootingRepository _ivehicle_troubleshootingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;
        private readonly IRedisOperationRepository _cache;
        private readonly IHubContext<ChatHub> _ChatHub;
        private static readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        static object myLock = new object();

        private readonly ISqlSugarClient _db;

        public vehicle_TCPSocketServices(
            IUnitOfWork unitOfWork
            , Ivehicle_TCPSocketRepository dal
            , IMapper Mapper
            , IHubContext<ChatHub> ChatHub
            , IRedisOperationRepository cache
            , IvehicleRepository ivehicleRepository
            , ISysOrganizationRepository isysOrganizationRepository
            , Ivehicle_loginlogRepository ivehicle_loginlogRepository
            , Ivehicle_logRepository ivehicle_logRepository
            , Ivehicle_terminalconfigureRepository ivehicle_terminalconfigureRepository
            , Ivehicle_parameterlistRepository ivehicle_parameterlistRepository
            , Ivehicle_gbt32960Repository ivehicle_gbt32960Repository
            , Ivehicle_mculogRepository ivehicle_mculogRepository
            , Ivehicle_batterydylogRepository ivehicle_batterydylogRepository
            , Ivehicle_batterywdlogRepository ivehicle_batterywdlogRepository
            , Ivehicle_troubleshootingRepository ivehicle_troubleshootingRepository
            , ISqlSugarClient context = null)
        {
            _Mapper = Mapper;
            _ChatHub = ChatHub;
            _cache = cache;
            this._dal = dal;
            base.BaseDal = dal;
            _unitOfWork = unitOfWork;
            _ivehicleRepository = ivehicleRepository;
            _isysOrganizationRepository = isysOrganizationRepository;
            _ivehicle_loginlogRepository = ivehicle_loginlogRepository;
            _ivehicle_logRepository = ivehicle_logRepository;
            _ivehicle_terminalconfigureRepository = ivehicle_terminalconfigureRepository;
            _ivehicle_parameterlistRepository = ivehicle_parameterlistRepository;
            _ivehicle_gbt32960Repository = ivehicle_gbt32960Repository;
            _ivehicle_mculogRepository = ivehicle_mculogRepository;
            _ivehicle_batterydylogRepository = ivehicle_batterydylogRepository;
            _ivehicle_batterywdlogRepository = ivehicle_batterywdlogRepository;
            _ivehicle_troubleshootingRepository = ivehicle_troubleshootingRepository;
        }

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
        public new async Task<IPageList<vehicle_log>> QueryPageAsync(Expression<Func<vehicle_log, bool>> predicate,
            Expression<Func<vehicle_log, object>> orderByExpression, OrderByType orderByType, int pageIndex = 1,
            int pageSize = 20, bool blUseNoLock = false)
        {
            return await _dal.QueryPageAsync(predicate, orderByExpression, orderByType, pageIndex, pageSize, blUseNoLock);
        }
        #endregion

        #region 车辆获取token

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

            var cehicle = _ivehicleRepository.QueryByClause(p => p.VIN == model.VIN && p.Pwd == model.Pwd);
            if (cehicle != null)
            {
                if (!cehicle.isUsing)
                {
                    jm.code = 401;
                    jm.status = false;
                    jm.msg = "车辆已移除，如有问题请联系管理员！";
                    return jm;
                }

                //用户标识

                var token = model.clientId;
                _cache.Set(token, cehicle, TimeSpan.FromMinutes(60 * 24));
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

        [VehicleReport(VehicleReport.点火)]
        public async Task<WebApiCallBack> SocketLoginAsync(string msgstr, List<vehicle_terminalconfigure> configstr, string vin, string clientid)
        {
            var jm = new WebApiCallBack();
            List<string> loginstr = YLQCHelper.TCPHexStrToDeString(msgstr, configstr);
            //if (await _cache.Exist("TCPLoginvehicle"))
            //{
            //    List<string> tcploginlist = (await _cache.GetSetAsync("TCPLoginvehicle")).Select(p => p.ToString()).ToList();
            //    if (tcploginlist.Contains(vin))
            //    {
            //        string lineid;
            //        if (TCPsocketClientCollection.IsOnline(vin, out lineid))
            //        {
            //            LogHelper.Debug("TCP服务器进行在线判断：在线！");
            //            jm.code = 403;
            //            jm.status = false;
            //            jm.msg = "车辆已登录，请勿重新登录！";
            //            return jm;
            //        }
            //        else
            //        {
            //            var cehicle1 = await base.Change<vehicle>().GetFirstAsync(p => p.VIN == vin);
            //            //获取所有组织上级
            //            List<string> orglist = new List<string>();
            //            var org = await base.Change<sysorganization>().GetFirstAsync(p => p.id == cehicle1.organizationId);
            //            orglist.AddRange(new List<string> { cehicle1.organizationId, "0" });
            //            do
            //            {
            //                org = await base.Change<sysorganization>().GetFirstAsync(p => p.id == org.parentId);
            //                orglist.Add(org.id);
            //            } while (org.parentId != "0");
            //            cehicle1.parentIdList = orglist;
            //            cehicle1.isUsing = true;
            //            cehicle1.isError = false;
            //            LogHelper.Debug("TCP服务器进行在线判断：不在线！");
            //            lock (myLock)
            //            {
            //                Task t = Task.WhenAll(_cache.Set(clientid, cehicle1, TimeSpan.FromMinutes(60 * 24)), base.Change<vehicle>().UpdateAsync(cehicle1), lineid != null ? _cache.Remove(lineid) : Task.CompletedTask);
            //                t.Wait();
            //            }
            //            //获取对应的连接
            //            var session1 = TCPsocketClientCollection.Getgroupone(clientid);
            //            //添加绑定
            //            TCPsocketClientCollection.AddChannelDic(vin, session1);

            //            // 双向绑定
            //            // channel -> userId
            //            AttributeKey<String> key = AttributeKey<String>.ValueOf("VIN");
            //            session1.GetAttribute(key).Set(vin);

            //            //signalR发送上线提醒
            //            foreach (string groupid in orglist)
            //            {
            //                _ChatHub.Clients.Group(groupid).SendAsync("ReceiveMessage", $"车辆{cehicle1.code}已上线！");
            //            }

            //            LogHelper.Debug("TCP服务器进行在线判断：发送指令！");
            //            //发送指令
            //            IByteBuffer resultbyte = Unpooled.CopiedBuffer(Encoding.Default.GetBytes("YLQC,OK;\r\n"));
            //            session1.WriteAsync(resultbyte);
            //            session1.WriteAsync(resultbyte);
            //            session1.WriteAndFlushAsync(resultbyte);
            //            return jm;

            //        }

            //    }
            //}


            var tcploginlist = TCPsocketClientCollection.GetOnlineVehicle();
            if (tcploginlist.Contains(vin))
            {
                string lineid;
                if (TCPsocketClientCollection.IsOnline(vin, out lineid))
                {
                    LogHelper.Debug("TCP服务器进行在线判断：在线！");
                    jm.code = 403;
                    jm.status = false;
                    jm.msg = "车辆已登录，请勿重新登录！";
                    return jm;
                }
                else
                {
                    var vehicle = new Model.Entities.vehicle();
                    var cehicle1 = await _ivehicleRepository.QueryByClauseAsync(p => p.VIN == vin);
                    //获取所有组织上级
                    List<string> orglist = new List<string>();
                    var org = await _isysOrganizationRepository.QueryByClauseAsync(p => p.id == cehicle1.organizationId);
                    orglist.AddRange(new List<string> { cehicle1.organizationId, "0" });
                    do
                    {
                        org = await _isysOrganizationRepository.QueryByClauseAsync(p => p.id == org.parentId);
                        orglist.Add(org.id);
                    } while (org.parentId != "0");
                    cehicle1.parentIdList = orglist;
                    cehicle1.isUsing = true;
                    cehicle1.isError = false;
                    LogHelper.Debug("TCP服务器进行在线判断：不在线！");
                    lock (myLock)
                    {
                        Task t = Task.WhenAll(_cache.Set(clientid, cehicle1, TimeSpan.FromMinutes(60 * 24)), _ivehicleRepository.UpdateAsync(cehicle1), lineid != null ? _cache.Remove(lineid) : Task.CompletedTask);
                        t.Wait();
                    }
                    //获取对应的连接
                    var session1 = TCPsocketClientCollection.Getgroupone(clientid);
                    //添加绑定
                    TCPsocketClientCollection.AddChannelDic(vin, session1);

                    // 双向绑定
                    // channel -> userId
                    AttributeKey<String> key = AttributeKey<String>.ValueOf("VIN");
                    session1.GetAttribute(key).Set(vin);

                    //signalR发送上线提醒
                    foreach (string groupid in orglist)
                    {
                        _ChatHub.Clients.Group(groupid).SendAsync("ReceiveMessage", $"车辆{cehicle1.code}已上线！");
                    }

                    LogHelper.Debug("TCP服务器进行在线判断：发送指令！");
                    //发送指令
                    IByteBuffer resultbyte = Unpooled.CopiedBuffer(Encoding.Default.GetBytes("YLQC,OK;\r\n"));
                    session1.WriteAsync(resultbyte);
                    session1.WriteAsync(resultbyte);
                    session1.WriteAndFlushAsync(resultbyte);
                    return jm;

                }

            }
            string kcdcnstr = msgstr.Substring(60);


            var cehicle = await _ivehicleRepository.QueryByClauseAsync(p => p.VIN == vin);


            if (cehicle != null)
            {
                //if (!cehicle.isUsing)
                //{
                //    jm.code = 401;
                //    jm.status = false;
                //    jm.msg = "车辆已移除，如有问题请联系管理员！";
                //    return jm;
                //}

                //获取所有组织上级
                List<string> orglist = new List<string>();
                var org = await _isysOrganizationRepository.QueryByClauseAsync(p => p.id == cehicle.organizationId);
                orglist.AddRange(new List<string> { cehicle.organizationId, "0" });
                do
                {
                    org = await _isysOrganizationRepository.QueryByClauseAsync(p => p.id == org.parentId);
                    orglist.Add(org.id);
                } while (org.parentId != "0");
                cehicle.parentIdList = orglist;

                //用户标识

                var token = clientid;
                lock (myLock)
                {
                    //Task t = Task.WhenAll(_cache.Set(token, cehicle, TimeSpan.FromMinutes(60 * 24)), _cache.AddSetAsync("TCPLoginvehicle", vin));
                    //t.Wait();
                    _cache.Set(token, cehicle, TimeSpan.FromMinutes(60 * 24));

                }

                var jwtData = new
                {
                    success = true,
                    token = token
                };
                jm.code = 0;
                jm.status = true;
                jm.msg = "认证成功";
                jm.data = jwtData;

                string infodate = "20";
                for (int j = 0; j < 6; j++)
                {
                    if (j < 2)
                    {
                        infodate += YLQCHelper.HexStringToDemicel(loginstr[0].Substring(j * 2, 2)).ToString().PadLeft(2, '0') + "-";
                    }
                    else if (j == 2)
                    {
                        infodate += YLQCHelper.HexStringToDemicel(loginstr[0].Substring(j * 2, 2)).ToString().PadLeft(2, '0') + " ";
                    }
                    else
                    {
                        infodate += YLQCHelper.HexStringToDemicel(loginstr[0].Substring(j * 2, 2)).ToString().PadLeft(2, '0') + ":";
                    }
                }
                //指定转换格式 
                System.IFormatProvider format = new System.Globalization.CultureInfo("zh-CN", true);
                string strDateFormat = "yyyy-MM-dd HH:mm:ss";
                DateTime date = DateTime.ParseExact(infodate.Substring(0, infodate.Length - 1), strDateFormat, format, DateTimeStyles.AllowWhiteSpaces);
                //获取储能系统组
                var KcdList = "";
                int kcdcodlenght = loginstr[4].Substring(0, loginstr[4].LastIndexOf('.')).ObjectToInt();
                while (kcdcodlenght > 0 && kcdcnstr.Length >= kcdcodlenght)
                {
                    KcdList += "," + kcdcnstr.Substring(0, kcdcodlenght);
                    kcdcnstr.Remove(0, kcdcodlenght);
                }
                vehicle_loginlog vll = new vehicle_loginlog
                {
                    logindate = date,
                    Serialnumber = int.Parse(loginstr[1].Substring(0, loginstr[1].IndexOf("."))),
                    ICCID = YLQCHelper.HexStringToASCII(loginstr[2]),
                    KcdNum = loginstr[3].Substring(0, loginstr[3].LastIndexOf('.')).ObjectToInt(),
                    KcdList = KcdList.IsNullOrWhiteSpace() ? "" : KcdList.Substring(1),
                    VIN = vin
                };
                //SqlSugarScope 强制 new一个SugarClient
                var db = SqlSugarHelper.Db.CopyNew(); //5.1.1版本支持(CopyNew)
                db.Insertable(vll).ExecuteReturnIdentity();
                //var session = TCPsocketClientCollection.Get(token);
                //获取对应的连接
                var session = TCPsocketClientCollection.Getgroupone(token);
                //添加绑定
                TCPsocketClientCollection.AddChannelDic(cehicle.VIN, session);

                // 双向绑定
                // channel -> userId
                AttributeKey<String> key = AttributeKey<String>.ValueOf("VIN");
                session.GetAttribute(key).Set(cehicle.VIN);

                //signalR发送上线提醒
                foreach (string groupid in orglist)
                {

                    //_ChatHub.Clients.Group("JG1546782546690445312").SendAsync("ReceiveMessage", $"车辆{cehicle.code}已上线！");
                    _ChatHub.Clients.Group(groupid).SendAsync("ReceiveMessage", $"车辆{cehicle.code}已上线！");
                }

                //发送指令
                IByteBuffer resultbyte = Unpooled.CopiedBuffer(Encoding.Default.GetBytes("YLQC,OK;\r\n"));
                session.WriteAsync(resultbyte);
                session.WriteAsync(resultbyte);
                session.WriteAndFlushAsync(resultbyte);
                return jm;
            }
            else
            {
                jm.code = 401;
                jm.msg = "账户密码错误";
                return jm;
            }
        }

        [VehicleReport(VehicleReport.熄火)]
        public WebApiCallBack SocketLogout(string LoginToken)
        {
            try
            {
                _cache.Remove(LoginToken);
                var jm = new WebApiCallBack();
                jm.code = 0;
                jm.status = true;
                jm.msg = "接口响应成功!";
                return jm;
            }
            catch (Exception e)
            {
                LogHelper.Error("车辆退出连接失败！", e);
                var jm = new WebApiCallBack();
                jm.code = 0;
                jm.status = false;
                jm.msg = "车辆退出连接失败!";
                return jm;
            }
        }

        [VehicleReport(VehicleReport.熄火)]
        public async Task<WebApiCallBack> TCPSocketLogout(string msgstr, List<vehicle_terminalconfigure> logouttr, string vin, string clientid)
        {
            var jm = new WebApiCallBack();

            //var client = TCPsocketClientCollection.Get(clientid);
            var client = TCPsocketClientCollection.Getgroupone(clientid);
            var vehicle = _cache.Get<vehicle>(clientid);
            List<string> list1 = new List<string>();
            string[] strsub = null;
            list1 = YLQCHelper.TCPHexStrToDeString(msgstr, logouttr);
            string infodate = "20";
            for (int j = 0; j < 6; j++)
            {
                if (j < 2)
                {
                    infodate += YLQCHelper.HexStringToDemicel(list1[0].Substring(j * 2, 2)).ToString().PadLeft(2, '0') + "-";
                }
                else if (j == 2)
                {
                    infodate += YLQCHelper.HexStringToDemicel(list1[0].Substring(j * 2, 2)).ToString().PadLeft(2, '0') + " ";
                }
                else
                {
                    infodate += YLQCHelper.HexStringToDemicel(list1[0].Substring(j * 2, 2)).ToString().PadLeft(2, '0') + ":";
                }
            }
            //指定转换格式 
            System.IFormatProvider format = new System.Globalization.CultureInfo("zh-CN", true);
            string strDateFormat = "yyyy-MM-dd hh:mm:ss";
            //string strDateFormat1 = "yyyy-MM-dd";
            DateTime date = DateTime.ParseExact(infodate.Substring(0, infodate.Length - 1), strDateFormat, format, DateTimeStyles.AllowWhiteSpaces);
            //DateTime datebegin = DateTime.ParseExact(infodate.Substring(0, infodate.Length - 1), strDateFormat1, format, DateTimeStyles.AllowWhiteSpaces);
            DateTime datebegin = date.Date;
            var loginlog = _ivehicle_loginlogRepository.QueryByClause(p => p.logindate > datebegin && p.logindate < datebegin.AddDays(1) && p.Serialnumber == int.Parse(list1[1]));
            loginlog.loginoutdate = date;
            vehicle.isUsing = false;
            Task t = Task.WhenAll(_ivehicle_loginlogRepository.UpdateAsync(loginlog), _ivehicleRepository.UpdateAsync(vehicle));
            t.Wait();
            jm.code = 0;
            jm.status = true;
            jm.msg = "接口响应成功!";
            return jm;
        }


        #endregion

        #region 车辆参数上传
        public async Task<WebApiCallBack> SocketVehicleParameterUp(FMVehicleParameter dto, vehicle vehicle, string LogToken)
        {
            WebApiCallBack result = new WebApiCallBack();
            vehicle_log model = _Mapper.Map<vehicle_log>(dto);
            var list = _ivehicle_terminalconfigureRepository.SelectAll();
            model.VIN = vehicle.VIN;
            model.isNormal = false;
            model.Action = 6;
            model.Msg = dto.ToJson().ToString();
            //var tran = base.AsSugarClient();
            //var client = TCPsocketClientCollection.Get(LogToken);
            var client = TCPsocketClientCollection.Getgroupone(LogToken);

            try
            {
                _unitOfWork.BeginTran();
                int num = _ivehicle_logRepository.Insert(model);
                vehicle_parameterlist vpl = new vehicle_parameterlist();
                vpl.Logid = num;
                vpl.date = model.date;
                vpl.VIN = model.VIN;
                foreach (var val in dto.Parameter)
                {
                    FMVehicleUP fvup = new FMVehicleUP
                    {
                        address = val.Key,
                        byteH = val.Value.ToString()
                    };
                    var dateList = list.QueryListByClause(p => p.MsgID.ToLower() == fvup.address);
                    if (dateList != null && dateList.Count > 0)
                    {
                        foreach (vehicle_terminalconfigure vtc in dateList)
                        {
                            var str = vtc.Type == (int)TransmissionAgreement.LSB ? YLQCHelper.HexadecimalToBinaryArrayListLsb(fvup.byteH) : YLQCHelper.HexadecimalToBinaryArrayListMsb(fvup.byteH);
                            if (vpl.ContainsProperty(vtc.SignalName))
                            {
                                vpl.SetPropertyValue(vtc.SignalName, YLQCHelper.BinaryArrayToDecimal(str, str.Length, vtc.StartBit, vtc.BitLength, vtc.Resolution.Value, vtc.Offset, (TransmissionAgreement)vtc.Type));
                            }
                            else
                            {
                                continue;
                            }
                        }

                    }
                }
                _ivehicle_parameterlistRepository.Insert(vpl);
                _unitOfWork.CommitTran();
                await Task.CompletedTask;
                result.code = 0;
                result.status = true;
                result.msg = "接口响应成功!";
                //本文唯一值得注意的就是这里，下文会阐述
                IByteBuffer resultbyte = Unpooled.CopiedBuffer(Encoding.Default.GetBytes(result.ToJson()));
                await client.WriteAndFlushAsync(resultbyte);

            }
            catch (Exception e)
            {
                LogHelper.Error("接收车辆数据报文报错", e);
                _unitOfWork.RollbackTran();
                result.code = 500;
                result.status = false;
                result.msg = "接收车辆数据报文报错!";
                //本文唯一值得注意的就是这里，下文会阐述
                IByteBuffer resultbyte = Unpooled.CopiedBuffer(Encoding.Default.GetBytes(result.ToJson()));
                client.WriteAndFlushAsync(resultbyte);
                await Task.CompletedTask;
            }
            return result;
        }
        #endregion

        #region 车辆GPS获取
        public async Task SocketVehicleGPS()
        {
            List<string> list = new List<string>();

            for (int i = 0; i < 100; i++)
            {
                list = TCPsocketClientCollection.GetGroup(i);
                if (list == null)
                {
                    Thread.Sleep(20); //使用堵塞线程模式,同步延时
                    continue;
                }
                foreach (string clientid in list)
                {
                    //var client = TCPsocketClientCollection.Get(clientid);
                    var client = TCPsocketClientCollection.Getgroupone(clientid);

                    //本文唯一值得注意的就是这里，下文会阐述
                    IByteBuffer resultbyte = Unpooled.CopiedBuffer(Encoding.Default.GetBytes("config,get,gps"));
                    await client.WriteAndFlushAsync(resultbyte);
                    Thread.Sleep(20); //使用堵塞线程模式,同步延时
                }

            }

        }
        #endregion

        [VehicleReport(VehicleReport.通信)]
        #region TCPSocket车辆参数上传
        public async Task<WebApiCallBack> TCPSocketVehicleParameterUp(string hexstrpart, string LogToken, string vin, List<vehicle_terminalconfigure> list)
        {
            WebApiCallBack result = new WebApiCallBack();
            //var client = TCPsocketClientCollection.Get(LogToken);
            var client = TCPsocketClientCollection.Getgroupone(LogToken);

            List<string> list1 = new List<string>();
            string[] strsub = null;
            int losslength = 0;
            //var tran = base.AsSugarClient();

            try
            {
                //var vclass = new vehicle_gbt32960();
                //var properties = vclass.GetType().GetProperties();
                var db = SqlSugarHelper.Db;
                //国标表
                DataTable dt = new DataTable();
                dt = db.GetSimpleClient<vehicle_gbt32960>().AsQueryable().Where(p => false).ToDataTable();
                //驱动电机列表
                DataTable dtmcu = new DataTable();
                dtmcu = db.GetSimpleClient<vehicle_mculog>().AsQueryable().Where(p => false).ToDataTable();
                //电池电压列表
                DataTable dtdcdy = new DataTable();
                dtdcdy = db.GetSimpleClient<vehicle_batterydylog>().AsQueryable().Where(p => false).ToDataTable();
                //电磁温度列表
                DataTable dtdcwd = new DataTable();
                dtdcwd = db.GetSimpleClient<vehicle_batterywdlog>().AsQueryable().Where(p => false).ToDataTable();

                //头部
                string headpart = hexstrpart.Substring(0, 12);
                //List<vehicle_terminalconfigure> listheadpart = list.FindAll(p => p.MsgID == "GBT32960-02");
                //YLQCHelper.TCPHexStrToStrArray(headpart, listheadpart);
                //list1 = YLQCHelper.TCPHexStrToStrArray(hexstrpart, listheadpart);
                //foreach (string str in list1)
                //{
                string infodate = "20";
                for (int j = 0; j < 6; j++)
                {
                    if (j < 2)
                    {
                        infodate += YLQCHelper.HexStringToDemicel(headpart.Substring(j * 2, 2)).ToString().PadLeft(2, '0') + "-";
                    }
                    else if (j == 2)
                    {
                        infodate += YLQCHelper.HexStringToDemicel(headpart.Substring(j * 2, 2)).ToString().PadLeft(2, '0') + " ";
                    }
                    else
                    {
                        infodate += YLQCHelper.HexStringToDemicel(headpart.Substring(j * 2, 2)).ToString().PadLeft(2, '0') + ":";
                    }
                }
                //1、添加空行
                DataRow dr1 = dt.NewRow();
                int roelenght = dt.Columns.Count;
                dr1[roelenght - 1] = infodate.Substring(0, infodate.Length - 1);
                dr1[1] = vin;


                int i = 2;

                hexstrpart = hexstrpart.Substring(12);

                while (hexstrpart.Length > 2)
                {
                    string parttype = hexstrpart.Substring(0, 2);
                    //整车数据
                    if (parttype == "01")
                    {
                        losslength = 42;
                        string vt = hexstrpart.Substring(2, 40);
                        List<vehicle_terminalconfigure> listvt = list.FindAll(p => p.MsgID == "GBT32960-02-01-01");
                        list1 = YLQCHelper.TCPHexStrToDeString(vt, listvt);
                        foreach (string str in list1)
                        {
                            dr1[i] = str;
                            i++;
                        }
                        hexstrpart = hexstrpart.Substring(losslength);
                    }
                    //驱动电机数据
                    else if (parttype == "02")
                    {
                        int qddjnum = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(2, 2));
                        losslength = 4 + qddjnum * 24;
                        string mcu = hexstrpart.Substring(2, losslength);

                        dr1[i] = qddjnum;
                        i++;

                        //添加MCUdatatable 

                        List<vehicle_terminalconfigure> listmcu = list.FindAll(p => p.MsgID == "GBT32960-02-02-02");

                        var mcucode = IdHelper.GetId("MCU_");
                        for (int p = 0; p < qddjnum; p++)
                        {
                            DataRow drmcu = dtmcu.NewRow();

                            drmcu[1] = mcucode;
                            int mcui = 2;

                            int mculenght = dtmcu.Columns.Count;
                            list1 = YLQCHelper.TCPHexStrToDeString(mcu.Substring(2 + p * 24, 24), listmcu);
                            foreach (string str in list1)
                            {
                                drmcu[mcui] = str;
                                mcui++;
                            }
                            dtmcu.Rows.Add(drmcu);

                        }

                        //继续补充国标表datatable mcu外键
                        dr1[i] = mcucode;
                        i++;

                        hexstrpart = hexstrpart.Substring(losslength);
                    }

                    //定位数据
                    else if (parttype == "05")
                    {
                        losslength = 20;
                        string gps = hexstrpart.Substring(2, 38);
                        List<vehicle_terminalconfigure> listvt = list.FindAll(p => p.MsgID == "GBT32960-02-03-01");
                        list1 = YLQCHelper.TCPHexStrToDeString(gps, listvt);
                        foreach (string str in list1)
                        {
                            strsub = str.Split(';');
                            dr1[i] = str;
                            i++;
                        }
                        hexstrpart = hexstrpart.Substring(losslength);
                    }

                    //极值数据
                    else if (parttype == "06")
                    {
                        losslength = 30;
                        string jz = hexstrpart.Substring(2, 28);
                        List<vehicle_terminalconfigure> listvt = list.FindAll(p => p.MsgID == "GBT32960-02-04-01");
                        list1 = YLQCHelper.TCPHexStrToDeString(jz, listvt);
                        foreach (string str in list1)
                        {
                            dr1[i] = str;

                            i++;
                        }
                        hexstrpart = hexstrpart.Substring(losslength);
                    }

                    //报警数据
                    else if (parttype == "07")
                    {

                        dr1[i] = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(2, 2));
                        i++;


                        //以下二选一
                        //var bitlist = YLQCHelper.HexStringToBinary(hexstrpart.Substring(4, 8));
                        //var bstr = "";
                        //foreach (var bit in bitlist) {
                        //    bstr += bit.ToString();
                        //}
                        //dr1[i] = bstr;
                        //i++;

                        dr1[i] = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(4, 8));
                        i++;
                        //储能装置故障
                        int index1 = 14;
                        int num1 = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(12, 2));
                        string bjlist1 = hexstrpart.Substring(index1, num1 * 8);
                        dr1[i] = num1;
                        i++;
                        string err1 = "";
                        for (int enum1 = 0; enum1 < num1; enum1++)
                        {
                            err1 += hexstrpart.Substring(index1 + enum1 * 8, 8) + " ";
                        }
                        dr1[i] = err1.TrimEnd();
                        i++;

                        //驱动电机故障

                        int index2 = index1 + num1 + 2;
                        int num2 = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(index2 - 2, 2));
                        string bjlist2 = hexstrpart.Substring(index2, num2 * 8);
                        dr1[i] = num2;
                        i++;
                        string err2 = "";
                        for (int enum2 = 0; enum2 < num2; enum2++)
                        {
                            err2 += hexstrpart.Substring(index2 + enum2 * 8, 8) + " ";
                        }
                        dr1[i] = err2.TrimEnd();
                        i++;

                        //发动机故障

                        int index3 = index2 + num2 + 2;
                        int num3 = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(index3 - 2, 2));
                        string bjlist3 = hexstrpart.Substring(index3, num3 * 8);
                        dr1[i] = num3;
                        i++;
                        string err3 = "";
                        for (int enum3 = 0; enum3 < num3; enum3++)
                        {
                            err3 += hexstrpart.Substring(index3 + enum3 * 8, 8) + " ";
                        }
                        dr1[i] = err3.TrimEnd();
                        i++;

                        //其他故障

                        int index4 = index3 + num3 + 2;
                        int num4 = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(index4 - 2, 2));
                        string bjlist4 = hexstrpart.Substring(index4, num4 * 8);
                        dr1[i] = num4;
                        i++;
                        string err4 = "";
                        for (int enum4 = 0; enum4 < num4; enum4++)
                        {
                            err4 += hexstrpart.Substring(index4 + enum4 * 8, 8) + " ";
                        }
                        dr1[i] = err4.TrimEnd();
                        i++;

                        hexstrpart = hexstrpart.Substring(index4 + num4 * 8);

                    }


                    //电池电压数据
                    else if (parttype == "08")
                    {
                        int dcdynum = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(2, 2));
                        int dtnum = 0;
                        losslength = 4;
                        dr1[i] = dcdynum;
                        i++;
                        hexstrpart = hexstrpart.Substring(losslength);

                        //添加电池电压datatable 

                        List<vehicle_terminalconfigure> listdcdy = list.FindAll(p => p.MsgID == "GBT32960-02-06-02");

                        var dcdycode = IdHelper.GetId("DCDY_");
                        for (int p = 0; p < dcdynum; p++)
                        {
                            DataRow drdcdy = dtdcdy.NewRow();

                            drdcdy[1] = dcdycode;

                            int dcdylenght = dtdcdy.Columns.Count;
                            string mcu = hexstrpart.Substring(0, 20);
                            list1 = YLQCHelper.TCPHexStrToDeString(mcu, listdcdy);
                            int mcui = 2;
                            foreach (string str in list1)
                            {
                                switch (dtdcdy.Columns[mcui].DataType.Name)
                                {

                                    case "String":
                                        drdcdy[mcui] = str;
                                        break;
                                    case "Decimal":
                                        drdcdy[mcui] = str;
                                        break;
                                    case "Int32":
                                        drdcdy[mcui] = int.Parse(str.Substring(0, str.IndexOf(".")));
                                        break;
                                    default:

                                        break;

                                }
                                mcui++;
                            }
                            hexstrpart = hexstrpart.Substring(20);

                            //储能装置电压列表
                            int num1 = int.Parse(list1[list1.Count - 1].Substring(0, list1[list1.Count - 1].IndexOf(".")));
                            string bjlist1 = hexstrpart.Substring(0, num1 * 4);
                            string err1 = "";
                            for (int enum1 = 0; enum1 < num1; enum1++)
                            {
                                err1 += hexstrpart.Substring(enum1 * 4, 4) + " ";
                            }

                            hexstrpart = hexstrpart.Substring(num1 * 4);
                            drdcdy[mcui] = err1.TrimEnd();

                            dtdcdy.Rows.Add(drdcdy);

                        }

                        //继续补充国标表datatable dcdy外键
                        dr1[i] = dcdycode;
                        i++;
                    }

                    //电池温度数据
                    else if (parttype == "09")
                    {
                        int dcdynum = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(2, 2));
                        int dtnum = 0;
                        losslength = 4;
                        dr1[i] = dcdynum;
                        i++;
                        hexstrpart = hexstrpart.Substring(losslength);

                        //添加电池电压datatable 

                        List<vehicle_terminalconfigure> listdcdy = list.FindAll(p => p.MsgID == "GBT32960-02-07-02");

                        var dcdycode = IdHelper.GetId("DCWD_");
                        for (int p = 0; p < dcdynum; p++)
                        {
                            DataRow drdcwd = dtdcwd.NewRow();

                            drdcwd[1] = dcdycode;

                            int dcdylenght = dtdcwd.Columns.Count;
                            string mcu = hexstrpart.Substring(0, 6);
                            list1 = YLQCHelper.TCPHexStrToDeString(mcu, listdcdy);
                            int mcui = 2;

                            foreach (string str in list1)
                            {
                                switch (dtdcwd.Columns[mcui].DataType.Name)
                                {

                                    case "String":
                                        drdcwd[mcui] = str;
                                        break;
                                    case "Decimal":
                                        drdcwd[mcui] = str;
                                        break;
                                    case "Int32":
                                        drdcwd[mcui] = int.Parse(str.Substring(0, str.IndexOf(".")));
                                        break;
                                    default:

                                        break;

                                }
                                mcui++;
                            }
                            hexstrpart = hexstrpart.Substring(6);

                            //储能装置温度列表
                            int num1 = int.Parse(list1[list1.Count - 1].Substring(0, list1[list1.Count - 1].IndexOf(".")));
                            string bjlist1 = hexstrpart.Substring(0, num1 * 2);
                            string err1 = "";
                            for (int enum1 = 0; enum1 < num1; enum1++)
                            {
                                err1 += hexstrpart.Substring(enum1 * 2, 2) + " ";
                            }

                            hexstrpart = hexstrpart.Substring(num1 * 2);
                            drdcwd[mcui] = err1.TrimEnd();

                            dtdcwd.Rows.Add(drdcwd);

                        }

                        //继续补充国标表datatable dcdy外键
                        dr1[i] = dcdycode;
                        i++;
                    }

                    else
                    {
                        break;
                    }

                }
                dt.Rows.Add(dr1);
                //datatable插入数据库 
                List<Dictionary<string, object>> dc = DataTableToDictionary(dt);//5.0.23版本支持

                List<Dictionary<string, object>> dcdcdy = DataTableToDictionary(dtdcdy);//5.0.23版本支持

                List<Dictionary<string, object>> dcdcwd = DataTableToDictionary(dtdcwd);//5.0.23版本支持


                _unitOfWork.BeginTran();
                db.Insertable(dc).AS("vehicle_gbt32960").ExecuteReturnIdentity();
                db.Insertable(dcdcdy).AS("vehicle_batterydylog").ExecuteReturnIdentity();
                db.Insertable(dcdcwd).AS("vehicle_batterywdlog").ExecuteReturnIdentity();
                _unitOfWork.CommitTran();
                //}
                //本文唯一值得注意的就是这里，下文会阐述
                IByteBuffer resultbyte = Unpooled.CopiedBuffer(Encoding.Default.GetBytes(result.ToJson()));
                await client.WriteAndFlushAsync(resultbyte);
            }





            catch (Exception e)
            {
                _unitOfWork.RollbackTran();
                LogHelper.Error("接收车辆数据报文报错", e);
                result.code = 500;
                result.status = false;
                result.msg = "接收车辆数据报文报错";
                //本文唯一值得注意的就是这里，下文会阐述
                IByteBuffer resultbyte = Unpooled.CopiedBuffer(Encoding.Default.GetBytes(result.ToJson()));
                client.WriteAndFlushAsync(resultbyte);
                await Task.CompletedTask;
            }
            return result;
        }
        #endregion

        #region TCPSocket车辆参数批量上传
        public async Task<WebApiCallBack> TCPSocketVehicleParameterBatchUp(List<string> hexstrpartlist)
        {
            WebApiCallBack result = new WebApiCallBack();
            if (hexstrpartlist.Count == 0)
            {
                result.code = 0;
                result.status = true;
                result.msg = "接口响应成功";
                return result;
            }

            var db = SqlSugarHelper.Db;
            List<vehicle_terminalconfigure> list = await db.GetSimpleClient<vehicle_terminalconfigure>().GetListAsync(p => p.MsgID.StartsWith("GBT32960"));

            var nowtime = DateTime.Now;
            var nextdate = nowtime.AddDays(1);
            //var vclass = new vehicle_gbt32960();
            //var properties = vclass.GetType().GetProperties();

            
            //国标表
            DataTable dt = new DataTable();
            dt = db.GetSimpleClient<vehicle_gbt32960>().AsQueryable().Where(p => false).SplitTable(tabs => tabs.Take(1)).ToDataTable();
            //驱动电机列表
            DataTable dtmcu = new DataTable();
            dtmcu = db.GetSimpleClient<vehicle_mculog>().AsQueryable().Where(p => false).SplitTable(tabs => tabs.Take(1)).ToDataTable();
            //电池电压列表
            DataTable dtdcdy = new DataTable();
            dtdcdy = db.GetSimpleClient<vehicle_batterydylog>().AsQueryable().Where(p => false).SplitTable(tabs => tabs.Take(1)).ToDataTable();
            //电磁温度列表
            DataTable dtdcwd = new DataTable();
            dtdcwd = db.GetSimpleClient<vehicle_batterywdlog>().AsQueryable().Where(p => false).SplitTable(tabs => tabs.Take(1)).ToDataTable();
            //故障表
            List<vehicle_troubleshooting> vtslist = await db.GetSimpleClient<vehicle_troubleshooting>().AsQueryable().Where(p => !p.state).ToListAsync();
            List<vehicle_troubleshooting> updatevtslist = new List<vehicle_troubleshooting>();
            List<string> orglist = new List<string>();

            List<vehicle_gbt32960> gbtlist = new List<vehicle_gbt32960>();
            List<vehicle_batterydylog> gbtdylist = new List<vehicle_batterydylog>();
            List<vehicle_batterywdlog> gbtwdlist = new List<vehicle_batterywdlog>();
            List<vehicle_mculog> gbtmculist = new List<vehicle_mculog>();
            List<string> vlist = new List<string>();

            //锁初始化
            object mymcuLock = new object();
            object myerrLock = new object();
            object mydyLock = new object();
            object mywdLock = new object();
            object mygbtLock = new object();
            #region 线程池多线程处理



            ParallelOptions parallelOptions = new ParallelOptions()
            {
                //线程数
                MaxDegreeOfParallelism = 16
            };
            Parallel.ForEach(hexstrpartlist, parallelOptions, msg =>
            {
                List<string> list1 = new List<string>();
                List<string> gpslist = new List<string>();
                string[] strsub = null;
                int losslength = 0;
                List<string> strlist = msg.Split('&').ToList();
                string hexstrpart = strlist[0];
                string cachecode = strlist[1];

                vehicle_gbt32960 vgbt = new vehicle_gbt32960();



                //先将行数据存入数组中再将数据赋值到datatable 行中
                object[] vhlist = new object[dt.Columns.Count];


                //判断起始字符是否存在
                string beginstr = hexstrpart.Substring(0, 4);
                string bstr = YLQCHelper.HexStringToASCII(beginstr);
                if (bstr != "##")
                {
                    LogHelper.Info($"传入数据格式起始符不正确!数据为：{msg}");
                    return;
                }
                //判断字符长度是否按照标准
                string Lengthstr = hexstrpart.Substring(44, 4);
                if (YLQCHelper.HexStringToDemicel(Lengthstr) != hexstrpart.Length / 2 - 25)
                {
                    LogHelper.Info($"传入数据长度不符!数据为：{hexstrpart}");
                    return;
                }
                string VINstr = hexstrpart.Substring(8, 34);


                //bbc校验编码
                string bbccode = YLQCHelper.HexToBBCString(hexstrpart.Substring(4, hexstrpart.Length - 6));
                hexstrpart = hexstrpart.Substring(0, hexstrpart.Length - 2) + bbccode;

                try
                {

                    //List<vehicle_terminalconfigure> listheadpart = list.FindAll(p => p.MsgID == "GBT32960-02");
                    //YLQCHelper.TCPHexStrToStrArray(headpart, listheadpart);
                    //list1 = YLQCHelper.TCPHexStrToStrArray(hexstrpart, listheadpart);
                    //foreach (string str in list1)
                    //{
                    hexstrpart = hexstrpart.Substring(48, hexstrpart.Length - 50);
                    string headpart = hexstrpart.Substring(0, 12);

                    string infodate = "20";
                    for (int j = 0; j < 6; j++)
                    {
                        if (j < 2)
                        {
                            infodate += YLQCHelper.HexStringToDemicel(headpart.Substring(j * 2, 2)).ToString().PadLeft(2, '0') + "-";
                        }
                        else if (j == 2)
                        {
                            infodate += YLQCHelper.HexStringToDemicel(headpart.Substring(j * 2, 2)).ToString().PadLeft(2, '0') + " ";
                        }
                        else
                        {
                            infodate += YLQCHelper.HexStringToDemicel(headpart.Substring(j * 2, 2)).ToString().PadLeft(2, '0') + ":";
                        }
                    }

                    int roelenght = dt.Columns.Count;
                    string update = infodate.Substring(0, infodate.Length - 1);
                    vhlist[roelenght - 1] = update;
                    string VIN = YLQCHelper.HexStringToASCII(VINstr);
                    if (!cachecode.EndsWith(TCPsocketClientCollection.GetChannelDic(VIN)?.Id.AsLongText()))
                    {

                        if (!TCPsocketClientCollection.GetChannelDic(VIN).IsNull())
                        {
                            //if (!TCPsocketClientCollection.GetChannelDic(VIN).Active)
                            //{
                            //    var newchannel = TCPsocketClientCollection.Getgroupone(cachecode);
                            //    _cache.Set(cachecode, _cache.Get(TCPsocketClientCollection.GetChannelDic(VIN).Id.AsLongText()), TimeSpan.FromMinutes(60 * 24));

                            //    //添加绑定
                            //    TCPsocketClientCollection.AddChannelDic(VIN, newchannel);

                            //    // 双向绑定
                            //    // channel -> userId
                            //    AttributeKey<String> key = AttributeKey<String>.ValueOf("VIN");
                            //    newchannel.GetAttribute(key).Set(VIN);
                            //    LogHelper.Info("重连机制完成！");
                            //}
                            //else {
                            LogHelper.Info("原通道依旧活跃不重复运行！");
                            _cache.ListLeftPushAsync(VIN + "_loss", msg);
                            return;
                            //}
                        }
                        else
                        {
                            LogHelper.Info("登录通道不存在！");
                            return;
                        }
                    }


                    //vhlist[0] = IdHelper.GetLongId();
                    vhlist[1] = VIN;


                    int i = 2;

                    hexstrpart = hexstrpart.Substring(12);
                    //信息体各部分参数判断
                    while (hexstrpart.Length > 2)
                    {
                        string parttype = hexstrpart.Substring(0, 2);
                        //整车数据
                        if (parttype == "01")
                        {
                            losslength = 42;
                            string vt = hexstrpart.Substring(2, 40);
                            List<vehicle_terminalconfigure> listvt = list.FindAll(p => p.MsgID == "GBT32960-02-01-01");
                            list1 = YLQCHelper.TCPHexStrToDeString(vt, listvt);
                            foreach (string str in list1)
                            {
                                if (str.IsNullOrEmpty() || str.IsNullOrWhiteSpace())
                                {
                                    LogHelper.Info("插入datatable解析为空：" + list1.ToJson());
                                }
                                vhlist[i] = str;
                                i++;
                            }
                            hexstrpart = hexstrpart.Substring(losslength);
                        }
                        //驱动电机数据
                        else if (parttype == "02")
                        {
                            int qddjnum = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(2, 2));
                            losslength = 4 + qddjnum * 24;
                            string mcu = hexstrpart.Substring(2, losslength);

                            vhlist[i] = qddjnum;
                            i++;

                            //添加MCUdatatable 

                            List<vehicle_terminalconfigure> listmcu = list.FindAll(p => p.MsgID == "GBT32960-02-02-02");

                            var mcucode = IdHelper.GetId("MCU_");
                            for (int p = 0; p < qddjnum; p++)
                            {

                                int mcui = 2;

                                int mculenght = dtmcu.Columns.Count;
                                list1 = YLQCHelper.TCPHexStrToDeString(mcu.Substring(2 + p * 24, 24), listmcu);

                                ////获取写入锁
                                //_lock.EnterWriteLock();
                                //DataRow drmcu = dtmcu.NewRow();
                                //drmcu[0] = IdHelper.GetLongId();
                                //drmcu[1] = mcucode;
                                //foreach (string str in list1)
                                //{
                                //    drmcu[mcui] = str;
                                //    mcui++;
                                //}
                                //dtmcu.Rows.Add(drmcu);
                                ////释放写入锁
                                //_lock.ExitWriteLock();

                                vehicle_mculog mcumodel = new vehicle_mculog();
                                //mcumodel.id= IdHelper.GetLongId();
                                mcumodel.code = mcucode;
                                mcumodel.date = update.ObjectToDate();
                                foreach (string str in list1)
                                {
                                    switch (dtmcu.Columns[mcui].DataType.Name)
                                    {

                                        case "String":
                                            mcumodel.SetPropertyValue(dtmcu.Columns[mcui].ColumnName, str.ToString());
                                            break;
                                        case "Decimal":
                                            mcumodel.SetPropertyValue(dtmcu.Columns[mcui].ColumnName, str.ObjectToDecimal());
                                            break;
                                        case "Int32":
                                            mcumodel.SetPropertyValue(dtmcu.Columns[mcui].ColumnName, int.Parse(str.ToString().Substring(0, str.ToString().IndexOf("."))));
                                            break;
                                        case "Int64":
                                            mcumodel.SetPropertyValue(dtmcu.Columns[mcui].ColumnName, long.Parse(str.ToString().Substring(0, str.ToString().IndexOf("."))));
                                            break;
                                        case "DateTime":
                                            mcumodel.SetPropertyValue(dtmcu.Columns[mcui].ColumnName, str.ObjectToDate());
                                            break;
                                        default:

                                            break;

                                    }
                                    mcui++;
                                }
                                lock (mymcuLock)
                                {
                                    gbtmculist.Add(mcumodel);
                                }

                            }

                            //继续补充国标表datatable mcu外键
                            vhlist[i] = mcucode;
                            i++;

                            hexstrpart = hexstrpart.Substring(losslength);
                        }

                        //定位数据
                        else if (parttype == "05")
                        {
                            losslength = 20;
                            string gps = hexstrpart.Substring(2, 38);
                            List<vehicle_terminalconfigure> listvt = list.FindAll(p => p.MsgID == "GBT32960-02-03-01");
                            gpslist = list1 = YLQCHelper.TCPHexStrToDeString(gps, listvt);
                            foreach (string str in list1)
                            {
                                //strsub = str.Split(';');
                                vhlist[i] = str;
                                i++;
                            }
                            hexstrpart = hexstrpart.Substring(losslength);
                        }

                        //极值数据
                        else if (parttype == "06")
                        {
                            losslength = 30;
                            string jz = hexstrpart.Substring(2, 28);
                            List<vehicle_terminalconfigure> listvt = list.FindAll(p => p.MsgID == "GBT32960-02-04-01");
                            list1 = YLQCHelper.TCPHexStrToDeString(jz, listvt);
                            foreach (string str in list1)
                            {
                                vhlist[i] = str;

                                i++;
                            }
                            hexstrpart = hexstrpart.Substring(losslength);
                        }

                        //报警数据
                        else if (parttype == "07")
                        {
                            int errorlev = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(2, 2));
                            vhlist[i] = errorlev;
                            if (errorlev != 0)
                            {
                                orglist.AddRange(_cache.Get<vehicle>(cachecode).parentIdList);
                                vlist.Add(VIN);
                            }
                            i++;


                            //以下二选一
                            //var bitlist = YLQCHelper.HexStringToBinary(hexstrpart.Substring(4, 8));
                            //var bstr = "";
                            //foreach (var bit in bitlist) {
                            //    bstr += bit.ToString();
                            //}
                            //vhlist[i] = bstr;
                            //i++;
                            vehicle_terminalconfigure vt = list.Find(p => p.SignalName == "Report_Sts");

                            vhlist[i] = YLQCHelper.TCPHexStrToDeString(hexstrpart.Substring(4, 8), vt);
                            i++;
                            //储能装置故障
                            int index1 = 14;
                            int num1 = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(12, 2));
                            string bjlist1 = hexstrpart.Substring(index1, num1 * 8);
                            vhlist[i] = num1;
                            i++;
                            string err1 = "";
                            if (num1 > 0)
                            {
                                for (int enum1 = 0; enum1 < num1; enum1++)
                                {
                                    var errcodestr = YLQCHelper.HexStringToASCII(hexstrpart.Substring(index1 + enum1 * 8, 8));
                                    err1 += errcodestr + " ";
                                    var errorcode = vtslist.FirstOrDefault(p => p.vin == VIN && p.warningcode == errcodestr);
                                    var nowdate = DateTime.Parse(update);
                                    if (errorcode != null)
                                    {
                                        errorcode.update = nowdate;
                                        lock (myerrLock)
                                        {
                                            updatevtslist.Add(errorcode);
                                        }
                                    }
                                    else
                                    {
                                        lock (myerrLock)
                                        {
                                            updatevtslist.Add(new vehicle_troubleshooting
                                            {
                                                level = errorlev,
                                                type = 1,
                                                warningcode = errcodestr,
                                                begindate = nowdate,
                                                update = nowdate,
                                                warninglng = decimal.Parse(gpslist[1]),
                                                warninglat = decimal.Parse(gpslist[2]),
                                                vin = VIN,
                                                state = false
                                            });
                                        }
                                    }
                                }
                            }
                            vhlist[i] = err1.TrimEnd();
                            i++;

                            //驱动电机故障

                            int index2 = index1 + num1 * 8 + 2;
                            int num2 = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(index2 - 2, 2));
                            string bjlist2 = hexstrpart.Substring(index2, num2 * 8);
                            vhlist[i] = num2;
                            i++;
                            string err2 = "";
                            if (num2 > 0)
                            {
                                for (int enum2 = 0; enum2 < num2; enum2++)
                                {
                                    var errcodestr = YLQCHelper.HexStringToASCII(hexstrpart.Substring(index2 + enum2 * 8, 8));
                                    err2 += errcodestr + " ";
                                    var errorcode = vtslist.FirstOrDefault(p => p.vin == VIN && p.warningcode == errcodestr);
                                    var nowdate = DateTime.Parse(update);
                                    if (errorcode != null)
                                    {
                                        errorcode.update = nowdate;
                                        lock (myerrLock)
                                        {
                                            updatevtslist.Add(errorcode);
                                        }

                                    }
                                    else
                                    {
                                        lock (myerrLock)
                                        {
                                            updatevtslist.Add(new vehicle_troubleshooting
                                            {
                                                level = errorlev,
                                                type = 2,
                                                warningcode = errcodestr,
                                                begindate = nowdate,
                                                update = nowdate,
                                                warninglng = decimal.Parse(gpslist[1]),
                                                warninglat = decimal.Parse(gpslist[2]),
                                                vin = VIN,
                                                state = false
                                            });
                                        }
                                    }
                                }
                            }
                            vhlist[i] = err2.TrimEnd();
                            i++;

                            //发动机故障

                            int index3 = index2 + num2 * 8 + 2;
                            int num3 = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(index3 - 2, 2));
                            string bjlist3 = hexstrpart.Substring(index3, num3 * 8);
                            vhlist[i] = num3;
                            i++;
                            string err3 = "";
                            if (num3 > 0)
                            {
                                for (int enum3 = 0; enum3 < num3; enum3++)
                                {
                                    var errcodestr = YLQCHelper.HexStringToASCII(hexstrpart.Substring(index3 + enum3 * 8, 8));
                                    err3 += errcodestr + " ";
                                    var errorcode = vtslist.FirstOrDefault(p => p.vin == VIN && p.warningcode == errcodestr);
                                    var nowdate = DateTime.Parse(update);
                                    if (errorcode != null)
                                    {
                                        errorcode.update = nowdate;
                                        lock (myerrLock)
                                        {
                                            updatevtslist.Add(errorcode);
                                        }

                                    }
                                    else
                                    {
                                        lock (myerrLock)
                                        {
                                            updatevtslist.Add(new vehicle_troubleshooting
                                            {
                                                level = errorlev,
                                                type = 3,
                                                warningcode = errcodestr,
                                                begindate = nowdate,
                                                update = nowdate,
                                                warninglng = decimal.Parse(gpslist[1]),
                                                warninglat = decimal.Parse(gpslist[2]),
                                                vin = VIN,
                                                state = false
                                            });
                                        }
                                    }
                                }
                            }
                            vhlist[i] = err3.TrimEnd();
                            i++;

                            //其他故障

                            int index4 = index3 + num3 * 8 + 2;
                            int num4 = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(index4 - 2, 2));
                            string bjlist4 = hexstrpart.Substring(index4, num4 * 8);
                            vhlist[i] = num4;
                            i++;
                            string err4 = "";
                            if (num4 > 0)
                            {
                                for (int enum4 = 0; enum4 < num4; enum4++)
                                {
                                    var errcodestr = YLQCHelper.HexStringToASCII(hexstrpart.Substring(index4 + enum4 * 8, 8));
                                    err4 += errcodestr + " ";
                                    var errorcode = vtslist.FirstOrDefault(p => p.vin == VIN && p.warningcode == errcodestr);
                                    var nowdate = DateTime.Parse(update);
                                    if (errorcode != null)
                                    {
                                        errorcode.update = nowdate;
                                        lock (myerrLock)
                                        {
                                            updatevtslist.Add(errorcode);
                                        }

                                    }
                                    else
                                    {
                                        lock (myerrLock)
                                        {
                                            updatevtslist.Add(new vehicle_troubleshooting
                                            {
                                                level = errorlev,
                                                type = 3,
                                                warningcode = errcodestr,
                                                begindate = nowdate,
                                                update = nowdate,
                                                warninglng = decimal.Parse(gpslist[1]),
                                                warninglat = decimal.Parse(gpslist[2]),
                                                vin = VIN,
                                                state = false
                                            });
                                        }
                                    }
                                }
                            }
                            vhlist[i] = err4.TrimEnd();
                            i++;

                            hexstrpart = hexstrpart.Substring(index4 + num4 * 8);

                        }


                        //电池电压数据
                        else if (parttype == "08")
                        {
                            int dcdynum = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(2, 2));
                            int dtnum = 0;
                            losslength = 4;
                            vhlist[i] = dcdynum;
                            i++;
                            hexstrpart = hexstrpart.Substring(losslength);

                            //添加电池电压datatable 

                            List<vehicle_terminalconfigure> listdcdy = list.FindAll(p => p.MsgID == "GBT32960-02-06-02");
                            vehicle_terminalconfigure terminal = list.Find(p => p.MsgID == "GBT32960-02-06-02-01");
                            var dcdycode = IdHelper.GetId("DCDY_");
                            for (int p = 0; p < dcdynum; p++)
                            {


                                int dcdylenght = dtdcdy.Columns.Count;
                                string mcu = hexstrpart.Substring(0, 20);
                                list1 = YLQCHelper.TCPHexStrToDeString(mcu, listdcdy);
                                int mcui = 2;

                                hexstrpart = hexstrpart.Substring(20);

                                //储能装置电压列表
                                int num1 = int.Parse(list1[list1.Count - 1].Substring(0, list1[list1.Count - 1].IndexOf(".")));
                                string bjlist1 = hexstrpart.Substring(0, num1 * 4);
                                string err1 = "";
                                for (int enum1 = 0; enum1 < num1; enum1++)
                                {
                                    err1 += (YLQCHelper.HexStringToDemicel(hexstrpart.Substring(enum1 * 4, 4)) * terminal.Resolution.Value + terminal.Offset) + " ";
                                }

                                hexstrpart = hexstrpart.Substring(num1 * 4);


                                ////获取写入锁
                                //_lock.EnterWriteLock();
                                //DataRow drdcdy = dtdcdy.NewRow();
                                //drdcdy[0] = IdHelper.GetLongId();

                                //drdcdy[1] = dcdycode;
                                //foreach (string str in list1)
                                //{
                                //    switch (dtdcdy.Columns[mcui].DataType.Name)
                                //    {

                                //        case "String":
                                //            drdcdy[mcui] = str;
                                //            break;
                                //        case "Decimal":
                                //            drdcdy[mcui] = str;
                                //            break;
                                //        case "Int32":
                                //            drdcdy[mcui] = int.Parse(str.Substring(0, str.IndexOf(".")));
                                //            break;
                                //        default:

                                //            break;

                                //    }
                                //    mcui++;
                                //}
                                //drdcdy[mcui] = err1.TrimEnd();

                                //dtdcdy.Rows.Add(drdcdy);
                                ////释放写入锁
                                //_lock.ExitWriteLock();

                                vehicle_batterydylog dymodel = new vehicle_batterydylog();
                                //dymodel.id = IdHelper.GetLongId();
                                dymodel.code = dcdycode;
                                dymodel.date = update.ObjectToDate();
                                dymodel.BatteryMonomerDY = err1.TrimEnd();
                                foreach (string str in list1)
                                {
                                    switch (dtdcdy.Columns[mcui].DataType.Name)
                                    {

                                        case "String":
                                            dymodel.SetPropertyValue(dtdcdy.Columns[mcui].ColumnName, str.ToString());
                                            break;
                                        case "Decimal":
                                            dymodel.SetPropertyValue(dtdcdy.Columns[mcui].ColumnName, str.ObjectToDecimal());
                                            break;
                                        case "Int32":
                                            dymodel.SetPropertyValue(dtdcdy.Columns[mcui].ColumnName, int.Parse(str.Substring(0, str.IndexOf("."))));
                                            break;
                                        case "Int64":
                                            dymodel.SetPropertyValue(dtdcdy.Columns[mcui].ColumnName, long.Parse(str.Substring(0, str.IndexOf("."))));
                                            break;
                                        default:

                                            break;

                                    }
                                    mcui++;
                                }
                                lock (mydyLock)
                                {
                                    gbtdylist.Add(dymodel);
                                }

                            }

                            //继续补充国标表datatable dcdy外键
                            vhlist[i] = dcdycode;
                            i++;
                        }

                        //电池温度数据
                        else if (parttype == "09")
                        {
                            int dcdynum = YLQCHelper.HexStringToDemicel(hexstrpart.Substring(2, 2));
                            int dtnum = 0;
                            losslength = 4;
                            vhlist[i] = dcdynum;
                            i++;
                            hexstrpart = hexstrpart.Substring(losslength);

                            //添加电池电压datatable 

                            List<vehicle_terminalconfigure> listdcdy = list.FindAll(p => p.MsgID == "GBT32960-02-07-02");
                            vehicle_terminalconfigure terminal = list.Find(p => p.MsgID == "GBT32960-02-07-02-01");

                            var dcdycode = IdHelper.GetId("DCWD_");
                            for (int p = 0; p < dcdynum; p++)
                            {


                                int dcdylenght = dtdcwd.Columns.Count;
                                string mcu = hexstrpart.Substring(0, 6);
                                list1 = YLQCHelper.TCPHexStrToDeString(mcu, listdcdy);
                                int mcui = 2;


                                hexstrpart = hexstrpart.Substring(6);

                                //储能装置温度列表
                                int num1 = int.Parse(list1[list1.Count - 1].Substring(0, list1[list1.Count - 1].IndexOf(".")));
                                string bjlist1 = hexstrpart.Substring(0, num1 * 2);
                                string err1 = "";
                                for (int enum1 = 0; enum1 < num1; enum1++)
                                {
                                    err1 += (YLQCHelper.HexStringToDemicel(hexstrpart.Substring(enum1 * 2, 2)) * terminal.Resolution.Value + terminal.Offset) + " ";
                                }

                                hexstrpart = hexstrpart.Substring(num1 * 2);


                                ////获取写入锁
                                //_lock.EnterWriteLock();
                                //DataRow drdcwd = dtdcwd.NewRow();
                                //drdcwd[0] = IdHelper.GetLongId();

                                //drdcwd[1] = dcdycode;
                                //foreach (string str in list1)
                                //{
                                //    switch (dtdcwd.Columns[mcui].DataType.Name)
                                //    {

                                //        case "String":
                                //            drdcwd[mcui] = str;
                                //            break;
                                //        case "Decimal":
                                //            drdcwd[mcui] = str;
                                //            break;
                                //        case "Int32":
                                //            drdcwd[mcui] = int.Parse(str.Substring(0, str.IndexOf(".")));
                                //            break;
                                //        default:

                                //            break;

                                //    }
                                //    mcui++;
                                //}
                                //drdcwd[mcui] = err1.TrimEnd();

                                //dtdcwd.Rows.Add(drdcwd);
                                ////释放写入锁
                                //_lock.ExitWriteLock();

                                vehicle_batterywdlog wdmodel = new vehicle_batterywdlog();
                                //wdmodel.id = IdHelper.GetLongId();
                                wdmodel.code = dcdycode;
                                wdmodel.date = update.ObjectToDate();
                                wdmodel.BatteryWDList = err1.TrimEnd();
                                foreach (string str in list1)
                                {
                                    switch (dtdcwd.Columns[mcui].DataType.Name)
                                    {

                                        case "String":
                                            wdmodel.SetPropertyValue(dtdcwd.Columns[mcui].ColumnName, str.ToString());
                                            break;
                                        case "Decimal":
                                            wdmodel.SetPropertyValue(dtdcwd.Columns[mcui].ColumnName, str.ObjectToDecimal());
                                            break;
                                        case "Int32":
                                            wdmodel.SetPropertyValue(dtdcwd.Columns[mcui].ColumnName, int.Parse(str.Substring(0, str.IndexOf("."))));
                                            break;
                                        case "Int64":
                                            wdmodel.SetPropertyValue(dtdcwd.Columns[mcui].ColumnName, long.Parse(str.Substring(0, str.IndexOf("."))));
                                            break;
                                        default:

                                            break;

                                    }
                                    mcui++;
                                }
                                lock (mywdLock)
                                {
                                    gbtwdlist.Add(wdmodel);
                                }

                            }

                            //继续补充国标表datatable dcdy外键
                            vhlist[i] = dcdycode;
                            i++;
                        }

                        else
                        {
                            break;
                        }

                    }

                    //    //获取写入锁
                    //    _lock.EnterWriteLock();
                    //    //1、添加空行
                    //    DataRow dr1 = dt.NewRow();
                    //    int dri = 0;

                    //    foreach (object obj in vhlist)
                    //    {
                    //        //if (dri == 0)
                    //        //{
                    //        //    dri++;
                    //        //    continue;
                    //        //}
                    //        switch (dt.Columns[dri].DataType.Name)
                    //        {

                    //            case "String":
                    //            case "Decimal":
                    //                dr1[dri] = obj.ObjectToString();
                    //                break;
                    //            case "Int32":
                    //            case "Int64":
                    //                dr1[dri] = long.Parse(obj.ToString());
                    //                break;
                    //            case "DateTime":
                    //                dr1[dri] = obj.ObjectToDate();
                    //                break;
                    //            default:

                    //                break;

                    //        }
                    //        dri++;
                    //    }

                    //dt.Rows.Add(dr1);

                    //    //释放写入锁
                    //    _lock.ExitWriteLock();

                    vehicle_gbt32960 gbmodel = new vehicle_gbt32960();
                    int dri = 0;
                    foreach (object str in vhlist)
                    {
                        if (dri == 0)
                        {
                            dri++;
                            continue;
                        }
                        switch (dt.Columns[dri].DataType.Name)
                        {

                            case "String":
                                string str1 = str.ToString();
                                gbmodel.SetPropertyValue(dt.Columns[dri].ColumnName, str.ToString());
                                break;
                            case "Decimal":
                                gbmodel.SetPropertyValue(dt.Columns[dri].ColumnName, str.ObjectToDecimal());
                                break;
                            case "Int32":
                                gbmodel.SetPropertyValue(dt.Columns[dri].ColumnName, int.Parse(str.ToString().Substring(0, str.ToString().IndexOf(".") > -1 ? str.ToString().IndexOf(".") : str.ToString().Length)));
                                break;
                            case "Int64":
                                gbmodel.SetPropertyValue(dt.Columns[dri].ColumnName, long.Parse(str.ToString().Substring(0, str.ToString().IndexOf(".") > -1 ? str.ToString().IndexOf(".") : str.ToString().Length)));
                                break;
                            case "DateTime":
                                gbmodel.SetPropertyValue(dt.Columns[dri].ColumnName, str.ObjectToDate());
                                break;
                            default:

                                break;

                        }
                        dri++;
                    }
                    lock (mygbtLock)
                    {
                        gbtlist.Add(gbmodel);
                    }


                }
                catch (Exception e)
                {

                    LogHelper.Error($"接收车辆数据报文报错,接收数量为{hexstrpartlist.Count}", e);
                    result.code = 500;
                    result.status = false;
                    result.msg = $"接收车辆数据报文报错,接收数量为{hexstrpartlist.Count}";
                    //本文唯一值得注意的就是这里，下文会阐述
                    //var client = TCPsocketClientCollection.Get(cachecode);
                    var client = TCPsocketClientCollection.Getgroupone(cachecode);

                    IByteBuffer resultbyte = Unpooled.CopiedBuffer(Encoding.Default.GetBytes(result.ToJson()));
                    client.WriteAndFlushAsync(resultbyte);
                }
            });
            #endregion
            //头部
            //var tran = base.AsSugarClient();
            try
            {
                ////datatable插入数据库 
                //List<Dictionary<string, object>> dc = base.Context.Utilities.DataTableToDictionaryList(dt);//5.0.23版本支持

                //List<Dictionary<string, object>> dcmcu = base.Context.Utilities.DataTableToDictionaryList(dtmcu);

                //List<Dictionary<string, object>> dcdcdy = base.Context.Utilities.DataTableToDictionaryList(dtdcdy);//5.0.23版本支持

                //List<Dictionary<string, object>> dcdcwd = base.Context.Utilities.DataTableToDictionaryList(dtdcwd);//5.0.23版本支持


                //tran.Ado.BeginTran();
                //base.Context.Insertable(dc).AS("vehicle_gbt32960").SplitTable().ExecuteCommand();
                //base.Context.Insertable(dcmcu).AS("vehicle_mculog").SplitTable().ExecuteCommand();
                //base.Context.Insertable(dcdcdy).AS("vehicle_batterydylog").SplitTable().ExecuteCommand();
                //base.Context.Insertable(dcdcwd).AS("vehicle_batterywdlog").SplitTable().ExecuteCommand();
                //base.Context.Storageable(updatevtslist).ExecuteCommand();
                //tran.Ado.CommitTran();
                _unitOfWork.BeginTran();
                //base.Context.Fastest<vehicle_gbt32960>().SplitTable().BulkCopy(gbtlist);

                db.Insertable(gbtlist).SplitTable().ExecuteReturnSnowflakeIdList();//插入返回雪花ID集合
                db.Insertable(gbtmculist).SplitTable().ExecuteReturnSnowflakeIdList();//插入返回雪花ID集合
                db.Insertable(gbtdylist).SplitTable().ExecuteReturnSnowflakeIdList();//插入返回雪花ID集合
                db.Insertable(gbtwdlist).SplitTable().ExecuteReturnSnowflakeIdList();//插入返回雪花ID集合
                db.Updateable<vehicle>()
                    .SetColumns(it => new vehicle() { isError = false })//类只能在表达示里面不能提取
                    .Where(it => vlist.Contains(it.VIN))
                    .ExecuteCommand();
                db.Storageable(updatevtslist).ExecuteCommand();
                _unitOfWork.CommitTran();
                foreach (string groupid in orglist)
                {
                    _ChatHub.Clients.Group(groupid).SendAsync("ReceiveError", "车辆出现新的故障报警请及时处理！");
                }
            }
            catch (Exception e)
            {
                LogHelper.Error($"datatable插入数据库出错", e);
                _unitOfWork.RollbackTran();
            }
            //}
            //本文唯一值得注意的就是这里，下文会阐述
            //IByteBuffer resultbyte = Unpooled.CopiedBuffer(Encoding.Default.GetBytes(result.ToJson()));
            //await client.SendAndFlush(resultbyte);

            return result;
        }
        #endregion


        #region 退出连接关闭通信通道redis缓存
        public async Task TCPSocketRemoveRedis(string LogToken)
        {
            //_lock.EnterWriteLock();
            //TCPsocketClientCollection.Remove(LogToken);
            //_lock.ExitWriteLock();
            await _cache.LockActionAsync($"Select{LogToken}", async () => {
                vehicle vehicle = await _cache.GetAsync<vehicle>(LogToken);
                if (vehicle != null)
                {
                    //Task t = Task.WhenAll(_cache.Remove(LogToken), _cache.DelSetAsync("TCPLoginvehicle", vehicle.VIN));
                    //t.Wait();
                    await _cache.Remove(LogToken);
                }
            });

        }
        #endregion

        /// <summary>
        /// 将List转换为DataTable
        /// </summary>
        /// <param name="list">请求数据</param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            //创建一个名为"tableName"的空表
            DataTable dt = new DataTable();

            //创建传入对象名称的列
            foreach (var item in list.FirstOrDefault().GetType().GetProperties())
            {
                dt.Columns.Add(item.Name);
            }
            //循环存储
            foreach (var item in list)
            {
                //新加行
                DataRow value = dt.NewRow();
                //根据DataTable中的值，进行对应的赋值
                foreach (DataColumn dtColumn in dt.Columns)
                {
                    int i = dt.Columns.IndexOf(dtColumn);
                    //基元元素，直接复制，对象类型等，进行序列化
                    if (value.GetType().IsPrimitive)
                    {
                        value[i] = item.GetType().GetProperty(dtColumn.ColumnName).GetValue(item);
                    }
                    else
                    {
                        value[i] = JsonConvert.SerializeObject(item.GetType().GetProperty(dtColumn.ColumnName).GetValue(item));
                    }
                }
                dt.Rows.Add(value);
            }
            return dt;
        }

        /// <summary>
        /// DataTable转Dictionary
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> DataTableToDictionary(DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    for (int i = 0; i < row.Table.Columns.Count; i++)
                    {
                        object obj = row[row.Table.Columns[i].ColumnName];
                        if (obj == DBNull.Value)
                        {
                            obj = null;
                        }

                        dictionary.Add(row.Table.Columns[i].ColumnName.ToString(), obj);
                    }

                    list.Add(dictionary);
                }
            }

            return list;
        }
    }
}
