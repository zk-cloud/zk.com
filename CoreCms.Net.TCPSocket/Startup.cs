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
            //��ӱ���·����ȡ֧��
            services.AddSingleton(new AppSettingsHelper(Env.ContentRootPath));

            //ע��Hangfire��ʱ����
            services.AddHangFireSetup();

            //redis����
            services.AddRedisCacheSetup();

            //Redis��Ϣ����
            services.AddRedisMessageQueueSetup();

            // AutoMapper֧��
            services.AddAutoMapper(typeof(AutoMapperConfiguration));

            //������ݿ�����sqlsugarע��֧��
            services.AddSqlSugarSetup();
            //���ÿ���cors��
            services.AddCorsSetup();
            //���session֧��
            services.AddSession();

            //AutoMapper֧��

            //ʹ�� SignalR
            //�û�IUserIdProviderʵ��ע��
            services.AddScoped<IUserIdProvider, ChatHubGetUserId>();
            services.AddSignalR().AddMessagePackProtocol();

            //ע�빤��HTTP�ͻ���
            services.AddHttpClient();

            //jwt��Ȩ֧��ע��
            services.AddAuthorizationSetupForAdmin();

            //������ע��
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IHttpContextUser, AspNetUser>();

            //Hnagfire
            GlobalStateHandlers.Handlers.Add(new SucceededStateExpireHandler(AppSettingsConstVars.JobExpirationTimeout));


            //���������м���AutoFac�������滻����
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());



            ////TCPSocket
            //services.AddHostedService<TcpServerHost>();

            //services.AddHostedService<RecurringJobsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Hangfire��ʱ����
            //��Ȩ
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
                DisplayStorageConnectionString = false,//�Ƿ���ʾ���ݿ�������Ϣ
                Authorization = new[] { filter },
                IsReadOnlyFunc = Context =>
                {
                    return false;//�Ƿ�ֻ�����
                }
            };

            app.UseHangfireDashboard("/job", options);//���Ըı�Dashboard��url
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

            // CORS����
            app.UseCors(AppSettingsConstVars.CorsPolicyName);

            //app.UseStaticFiles();

            // ʹ��cookie
            app.UseCookiePolicy();
            // ���ش�����
            app.UseStatusCodePages();

            app.UseRouting();

            // �ȿ�����֤
            app.UseAuthentication();
            // Ȼ������Ȩ�м��
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                //ʹ�ü�����
                endpoints.MapHub<ChatHub>("/chatHub", options =>
                {
                    //options.Transports = HttpTransportType.WebSockets;
                });
            });
        }
    }
}
