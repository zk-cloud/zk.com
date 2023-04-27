using Autofac;
using AutoMapper;
using CoreCms.Net.Auth;
using CoreCms.Net.Configuration;
using CoreCms.Net.Core.AutoFac;
using CoreCms.Net.Core.Config;
using CoreCms.Net.Filter;
using CoreCms.Net.Loging;
using CoreCms.Net.Mapping;
using CoreCms.Net.Middlewares;
using CoreCms.Net.Swagger;
using CoreCms.Net.Task;
using CoreCms.Net.WeChat.Service.Mediator;
using Essensoft.Paylink.Alipay;
using Essensoft.Paylink.WeChatPay;
using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace CoreCms.Net.Web.WebApi
{
    /// <summary>
    /// ��������
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }
        /// <summary>
        /// ��������
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// web����
        /// </summary>
        public IWebHostEnvironment Env { get; }

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //��ӱ���·����ȡ֧��
            services.AddSingleton(new AppSettingsHelper(Env.ContentRootPath));
            services.AddSingleton(new LogLockHelper(Env.ContentRootPath));

            //Memory����
            services.AddMemoryCacheSetup();
            //Redis����
            services.AddRedisCacheSetup();

            //������ݿ�����SqlSugarע��֧��
            services.AddSqlSugarSetup();
            //���ÿ���CORS��
            services.AddCorsSetup();


            //���session֧��(session������cache���д洢)
            services.AddSession();
            // AutoMapper֧��
            services.AddAutoMapper(typeof(AutoMapperConfiguration));

            //MediatR��ֻ��Ҫע��һ��,ͬ��Ŀ������¾Ͳ���Ҫע������
            services.AddMediatR(typeof(TextMessageEventCommand).Assembly);

            //ʹ�� SignalR
            services.AddSignalR();

            //Redis��Ϣ����
            services.AddRedisMessageQueueSetup();

            // ����Payment ����ע��(֧����֧��/΢��֧��)
            services.AddAlipay();
            services.AddWeChatPay();

            // �� appsettings.json �� ����ѡ��
            services.Configure<WeChatPayOptions>(Configuration.GetSection("WeChatPay"));
            services.Configure<AlipayOptions>(Configuration.GetSection("Alipay"));


            //ע���Զ���΢�Žӿ������ļ�
            services.Configure<WeChat.Service.Options.WeChatOptions>(Configuration.GetSection(nameof(WeChat.Service.Options.WeChatOptions)));

            // ע�빤�� HTTP �ͻ���
            services.AddHttpClient();
            services.AddSingleton<WeChat.Service.HttpClients.IWeChatApiHttpClientFactory, WeChat.Service.HttpClients.WeChatApiHttpClientFactory>();


            //Swagger�ӿ��ĵ�ע��
            services.AddClientSwaggerSetup();

            //���������ƴ�ӡ��
            services.AddYiLianYunSetup();

            //ע��Hangfire��ʱ����
            services.AddHangFireSetup();


            //��Ȩ֧��ע��
            services.AddAuthorizationSetupForClient();
            //������ע��
            services.AddHttpContextSetup();

            //���������м���AutoFac�������滻����
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            //ע��mvc��ע��razor������ͼ
            services.AddMvc(options =>
                {
                    //ʵ����֤
                    options.Filters.Add<RequiredErrorForClent>();
                    //�쳣����
                    options.Filters.Add<GlobalExceptionsFilterForClent>();
                    //Swagger�޳�����Ҫ����apiչʾ���б�
                    options.Conventions.Add(new ApiExplorerIgnores());
                })
                .AddNewtonsoftJson(p =>
                {
                    //���ݸ�ʽ����ĸСд ��ʹ���շ�
                    p.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    //��ʹ���շ���ʽ��key
                    //p.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    //����ѭ������
                    p.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //����ʱ���ʽ������ʹ��yyyy/MM/dd��ʽ����Ϊiosϵͳ��֧��2018-03-29��ʽ��ʱ�䣬ֻʶ��2018/03/09���ָ�ʽ����
                    p.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";
                });



        }

        /// <summary>
        ///     Autofac��������
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //��ȡ���п��������Ͳ�ʹ������ע��
            var controllerBaseType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();

            builder.RegisterModule(new AutofacModuleRegister());

        }

        // public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<SenparcSetting> senparcSetting, IOptions<SenparcWeixinSetting> senparcWeixinSetting)
        /// <summary>
        ///  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region ���Ubuntu Nginx �����ܻ�ȡIP����
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            #endregion

            // ��¼�����뷵������ (ע�⿪��Ȩ�ޣ���Ȼ�����޷�д��)
            app.UseRequestResponseLog();
            // �û����ʼ�¼(����ŵ���㣬��Ȼ��������쳣���ᱨ����Ϊ���ܷ�����)(ע�⿪��Ȩ�ޣ���Ȼ�����޷�д��)
            app.UseRecordAccessLogsMildd();
            // ��¼ip���� (ע�⿪��Ȩ�ޣ���Ȼ�����޷�д��)
            app.UseIpLogMildd();


            //ǿ����ʾ����
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");

            app.UseSwagger().UseSwaggerUI(c =>
            {
                //���ݰ汾���Ƶ��� ����չʾ
                typeof(CustomApiVersion.ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(
                    version =>
                    {
                        c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"Doc {version}");
                    });
                //����Ĭ����ת��swagger-ui
                c.RoutePrefix = "doc";
                //c.RoutePrefix = string.Empty;
            });


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
                    Users = new[]
                    {
                        new BasicAuthAuthorizationUser
                        {
                            Login = AppSettingsConstVars.HangFireLogin,
                            PasswordClear = AppSettingsConstVars.HangFirePassWord
                        }
                    }
                });
            var options = new DashboardOptions
            {
                AppPath = "/",//����ʱ��ת�ĵ�ַ
                DisplayStorageConnectionString = false,//�Ƿ���ʾ���ݿ�������Ϣ
                Authorization = new[]
                {
                    filter
                },
                IsReadOnlyFunc = Context =>
                {
                    return false;//�Ƿ�ֻ�����
                }
            };

            app.UseHangfireDashboard("/job", options); //���Ըı�Dashboard��url
            //HangfireDispose.HangfireService();
            HangfireDispose.TCPHangfireService();
            #endregion




            //ʹ�� Session
            app.UseSession();

            if (env.IsDevelopment())
            {
                // �ڿ��������У�ʹ���쳣ҳ�棬�������Ա�¶�����ջ��Ϣ�����Բ�Ҫ��������������
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // CORS����
            app.UseCors(AppSettingsConstVars.CorsPolicyName);

            // Routing
            app.UseRouting();

            // ʹ�þ�̬�ļ�
            app.UseStaticFiles();
            // �ȿ�����֤
            app.UseAuthentication();
            // Ȼ������Ȩ�м��
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "areas",
                    "{area:exists}/{controller=Default}/{action=Index}/{id?}"
                );

                //endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Default}/{action=Index}/{id?}");
            });



            //����Ĭ����ʼҳ����default.html��
            //�˴���·���������wwwroot�ļ��е����·��
            var defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();

        }


    }
}