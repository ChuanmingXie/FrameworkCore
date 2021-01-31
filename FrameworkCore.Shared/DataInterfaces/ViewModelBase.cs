/*****************************************************************************
*项目名称:FrameworkCore.Shared.DataInterfaces
*项目描述:
*类 名 称:ViewModelBase
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 22:59:44
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FrameworkCore.Shared.DataInterfaces
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void RaisePropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
