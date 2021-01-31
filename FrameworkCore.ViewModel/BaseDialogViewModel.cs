/*****************************************************************************
*项目名称:FrameworkCore.ViewModel
*项目描述:
*类 名 称:BaseDialogViewModel
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:19:50
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
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;

    public class BaseDialogViewModel : ObservableObject
    {
        public BaseDialogViewModel()
        {
            ExitCommand = new RelayCommand(Exit);
        }

        public RelayCommand ExitCommand { get; private set; }

        /// <summary>
        /// 传递True代表需要确认用户是否关闭,你可以选择传递false强制关闭
        /// </summary>
        public virtual void Exit()
        {
            WeakReferenceMessenger.Default.Send("", "Exit");
        }

        private bool isOpen;

        /// <summary>
        /// 窗口是否显示
        /// </summary>
        public bool DialogIsOpen
        {
            get { return isOpen; }
            set
            {
                isOpen = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 通知异常
        /// </summary>
        /// <param name="msg"></param>
        public void SnackBar(string msg)
        {
            WeakReferenceMessenger.Default.Send(msg, "Snackbar");
        }
    }
}
