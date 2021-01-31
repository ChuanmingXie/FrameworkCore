/*****************************************************************************
*项目名称:FrameworkCore.Shared.Common
*项目描述:
*类 名 称:NetCoreProvider
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 22:57:32
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/


namespace FrameworkCore.Shared.Common
{
    using Autofac;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class NetCoreProvider
    {
        public static IContainer Instance { get; private set; }

        public static void RegisterServiceLocator(IContainer locator)
        {
            if (Instance == null)
                Instance = locator;
        }

        public static T Resolve<T>()
        {
            if (!Instance.IsRegistered<T>())
                new ArgumentNullException(nameof(T));
            return Instance.Resolve<T>();
        }

        public static T ResolveNamed<T>(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                new ArgumentNullException(nameof(T));

            return Instance.ResolveNamed<T>(typeName);
        }
    }
}
