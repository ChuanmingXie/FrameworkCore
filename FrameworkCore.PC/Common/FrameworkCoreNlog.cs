/*****************************************************************************
*项目名称:FrameworkCore.PC.Common
*项目描述:
*类 名 称:FrameworkCoreNlog
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 21:10:58
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using FrameworkCore.Shared.DataInterfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.PC.Common
{
    public class FrameworkCoreNLog : ILog
    {
        private readonly Logger logger;

        public FrameworkCoreNLog()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        public void Debug(Exception exception, string message)
        {
            logger.Debug(exception, message);
        }

        public void Debug(string message, params object[] args)
        {
            logger.Debug(message, args);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(Exception exception, string message)
        {
            logger.Error(exception, message);
        }

        public void Error(string message, params object[] args)
        {
            logger.Error(message, args);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Info(Exception exception, string message)
        {
            logger.Info(exception, message);
        }

        public void Info(string message, params object[] args)
        {
            logger.Info(message, args);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Warn(Exception exception, string message)
        {
            logger.Warn(exception, message);
        }

        public void Warn(string message, params object[] args)
        {
            logger.Warn(message, args);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }
    }
}
