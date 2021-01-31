/*****************************************************************************
*项目名称:FrameworkCore.PC.Common
*项目描述:
*类 名 称:VisualHelper
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 21:12:43
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/

namespace FrameworkCore.PC.Common
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// UI可视化帮助类
    /// </summary>
    public static class VisualHelper
    {
        /// <summary>
        /// 设置数据网格列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="frameworkElement"></param>
        /// <param name="name">表格名称</param>
        /// <param name="type">表格绑定类型</param>
        public static void SetDataGridColumns<T>(T frameworkElement, string name, Type type) where T : FrameworkElement
        {
            if (frameworkElement == null)
                throw new ArgumentNullException(nameof(frameworkElement));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            var dataGrid = frameworkElement.FindName(name) as DataGrid;
            if (dataGrid != null)
            {
                var properties = type.GetProperties();
                foreach (var item in properties)
                {
                    var attr = item.GetCustomAttribute<DescriptionAttribute>();
                    if (attr != null)
                    {
                        dataGrid.Columns.Add(new DataGridTextColumn()
                        {
                            Header = attr.Description,
                            Binding = new Binding(item.Name)
                        });
                    }
                }
            }
        }
    }
}
