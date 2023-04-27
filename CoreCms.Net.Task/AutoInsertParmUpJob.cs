using CoreCms.Net.Caching;
using CoreCms.Net.Caching.AutoMate.RedisCache;
using CoreCms.Net.Configuration;
using CoreCms.Net.IServices;
using System.Collections.Generic;

namespace CoreCms.Net.Task
{
    public class AutoInsertParmUpJob
    {

        private readonly Ivehicle_TCPSocketServices _Ivehicle_TCPSocketServices;
        private readonly IRedisOperationRepository _cache;


        public AutoInsertParmUpJob(Ivehicle_TCPSocketServices Ivehicle_TCPSocketServices, IRedisOperationRepository cache) {
            _Ivehicle_TCPSocketServices = Ivehicle_TCPSocketServices;
            _cache = cache;
        }

        public async System.Threading.Tasks.Task Execute() {
            long lenght = await _cache.ListLengthAsync(RedisMessageQueueKey.TCPSocketGBT32960ParameterBatchUp);
            List<string> stringlist = new List<string>();
            for (long i= 0;i < lenght;i++) {
                stringlist.Add(await _cache.ListRightPopAsync(RedisMessageQueueKey.TCPSocketGBT32960ParameterBatchUp));
            }
            await _Ivehicle_TCPSocketServices.TCPSocketVehicleParameterBatchUp(stringlist);
        }

    }
}
