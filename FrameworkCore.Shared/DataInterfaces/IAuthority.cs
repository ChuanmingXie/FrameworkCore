/*****************************************************************************
*项目名称:FrameworkCore.Shared.DataInterfaces
*项目描述:
*类 名 称:IAuthority
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 22:58:05
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.DataInterfaces
{
    using FrameworkCore.Shared.Common;
    using System.Collections.ObjectModel;

    public interface IAuthority
    {
        void InitPermissions(int AuthValue);

        ObservableCollection<CommandStruct> ToolBarCommandList { get; set; }
    }
}
