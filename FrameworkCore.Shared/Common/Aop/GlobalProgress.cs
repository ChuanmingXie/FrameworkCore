/*****************************************************************************
*项目名称:FrameworkCore.Shared.Common.Aop
*项目描述:
*类 名 称:GlobalProgess
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:40:35
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.Common.Aop
{
    using AspectInjector.Broker;
    using FrameworkCore.Shared.Common;
    using System;
    using System.Threading.Tasks;


    [Aspect(Scope.Global)]
    [Injection(typeof(GlobalProgress))]
    public class GlobalProgress : Attribute
    {
        [Advice(Kind.Before, Targets = Target.Method)]
        public void Start([Argument(Source.Name)] string name)
        {
            UpdateLoading(true);
        }

        [Advice(Kind.After, Targets = Target.Method)]
        public async void End([Argument(Source.Name)] string name)
        {
            await Task.Delay(300);
            UpdateLoading(false);
        }

        private void UpdateLoading(bool isOpen, string msg = "Loading......")
        {

        }
    }
}
