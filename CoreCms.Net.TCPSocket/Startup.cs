using CoreCms.Net.Auth;
using CoreCms.Net.Auth.HttpContextUser;
using CoreCms.Net.Configuration;
using CoreCms.Net.Core.Config;
using CoreCms.Net.Core.HangFire;
using CoreCms.Net.Mapping;
using CoreCms.Net.Task;
using CoreCms.Net.Utility.Hub;
using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CoreCms.Net.Utility.Hub.ChatHub;

namespace CoreCms.Net.TCPSocket
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddRazorPages();
            //添加本地路径获取支持
            services.AddSingleton(new AppSettingsHelper(Env.ContentRootPath));

            //注册Hangfire定时任务
            services.AddHangFireSetup();

            //redis缓存
            services.AddRedisCacheSetup();

            //Redis消息队列
            services.AddRedisMessageQueueSetup();

            // AutoMapper支持
            services.AddAutoMapper(typeof(AutoMapperConfiguration));

            //添加数据库连接sqlsugar注入支持
            services.AddSqlSugarSetup();
            //配置跨域（cors）
            services.AddCorsSetup();
            //添加session支持
            services.AddSession();

            //AutoMapper支持

            //使用 SignalR
            //用户IUserIdProvider实例注册
            services.AddScoped<IUserIdProvider, ChatHubGetUserId>();
            services.AddSignalR().AddMessagePackProtocol();

            //注入工厂HTTP客户端
            services.AddHttpClient();

            //jwt授权支持注入
            services.AddAuthorizationSetupForAdmin();

            //上下文注入
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IHttpContextUser, AspNetUser>();

            //Hnagfire
            GlobalStateHandlers.Handlers.Add(new SucceededStateExpireHandler(AppSettingsConstVars.JobExpirationTimeout));


            //服务配置中加入AutoFac控制器替换规则
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());



            ////TCPSocket
            //services.AddHostedService<TcpServerHost>();

            //services.AddHostedService<RecurringJobsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Hangfire定时任务
            //授权
            var filter = new BasicAuthAuthorizationFilter(
                new BasicAuthAuthorizationFilterOptions
                {
                    SslRedirect = false,
                    // Require secure connection for dashboard
                    RequireSsl = false,
                    // Case sensitive login checking
                    LoginCaseSensitive = false,
                    // Users
                    Users = new[] {
                        new BasicAuthAuthorizationUser{
                            Login="kf",
                            PasswordClear="123456"
                        }
                    }

                }

                );

            var options = new DashboardOptions
            {
                DisplayStorageConnectionString = false,//是否显示数据库连接信息
                Authorization = new[] { filter },
                IsReadOnlyFunc = Context =>
                {
                    return false;//是否只读面板
                }
            };

            app.UseHangfireDashboard("/job", options);//可以改变Dashboard的url
            HangfireDispose.TCPHangfireService();
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // CORS跨域
            app.UseCors(AppSettingsConstVars.CorsPolicyName);

            //app.UseStaticFiles();

            // 使用cookie
            app.UseCookiePolicy();
            // 返回错误码
            app.UseStatusCodePages();

            app.UseRouting();

            // 先开启认证
            app.UseAuthentication();
            // 然后是授权中间件
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                //使用集线器
                endpoints.MapHub<ChatHub>("/chatHub", options =>
                {
                    //options.Transports = HttpTransportType.WebSockets;
                });
            });
        }
    }
}
