/*****************************************************************************
*项目名称:FrameworkCore.Shared.Common
*项目描述:
*类 名 称:Contract
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 22:56:56
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using FrameworkCore.Shared.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.Common
{
    public class Contract
    {
        #region 用户信息

        /// <summary>
        /// 登录名
        /// </summary>
        public static string Account = string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        public static string UserName = string.Empty;

        /// <summary>
        /// 是否属于管理员
        /// </summary>
        public static bool IsAdmin;

        #endregion

        #region 权限验证信息

        /// <summary>
        /// 系统中已定义的功能菜单-缓存用于页面验证
        /// </summary>
        public static List<AuthItem> AuthItems;

        /// <summary>
        /// 获取用户的所有模块
        /// </summary>
        public static List<Menu> Menus;

        #endregion

        #region 接口地址

        /// <summary>
        /// 接口地址
        /// </summary>
        public static string serverUrl = string.Empty;

        #endregion
    }
}
