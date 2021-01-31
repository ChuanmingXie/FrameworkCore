/*****************************************************************************
*项目名称:FrameworkCore.PC.Template
*项目描述:
*类 名 称:Module
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 21:38:58
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using FrameworkCore.ViewModel.Common;
using System.Windows;
using System.Windows.Controls;

namespace FrameworkCore.PC.Template
{
    public class ModuleTemplateSelector : DataTemplateSelector
    {
        public DataTemplate GroupTemplate { get; set; }

        public DataTemplate ExpanderTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ModuleGroup group = (ModuleGroup)item;
            if (group != null)
            {
                if (!group.ContractionTemplate)
                    return ExpanderTemplate;
                else
                    return GroupTemplate;
            }
            return ExpanderTemplate;
        }
    }
}
