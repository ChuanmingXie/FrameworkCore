/*****************************************************************************
*项目名称:FrameworkCore.Shared.Common.Collections
*项目描述:
*类 名 称:PagedList
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:45:46
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/


namespace FrameworkCore.Shared.Common.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 页类型的数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : IPagedList<T>
    {
        /// <summary>
        /// 从索引起
        /// </summary>
        public int IndexFrom { get; set; }

        /// <summary>
        /// 获得页的起始页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        ///  获得页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 获得总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 获得总页数
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public IList<T> Items { get; set; }

        /// <summary>
        /// 获取前一页
        /// </summary>
        public bool HasPreviousPage => PageIndex - IndexFrom > 0;

        /// <summary>
        /// 获取下一页
        /// </summary>
        public bool HasNextPage => PageIndex - IndexFrom + 1 < TotalPages;

        /// <summary>
        /// 初始化一个新的分页列表类
        /// </summary>
        /// <param name="source">源数据</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">页内数据量</param>
        /// <param name="indexFrom">起始页</param>
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int indexFrom)
        {
            if (indexFrom > PageIndex)
            {
                throw new ArgumentException($"indexFrom:{indexFrom}>pageIndex:{pageIndex},must indexFrom<=pageIndex");
            }
            if (source is IQueryable<T> querable)
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                IndexFrom = indexFrom;
                TotalCount = querable.Count();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

                Items = querable.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToList();
            }
            else
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                IndexFrom = indexFrom;
                TotalCount = source.Count();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

                Items = source.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToList();
            }
        }

        public PagedList() => Items = new T[0];
    }

    internal class PagedList<TSource, TResult> : IPagedList<TResult>
    {
        /// <summary>
        /// 起始索引
        /// </summary>
        public int IndexFrom { get; }

        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex { get; }

        /// <summary>
        /// 页内大小
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// 页数
        /// </summary>
        public int TotalPages { get; }

        public IList<TResult> Items { get; }

        /// <summary>
        /// 前一页
        /// </summary>
        public bool HasPreviousPage => PageIndex - IndexFrom > 0;

        /// <summary>
        /// 后一页
        /// </summary>
        public bool HasNextPage => PageIndex - IndexFrom + 1 < TotalPages;

        /// <summary>
        /// 初始化分页列表类
        /// </summary>
        /// <param name="source"></param>
        /// <param name="converter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="indexFrom"></param>
        public PagedList(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int pageIndex, int pageSize, int indexFrom)
        {
            if (indexFrom > pageIndex)
            {
                throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");
            }

            if (source is IQueryable<TSource> querable)
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                IndexFrom = indexFrom;
                TotalCount = querable.Count();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

                var items = querable.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToArray();

                Items = new List<TResult>(converter(items));
            }
            else
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                IndexFrom = indexFrom;
                TotalCount = source.Count();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

                var items = source.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToArray();
                Items = new List<TResult>(converter(items));
            }
        }

        /// <summary>
        /// 初始化一个分一列表类
        /// </summary>
        /// <param name="source"></param>
        /// <param name="converter"></param>
        public PagedList(IPagedList<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
        {
            PageIndex = source.PageIndex;
            PageSize = source.PageSize;
            IndexFrom = source.IndexFrom;
            TotalCount = source.TotalCount;
            TotalPages = source.TotalPages;

            Items = new List<TResult>(converter(source.Items));
        }
    }

    public static class PagedList
    {
        public static IPagedList<T> Empty<T>()
            => new PagedList<T>();

        public static IPagedList<TResult> From<TResult, TSource>(IPagedList<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
            => new PagedList<TSource, TResult>(source, converter);
    }
}
