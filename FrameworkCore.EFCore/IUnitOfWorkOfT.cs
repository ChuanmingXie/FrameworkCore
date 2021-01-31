/*****************************************************************************
*项目名称:FrameworkCore.EFCore
*项目描述:
*类 名 称:IUnitOfWorkOfT
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 0:05:12
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
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext DbContext { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ensureAutoHistory">在保存更改时自动记录更改历史</param>
        /// <param name="unitOfWorks">操作组</param>
        /// <returns>异步保存操作,任务结果包含写入数据库的状态实体数目.</returns>
        Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks);
    }
}
