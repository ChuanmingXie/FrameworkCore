/*****************************************************************************
*项目名称:FrameworkCore.ViewModel
*项目描述:
*类 名 称:DashBoardViewModel
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:27:40
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.ViewModel
{
    using FrameworkCore.ViewModel.Interfaces;
    using Microsoft.Toolkit.Mvvm.ComponentModel;

    /// <summary>
    /// 数据面板
    /// </summary>
    public class DashboardViewModel : ObservableObject, IDashBoardViewModel
    {
    }
}
