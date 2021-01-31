/*****************************************************************************
*项目名称:FrameworkCore.Shared.Dto
*项目描述:
*类 名 称:MenuModuleGroupDto
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:28:57
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.Dto
{
    using System.Collections.ObjectModel;
    using FrameworkCore.Shared.DataInterfaces;

    public class MenuModuleGroupDto
    {
        public string MenuCode { get; set; }

        public string MenuName { get; set; }

        public ObservableCollection<MenuModuleDto> Modules { get; set; }
    }

    public class MenuModuleDto : ViewModelBase
    {
        public string Name { get; set; }

        private int _value;
        private bool ischecked;

        public bool IsChecked
        {
            get { return ischecked; }
            set { ischecked = value; RaisePropertyChanged(); }
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; RaisePropertyChanged(); }
        }
    }
}
