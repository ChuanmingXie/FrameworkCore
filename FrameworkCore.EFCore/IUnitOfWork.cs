/*****************************************************************************
*项目名称:FrameworkCore.EFCore
*项目描述:
*类 名 称:IUnitOfWork
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 0:04:59
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
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    /// <summary>
    /// 定义工作单元的接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 修改数据库名称,这需要数据库在同一个机台上.
        /// </summary>
        /// <param name="database">数据库名称</param>
        void ChangeDatabase(string database);

        /// <summary>
        /// 获取指定的存储库
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="hasCustomRepository">是否提供自定义存储库</param>
        /// <returns></returns>
        IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class;

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        TContext GetDbContext<TContext>() where TContext : DbContext;

        /// <summary>
        /// 保存所有的数据库上下文的改变
        /// </summary>
        /// <param name="ensuerAutoHistory">是否在保存更改时确保自动记录更改历史</param>
        /// <returns></returns>
        int SaveChanges(bool ensuerAutoHistory = false);

        /// <summary>
        /// 将此工作单元中的所有更改保存到数据库中
        /// </summary>
        /// <param name="ensureAutoHistory">是否在保存更改时确保自动记录更改历史</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(bool ensureAutoHistory = false);

        /// <summary>
        /// 执行特定原生SQL命令
        /// </summary>
        /// <param name="sql">原生sql</param>
        /// <param name="parameters">参数</param>
        /// <returns>被写入数据库中的数目</returns>
        int ExecuteSqlCommand(string sql, params object[] parameters);

        /// <summary>
        /// 使用原生SQL查询,获取特定数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql">原生SQL</param>
        /// <param name="parameters">参数</param>
        /// <returns>满足原生SQL语句所包含的元素</returns>
        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class;

        /// <summary>
        /// 使用TrakGraph 接口来附加断开连接的实体
        /// </summary>
        /// <param name="rootEntity">根实体</param>
        /// <param name="callback">定义委托,用以将对象状态属性转化为实体条目</param>
        void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback);
    }
}
