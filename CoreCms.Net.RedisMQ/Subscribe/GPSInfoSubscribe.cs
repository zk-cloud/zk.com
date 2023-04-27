using AutoMapper;
using CoreCms.Net.Configuration;
using CoreCms.Net.IServices;
using CoreCms.Net.Model.Entities;
using CoreCms.Net.Model.FromDate;
using CoreCms.Net.Model.ViewModels.UI;
using CoreCms.Net.Utility;
using CoreCms.Net.Utility.Extensions;
using InitQ.Abstractions;
using InitQ.Attributes;
using System;
using System.Threading.Tasks;

namespace CoreCms.Net.RedisMQ
{
    public class GPSInfoSubscribe: IRedisSubscribe
    {
        private readonly Ivehicle_gpslocationServices _vehicle_gpslocationServices;
        private readonly IMapper _Mapper;

        public GPSInfoSubscribe(
            Ivehicle_gpslocationServices vehicle_gpslocationServices
            , IMapper Mapper
            )
        {
            _vehicle_gpslocationServices = vehicle_gpslocationServices;
            _Mapper = Mapper;
        }

        [Subscribe(RedisMessageQueueKey.GPSInfoUp)]
        public async Task GPSInfoUp(string msg)
        {
            string[] msgs = msg.Split("&&");
            FMGPSLocation dto = SerializeExtensions.ToObject<FMGPSLocation>(msgs[0]);
            vehicle vdto = SerializeExtensions.ToObject<vehicle>(msgs[1]);
            var LogToken = msgs[2];
            try
            {
                vehicle_gpslocation model = _Mapper.Map<vehicle_gpslocation>(dto);
                model.VIN = vdto.VIN;
                await _vehicle_gpslocationServices.InsertAsync(model);
            }
            catch (Exception e) {
                LogHelper.Error(e.Message);
                var client = Utility.YLQCHelper.WebsocketClientCollection.Get(LogToken);
                await client.SendMessageAsync("tcp服务器出错！");
                await Task.CompletedTask;
            }
        }
    }
}
