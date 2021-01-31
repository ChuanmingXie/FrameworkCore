/*****************************************************************************
*项目名称:FrameworkCore.EFCore
*项目描述:
*类 名 称:IQueryablePageListExtensions
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 0:04:11
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.EFCore
{
    using FrameworkCore.Shared.Common.Collections;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public static class IQueryablePageListExtensions
    {
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, int indexFrom = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (indexFrom > pageIndex)
            {
                throw new ArgumentException($"indexFrom:{indexFrom}>pageIndex:{pageIndex},must indexFrom<=pageIndex");
            }
            var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);

            var items = await source.ToListAsync(cancellationToken).ConfigureAwait(false);

            /* SQL Server 2008不支持.net Core 3.X 版本的Skip和Take函数*/
            //var items = await source
            //    .Skip((pageIndex - indexFrom) * pageSize)
            //    .Take(pageSize).ToListAsync(cancellationToken)
            //    .ConfigureAwait(false);

            var pagedList = new PagedList<T>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                IndexFrom = indexFrom,
                TotalCount = count,
                Items = items,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            return pagedList;
        }
    }
}
