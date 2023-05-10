using BaiduMapAPI.APIs.LogisticsDirection.V1;
using BaiduMapAPI.APIs.Place.V2;
using BaiduMapAPI.Models;
using CoreCms.Net.Configuration;
using CoreCms.Net.Model.ViewModels.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoreCms.Net.Web.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaiduMapController : ControllerBase
    {
        #region 正地理编码
        /// <summary>
        /// 正地理编码
        /// </summary>
        /// <param name="address">位置</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<WebApiCallBack> Geocoding(string address)
        {
            var jm = new WebApiCallBack();

            BaiduMapAPI.APIs.Geocoding.V3.Get search = new BaiduMapAPI.APIs.Geocoding.V3.Get()
            {
                Address = address,
                City = "柳州",
            };

            var result = await search.GetResultAsync(AppSettingsConstVars.BaiduMapAPIAK, "");
            jm.msg = result.Message;
            jm.code = result.Status;
            jm.status = result.Status == 0 ? true : false;
            jm.data = result;

            return jm;
        }
        #endregion

        #region 逆地理编码
        /// <summary>
        /// 逆地理编码
        /// </summary>
        /// <param name="Lng">纬度</param>
        /// <param name="Lat">经度</param>
        /// <returns></returns>
        [HttpGet]
        public WebApiCallBack ReverseGeocoding(double Lng, double Lat)
        {
            var jm = new WebApiCallBack();

            Location location = new Location();
            location.Lat = Lat;
            location.Lng = Lng;
            BaiduMapAPI.APIs.ReverseGeocoding.V3.Get search = new BaiduMapAPI.APIs.ReverseGeocoding.V3.Get()
            {
                Location = location,
                Coordtype = BaiduMapAPI.Models.Enums.CoordType.wgs84ll//GPS坐标

            };

            var result = search.GetResult(AppSettingsConstVars.BaiduMapAPIAK, "");
            jm.msg = result.Message;
            jm.code = result.Status;
            jm.status = result.Status == 0 ? true : false;
            jm.data = result;

            return jm;
        }
        #endregion

        #region 货车路线规划
        /// <summary>
        /// 货车路线规划
        /// </summary>
        /// <param name="origin">起点（经度,纬度），例如：113.077553,39.987654</param>
        /// <param name="destination">终点（经度,纬度），例如：113.077553,39.987654</param>
        /// <param name="waypoints">途径点（经度,纬度|经度,纬度），例如：113.077553,39.987654|113.833762,39.713354|...</param>
        /// <param name="tactics">驾驶政策（0-默认时间最少,1-距离优先,3-少走高速,7-经济优先）</param>
        /// <returns></returns>
        [HttpGet]
        public WebApiCallBack LogisticsDirection(string origin, string destination, string waypoints, string tactics)
        {
            var jm = new WebApiCallBack();
            BaiduMapAPI.Models.Enums.LogisticsDirectionTruckTactics tac;
            switch (tactics)
            {
                case "0": tac = BaiduMapAPI.Models.Enums.LogisticsDirectionTruckTactics.Normal;
                    break;
                case "1": tac = BaiduMapAPI.Models.Enums.LogisticsDirectionTruckTactics.ShortWay;
                    break;
                case "3": tac = BaiduMapAPI.Models.Enums.LogisticsDirectionTruckTactics.Less_HightWay; 
                    break;
                case "7": tac = BaiduMapAPI.Models.Enums.LogisticsDirectionTruckTactics.LessFee;
                    break;
                default : tac = BaiduMapAPI.Models.Enums.LogisticsDirectionTruckTactics.Normal;
                    break;
            }

            Location originlocation = new Location();
            originlocation.Lat = double.Parse(origin.Split(",")[0]);
            originlocation.Lng = double.Parse(origin.Split(",")[1]);
            Location destinationlocation = new Location();
            destinationlocation.Lat = double.Parse(destination.Split(",")[0]);
            destinationlocation.Lng = double.Parse(destination.Split(",")[1]);
            var waypointsList = new List<Location>();
            if (!string.IsNullOrEmpty(waypoints))
            {
                var arr = waypoints.Split("|");
                foreach (var item in arr)
                {
                    Location location = new Location();
                    location.Lat = double.Parse(item.Split(",")[0]);
                    location.Lng = double.Parse(item.Split(",")[1]);
                    waypointsList.Add(location);
                }
            }

            Regex reg = new Regex(@"[\u4e00-\u9fa5]");

            BaiduMapAPI.APIs.LogisticsDirection.V1.Truck search = new BaiduMapAPI.APIs.LogisticsDirection.V1.Truck()
            {
                Origin = originlocation,//起始点
                Destination = destinationlocation,//终点
                WayPoints = waypointsList,//途径点
                Tactics = tac,//驾驶政策
                PlateColor = BaiduMapAPI.Models.Enums.PlateColor.Green,//绿色牌照
                PowerType = BaiduMapAPI.Models.Enums.PowerType.Electric//纯电
            };

            var result = search.GetResult(AppSettingsConstVars.BaiduMapAPIAK, "");
            jm.msg = result.Message;
            jm.code = result.Status;
            jm.status = result.Status == 0 ? true : false;
            jm.data = result;

            return jm;

        }
        #endregion

        #region 地点输入提示
        /// <summary>
        /// 地点输入提示
        /// </summary>
        /// <param name="query">输入查询地点</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<WebApiCallBack> Place(string query)
        {
            var jm = new WebApiCallBack();
            BaiduMapAPI.APIs.Place.V2.Suggestion search = new BaiduMapAPI.APIs.Place.V2.Suggestion()
            {
                Query = query,
                Region = "柳州市"
            };

            var result = await search.GetResultAsync(AppSettingsConstVars.BaiduMapAPIAK, "");
            jm.msg = result.Message;
            jm.code = result.Status;
            jm.status = result.Status == 0 ? true : false;
            jm.data = result;

            return jm;
        }
        #endregion

    }
}
