/*****************************************************************************
*项目名称:FrameworkCore.PC.Common
*项目描述:
*类 名 称:PaletteHelperExtensions
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 21:12:06
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/

namespace FrameworkCore.PC.Common
{
    using MaterialDesignColors;
    using MaterialDesignThemes.Wpf;
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;
    using System.Text;
    using MaterialDesignColors.ColorManipulation;

    /// <summary>
    /// 主题扩展类
    /// </summary>
    public static class PaletteHelperExtensions
    {
        public static void ChangePrimaryColor(this PaletteHelper paletteHelper, Color color)
        {
            ITheme theme = paletteHelper.GetTheme();

            theme.PrimaryLight = new ColorPair(color.Lighten(), theme.PrimaryLight.ForegroundColor);
            theme.PrimaryMid = new ColorPair(color, theme.PrimaryMid.ForegroundColor);
            theme.PrimaryDark = new ColorPair(color.Darken(), theme.PrimaryDark.ForegroundColor);

            paletteHelper.SetTheme(theme);
        }

        public static void ChangeSecondaryColor(this PaletteHelper paletteHelper, Color color)
        {
            ITheme theme = paletteHelper.GetTheme();

            theme.SecondaryLight = new ColorPair(color.Lighten(), theme.SecondaryLight.ForegroundColor);
            theme.SecondaryMid = new ColorPair(color, theme.SecondaryMid.ForegroundColor);
            theme.SecondaryDark = new ColorPair(color.Darken(), theme.SecondaryDark.ForegroundColor);

            paletteHelper.SetTheme(theme);
        }
    }
}
