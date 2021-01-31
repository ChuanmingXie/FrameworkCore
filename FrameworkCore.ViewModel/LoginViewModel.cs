/*****************************************************************************
*项目名称:FrameworkCore.ViewModel
*项目描述:
*类 名 称:LoginViewModel
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:21:01
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/

using FrameworkCore.Shared.DataInterfaces;
using FrameworkCore.ViewModel.Interfaces;
using FrameworkCore.Shared.Common;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace FrameworkCore.ViewModel
{
    public class LoginViewModel : BaseDialogViewModel, ILoginViewModel
    {
        private readonly IUserRepository repository;
        private string userName;
        private string passWord;
        private string report;
        private string isCancel;

        public RelayCommand LoginCommand { get; private set; }

        public LoginViewModel(IUserRepository repository)
        {
            this.repository = repository;
            LoginCommand = new RelayCommand(Login);
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; OnPropertyChanged(); }
        }

        public string Report
        {
            get { return report; }
            set { report = value; OnPropertyChanged(); }
        }

        public string IsCancel
        {
            get { return isCancel; }
            set { isCancel = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 登录系统
        /// </summary>
        private async void Login()
        {
            try
            {
                if (DialogIsOpen) return;
                if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(PassWord))
                {
                    SnackBar("请输入用户名和密码");
                    return;
                }
                DialogIsOpen = true;
                await Task.Delay(300);
                var loginResult = await repository.LoginAsync(UserName, PassWord);
                if (loginResult.SatusCode != 200)
                {
                    SnackBar(loginResult.Message);
                    return;
                }
                var authResult = await repository.GetAuthListAsync();
                if (authResult.SatusCode != 200)
                {
                    SnackBar(authResult.Message);
                    return;
                }

                /* 关联用户信息,缓存 */
                Contract.Account = loginResult.Result.User.Account;
                Contract.UserName = loginResult.Result.User.UserName;
                Contract.IsAdmin = loginResult.Result.User.FlagAdmin == 1;
                Contract.Menus = loginResult.Result.Menus;
                Contract.AuthItems = authResult.Result;

                WeakReferenceMessenger.Default.Send(string.Empty, "NavigationPage");
            }
            catch (Exception ex)
            {
                SnackBar(ex.Message);
            }
            finally
            {
                DialogIsOpen = false;
            }
        }

        public override void Exit()
        {
            WeakReferenceMessenger.Default.Send(string.Empty, "Exit");
        }
    }
}
