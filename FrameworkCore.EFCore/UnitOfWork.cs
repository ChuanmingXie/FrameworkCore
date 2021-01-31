/*****************************************************************************
*项目名称:FrameworkCore.EFCore
*项目描述:
*类 名 称:UnitOfWork
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 0:05:51
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
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Transactions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata;

    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>, IUnitOfWork where TContext : DbContext
    {
        private readonly TContext context;
        private bool disposed = false;
        private Dictionary<Type, object> repositories;

        public UnitOfWork(TContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TContext Dbcontext => context;

        public TContext DbContext => throw new NotImplementedException();

        public void ChangeDatabase(string database)
        {
            var connection = context.Database.GetDbConnection();
            if (connection.State.HasFlag(ConnectionState.Open))
                connection.ChangeDatabase(database);
            else
            {
                var connectionString = Regex.Replace(connection.ConnectionString.Replace(" ", "")
                    , @"(?<=[Db]atabase=)\w+(?=;)", database, RegexOptions.Singleline);
                connection.ConnectionString = connectionString;
            }
            var items = context.Model.GetEntityTypes();
            foreach (var item in items)
            {
                if (item is IConventionEntityType entityType)
                    entityType.SetSchema(database);
            }
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
            => context.Database.ExecuteSqlRaw(sql, parameters);

        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
            => context.Set<TEntity>().FromSqlRaw(sql, parameters);

        public TDbContext GetDbContext<TDbContext>() where TDbContext : DbContext
        {
            if (context == null)
                return null;
            return context as TDbContext;
        }

        public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class
        {
            if (repositories == null)
                repositories = new Dictionary<Type, object>();
            if (hasCustomRepository)
            {
                var customRepo = context.GetService<IRepository<TEntity>>();
                if (customRepo != null)
                    return customRepo;
            }
            var type = typeof(TEntity);
            if (!repositories.ContainsKey(type))
                repositories[type] = new Repository<TEntity>(context);
            return (IRepository<TEntity>)repositories[type];
        }

        public int SaveChanges(bool ensuerAutoHistory = false)
        {
            if (ensuerAutoHistory)
                context.EnsureAutoHistory();
            return context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks)
        {
            using var ts = new TransactionScope();
            var count = 0;
            foreach (var unitOfWork in unitOfWorks)
            {
                count += await unitOfWork.SaveChangesAsync(ensureAutoHistory);
            }
            count += await SaveChangesAsync(ensureAutoHistory);

            ts.Complete();
            return count;
        }

        /// <summary>
        /// 将工作单元中的所有操作保存到数据库中
        /// </summary>
        /// <param name="ensureAutoHistory"></param>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false)
        {
            if (ensureAutoHistory)
                context.EnsureAutoHistory();
            return await context.SaveChangesAsync();
        }

        public void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback)
        {
            context.ChangeTracker.TrackGraph(rootEntity, callback);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                if (repositories != null)
                    repositories.Clear();
                context.Dispose();
            }
            disposed = true;
        }
    }
}
