using Autofac;
using CoreNotes.AutoFac.Ioc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CoreNotes.AutoFac.CoreApi.AuthHelper.Policy;
using CoreNotes.AutoFac.CoreApi.Filters;
using CoreNotes.AutoFac.CoreApi.Middleware;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace CoreNotes.AutoFac.CoreApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // 在ConfigureServices中注入验证（Authentication），授权（Authorization），和JWT(JwtBearer)
        // 在运行时被调用，使用该方法注册服务到容器中（使用DI注入）
        public void ConfigureServices(IServiceCollection services)
        {
            /* 使用微软内置的DI
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            */
            services.AddHttpContextAccessor();

            #region 官方认证

            // 读取配置文件
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);



            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            // 如果要数据库动态绑定，这里先留个空，后边处理器里动态赋值
            var permission = new List<PermissionItem>();

            // 角色与接口的权限要求参数
            var permissionRequirement = new PermissionRequirement(
                "/api/denied",// 拒绝授权的跳转地址（目前无用）
                permission,//这里还记得么，就是我们上边说到的角色地址信息凭据实体类 Permission
                ClaimTypes.Role,//基于角色的授权
                audienceConfig["Issuer"],//发行人
                audienceConfig["Audience"],//订阅人
                signingCredentials,//签名凭据
                expiration: TimeSpan.FromSeconds(60 * 2)//接口的过期时间，注意这里没有了缓冲时间，你也可以自定义，在上边的TokenValidationParameters的 ClockSkew
                );

            // 令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true, // 验证发行人的签名密钥
                IssuerSigningKey = signingKey, // 从appsettings.json中拿到密钥
                ValidateIssuer = true, // 是否验证Issuer
                ValidIssuer = audienceConfig["Issuer"], // 发行人
                ValidateAudience = true, // 是否验证Audience
                ValidAudience = audienceConfig["Audience"], // 订阅人
                ValidateLifetime = true, // 是否验证超时，当设置exp和nbf时有效，同时启用ClockSkew
                ClockSkew = TimeSpan.Zero, // 缓冲过期时间，总的有效时间等于这个时间+jwt的过期时间，如果不配置，默认是5分钟
                RequireExpirationTime = true,
            };
            // ① 核心之一，配置授权服务，也就是具体的规则，已经对应的权限策略，比如公司不同权限的门禁卡
            services.AddAuthorization(options =>
            {
                /*
                options.AddPolicy("Client",
                    policy => policy.RequireRole("Client").Build());
                options.AddPolicy("Admin",
                    policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("SystemOrAdmin",
                    policy => policy.RequireRole("Admin", "System"));
                */
                // 自定义基于策略的授权权限
                options.AddPolicy("Permission",
                         policy => policy.Requirements.Add(permissionRequirement));
            })

            // ② 核心之二，必需要配置认证服务，这里是jwtBearer默认认证，比如光有卡没用，得能识别他们
            .AddAuthentication("Bearer")
            // ③ 核心之三，针对JWT的配置，比如门禁是如何识别的，是放射卡，还是磁卡
            .AddJwtBearer(o =>
            {
                // 不使用https
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = tokenValidationParameters;
                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        // 如果过期，则把<是否过期>添加到，返回头信息中
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });


            // 依赖注入，将自定义的授权处理器 匹配给官方授权处理器接口，这样当系统处理授权的时候，就会直接访问我们自定义的授权处理器了。
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            // 将授权必要类注入生命周期内
            services.AddSingleton(permissionRequirement);


            #endregion

            // 第一种：自定义过滤器并捕获全局异常
            services.AddMvc(options =>
            {
                options.Filters.Add<CustomExceptionFilter>();

            });
            services.AddControllers();

            #region Swagger UI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.1.0",
                    Title = "CoreNotes.Autofac.CoreApi",
                    Description = "Api Server",
                    Contact = new OpenApiContact { Name = "jinjupeng", Email = "2365697576@qq.com", Url = new Uri("https://github.com/jinjupeng") }
                });
            });
            #endregion
        }

        #region AutoFac的DI实现

        // This is the default if you don't have an environment specific method.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add things to the Autofac ContainerBuilder.
            builder.RegisterModule(new AutofacModule());
        }
        #endregion

        // 该方法在运行时被调用，通过该方法配置HTTP请求管道
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}


            #region Swagger
            // 启动中间件服务生成Swagger作为JSON的终结点
            app.UseSwagger();
            // 启用中间件服务对swagger ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                // http://localhost:<port>/
                c.RoutePrefix = string.Empty;
            });
            #endregion


            // 第二种：自定义中间件ExceptionMiddleware并加入管道用于捕获全局异常
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseSerilogRequestLogging();

            // 跳转https
            //app.UseHttpsRedirection();
            // 使用静态文件
            app.UseStaticFiles();

            // 使用cookie
            app.UseCookiePolicy();
            // 返回错误码
            app.UseStatusCodePages();//把错误码返回前台，比如是404
            // Routing
            app.UseRouting();

            // 先开启认证
            app.UseAuthentication();
            // 然后是授权中间件
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
