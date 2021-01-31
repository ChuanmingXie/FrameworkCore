/*****************************************************************************
*项目名称:FrameworkCore.Shared.Common.Query
*项目描述:
*类 名 称:QueryParameters
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:34:06
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.Common.Query
{
    public class QueryParameters
    {
        private int _pageIndex = 0;

        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value;
        }

        private int _pageSize = 30;
        public virtual int PageSize
        {
            get => _pageSize;
            set => _pageSize = value;
        }

        public string Search { get; set; }
    }
}
