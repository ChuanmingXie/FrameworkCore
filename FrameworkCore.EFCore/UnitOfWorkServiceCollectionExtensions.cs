/*****************************************************************************
*项目名称:FrameworkCore.EFCore
*项目描述:
*类 名 称:UnitOfWorkServiceCollectionExtensions
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 0:06:05
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.EFCore
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class UnitOfWorkServiceCollectionExtensions
    {
        /// <summary>
        /// 将上下文数据库工作单元注册为服务
        /// </summary>
        /// <typeparam name="TContext">上下文类型</typeparam>
        /// <param name="services">添加服务的收集器</param>
        /// <returns>相同的服务收集器实现多个调用相连</returns>
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            services.AddScoped<IRepositoryFactory, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();

            return services;
        }

        /// <summary>
        /// 将上下文数据库工作单元注册为服务
        /// </summary>
        /// <typeparam name="TContext1">上下文类型1</typeparam>
        /// <typeparam name="TContext2">上下文类型2</typeparam>
        /// <param name="services"></param>
        /// <returns>相同的服务收集器实现多个调用相连</returns>
        public static IServiceCollection AddUnitOfWork<TContext1, TContext2>(this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
        {
            services.AddScoped<IUnitOfWork<TContext1>, UnitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();
            return services;
        }

        /// <summary>
        /// 将上下文数据库工作单元注册为服务
        /// </summary>
        /// <typeparam name="TContext1">上下文类型1</typeparam>
        /// <typeparam name="TContext2">上下文类型2</typeparam>
        /// <typeparam name="TContext3">上下文类型3</typeparam>
        /// <param name="services"></param>
        /// <returns>相同的服务收集器实现多个调用相连</returns>
        public static IServiceCollection AddUnitOfWork<TContext1, TContext2, TContext3>(this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
            where TContext3 : DbContext
        {
            services.AddScoped<IUnitOfWork<TContext1>, UnitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();
            services.AddScoped<IUnitOfWork<TContext3>, UnitOfWork<TContext3>>();
            return services;
        }

        /// <summary>
        /// 将上下文数据库工作单元注册为服务
        /// </summary>
        /// <typeparam name="TContext1">上下文类型1</typeparam>
        /// <typeparam name="TContext2">上下文类型2</typeparam>
        /// <typeparam name="TContext3">上下文类型3</typeparam>
        /// <typeparam name="TContext4">上下文类型4</typeparam>
        /// <param name="services"></param>
        /// <returns>相同的服务收集器实现多个调用相连</returns>
        public static IServiceCollection AddUnitOfWork<TContext1, TContext2, TContext3, TContext4>(this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
            where TContext3 : DbContext
            where TContext4 : DbContext
        {
            services.AddScoped<IUnitOfWork<TContext1>, UnitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();
            services.AddScoped<IUnitOfWork<TContext3>, UnitOfWork<TContext3>>();
            services.AddScoped<IUnitOfWork<TContext4>, UnitOfWork<TContext4>>();
            return services;

        }

        /// <summary>
        /// 将自定义存储库注册为服务
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TRepository"></typeparam>
        /// <param name="services"></param>
        /// <returns>相同的服务收集器实现多个调用相连</returns>
        public static IServiceCollection AddCustomRepository<TEntity, TRepository>(this IServiceCollection services)
            where TEntity : class
            where TRepository : class, IRepository<TEntity>
        {
            services.AddScoped<IRepository<TEntity>, TRepository>();
            return services;
        }
    }
}
