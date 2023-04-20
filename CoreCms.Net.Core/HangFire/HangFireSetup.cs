using CoreCms.Net.Configuration;
using Hangfire;
using Hangfire.MySql;
using Hangfire.Redis;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;

using System.Transactions;

namespace CoreCms.Net.Core
{
    /// <summary>
    /// 增加HangFire到单独配置
    /// </summary>
    public static class HangFireSetup
    {
        public static void AddHangFireSetup(this IServiceCollection services) {
            if (services == null) throw new ArgumentException(nameof(services));

            //注册HangFire定时任务
            var isEnabledRedis = AppSettingsConstVars.RedisUseTimedTask;
            if (isEnabledRedis)
            {
                services.AddHangfire(x => x.UseRedisStorage(AppSettingsConstVars.RedisConfigConnectionString,new RedisStorageOptions {
                    InvisibilityTimeout=  TimeSpan.FromMinutes(30),    //任务转移间隔, 在这段间隔内，后台任务任为同一个worker处理；超时后将转移到另一个worker处理
                    //Prefix =  "YLQC",   //在Redis存储中Hangfire使用的Key前缀
                    Db=3,
                    SucceededListSize =  1000,//   成功列表中的最大可见后台任务，以防止其无限期增长。
                    DeletedListSize =    1000,    //删除列表中的最大可见后台作业，以防止其无限期增长。
                }));
            }
            else
            {
                string dbTypeString = AppSettingsConstVars.DbDbType;
                if (dbTypeString == DbType.MySql.ToString())
                {
                    services.AddHangfire(x => x.UseStorage(new MySqlStorage(AppSettingsConstVars.DbSqlConnection, new MySqlStorageOptions
                    {
                        TransactionIsolationLevel = IsolationLevel.ReadCommitted, // 事务隔离级别。默认是读取已提交。
                        QueuePollInterval = TimeSpan.FromSeconds(15),             //- 作业队列轮询间隔。默认值为15秒。
                        JobExpirationCheckInterval = TimeSpan.FromHours(1),       //- 作业到期检查间隔（管理过期记录）。默认值为1小时。
                        CountersAggregateInterval = TimeSpan.FromMinutes(5),      //- 聚合计数器的间隔。默认为5分钟。
                        PrepareSchemaIfNecessary = true,                          //- 如果设置为true，则创建数据库表。默认是true。

                        DashboardJobListLimit = 50000,                            //- 仪表板作业列表限制。默认值为50000。
                        TransactionTimeout = TimeSpan.FromMinutes(1),             //- 交易超时。默认为1分钟。
                        TablesPrefix = "Hangfire"                                  //- 数据库中表的前缀。默认为none
                    })));
                }
                else if (dbTypeString == DbType.SqlServer.ToString())
                {
                    services.AddHangfire(x => x.UseSqlServerStorage(AppSettingsConstVars.DbSqlConnection));
                }
            }

            services.AddHangfireServer(options =>
            {
                options.Queues = new[] { GlobalEnumVars.HangFireQueuesConfig.@default.ToString(), GlobalEnumVars.HangFireQueuesConfig.apis.ToString(), GlobalEnumVars.HangFireQueuesConfig.web.ToString(), GlobalEnumVars.HangFireQueuesConfig.recurring.ToString() };
                options.ServerTimeout = TimeSpan.FromMinutes(4);
                options.SchedulePollingInterval = TimeSpan.FromSeconds(1);//秒级任务需要配置短点，一般任务可以配置默认时间，默认15秒
                options.ShutdownTimeout = TimeSpan.FromMinutes(30); //超过时间
                options.WorkerCount = Math.Max(Environment.ProcessorCount, 20); //工作线程数，当前允许的最大线程，默认20
            });

        }
    }
}
