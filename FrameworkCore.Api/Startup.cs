

namespace FrameworkCore.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Reflection;
    using AutoMapper;
    using FrameworkCore.Api.Extensions;
    using Microsoft.OpenApi.Models;
    using FrameworkCore.EFCore.Context;
    using FrameworkCore.EFCore;
    using FrameworkCore.Shared.DataModel;
    using FrameworkCore.Shared.DataInterfaces;
    using FrameworkCore.Api.ApiManager;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// ע�����
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin();
                });
            });
            services.AddControllers();
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });

            var migrationsAssembly = typeof(EntityContext).GetTypeInfo().Assembly.GetName();
            var migrationsAssemblyName = migrationsAssembly.Name;

            //ע�����ݿ������ķ���
            services.AddDbContext<EntityContext>(options =>
            {
                var connestionString = Configuration.GetConnectionString("MsSqlNoteConnection");
                options.UseSqlServer(connestionString, sql =>
                {
                    sql.MigrationsAssembly(migrationsAssemblyName);
                    sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });

            })
            .AddUnitOfWork<EntityContext>()
            .AddCustomRepository<User, CustomUserRepository>()
            .AddCustomRepository<UserLog, CustomUserLogRepository>()
            .AddCustomRepository<Menu, CustomMenuRepository>()
            .AddCustomRepository<Group, CustomGroupRepository>()
            .AddCustomRepository<AuthItem, CustomAuthRepository>()
            .AddCustomRepository<Basic, CustomBasicRepository>();

            //ע��Transient����
            services.AddTransient<IDataInitializer, DataInitializer>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IMenuManager, MenuManager>();
            services.AddTransient<IGroupManager, GroupManager>();
            services.AddTransient<IBasicManager, BasicManager>();
            services.AddTransient<IAuthItemManager, AuthManager>();

            //ע��autoMapper����
            var autoMapperConfig = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new AutoMappingFile());
            });
            var autoMapper = autoMapperConfig.CreateMapper();
            services.AddSingleton(autoMapper);
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = ".NET Core WebApi v1", Version = "v1" });
            });

        }

        /// <summary>
        /// �����м��
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var databaseInitializer = serviceScope.ServiceProvider.GetService<IDataInitializer>();
                await databaseInitializer.InitSampleDataAsync();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseFileServer();
            app.UseHttpsRedirection();
            app.UseCors("any"); //��WebӦ�ùܵ������һ��CORS�м�����������Ӧ.
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.ShowExtensions();
                options.SwaggerEndpoint("/swagger/v1/swagger.json", ".NET Core WebApi v1");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger();
            });
        }
    }
}
