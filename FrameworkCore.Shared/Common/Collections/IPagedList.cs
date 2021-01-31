/*****************************************************************************
*项目名称:FrameworkCore.Shared.Common.Collections
*项目描述:
*类 名 称:IPage
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:44:30
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/


namespace FrameworkCore.Shared.Common.Collections
{
    using System.Collections.Generic;

    /// <summary>
    /// 为任何类型的分页列表提供接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedList<T>
    {
        /// <summary>
        /// 获取索引起始值
        /// </summary>
        int IndexFrom { get; }

        /// <summary>
        /// 获取当前页索引
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// 获取页面大小
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// 获取类型列表的总计数
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// 获取页面总数
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// 获取当前页项
        /// </summary>
        IList<T> Items { get; }

        /// <summary>
        /// 获取前一页
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// 获取下一页
        /// </summary>
        bool HasNextPage { get; }
    }
}
