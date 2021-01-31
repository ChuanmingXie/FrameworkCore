/*****************************************************************************
*项目名称:FrameworkCore.PC.Common.Converters
*项目描述:
*类 名 称:IUrlToBitmapConverter
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 20:32:26
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.PC.Common.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Windows.Data;
    using System.Windows.Media.Imaging;

    class IUrlToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string fileurl = $"{AppDomain.CurrentDomain.BaseDirectory}Skin\\Kind\\{value.ToString()}";
                if (File.Exists(fileurl))
                {
                    BitmapImage fileImg = ImageHelper.ConvertToImage(fileurl);
                    return fileImg;
                }
            }
            BitmapImage img = ImageHelper.ConvertToImage($"{AppDomain.CurrentDomain.BaseDirectory}Images\\background.png");
            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
