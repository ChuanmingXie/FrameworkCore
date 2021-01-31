/*****************************************************************************
*项目名称:FrameworkCore.PC.Template
*项目描述:
*类 名 称:DataPage
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 21:34:53
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.PC.Template
{
    using Microsoft.Toolkit.Mvvm.Input;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// 数据分页
    /// </summary>
    public class DataPager : Control
    {
        /// <summary>
        /// 页面大小
        /// </summary>
        public static readonly DependencyProperty PageSizeProperty = DependencyProperty
            .Register("PageSize", typeof(string), typeof(DataPager), new PropertyMetadata(""));
        public string PageSize
        {
            get { return (string)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        /// <summary>
        /// 数据总数
        /// </summary>
        public static readonly DependencyProperty TotalCountProperty = DependencyProperty
            .Register("TotalCount", typeof(string), typeof(DataPager), new PropertyMetadata(""));
        public string TotalCount
        {
            get { return (string)GetValue(TotalCountProperty); }
            set { SetValue(TotalCountProperty, value); }
        }

        /// <summary>
        /// 页面序号
        /// </summary>
        public static readonly DependencyProperty PageIndexProperty = DependencyProperty
            .Register("PageIndex", typeof(string), typeof(DataPager), new PropertyMetadata(""));
        public string PageIndex
        {
            get { return (string)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }

        /// <summary>
        /// 页面总数
        /// </summary>
        public static readonly DependencyProperty PageCountProperty = DependencyProperty
            .Register("PageCount", typeof(string), typeof(DataPager), new PropertyMetadata(""));
        public string PageCount
        {
            get { return (string)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }

        /// <summary>
        /// 到达首页命令
        /// </summary>
        public static readonly DependencyProperty GoHomeCommandProperty = DependencyProperty
            .Register("GoHomePageCommand", typeof(RelayCommand), typeof(DataPager));
        public RelayCommand GoHomePageCommand
        {
            get { return (RelayCommand)GetValue(GoHomeCommandProperty); }
            set { SetValue(GoHomeCommandProperty, value); }
        }

        /// <summary>
        /// 到达下一页
        /// </summary>
        public static readonly DependencyProperty GoNextCommandProperty = DependencyProperty
            .Register("GoNextPageCommad", typeof(RelayCommand), typeof(DataPager));
        public RelayCommand GoNextPageCommad
        {
            get { return (RelayCommand)GetValue(GoNextCommandProperty); }
            set { SetValue(GoNextCommandProperty, value); }
        }

        /// <summary>
        /// 到达指定页
        /// </summary>
        public static readonly DependencyProperty GoOnPageCommandProperty = DependencyProperty
            .Register("GoOnPageCommand", typeof(RelayCommand), typeof(DataPager));
        public RelayCommand GoNextPageCommand
        {
            get { return (RelayCommand)GetValue(GoOnPageCommandProperty); }
            set { SetValue(GoOnPageCommandProperty, value); }
        }

        /// <summary>
        /// 到达尾页
        /// </summary>
        public static readonly DependencyProperty GoEndPageCommandProperty = DependencyProperty
            .Register("GoEndPageCommand", typeof(RelayCommand), typeof(DataPager));
        public RelayCommand GoEndPageCommand
        {
            get { return (RelayCommand)GetValue(GoEndPageCommandProperty); }
            set { SetValue(GoEndPageCommandProperty, value); }
        }
    }
}
