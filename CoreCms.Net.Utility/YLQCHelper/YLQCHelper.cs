using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreCms.Net.Model.Entities;
using CoreCms.Net.Model.TCPSocket;

namespace CoreCms.Net.Utility.YLQCHelper
{
    public class YLQCHelper
    {
        #region Msb
        /// <summary>
        /// 字符转换为2进制数组(MSB)
        /// </summary>
        /// <param name="strbytes"></param>
        /// <returns></returns>
        public static string HexadecimalToBinaryArrayListMsb(string strbytes) {
            var list = strbytes.Trim().Split(" ");
            string str = "";
            foreach (string strbyte in list) {
                str += HexadecimalToBinary(Convert.ToInt32(strbyte, 16));
            }
            return str;
        }





        #endregion

        #region Lsb
        /// <summary>
        /// 字符转换为2进制数组
        /// </summary>
        /// <param name="strbytes"></param>
        /// <returns></returns>
        public static string HexadecimalToBinaryArrayListLsb(string strbytes)
        {
            var list = strbytes.Trim().Split(" ");
            Array.Reverse(list);
            string str = "";
            foreach (string strbyte in list)
            {
                var strb = HexadecimalToBinary(Convert.ToInt32(strbyte, 16));
                strb = strb.PadRight(8,'0');
                str += strb;
            }
            return str;
        }



        #endregion

        #region 百度地图坐标点转换

        const double pi = 3.14159265358979324;
        //  
        // Krasovsky 1940  
        //   
        // a = 6378245.0, 1/f = 298.3   
        // b = a * (1 - f)  
        // ee = (a^2 - b^2) / a^2;   
        const double a = 6378245.0;
        const double ee = 0.00669342162296594323;
        static bool outOfChina(double lat, double lon)
        {
            if (lon < 72.004 || lon > 137.8347)
                return true;
            if (lat < 0.8293 || lat > 55.8271)
                return true;
            return false;
        }
        static double transformLat(double x, double y)
        {
            double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y * pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }
        static double transformLon(double x, double y)
        {
            double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0 * pi)) * 2.0 / 3.0;
            return ret;
        }
        /* 
        参数 
        wgLat:WGS-84纬度wgLon:WGS-84经度 
        返回值： 
        mgLat：GCJ-02纬度mgLon：GCJ-02经度 
        */
        public static void gps_transform(double wgLat, double wgLon, out double mgLat, out double mgLon)
        {
            if (outOfChina(wgLat, wgLon))
            {
                mgLat = wgLat;
                mgLon = wgLon;
                return;
            }
            double dLat = transformLat(wgLon - 105.0, wgLat - 35.0);
            double dLon = transformLon(wgLon - 105.0, wgLat - 35.0);
            double radLat = wgLat / 180.0 * pi; double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic; double sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            mgLat = wgLat + dLat; mgLon = wgLon + dLon;
        }


        const double x_pi = 3.14159265358979324 * 3000.0 / 180.0;
        //将 GCJ-02 坐标转换成 BD-09 坐标  
        public static void bd_encrypt(double gg_lat, double gg_lon, out double bd_lat, out double bd_lon)
        {
            double x = gg_lon, y = gg_lat;
            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * x_pi);
            bd_lon = z * Math.Cos(theta) + 0.0065;
            bd_lat = z * Math.Sin(theta) + 0.006;
        }

        //将BD-09  坐标转换成 GCJ-02 坐标 
        public static void bd_decrypt(double bd_lat, double bd_lon, out double gg_lat, out double gg_lon)
        {
            double x = bd_lon - 0.0065, y = bd_lat - 0.006;
            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_pi);
            gg_lon = z * Math.Cos(theta);
            gg_lat = z * Math.Sin(theta);
        }


        /// <summary>
        /// 字符串bbc异或校验编码
        /// </summary>
        /// <returns></returns>
        public static string HexToBBCString(string hexstr) {
            // BCC和校验代码返回16进制
            byte CheckCode = 0;
            var chardata = HexStringToBinary(hexstr);
            int len = chardata.Length;
            for (int a = 0; a < len; a++)
            {
                CheckCode ^= chardata[a];
            }
            string bbccode = Convert.ToString(CheckCode, 16).ToLower();
            return bbccode;
        }

        #endregion


        /// <summary>
        /// 16进制转换为2进制（倒置）
        /// </summary>
        /// <returns></returns>
        public static string HexadecimalToBinary(int num)
        {
            return new string(Convert.ToString(num, 2).ToCharArray().Reverse<char>().ToArray<char>());
        }

        /// <summary>
        /// 2进制转换为10进制并满足漂移量
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="Resolution"></param>
        /// <param name="Offset"></param>
        /// <returns></returns>
        public static decimal BinaryArrayToDecimal(string bytes,int length, int beginbit,int bitlength, decimal Resolution, decimal Offset, TransmissionAgreement ta)
        {
            if (ta == TransmissionAgreement.MSB)
            {
                bytes = new string(bytes.Substring(beginbit, bitlength).ToCharArray().Reverse<char>().ToArray<char>());
                return Convert.ToInt32(bytes, 2) * Resolution + Offset;
            }
            else if (ta == TransmissionAgreement.LSB)
            {
                int bytecount = length / 8;
                int byteindex = beginbit / 8;
                int bitindex = beginbit % 8;
                bytes = new string(bytes.Substring((bytecount - byteindex - 1) * 8 + bitindex, bitlength).ToCharArray().Reverse<char>().ToArray<char>());
                return Convert.ToInt32(bytes, 2) * Resolution + Offset;
            }
            else {
                return 0;
            }
        }

        /// <summary>
        /// 批量转换16进制到时间
        /// </summary>
        /// <param name="hexstr"></param>
        /// <param name="list"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static List<string> TCPHexStrToStrArray(string hexstr,List<vehicle_terminalconfigure> list, int i = 1) {
            
            List<string> strlist = new List<string>();
            try
            {
                
                foreach (vehicle_terminalconfigure vt in list)
                {
                    
                    string str = hexstr.Substring(vt.StartBit * 2, vt.BitLength * 2);
                    if (i == 0)
                    {
                        string infodate = "";
                        for (int j = 0; j < 6; j++)
                        {
                            if (j < 2)
                            {
                                infodate += YLQCHelper.HexStringToDemicel(str.Substring(j * 2, 2)).ToString().PadLeft(2, '0') + "-";
                            }
                            else if (j == 2)
                            {
                                infodate += YLQCHelper.HexStringToDemicel(str.Substring(j * 2, 2)).ToString().PadLeft(2, '0') + " ";
                            }
                            else
                            {
                                infodate += YLQCHelper.HexStringToDemicel(str.Substring(j * 2, 2)).ToString().PadLeft(2, '0') + ":";
                            }
                        }
                        str = $"{vt.SignalDescription};{str};{infodate}";
                        i++;
                    }
                    else
                    {
                        str = $"{vt.SignalDescription};{str};{(vt.Resolution.HasValue ? (vt.Resolution.Value!= 0 ? "十进制：" + (Convert.ToInt32(str, 16) * vt.Resolution.Value + vt.Offset) : YLQCHelper.HexStringToASCII(str)):Convert.ToString(int.Parse(str), 2))}";
                        i++;
                    }
                    strlist.Add(str);
                }
            }
            catch (Exception e) {
                LogHelper.Error($"错误hexstr：{hexstr};configid：{list[0].MsgID}");
            }
            return strlist;
        }


        public static string TCPHexStrToDeString(string str, vehicle_terminalconfigure vt)
        {
            string destr = "";
            List<string> strlist = new List<string>();
            try
            {

                destr = vt.Resolution.HasValue ? (vt.Resolution.Value != 0 ? (Convert.ToInt32(str, 16) * vt.Resolution.Value + vt.Offset).ToString() : str) : Convert.ToString(Convert.ToInt32(str, 16), 2).PadLeft(8 * vt.BitLength, '0');
            }
            catch (Exception e)
            {
                LogHelper.Error($"错误hexstr：{str};configid：{vt.MsgID}");
            }
            return destr;
        }

        /// <summary>
        /// 批量转换16进制到10进制数字符串
        /// </summary>
        /// <param name="hexstr"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<string> TCPHexStrToDeString(string hexstr, List<vehicle_terminalconfigure> list)
        {

            List<string> strlist = new List<string>();
            try
            {
                foreach (vehicle_terminalconfigure vt in list)
                {
                    string str = hexstr.Substring(vt.StartBit * 2, vt.BitLength * 2);
                    str = vt.Resolution.HasValue? (vt.Resolution.Value != 0?(Convert.ToInt32(str, 16) * vt.Resolution.Value + vt.Offset).ToString(): str): Convert.ToString(Convert.ToInt32(str, 16), 2).PadLeft(8* vt.BitLength, '0');
                    strlist.Add(str);
                }
            }
            catch (Exception e)
            {
                LogHelper.Error($"错误hexstr：{hexstr};configid：{list[0].MsgID}");
            }
            return strlist;
        }

        public static Dictionary<string, List<string>> TCPHexStrToDeDictionary(string hexstr, List<vehicle_terminalconfigure> list)
        {

            Dictionary<string, List<string>> strlist=new Dictionary<string, List<string>>();
            try
            {
                foreach (vehicle_terminalconfigure vt in list)
                {
                    string key = hexstr.Substring(vt.StartBit * 2, vt.BitLength * 2);
                    var str = vt.Resolution.Value != 0 ? (Convert.ToInt32(key, 16) * vt.Resolution.Value + vt.Offset).ToString() : key;
                    strlist.Add(key, new List<string>{ vt.SignalDescription,str});
                }
            }
            catch (Exception e)
            {
                LogHelper.Error($"错误hexstr：{hexstr};configid：{list[0].MsgID}");
            }
            return strlist;
        }

        /// <summary>
        /// 将一条十六进制字符串转换为ASCII
        /// </summary>
        /// <param name="hexstring">一条十六进制字符串</param>
        /// <returns>返回一条ASCII码</returns>
        public static string HexStringToASCII(string hexstring)
        {
            byte[] bt = HexStringToBinary(hexstring);
            string lin = "";
            for (int i = 0; i < bt.Length; i++)
            {
                lin = lin + bt[i] + " ";
            }


            string[] ss = lin.Trim().Split(new char[] { ' ' });
            char[] c = new char[ss.Length];
            int a;
            for (int i = 0; i < c.Length; i++)
            {
                a = Convert.ToInt32(ss[i]);
                c[i] = Convert.ToChar(a);
            }

            string b = new string(c);
            return b;
        }

        /**/
        /// <summary>
        /// 16进制字符串转换为二进制数组
        /// </summary>
        /// <param name="hexstring">用空格切割字符串</param>
        /// <returns>返回一个二进制字符串</returns>
        public static byte[] HexStringToBinary(string hexstring)
        {
            List<string> tmpary = new List<string>();
            for (int i = 0; i < hexstring.Length/2;i++) {
                tmpary.Add(hexstring.Substring(2*i,2));
            }
            byte[] buff = new byte[tmpary.Count];
            for (int i = 0; i < buff.Length; i++)
            {
                buff[i] = Convert.ToByte(tmpary[i], 16);
            }
            return buff;
        }

        /// <summary>
        /// 16进制转换为10进制
        /// </summary>
        /// <param name="hexstring"></param>
        /// <returns></returns>
        public static int HexStringToDemicel(string hexstring)
        {
            return System.Convert.ToInt32(hexstring, 16);
        }

        /// <summary>
        /// 时间描述（xxx时xx分xx秒）
        /// </summary>
        /// <param name="sec"></param>
        /// <returns></returns>
        public static string SecondtoDate(long sec)
        {
            Int64 shi;
            Int64 fen;
            Int64 miao;
            if (sec < 0)
                sec = 0;
            miao = sec % 60;
            sec = sec - miao;
            sec /= 60;
            fen = sec % 60;
            sec -= fen;
            shi = sec / 60;
            return string.Format("{0:000} 时 {1:00} 分 {2:00} 秒", shi, fen, miao);
        }
    }
}
