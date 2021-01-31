/*****************************************************************************
*项目名称:FrameworkCore.EFCore
*项目描述:
*类 名 称:Repository
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 0:05:35
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using FrameworkCore.Shared.Common.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace FrameworkCore.EFCore
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            dbSet = dbContext.Set<TEntity>();
        }

        /// <summary>
        /// 获取均值
        /// </summary>
        /// <param name="predicate">条件函数</param>
        /// <param name="selector">映射选择器</param>
        /// <returns></returns>
        public virtual decimal Average(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null)
        {
            if (predicate == null)
                return dbSet.Average(selector);
            else
                return dbSet.Where(predicate).Average(selector);
        }

        /// <summary>
        /// 异步获取均值
        /// </summary>
        /// <param name="predicate">条件函数</param>
        /// <param name="selector">映射选择器</param>
        /// <returns></returns>
        public virtual async Task<decimal> AverageAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null)
        {
            if (predicate == null)
                return await dbSet.AverageAsync(selector);
            else
                return await dbSet.Where(predicate).AverageAsync(selector);
        }

        /// <summary>
        /// 更新实体状态
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <param name="state">状态</param>
        public void ChangeEntityState(TEntity entity, EntityState state)
        {
            dbContext.Entry(entity).State = state;
        }

        /// <summary>
        /// 更新表名称
        /// </summary>
        /// <param name="table">表名</param>
        public virtual void ChangeTable(string table)
        {
            if (dbContext.Model.FindEntityType(typeof(TEntity)) is IConventionEntityType relational)
            {
                relational.SetTableName(table);
            }
        }

        /// <summary>
        /// 基于条件函数,获取行数
        /// </summary>
        /// <param name="predicate">条件函数</param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return dbSet.Count();
            else
                return dbSet.Count(predicate);
        }

        /// <summary>
        /// 基于条件函数,获取行数
        /// </summary>
        /// <param name="predicate">条件函数</param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return await dbSet.CountAsync();
            else
                return await dbSet.CountAsync(predicate);
        }

        /// <summary>
        /// 通过特定主键,删除实体数据
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            var typeInfo = typeof(TEntity).GetTypeInfo();
            var key = dbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                property.SetValue(entity, id);
                dbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                var entity = dbSet.Find(id);
                if (entity != null)
                    Delete(entity);
            }
        }

        /// <summary>
        /// 删除特定实体数据
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity entity) => dbSet.Remove(entity);

        /// <summary>
        /// 删除特定实体数据
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Delete(params TEntity[] entities) => dbSet.RemoveRange(entities);

        /// <summary>
        /// 删除特定实体数据
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Delete(IEnumerable<TEntity> entities) => dbSet.RemoveRange(entities);

        /// <summary>
        /// 基于映射条件,判断是否存在
        /// </summary>
        /// <param name="selector">映射选择器</param>
        /// <returns></returns>
        public bool Exists(Expression<Func<TEntity, bool>> selector = null)
        {
            if (selector == null)
                return dbSet.Any();
            else
                return dbSet.Any(selector);
        }

        /// <summary>
        /// 基于映射条件,异步判断是否存在
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> selector = null)
        {
            if (selector == null)
                return await dbSet.AnyAsync();
            else
                return await dbSet.AnyAsync(selector);
        }

        /// <summary>
        /// 通过主键数组查找实体数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public virtual TEntity Find(params object[] keyValues) => dbSet.Find(keyValues);

        /// <summary>
        /// 通过主键数组,异步查找实体数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public virtual ValueTask<TEntity> FindAsync(params object[] keyValues) => dbSet.FindAsync(keyValues);

        /// <summary>
        /// 通过主键数组,异步查找实体数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken) => dbSet.FindAsync(keyValues, cancellationToken);

        /// <summary>
        /// 用原生的SQL语句获取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IQueryable<TEntity> FromSql(string sql, params object[] parameters) => dbSet.FromSqlRaw(sql, parameters);

        /// <summary>
        /// 获取全部实体,不推荐使用该方法
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }

        /// <summary>
        /// 获取全部实体,不推荐使用该方法
        /// </summary>
        /// <param name="predicate">条件函数</param>
        /// <param name="orderBy">排序函数</param>
        /// <param name="include">包含导航属性的函数</param>
        /// <param name="disableTracking">禁止追踪</param>
        /// <param name="ignoreQueryFilterts">忽略查询过略器</param>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true, bool ignoreQueryFilterts = false)
        {
            IQueryable<TEntity> query = dbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);
            if (ignoreQueryFilterts)
                query = query.IgnoreQueryFilters();
            if (orderBy != null)
                return orderBy(query);
            else
                return query;
        }

        /// <summary>
        /// 异步获取全部实体,不推荐使用该方法
        /// </summary>
        /// <returns></returns>
        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        /// <summary>
        /// 异步获取全部实体,不推荐使用该方法
        /// </summary>
        /// <param name="predicate">条件函数</param>
        /// <param name="orderBy">排序函数</param>
        /// <param name="include">包含导航属性的函数</param>
        /// <param name="disableTracking">禁止追踪</param>
        /// <param name="ignoreQueryFilterts">忽略查询过略器</param>
        /// <returns></returns>
        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = dbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);
            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();
            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            else
                return await query.ToListAsync();
        }

        /// <summary>
        /// 获取首行实体信息
        /// </summary>
        /// <param name="predicate">条件函数</param>
        /// <param name="orderBy">排序函数</param>
        /// <param name="include">包含导航属性的函数</param>
        /// <param name="disableTracking">禁止追踪</param>
        /// <param name="ignoreQueryFilters">忽略查询过略器</param>
        /// <returns></returns>
        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = dbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);
            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();
            if (orderBy != null)
                return orderBy(query).FirstOrDefault();
            else
                return query.FirstOrDefault();
        }

        /// <summary>
        /// 获取首行实体信息
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector">映射选择器</param>
        /// <param name="predicate">条件函数</param>
        /// <param name="orderBy">排序函数</param>
        /// <param name="include">包含导航属性的函数</param>
        /// <param name="disableTracking">禁止追踪</param>
        /// <param name="ignoreQueryFilters">忽略查询过略器</param>
        /// <returns></returns>
        public virtual TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector
            , Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = dbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);
            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();
            if (orderBy != null)
                return orderBy(query).Select(selector).FirstOrDefault();
            else
                return query.Select(selector).FirstOrDefault();
        }

        /// <summary>
        /// 异步获取首行实体信息
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector">映射选择器</param>
        /// <param name="predicate">条件函数</param>
        /// <param name="orderBy">排序函数</param>
        /// <param name="include">包含导航属性的函数</param>
        /// <param name="disableTracking">禁止追踪</param>
        /// <param name="ignoreQueryFilters">忽略查询过略器</param>
        /// <returns></returns>
        public virtual async Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector
            , Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = dbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);
            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();
            if (orderBy != null)
                return await orderBy(query).Select(selector).FirstOrDefaultAsync();
            else
                return await query.Select(selector).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 异步获取首行实体信息
        /// </summary>
        /// <param name="predicate">条件函数</param>
        /// <param name="orderBy">排序函数</param>
        /// <param name="include">包含导航属性的函数</param>
        /// <param name="disableTracking">禁止追踪</param>
        /// <param name="ignoreQueryFilters">忽略查询过略器</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = dbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);
            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();
            if (orderBy != null)
                return await orderBy(query).FirstOrDefaultAsync();
            else
                return await query.FirstOrDefaultAsync();

        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="predicate">条件函数</param>
        /// <param name="orderBy">排序函数</param>
        /// <param name="include">包含导航属性函数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="disableTracking">禁止追踪</param>
        /// <param name="ignoreQueryFilters">忽略查询过滤</param>
        /// <returns></returns>
        public virtual IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , int pageIndex = 0, int pageSize = 30, bool disableTracking = true, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = dbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);
            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();
            if (orderBy != null)
                return orderBy(query).ToPagedList(pageIndex, pageSize);
            else
                return query.ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector">映射选择器</param>
        /// <param name="predicate">条件函数</param>
        /// <param name="orderBy">排序函数</param>
        /// <param name="include">包含导航属性函数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="disableTracking">禁止追踪</param>
        /// <param name="ignoreQueryFilters">忽略查询过滤器</param>
        /// <returns></returns>
        public virtual IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector
            , Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , int pageIndex = 0, int pageSize = 30, bool disableTracking = true, bool ignoreQueryFilters = false) where TResult : class
        {
            IQueryable<TEntity> query = dbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);
            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();
            if (orderBy != null)
                return orderBy(query).Select(selector).ToPagedList(pageIndex, pageSize);
            else
                return query.Select(selector).ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 异步获取分页数据
        /// </summary>
        /// <param name="predicate">条件函数</param>
        /// <param name="orderBy">排序函数</param>
        /// <param name="include">包含导航属性的函数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="disableTracking">禁止追踪</param>
        /// <param name="cancellationToken">传播取消操作的通知</param>
        /// <param name="ignoreQueryFilters">忽略查询过滤器</param>
        /// <returns></returns>
        public virtual Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , int pageIndex = 0, int pageSize = 30, bool disableTracking = true
            , CancellationToken cancellationToken = default, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = dbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);
            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();
            if (orderBy != null)
                return orderBy(query).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
            else
                return query.ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
        }

        /// <summary>
        /// 异步获取分页数据
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector">选择映射函数</param>
        /// <param name="predicate">条件函数</param>
        /// <param name="orderBy">排序函数</param>
        /// <param name="include">包含导航属性的函数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="disableTracking">禁止追踪</param>
        /// <param name="cancellationToken">传播取消操作的通知</param>
        /// <param name="ignoreQueryFilters">忽略查询过滤器</param>
        /// <returns></returns>
        public virtual Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector
            , Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , int pageIndex = 0, int pageSize = 30, bool disableTracking = true
            , CancellationToken cancellationToken = default, bool ignoreQueryFilters = false) where TResult : class
        {
            IQueryable<TEntity> query = dbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);
            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();
            if (orderBy != null)
                return orderBy(query).Select(selector).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
            else
                return query.Select(selector).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
        }

        /// <summary>
        /// 同步插入一行实体数据
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public virtual TEntity Insert(TEntity entity)
        {
            return dbSet.Add(entity).Entity;
        }

        /// <summary>
        /// 同步插入多行实体数据
        /// </summary>
        /// <param name="entities">实体数据</param>
        public virtual void Insert(params TEntity[] entities) => dbSet.AddRange(entities);

        /// <summary>
        /// 同步插入多行实体数据
        /// </summary>
        /// <param name="entities">实体数据</param>
        public virtual void Insert(IEnumerable<TEntity> entities) => dbSet.AddRange(entities);

        /// <summary>
        /// 异步插入一行实体数据
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <param name="cancellationToken">传播取消操作的通知</param>
        /// <returns></returns>
        public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return dbSet.AddAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 异步插入多行实体数据
        /// </summary>
        /// <param name="entities">实体数据</param>
        /// <returns></returns>
        public virtual Task InsertAsync(params TEntity[] entities) => dbSet.AddRangeAsync(entities);

        /// <summary>
        /// 异步插入多行实体数据
        /// </summary>
        /// <param name="entities">实体数据</param>
        /// <param name="cancellationToken">传播取消操作的通知</param>
        /// <returns></returns>
        public virtual Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) => dbSet.AddRangeAsync(entities, cancellationToken);

        /// <summary>
        /// 基于条件函数,获取数据个数
        /// </summary>
        /// <param name="predicate">条件函数</param>
        /// <returns></returns>
        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return dbSet.LongCount();
            else
                return dbSet.LongCount(predicate);
        }

        /// <summary>
        /// 基于条件函数,异步获取数据个数
        /// </summary>
        /// <param name="predicate">条件函数</param>
        /// <returns></returns>
        public virtual async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return await dbSet.LongCountAsync();
            else
                return await dbSet.LongCountAsync(predicate);

        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">条件函数</param>
        /// <param name="selector">映射选择器</param>
        /// <returns></returns>
        public virtual T Max<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null)
        {
            if (predicate == null)
                return dbSet.Max(selector);
            else
                return dbSet.Where(predicate).Max(selector);
        }

        /// <summary>
        /// 异步获取最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">条件函数</param>
        /// <param name="selector">映射选择器</param>
        /// <returns></returns>
        public virtual async Task<T> MaxAsync<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null)
        {
            if (predicate == null)
                return await dbSet.MaxAsync(selector);
            else
                return await dbSet.Where(predicate).MaxAsync(selector);
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">条件函数</param>
        /// <param name="selector">映射选择器</param>
        /// <returns></returns>
        public virtual T Min<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null)
        {
            if (predicate == null)
                return dbSet.Min(selector);
            else
                return dbSet.Where(predicate).Min(selector);
        }

        /// <summary>
        /// 异步获取最小值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">条件函数</param>
        /// <param name="selector">映射选择器</param>
        /// <returns></returns>
        public virtual async Task<T> MinAsync<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null)
        {
            if (predicate == null)
                return await dbSet.MinAsync(selector);
            else
                return await dbSet.Where(predicate).MinAsync(selector);
        }

        /// <summary>
        /// 获取总和
        /// </summary>
        /// <param name="predicate">条件函数</param>
        /// <param name="selector">映射选择器</param>
        /// <returns></returns>
        public virtual decimal Sum(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null)
        {
            if (predicate == null)
                return dbSet.Sum(selector);
            else
                return dbSet.Where(predicate).Sum(selector);
        }

        /// <summary>
        /// 异步获取总和
        /// </summary>
        /// <param name="predicate">条件函数</param>
        /// <param name="selector">映射选择器</param>
        /// <returns></returns>
        public virtual async Task<decimal> SumAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null)
        {
            if (predicate == null)
                return await dbSet.SumAsync(selector);
            else
                return await dbSet.Where(predicate).SumAsync(selector);
        }

        /// <summary>
        /// 更新一行实体数据
        /// </summary>
        /// <param name="entity">实体数据</param>
        public virtual void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        /// <summary>
        /// 更新多行实体数据
        /// </summary>
        /// <param name="entities">实体数据</param>
        public virtual void Update(params TEntity[] entities) => dbSet.UpdateRange(entities);

        /// <summary>
        /// 异步更新多行实体数据
        /// </summary>
        /// <param name="entities">实体数据</param>
        public virtual void Update(IEnumerable<TEntity> entities) => dbSet.UpdateRange(entities);
    }
}
