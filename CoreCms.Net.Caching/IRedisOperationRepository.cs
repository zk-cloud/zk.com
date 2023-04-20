using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreCms.Net.Caching
{
    /// <summary>
    /// Redis缓存接口
    /// </summary>
    public interface IRedisOperationRepository
    {

        //获取 Reids 缓存值
        Task<string> Get(string key);

        //获取值，并序列化
        public TEntity Get<TEntity>(string key);

        //获取值，并序列化的异步方法
        Task<TEntity> GetAsync<TEntity>(string key);

        //保存
        Task Set(string key, object value, TimeSpan cacheTime);

        //判断是否存在
        Task<bool> Exist(string key);

        //移除某一个缓存值
        Task Remove(string key);

        //全部清除
        Task Clear();

        /// <summary>
        /// 根据key获取RedisValue
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<RedisValue[]> ListRangeAsync(string redisKey);

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync(string redisKey, string redisValue);

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync(string redisKey, string redisValue);

        /// <summary>
        /// 在列表尾部插入数组集合。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync(string redisKey, IEnumerable<string> redisValue);

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素  反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<T> ListLeftPopAsync<T>(string redisKey) where T : class;

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素   反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<T> ListRightPopAsync<T>(string redisKey) where T : class;

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<string> ListLeftPopAsync(string redisKey);

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<string> ListRightPopAsync(string redisKey);

        /// <summary>
        /// 列表长度
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<long> ListLengthAsync(string redisKey);

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> ListRangeAsync(string redisKey, int db = -1);

        /// <summary>
        /// 根据索引获取指定位置数据
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> ListRangeAsync(string redisKey, int start, int stop);

        /// <summary>
        /// 删除List中的元素 并返回删除的个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<long> ListDelRangeAsync(string redisKey, string redisValue, long type = 0);

        /// <summary>
        /// 清空List
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task ListClearAsync(string redisKey);


        /// <summary>
        /// 有序集合/定时任务延迟队列用的多
        /// </summary>
        /// <param name="redisKey">key</param>
        /// <param name="redisValue">元素</param>
        /// <param name="score">分数</param>
        Task SortedSetAddAsync(string redisKey, string redisValue, double score);

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task AddSetAsync(string redisKey, string redisValue);

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task AddSetAsync(string redisKey, string[] redisValue);

        /// <summary>
        /// 获取集合内所有值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<RedisValue[]> GetSetAsync(string redisKey);

        /// <summary>
        /// 删除集合内某一值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisvalue"></param>
        /// <returns></returns>
        Task<bool> DelSetAsync(string redisKey, string redisvalue);


        /// <summary>
        /// 判断集合内是否存在某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> ExistSetAsync(string key, string value);

        /// <summary>
        /// 异步锁
        /// </summary>
        /// <param name="lockName"></param>
        /// <param name="act"></param>
        /// <param name="expiry"></param>
        /// <param name="retry"></param>
        /// <param name="tryDelay"></param>
        /// <returns></returns>
        Task LockActionAsync(string lockName, Func<Task> act, int expiry = 10, int retry = 100, int tryDelay = 20);

        /// <summary>
        /// 同步锁
        /// </summary>
        /// <param name="lockName"></param>
        /// <param name="act"></param>
        /// <param name="expiry"></param>
        /// <param name="retry"></param>
        /// <param name="tryDelay"></param>
        /// <returns></returns>
        void LockAction(string lockName, Action act, int expiry = 10, int retry = 100, int tryDelay = 20);
    }
}
