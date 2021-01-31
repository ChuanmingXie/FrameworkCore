/*****************************************************************************
*项目名称:FrameworkCore.Shared.DataInterfaces
*项目描述:
*类 名 称:ILog
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 22:58:58
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
    public interface ILog
    {
        void Error(Exception exception, string message);

        void Error(string message, params object[] args);

        void Error(string message);

        void Info(Exception exception, string message);

        void Info(string message, params object[] args);

        void Info(string message);

        void Warn(Exception exception, string message);

        void Warn(string message, params object[] args);

        void Warn(string message);

        void Debug(Exception exception, string message);

        void Debug(string message, params object[] args);

        void Debug(string message);
    }
}
