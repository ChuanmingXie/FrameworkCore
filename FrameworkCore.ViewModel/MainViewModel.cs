/*****************************************************************************
*项目名称:FrameworkCore.ViewModel
*项目描述:
*类 名 称:MainViewModel
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:21:18
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/

namespace FrameworkCore.ViewModel
{
    using FrameworkCore.Shared.Common;
    using FrameworkCore.ViewModel.Common;
    using FrameworkCore.ViewModel.Interfaces;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    public class MainViewModel : BaseDialogViewModel, IMainViewModel
    {
        public MainViewModel()
        {
            OpenPageCommand = new AsyncRelayCommand<string>(OpenPage);
            ClosePageCommand = new RelayCommand<string>(ClosePage);
            GoHomeCommand = new RelayCommand(InitHomeView);
            ExpandMenuCommand = new RelayCommand(() =>
            {
                for (int i = 0; i < ModuleManager.ModuleGroups.Count; i++)
                {
                    var arg = ModuleManager.ModuleGroups[i];
                    arg.ContractionTemplate = !arg.ContractionTemplate;
                }
                WeakReferenceMessenger.Default.Send("", "ExpandMenu");
            });
        }

        #region Property

        private ModuleUIComponent currentModule;
        public ModuleUIComponent CurrentModule
        {
            get { return currentModule; }
            set { currentModule = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ModuleUIComponent> moduleList;
        public ObservableCollection<ModuleUIComponent> ModuleList
        {
            get { return moduleList; }
            set { moduleList = value; OnPropertyChanged(); }
        }

        private ModuleManager moduleManager;
        public ModuleManager ModuleManager
        {
            get { return moduleManager; }
            set { moduleManager = value; OnPropertyChanged(); }
        }

        #endregion

        #region Command

        /// <summary>
        /// 菜单栏收缩
        /// </summary>
        public RelayCommand ExpandMenuCommand { get; private set; }

        /// <summary>
        /// 返回首页
        /// </summary>
        public RelayCommand GoHomeCommand { get; private set; }

        /// <summary>
        /// 打开新页面
        /// </summary>
        public AsyncRelayCommand<string> OpenPageCommand { get; private set; }

        /// <summary>
        /// 关闭页面
        /// </summary>
        public RelayCommand<string> ClosePageCommand { get; private set; }

        public RelayCommand MinCommand { get; private set; } = new RelayCommand(() =>
        {
            WeakReferenceMessenger.Default.Send("", "WindowMinimize");
        });

        public RelayCommand MaxCommand { get; private set; } = new RelayCommand(() =>
        {
            WeakReferenceMessenger.Default.Send("", "WindowMaximize");
        });

        #endregion

        #region Method

        /// <summary>
        /// 打开页面
        /// </summary>
        /// <param name="pageName"></param>
        /// <returns></returns>
        public async virtual Task OpenPage(string pageName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(pageName)) return;
                var pageModule = this.ModuleManager.Modules.FirstOrDefault(t => t.Name.Equals(pageName));
                if (pageModule == null) return;
                var module = this.ModuleList.FirstOrDefault(t => t.Name == pageModule.TypeName);
                if (module == null)
                {
                    var dialog = NetCoreProvider.ResolveNamed<IBaseCenter>(pageModule.TypeName);
                    await dialog.BindDefaultModel(pageModule.Auth);
                    ModuleList.Add(new ModuleUIComponent()
                    {
                        Code = pageModule.Code,
                        Auth = pageModule.Auth,
                        Name = pageModule.Name,
                        TypeName = pageModule.TypeName,
                        Body = dialog.GetView()
                    });
                    currentModule = ModuleList.Last();
                }
                else
                    CurrentModule = module;
            }
            catch (Exception ex)
            {
                Msg.Error(ex.Message);
            }
        }

        /// <summary>
        /// 关闭页面
        /// </summary>
        /// <param name="pageName"></param>
        public void ClosePage(string pageName)
        {
            var module = this.ModuleList.FirstOrDefault(t => t.Name.Equals(pageName));
            if (module != null)
            {
                this.ModuleList.Remove(module);
                if (this.ModuleList.Count > 0)
                    this.CurrentModule = this.ModuleList.Last();
                else
                    this.CurrentModule = null;
            }
        }

        /// <summary>
        /// 初始化页面上下文内容
        /// </summary>
        /// <returns></returns>
        public async Task InitDefaultView()
        {
            /* 加载首页的程序集模块
             * 1. 获取本机所有可用模块
               2. 利用服务器验证,过滤掉不可用模块
               注： 理论上管理员应该可用本机的所有模块
               当监测本机用户属于管理员可不像服务器验证
            */
            ModuleManager = new ModuleManager();
            ModuleList = new ObservableCollection<ModuleUIComponent>();
            await ModuleManager.LoadAssemblyModule();

            InitHomeView();
        }

        /// <summary>
        /// 初始化首页
        /// </summary>
        private void InitHomeView()
        {
            var dialog = NetCoreProvider.ResolveNamed<IHomeCenter>("HomeCenter");
            dialog.BindDefaultModel();
            ModuleUIComponent component = new ModuleUIComponent();
            component.Name = "首页";
            component.Body = dialog.GetView();
            ModuleList.Add(component);
            ModuleManager.Modules.Add(component);
            CurrentModule = ModuleList.Last();
        }

        #endregion
    }
}
