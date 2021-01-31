/*****************************************************************************
*项目名称:FrameworkCore.ViewModel.Common
*项目描述:
*类 名 称:ModuleGroup
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:15:34
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/


namespace FrameworkCore.ViewModel.Common
{
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using System.Collections.ObjectModel;

    public class ModuleGroup : ObservableObject
    {
        private string groupName;
        private bool contractionTemplate;
        private ObservableCollection<Module> modules;

        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 收缩面板-模板
        /// </summary>
        public bool ContractionTemplate
        {
            get { return contractionTemplate; }
            set { contractionTemplate = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 包含的子模块
        /// </summary>
        public ObservableCollection<Module> Modules
        {
            get { return modules; }
            set { modules = value; OnPropertyChanged(); }
        }
    }
}
