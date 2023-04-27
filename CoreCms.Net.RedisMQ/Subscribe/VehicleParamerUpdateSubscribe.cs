using CoreCms.Net.Caching.AutoMate.RedisCache;
using CoreCms.Net.Configuration;
using CoreCms.Net.IServices;
using CoreCms.Net.Model.Entities;
using CoreCms.Net.Model.FromDate;
using CoreCms.Net.Utility;
using CoreCms.Net.Utility.Extensions;
using CoreCms.Net.Utility.YLQCHelper;
using InitQ.Abstractions;
using InitQ.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCms.Net.RedisMQ
{
    public class VehicleParamerUpdateSubscribe: IRedisSubscribe
    {
        private readonly Ivehicle_SocketServices _vehicle_SocketServices;
        private readonly Ivehicle_TCPSocketServices _vehicle_TCPSocketServices;
        private readonly Ivehicle_terminalconfigureServices _vehicle_terminalconfigureServices;
        private readonly IRedisOperationRepository _cache;



        public VehicleParamerUpdateSubscribe(
            Ivehicle_SocketServices vehicle_SocketServices,
            Ivehicle_TCPSocketServices vehicle_TCPSocketServices,
            Ivehicle_terminalconfigureServices vehicle_terminalconfigureServices,
        IRedisOperationRepository cache
            ) 
        {
            _vehicle_TCPSocketServices = vehicle_TCPSocketServices;
            _vehicle_SocketServices = vehicle_SocketServices;
            _vehicle_terminalconfigureServices = vehicle_terminalconfigureServices;
            _cache = cache;
        }

        [Subscribe(RedisMessageQueueKey.SocketVehicleParameterUp)]
        public async Task SocketVehicleParameterUp(string msg)
        {
            string[] msgs = msg.Split("&&");
            FMVehicleParameter dto= SerializeExtensions.ToObject<FMVehicleParameter>(msgs[0]);
            vehicle vdto = SerializeExtensions.ToObject<vehicle>(msgs[1]);
            await _vehicle_SocketServices.SocketVehicleParameterUp(dto, vdto,msgs[2]);
        }

        [Subscribe(RedisMessageQueueKey.TCPSocketVehicleParameterUp)]
        public async Task TCPSocketVehicleParameterUp(string msg)
        {
            string[] msgs = msg.Split("&&");
            FMVehicleParameter dto = SerializeExtensions.ToObject<FMVehicleParameter>(msgs[0]);
            vehicle vdto = SerializeExtensions.ToObject<vehicle>(msgs[1]);
            await _vehicle_TCPSocketServices.SocketVehicleParameterUp(dto, vdto, msgs[2]);
        }

        [Subscribe(RedisMessageQueueKey.TCPSocketGBT32960ParameterUp)]
        public async Task TCPSocketGBT32960ParameterUp(string msg)
        {
            
                List<string> strlist = msg.Split('&').ToList();
                string hexstr = strlist[0];
                string cachecode = strlist[1];
                var list = _vehicle_terminalconfigureServices.QueryListByClause(p => p.MsgID.StartsWith("GBT32960"));
                string path = AppDomain.CurrentDomain.BaseDirectory + "";
                string hexstrpart = "";
                List<string> list1 = new List<string>();
                try
                {

                    //判断起始字符是否存在
                    string beginstr = hexstr.Substring(0, 4);
                    string bstr = YLQCHelper.HexStringToASCII(beginstr);
                    if (bstr != "##")
                    {
                        LogHelper.Info($"传入数据格式起始符不正确!数据为：{hexstr}");
                        return;
                    }
                    var vehicleresult = new vehicle();
                    //输出基础信息内容
                    //sheet1.CreateRow(1).CreateCell(0).SetCellValue("起始：");
                    //sheet1.GetRow(1).CreateCell(1).SetCellValue(beginstr);
                    //sheet1.GetRow(1).CreateCell(2).SetCellValue("--");
                    //sheet1.GetRow(1).CreateCell(3).SetCellValue(bstr);
                    //命令行内容
                    string MLtr = hexstr.Substring(4, 2);
                    string mstr = "";
                    switch (MLtr)
                    {
                        case "01":
                            mstr = "车辆登入";
                            hexstrpart = hexstr.Substring(48, 60);
                            break;
                        case "02":
                            mstr = "实时信息上报";
                            hexstrpart = hexstr.Substring(48, hexstr.Length - 50);
                            break;
                        case "03":
                            mstr = "补发信息上报";
                            hexstrpart = hexstr.Substring(48, hexstr.Length - 50);
                            break;
                        case "04":
                            mstr = "车辆登出";
                            hexstrpart = hexstr.Substring(48, 16);
                            break;

                        default:
                            mstr = "未知命令！";
                            break;
                    }
                    //应答行内容
                    string YDstr = hexstr.Substring(6, 2);
                    string ystr = "";
                    switch (YDstr.ToUpper())
                    {
                        case "01":
                            ystr = "成功";
                            break;
                        case "02":
                            ystr = "错误";
                            break;
                        case "03":
                            ystr = "VIN重复";
                            break;
                        case "FE":
                            ystr = "命令";
                            break;

                        default:
                            ystr = "未知命令！";
                            break;
                    }

                    string VINstr = hexstr.Substring(8, 34);
                    string VIN = YLQCHelper.HexStringToASCII(VINstr);


                    string JMstr = hexstr.Substring(42, 2);

                    string Lengthstr = hexstr.Substring(44, 4);
                    if (YLQCHelper.HexStringToDemicel(Lengthstr) != hexstr.Length / 2 - 25)
                    {
                        LogHelper.Info($"传入数据长度不符!数据为：{hexstr}");
                        return;
                    }


                    //这么用有个前提，那就是第0行还没创建过，否则得这么用：

                    //sheet1.GetRow(0).CreateCell(0).SetCellValue("This is a Sample");


                    #region 实时信息上报
                    //if (MLtr == "02" || MLtr == "03")
                    //{
                    vehicleresult = SerializeExtensions.ToObject<vehicle>(_cache.Get(cachecode).Result);
                    if (VIN != vehicleresult.VIN)
                    {
                        LogHelper.Info($"传入数据VIN与登录验证不符!数据为：{hexstr}");
                        return;
                    }

                    await _vehicle_TCPSocketServices.TCPSocketVehicleParameterUp(hexstrpart, cachecode, VIN, list);



                    //}
                    #endregion



                }
                catch (Exception ex)
                {
                    LogHelper.Error($"TCP解析失败：hexstrpart:{strlist[0]}", ex);
                }

        }

        [Subscribe(RedisMessageQueueKey.TCPSocketGBT32960Login)]
        public async Task TCPSocketGBT32960Login(string msg) {
            List<string> strlist = msg.Split('&').ToList();
            string hexstr = strlist[0];
            string cachecode = strlist[1];

            var list = _vehicle_terminalconfigureServices.QueryListByClause(p => p.MsgID.StartsWith("GBT32960"));
            string hexstrpart = "";
            List<string> list1 = new List<string>();

            try
            {

                //判断起始字符是否存在
                string beginstr = hexstr.Substring(0, 4);
                string bstr = YLQCHelper.HexStringToASCII(beginstr);
                if (bstr != "##")
                {
                    LogHelper.Info($"传入数据格式起始符不正确!数据为：{hexstr}");
                    return;
                }
                var vehicleresult = new vehicle();
                //输出基础信息内容
                //sheet1.CreateRow(1).CreateCell(0).SetCellValue("起始：");
                //sheet1.GetRow(1).CreateCell(1).SetCellValue(beginstr);
                //sheet1.GetRow(1).CreateCell(2).SetCellValue("--");
                //sheet1.GetRow(1).CreateCell(3).SetCellValue(bstr);
                //命令行内容
                string MLtr = hexstr.Substring(4, 2);
                string mstr = "";
                switch (MLtr)
                {
                    case "01":
                        mstr = "车辆登入";
                        hexstrpart = hexstr.Substring(48, 60);
                        break;
                    case "02":
                        mstr = "实时信息上报";
                        hexstrpart = hexstr.Substring(48, hexstr.Length - 50);
                        break;
                    case "03":
                        mstr = "补发信息上报";
                        hexstrpart = hexstr.Substring(48, hexstr.Length - 50);
                        break;
                    case "04":
                        mstr = "车辆登出";
                        hexstrpart = hexstr.Substring(48, 16);
                        break;

                    default:
                        mstr = "未知命令！";
                        break;
                }
                //应答行内容
                string YDstr = hexstr.Substring(6, 2);
                string ystr = "";
                switch (YDstr.ToUpper())
                {
                    case "01":
                        ystr = "成功";
                        break;
                    case "02":
                        ystr = "错误";
                        break;
                    case "03":
                        ystr = "VIN重复";
                        break;
                    case "FE":
                        ystr = "命令";
                        break;

                    default:
                        ystr = "未知命令！";
                        break;
                }

                string VINstr = hexstr.Substring(8, 34);
                string VIN = YLQCHelper.HexStringToASCII(VINstr);


                string JMstr = hexstr.Substring(42, 2);

                string Lengthstr = hexstr.Substring(44, 4);
                if (YLQCHelper.HexStringToDemicel(Lengthstr) != hexstr.Length / 2 - 25)
                {
                    LogHelper.Info($"传入数据长度不符!数据为：{hexstr}");
                    return;
                }

                //bbc校验编码
                string bbccode = YLQCHelper.HexToBBCString(hexstr.Substring(4, hexstr.Length - 6));
                hexstr = hexstr.Substring(0, hexstr.Length-2) + bbccode;


                //这么用有个前提，那就是第0行还没创建过，否则得这么用：

                //sheet1.GetRow(0).CreateCell(0).SetCellValue("This is a Sample");

                #region 登录包体解析
                //if (MLtr == "01")
                //{

                List<vehicle_terminalconfigure> listlogin = list.FindAll(p => p.MsgID == "GBT32960-01");
                    await _vehicle_TCPSocketServices.SocketLoginAsync(hexstrpart, listlogin, VIN, cachecode);
                //}
                #endregion


            }
            catch (Exception ex)
            {
                

                LogHelper.Error($"TCP登入报错：" + ex.Message, ex);
            }
        }

        [Subscribe(RedisMessageQueueKey.TCPSocketGBT32960Logout)]
        public async Task TCPSocketGBT32960Logout(string msg)
        {
            List<string> strlist = msg.Split('&').ToList();
            string hexstr = strlist[0];
            string cachecode = strlist[1];
            var list = _vehicle_terminalconfigureServices.QueryListByClause(p => p.MsgID.StartsWith("GBT32960"));
            string hexstrpart = "";
            List<string> list1 = new List<string>();
            try
            {

                //判断起始字符是否存在
                string beginstr = hexstr.Substring(0, 4);
                string bstr = YLQCHelper.HexStringToASCII(beginstr);
                if (bstr != "##")
                {
                    LogHelper.Info($"传入数据格式起始符不正确!数据为：{hexstr}");
                    return;
                }
                var vehicleresult = new vehicle();
                //输出基础信息内容
                //sheet1.CreateRow(1).CreateCell(0).SetCellValue("起始：");
                //sheet1.GetRow(1).CreateCell(1).SetCellValue(beginstr);
                //sheet1.GetRow(1).CreateCell(2).SetCellValue("--");
                //sheet1.GetRow(1).CreateCell(3).SetCellValue(bstr);
                //命令行内容
                string MLtr = hexstr.Substring(4, 2);
                string mstr = "";
                switch (MLtr)
                {
                    case "01":
                        mstr = "车辆登入";
                        hexstrpart = hexstr.Substring(48, 60);
                        break;
                    case "02":
                        mstr = "实时信息上报";
                        hexstrpart = hexstr.Substring(48, hexstr.Length - 50);
                        break;
                    case "03":
                        mstr = "补发信息上报";
                        hexstrpart = hexstr.Substring(48, hexstr.Length - 50);
                        break;
                    case "04":
                        mstr = "车辆登出";
                        hexstrpart = hexstr.Substring(48, 16);
                        break;

                    default:
                        mstr = "未知命令！";
                        break;
                }
                //应答行内容
                string YDstr = hexstr.Substring(6, 2);
                string ystr = "";
                switch (YDstr.ToUpper())
                {
                    case "01":
                        ystr = "成功";
                        break;
                    case "02":
                        ystr = "错误";
                        break;
                    case "03":
                        ystr = "VIN重复";
                        break;
                    case "FE":
                        ystr = "命令";
                        break;

                    default:
                        ystr = "未知命令！";
                        break;
                }

                string VINstr = hexstr.Substring(8, 34);
                string VIN = YLQCHelper.HexStringToASCII(VINstr);


                string JMstr = hexstr.Substring(42, 2);

                string Lengthstr = hexstr.Substring(44, 4);
                if (YLQCHelper.HexStringToDemicel(Lengthstr) != hexstr.Length / 2 - 25)
                {
                    LogHelper.Info($"传入数据长度不符!数据为：{hexstr}");
                    return;
                }

                //bbc校验编码
                string bbccode = YLQCHelper.HexToBBCString(hexstr.Substring(4, hexstr.Length - 6));
                hexstr = hexstr.Substring(0, hexstr.Length -2) + bbccode;


                //这么用有个前提，那就是第0行还没创建过，否则得这么用：

                //sheet1.GetRow(0).CreateCell(0).SetCellValue("This is a Sample");



                #region 登出包体解析
                //else if (MLtr == "04")
                //{
                vehicleresult = SerializeExtensions.ToObject<vehicle>(_cache.Get(cachecode).Result);
                    if (VIN != vehicleresult.VIN)
                    {
                        LogHelper.Info($"传入数据VIN与登录验证不符!数据为：{hexstr}");
                        return;
                    }
                    List<vehicle_terminalconfigure> listlogin = list.FindAll(p => p.MsgID == "GBT32960-03-01");
                    await _vehicle_TCPSocketServices.TCPSocketLogout(hexstrpart, listlogin, VIN, cachecode);
                    //var client = TCPsocketClientCollection.Get(cachecode);
                    //await client.Channel.CloseAsync();
                    var client = TCPsocketClientCollection.Getgroupone(cachecode);
                    await client.CloseAsync();
                //}

                #endregion

            }
            catch (Exception ex)
            {

                LogHelper.Error($"TCP登出报错：" + ex.Message, ex);
            }
        }

        [Subscribe(RedisMessageQueueKey.TCPSocketLoss)]
        public async Task TCPSocketLoss(string msg)
        {
            try {
                await _vehicle_TCPSocketServices.TCPSocketRemoveRedis(msg);
            }
            catch (Exception ex)
            {

                LogHelper.Error($"TCP连接关闭报错：" + ex.Message, ex);
            }
        }
    }
}
