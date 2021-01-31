/*****************************************************************************
*项目名称:FrameworkCore.PC.Common
*项目描述:
*类 名 称:ImageHelper
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 20:33:40
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace FrameworkCore.PC.Common
{
    /// <summary>
    /// 图标操作类
    /// </summary>
    public class ImageHelper
    {
        public static BitmapImage ConvertToImage(string fileName)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(fileName);
            bmp.EndInit();
            bmp.Freeze();
            return bmp;
        }
    }
}
