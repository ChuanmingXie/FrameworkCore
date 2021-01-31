/*****************************************************************************
*项目名称:FrameworkCore.Shared.DataModel
*项目描述:
*类 名 称:MenuModuleGroup
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:07:27
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FrameworkCore.Shared.DataModel
{
    public class MenuModuleGroup
    {
        /// <summary>
        /// 菜单编码Code
        /// </summary>
        public string MenuCode { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string MenuName { get; set; }

        public ObservableCollection<MenuModule> Modules { get; set; }
    }

    public class MenuModule
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public int Value { get; set; }
    }
}
