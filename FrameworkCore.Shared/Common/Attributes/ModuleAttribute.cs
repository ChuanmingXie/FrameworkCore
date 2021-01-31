/*****************************************************************************
*项目名称:FrameworkCore.Shared.Common.Attributes
*项目描述:
*类 名 称:ModuleAttributes
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:41:43
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.Common.Attributes
{
    using FrameworkCore.Shared.Common.Enums;
    using System;

    /// <summary>
    /// 模块特性,标记该特性表示属于应用模块的部分
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleAttribute : Attribute
    {
        private string name;

        /// <summary>
        /// 描述
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        private ModuleType moduleType;

        /// <summary>
        /// 模块类型
        /// </summary>
        public ModuleType ModuleType
        {
            get { return moduleType; }
        }

        public ModuleAttribute(string name, ModuleType moduleType)
        {
            this.name = name;
            this.moduleType = moduleType;
        }
    }
}
