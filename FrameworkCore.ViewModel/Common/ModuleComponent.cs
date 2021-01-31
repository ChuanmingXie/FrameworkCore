/*****************************************************************************
*项目名称:FrameworkCore.ViewModel.Common
*项目描述:
*类 名 称:ModuleCompent
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:14:40
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.ViewModel.Common
{
    using FrameworkCore.Shared.Common;
    using FrameworkCore.Shared.Common.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// 模块组件
    /// </summary>
    public class ModuleComponent
    {
        /// <summary>
        /// 获取程序集下的所有具备模块特征的集合
        /// </summary>
        /// <returns></returns>
        public async Task<List<ModuleAttribute>> GetAssemblyModules()
        {
            try
            {
                List<ModuleAttribute> list = new List<ModuleAttribute>();
                await Task.Run(() =>
                {
                    Assembly assembly = Assembly.GetEntryAssembly();
                    var types = assembly.GetTypes();
                    foreach (var t in types)
                    {
                        var attr = (ModuleAttribute)t.GetCustomAttribute(typeof(ModuleAttribute), false);
                        if (attr != null)
                            list.Add(attr);
                    }
                });
                return list;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 模块验证
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public bool ModuleVerify(ModuleAttribute module)
        {
            if (Contract.IsAdmin)
                return true;
            else
            {
                if (Contract.Menus.FirstOrDefault(t => t.MenuName.Equals(module.Name)) != null)
                    return true;
            }
            return false;
        }
    }
}
