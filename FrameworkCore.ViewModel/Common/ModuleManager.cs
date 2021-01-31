/*****************************************************************************
*项目名称:FrameworkCore.ViewModel.Common
*项目描述:
*类 名 称:Class1
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:16:19
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
    using FrameworkCore.Shared.Common.Enums;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// 模块管理器
    /// </summary>
    public class ModuleManager : ObservableObject
    {
        private ObservableCollection<Module> modules;
        /// <summary>
        /// 已加载模块
        /// </summary>
        public ObservableCollection<Module> Modules
        {
            get { return modules; }
            set { modules = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ModuleGroup> moduleGroups;
        /// <summary>
        /// 已加载模块—分组
        /// </summary>
        public ObservableCollection<ModuleGroup> ModuleGroups
        {
            get { return moduleGroups; }
            set { moduleGroups = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 加载程序集模块
        /// </summary>
        /// <returns></returns>
        public async Task LoadAssemblyModule()
        {
            try
            {
                Modules = new ObservableCollection<Module>();
                ModuleGroups = new ObservableCollection<ModuleGroup>();
                var enumInfo = Enum.GetValues(typeof(ModuleType));
                for (int i = 0; i < enumInfo.Length; i++)
                {
                    ModuleGroups.Add(new ModuleGroup()
                    {
                        GroupName = enumInfo.GetValue(i).ToString(),
                        Modules = new ObservableCollection<Module>()
                    });
                }
                var assemblyModule = await new ModuleComponent().GetAssemblyModules();
                foreach (var aModule in assemblyModule)
                {
                    //如果当前程序集的模快在服务器上可以匹配到就添加模块列表
                    var Menu = Contract.Menus.FirstOrDefault(t => t.MenuName.Equals(aModule.Name));
                    if (Menu != null)
                    {
                        var group = ModuleGroups.FirstOrDefault(t => t.GroupName == aModule.ModuleType.ToString());
                        if (group == null)
                        {
                            ModuleGroup newGroup = new ModuleGroup();
                            newGroup.GroupName = aModule.ModuleType.ToString();
                            newGroup.Modules = new ObservableCollection<Module>();
                            newGroup.Modules.Add(new Module()
                            {
                                Name = aModule.Name,
                                Code = Menu.MenuCaption,
                                TypeName = Menu.MenuNameSpcace,
                                Auth = Menu.MenuAuth
                            });
                        }
                        else
                        {
                            group.Modules.Add(new Module()
                            {
                                Name = aModule.Name,
                                Code = Menu.MenuCaption,
                                TypeName = Menu.MenuNameSpcace,
                                Auth = Menu.MenuAuth
                            });
                        }
                        Modules.Add(new Module()
                        {
                            Name = aModule.Name,
                            Code = Menu.MenuCaption,
                            TypeName = Menu.MenuNameSpcace,
                            Auth = Menu.MenuAuth
                        });
                    }
                }
                GC.Collect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
