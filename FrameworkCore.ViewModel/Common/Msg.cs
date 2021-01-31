/*****************************************************************************
*项目名称:FrameworkCore.ViewModel.Common
*项目描述:
*类 名 称:Msg
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:17:16
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
    using FrameworkCore.ViewModel.Interfaces;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using System;

    /// <summary>
    /// 消息显示类
    /// </summary>
    public class Msg
    {
        /// <summary>
        /// 信息提示
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            WeakReferenceMessenger.Default.Send(msg, "Snackbar");
        }

        /// <summary>
        /// 错误提示
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            WeakReferenceMessenger.Default.Send(msg, "Snackbar");
        }

        /// <summary>
        /// 操作警告
        /// </summary>
        /// <param name="msg"></param>
        public static void Warning(string msg)
        {
            WeakReferenceMessenger.Default.Send(msg, "Snackbar");
        }

        /// <summary>
        /// 操作确认
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public async static Task<bool> Question(string msg)
        {
            return await Show(Notify.Question, msg);
        }

        /// <summary>
        /// 弹窗提示
        /// </summary>
        /// <param name="question"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private async static Task<bool> Show(Notify notify, string msg)
        {
            string Icon = string.Empty;
            string Color = string.Empty;
            switch (notify)
            {
                case Notify.Error:
                    Icon = "CommentWarning";
                    Color = "#FF4500";
                    break;
                case Notify.Waring:
                    Icon = "CommentWaring";
                    Color = "#FF8247";
                    break;
                case Notify.Info:
                    Icon = "CommentProcessingOutline";
                    Color = "#1C86EE";
                    break;
                case Notify.Question:
                    Icon = "CommentQuestionQutline";
                    Color = "#20B2AA";
                    break;
            }
            var dialog = NetCoreProvider.ResolveNamed<IMsgCenter>("MsgCenter");
            return await dialog.Show(new { Msg = msg, Color, Icon });
        }
    }

    /// <summary>
    /// 系统提示的集中类型
    /// </summary>
    public enum Notify
    {
        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error,

        /// <summary>
        /// 警告
        /// </summary>
        [Description("警告")]
        Waring,

        /// <summary>
        /// 提示信息
        /// </summary>
        [Description("提示信息")]
        Info,

        /// <summary>
        /// 询问信息
        /// </summary>
        [Description("询问信息")]
        Question
    }
}
