
using CoreCms.Net.Utility;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoreCms.Net.Caching
{
    public class RedisOperationRepository : IRedisOperationRepository
    {
        private readonly ILogger<RedisOperationRepository> _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisOperationRepository(ILogger<RedisOperationRepository> logger, ConnectionMultiplexer redis)
        {
            _logger = logger;
            _redis = redis;
            _database = redis.GetDatabase();
        }

        private IServer GetServer()
        {
            var endpoint = _redis.GetEndPoints();
            return _redis.GetServer(endpoint.First());
        }

        public async Task Clear()
        {
            foreach (var endPoint in _redis.GetEndPoints())
            {
                var server = GetServer();
                foreach (var key in server.Keys())
                {
                    await _database.KeyDeleteAsync(key);
                }
            }
        }

        public async Task<bool> Exist(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task<string> Get(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task Remove(string key)
        {
            try
            {

                await _database.KeyDeleteAsync(key);
                await Task.CompletedTask;

            }
            catch (Exception e) {
                LogHelper.Error("redis删除键不成功！",e);
            }

        }

        public async Task Set(string key, object value, TimeSpan cacheTime)
        {
            if (value != null)
            {
                //序列化，将object值生成RedisValue
                await _database.StringSetAsync(key, JsonConvert.SerializeObject(value), cacheTime);
            }
        }

        public TEntity Get<TEntity>(string key)
        {
            var value =  _database.StringGet(key);
            if (value.HasValue)
            {
                //需要用的反序列化，将Redis存储的Byte[]，进行反序列化
                return JsonConvert.DeserializeObject<TEntity>(value);
            }
            else
            {
                return default;
            }
        }

        public async Task<TEntity> GetAsync<TEntity>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.HasValue)
            {
                //需要用的反序列化，将Redis存储的Byte[]，进行反序列化
                return JsonConvert.DeserializeObject<TEntity>(value);
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// 根据key获取RedisValue
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<RedisValue[]> ListRangeAsync(string redisKey)
        {
            return await _database.ListRangeAsync(redisKey);
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync(string redisKey, string redisValue)
        {
            return await _database.ListLeftPushAsync(redisKey, redisValue);
        }
        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync(string redisKey, string redisValue)
        {
            return await _database.ListRightPushAsync(redisKey, redisValue);
        }

        /// <summary>
        /// 在列表尾部插入数组集合。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync(string redisKey, IEnumerable<string> redisValue)
        {
            var redislist = new List<RedisValue>();
            foreach (var item in redisValue)
            {
                redislist.Add(item);
            }
            return await _database.ListRightPushAsync(redisKey, redislist.ToArray());
        }


        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素  反序列化
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<T> ListLeftPopAsync<T>(string redisKey) where T : class
        {
            var cacheValue = await _database.ListLeftPopAsync(redisKey);
            if (string.IsNullOrEmpty(cacheValue)) return null;
            var res = JsonConvert.DeserializeObject<T>(cacheValue);
            return res;
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素   反序列化
        /// 只能是对象集合
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<T> ListRightPopAsync<T>(string redisKey) where T : class
        {
            var cacheValue = await _database.ListRightPopAsync(redisKey);
            if (string.IsNullOrEmpty(cacheValue)) return null;
            var res = JsonConvert.DeserializeObject<T>(cacheValue);
            return res;
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素   
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<string> ListLeftPopAsync(string redisKey)
        {
            return await _database.ListLeftPopAsync(redisKey);
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素   
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<string> ListRightPopAsync(string redisKey)
        {
            return await _database.ListRightPopAsync(redisKey);
        }

        /// <summary>
        /// 列表长度
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<long> ListLengthAsync(string redisKey)
        {
            return await _database.ListLengthAsync(redisKey);
        }

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> ListRangeAsync(string redisKey, int db = -1)
        {
            var result = await _database.ListRangeAsync(redisKey);
            return result.Select(o => o.ToString());
        }

        /// <summary>
        /// 根据索引获取指定位置数据
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> ListRangeAsync(string redisKey, int start, int stop)
        {
            var result = await _database.ListRangeAsync(redisKey, start, stop);
            return result.Select(o => o.ToString());
        }

        /// <summary>
        /// 删除List中的元素 并返回删除的个数
        /// </summary>
        /// <param name="redisKey">key</param>
        /// <param name="redisValue">元素</param>
        /// <param name="type">大于零 : 从表头开始向表尾搜索，小于零 : 从表尾开始向表头搜索，等于零：移除表中所有与 VALUE 相等的值</param>
        /// <returns></returns>
        public async Task<long> ListDelRangeAsync(string redisKey, string redisValue, long type = 0)
        {
            return await _database.ListRemoveAsync(redisKey, redisValue, type);
        }

        /// <summary>
        /// 清空List
        /// </summary>
        /// <param name="redisKey"></param>
        public async Task ListClearAsync(string redisKey)
        {
            await _database.ListTrimAsync(redisKey, 1, 0);
        }


        /// <summary>
        /// 有序集合/定时任务延迟队列用的多
        /// </summary>
        /// <param name="redisKey">key</param>
        /// <param name="redisValue">元素</param>
        /// <param name="score">分数</param>
        public async Task SortedSetAddAsync(string redisKey, string redisValue, double score)
        {
            await _database.SortedSetAddAsync(redisKey, redisValue, score);
        }

        /// <summary>
        /// set集添加
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task AddSetAsync(string redisKey, string redisValue) {
            await _database.SetAddAsync(redisKey, redisValue);
        }

        /// <summary>
        /// set集添加
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task AddSetAsync(string redisKey, string[] redisValue)
        {
            await _database.SetAddAsync(redisKey, redisValue.ToRedisValueArray());
        }

        /// <summary>
        /// 获取set集
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<RedisValue[]> GetSetAsync(string redisKey)
        {
            return await _database.SetMembersAsync(redisKey);
        }

        /// <summary>
        /// 删除set集合某一数据
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<bool> DelSetAsync(string redisKey,string redisvalue)
        {
            if (await _database.SetContainsAsync(redisKey, redisvalue))
            {
                return await _database.SetRemoveAsync(redisKey, redisvalue);
            }
            else {
                return await Task.FromResult(true);
            }
        }

        /// <summary>
        /// 判断元素是否在集合中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> ExistSetAsync(string key, string value)
        {
            return await _database.SetContainsAsync(key, value);
        }


        /// <summary>
        /// 使用Redis分布式锁执行某些操作
        /// </summary>
        /// <param name="lockName">锁名</param>
        /// <param name="act">操作</param>
        /// <param name="expiry">锁过期时间，若超出时间自动解锁 单位：sec</param>
        /// <param name="retry">获取锁的重复次数</param>
        /// <param name="tryDelay">获取锁的重试间隔  单位：ms</param>
        public void LockAction(string lockName, Action act, int expiry = 10, int retry = 100, int tryDelay = 20)
        {
            if (act.Method.IsDefined(typeof(AsyncStateMachineAttribute), false))
            {
                throw new ArgumentException("使用异步Action请调用LockActionAsync");
            }

            TimeSpan exp = TimeSpan.FromSeconds(expiry);
            string token = Guid.NewGuid().ToString("N");
            try
            {
                bool ok = false;
                // 延迟重试
                for (int test = 0; test < retry; test++)
                {
                    if (_database.LockTake(lockName, token, exp))
                    {
                        ok = true;
                        break;
                    }
                    else
                    {
                        Task.Delay(tryDelay).Wait();
                    }
                }
                if (!ok)
                {
                    throw new InvalidOperationException($"获取锁[{lockName}]失败");
                }
                act();
            }
            finally
            {
                _database.LockRelease(lockName, token);
            }
        }

        /// <summary>
        /// 使用Redis分布式锁执行某些异步操作
        /// </summary>
        /// <param name="lockName">锁名</param>
        /// <param name="act">操作</param>
        /// <param name="expiry">锁过期时间，若超出时间自动解锁 单位：sec</param>
        /// <param name="retry">获取锁的重复次数</param>
        /// <param name="tryDelay">获取锁的重试间隔  单位：ms</param>
        public async Task LockActionAsync(string lockName, Func<Task> act, int expiry = 10, int retry = 100, int tryDelay = 20)
        {
            TimeSpan exp = TimeSpan.FromSeconds(expiry);
            string token = Guid.NewGuid().ToString("N");
            try
            {
                bool ok = false;
                // 延迟重试
                for (int test = 0; test < retry; test++)
                {
                    if (await _database.LockTakeAsync(lockName, token, exp))
                    {
                        ok = true;
                        break;
                    }
                    else
                    {
                        await Task.Delay(tryDelay);
                    }
                }
                if (!ok)
                {
                    throw new InvalidOperationException($"获取锁[{lockName}]失败");
                }
                await act();
            }
            finally
            {
                await _database.LockReleaseAsync(lockName, token);
            }
        }

    }
}
