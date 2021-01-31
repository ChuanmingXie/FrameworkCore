using Autofac;
using FrameworkCore.PC.Common;
using FrameworkCore.Service;
using FrameworkCore.Shared.Common;
using FrameworkCore.Shared.DataInterfaces;
using FrameworkCore.ViewModel;
using FrameworkCore.ViewModel.Core;
using FrameworkCore.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FrameworkCore.PC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            App.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
            NetCoreProvider.Resolve<ILog>()?.Warn(e.Exception, e.Exception.Message);
            e.Handled = true;
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            Contract.serverUrl = ConfigurationManager.AppSettings["serverAddress"];
            this.ConfigureServices();
            var login = NetCoreProvider.ResolveNamed<ILoginCenter>("LoginCenter");
            await login.ShowDialog();
        }

        private void ConfigureServices()
        {
            var service = new ContainerBuilder();
            /* 注册存储库 */
            service.AddRepository<UserService, IUserRepository>()
                .AddRepository<GroupService, IGroupRepository>()
                .AddRepository<MenuService, IMenuRepository>()
                .AddRepository<BasicService, IBasicRepository>()
                .AddRepository<FrameworkCoreNLog, ILog>();

            /* 注册视图模型服务 */
            service.AddViewModel<UserViewModel, IUserViewModel>()
                .AddViewModel<LoginViewModel, ILoginViewModel>()
                .AddViewModel<MainViewModel, IMainViewModel>()
                .AddViewModel<GroupViewModel, IGroupViewModel>()
                .AddViewModel<MenuViewModel, IMenuViewModel>()
                .AddViewModel<BasicViewModel, IBasicViewModel>();


            /* 注册视图控制服务 */

            NetCoreProvider.RegisterServiceLocator(service.Build());
        }
    }
}
