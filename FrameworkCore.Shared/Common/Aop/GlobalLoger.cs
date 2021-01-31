/*****************************************************************************
*项目名称:FrameworkCore.Shared.Common.Aop
*项目描述:
*类 名 称:GlobalLogger
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:38:55
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/


namespace FrameworkCore.Shared.Common.Aop
{
    using AspectInjector.Broker;
    using FrameworkCore.Shared.Common;
    using FrameworkCore.Shared.DataInterfaces;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    [Aspect(Scope.Global)]
    [Injection(typeof(GlobalLoger))]
    public class GlobalLoger : Attribute
    {
        private readonly ILog log;

        public GlobalLoger()
        {
            this.log = NetCoreProvider.Resolve<ILog>();
        }

        [Advice(Kind.Before, Targets = Target.Method)]
        public void Start([Argument(Source.Name)] string methodName, [Argument(Source.Arguments)] object[] arg)
        {
            log.Debug($"开始调用方法:{methodName},参数:{string.Join(",", arg)}");
        }

        [Advice(Kind.After, Targets = Target.Method)]
        public void End([Argument(Source.Name)] string methodName, [Argument(Source.ReturnValue)] object arg)
        {
            log.Debug($"结束调用方法:{methodName},返回值:{arg}");
        }
    }
}
