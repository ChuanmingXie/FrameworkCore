/*****************************************************************************
*项目名称:FrameworkCore.Shared.Common.Collections
*项目描述:
*类 名 称:IEnumber
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:43:41
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.Common.Collections
{
    /// <summary>
    /// 提供扩展方法来提供分页
    /// </summary>
    public static class IEnumerablePagedListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source
            , int pageIndex
            , int pageSize
            , int indexFrom = 0)
            => new PagedList<T>(source, pageIndex, pageSize, indexFrom);

        public static IPagedList<TResult> ToPagedList<TSource, TResult>(this IEnumerable<TSource> source
            , Func<IEnumerable<TSource>, IEnumerable<TResult>> converter
            , int pageIndex, int pageSize
            , int indexFrom = 0)
            => new PagedList<TSource, TResult>(source, converter, pageIndex, pageSize, indexFrom);
    }
}
