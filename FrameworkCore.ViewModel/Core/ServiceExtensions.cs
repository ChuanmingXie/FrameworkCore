/*****************************************************************************
*项目名称:FrameworkCore.ViewModel.Core
*项目描述:
*类 名 称:ServiceExtensions
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:10:16
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.ViewModel.Core
{
    using Autofac;
    using FrameworkCore.Shared.DataInterfaces;


    public static class ServiceExtensions
    {
        public static ContainerBuilder AddViewCenter<TCenter, ICenter>(this ContainerBuilder services)
        {
            services.RegisterType<ICenter>().Named(typeof(TCenter).Name, typeof(ICenter));
            return services;
        }

        public static ContainerBuilder AddRepository<TRepository, IRepository>(this ContainerBuilder services) where TRepository : class
        {
            services.RegisterType<TRepository>().As<IRepository>();
            return services;
        }

        public static ContainerBuilder AddViewModel<TRepository, IRepository>(this ContainerBuilder services) where TRepository : class
        {
            services.RegisterType<TRepository>().As<IRepository>();
            return services;
        }
    }
}
