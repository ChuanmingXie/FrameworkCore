/*****************************************************************************
*项目名称:FrameworkCore.EFCore
*项目描述:
*类 名 称:IRepository
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 0:04:26
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
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.Query;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore;
    using FrameworkCore.Shared.Common.Collections;

    /// <summary>
    /// 为通用存储库定义接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 更改表名
        /// </summary>
        /// <param name="table"></param>
        /// 这只用于支持同意模型中的多个表,需要相同数据库中的表
        void ChangeTable(string table);

        /// <summary>
        /// 基于谓词、orderby委托和页面信息获取IPagedList<TEntity>
        /// </summary>
        /// <param name="predicate">用于测试条件的每个元素的函数</param>
        /// <param name="orderBy">对元素进行排序的函数</param>
        /// <param name="include">包含导航属性的函数</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="disableTracking">禁用更改跟踪</param>
        /// <param name="ignoreQueryFilters">忽略查询过滤器</param>
        /// <returns></returns>
        IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , int pageIndex = 0
            , int pageSize = 30
            , bool disableTracking = true
            , bool ignoreQueryFilters = false);

        /// <summary>
        /// 基于谓词、orderby委托和页面信息获取IPagedList<TEntity>
        /// </summary>
        /// <param name="predicate">用于测试条件的每个元素的函数</param>
        /// <param name="orderBy">对元素进行排序的函数</param>
        /// <param name="include">包含导航属性的函数</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="disableTracking">禁用跟踪更改</param>
        /// <param name="cancellationToken">观察等待任务结束</param>
        /// <param name="ingoreQueryFilters">忽略查询过滤器</param>
        /// <returns></returns>
        Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , int pageIndex = 0
            , int pageSize = 30
            , bool disableTracking = true
            , CancellationToken cancellationToken = default(CancellationToken)
            , bool ignoreQueryFilters = false);

        /// <summary>
        /// 基于谓词、orderby委托和页面信息获取IPagedList<TResult>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector">映射选择器</param>
        /// <param name="predicate">用于测试每个元素的条件函数</param>
        /// <param name="orderBy">对元素进行排序的函数</param>
        /// <param name="include">包含导航属性函数</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="disableTracking">禁用追踪更改</param>
        /// <param name="ignoreQueryFilters">忽略查询过滤器</param>
        /// <returns></returns>
        IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector
            , Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , int pageIndex = 0
            , int pageSize = 30
            , bool disableTracking = true
            , bool ignoreQueryFilters = false) where TResult : class;

        /// <summary>
        /// 基于谓词、orderby委托和页面信息获取IPagedList<TResult>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector">映射选择器</param>
        /// <param name="predicate">用于测试每个元素的条件函数</param>
        /// <param name="orderBy">对元素进行排序的函数</param>
        /// <param name="include">包含导航属性函数</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="disableTracking">禁止追踪更改</param>
        /// <paramref name="cancellationToken">一个观察等待任务完成的令牌</paramref>
        /// <param name="ignoreQueryFilters">忽略查询过滤器</param>
        /// <returns></returns>
        Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector
            , Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , int pageIndex = 0
            , int pageSize = 30
            , bool disableTracking = true
            , CancellationToken cancellationToken = default(CancellationToken)
            , bool ignoreQueryFilters = false) where TResult : class;

        /// <summary>
        /// 基于条件函数、orderby委托和导航属性委托获取首个或默认的实体信息.该方法默认只获取一行,不追踪
        /// </summary>
        /// <param name="predicate">测试元素的条件函数</param>
        /// <param name="orderBy">排序函数</param>
        /// <param name="include">导航属性函数</param>
        /// <param name="disableTracking">禁止追踪更改</param>
        /// <param name="ignoreQueryFilters">忽略查询过滤器</param>
        /// <returns></returns>
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true
            , bool ignoreQueryFilters = false);

        /// <summary>
        /// 基于条件函数、排序委托、包含的属性导航委托获取首条或默认的实体信息
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector">映射选择器</param>
        /// <param name="predicate">测试每个元素的条件函数</param>
        /// <param name="orderBy">排序函数</param>
        /// <param name="include">包含导航属性的函数</param>
        /// <param name="disableTracking">禁止追踪</param>
        /// <param name="ignoreQueryFilters">忽略查询过滤器</param>
        /// <returns></returns>
        TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector
            , Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true
            , bool ignoreQueryFilters = false);

        /// <summary>
        /// 基于条件函数、orderby委托、包含导航属性的委托获取首条或默认的信息行,该方法默认只读取一个
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector">映射选择器</param>
        /// <param name="predicate">测试每个元素的条件函数</param>
        /// <param name="orderBy">排序函数</param>
        /// <param name="include">包含导航属性的函数</param>
        /// <param name="disableTracking">禁止追踪</param>
        /// <param name="ignoreQueryFilters">忽略查询过滤器</param>
        /// <returns></returns>
        Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector
            , Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true
            , bool ignoreQueryFilters = false);

        /// <summary>
        /// 基于条件函数、orderby委托、包含导航属性的委托获取首条或默认的信息行,该方法默认只读取一个
        /// </summary>
        /// <param name="predicate">测试每个元素的条件函数</param>
        /// <param name="orderBy">排序函数</param>
        /// <param name="include">包含导航属性的函数</param>
        /// <param name="disableTracking">禁止追踪</param>
        /// <param name="ignoreQueryFilters">忽略查询过滤器</param>
        /// <returns></returns>
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true
            , bool ignoreQueryFilters = false);

        /// <summary>
        /// 用原生的SQL语句获取指定的数据
        /// </summary>
        /// <param name="sql">SQl语句</param>
        /// <param name="parameters">其他参数</param>
        /// <returns></returns>
        IQueryable<TEntity> FromSql(string sql, params object[] parameters);

        /// <summary>
        /// 通过主键值查询实体,如果找到则返回相应内容,否则返回null
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// 通过主键值查询实体,如果找到则返回相应内容,否则返回null
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        ValueTask<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        /// 通过主键值查询实体,如果找到则返回相应内容,否则返回null
        /// </summary>
        /// <param name="keyValues"></param>
        /// <param name="cancellationToken">观察等待完成的令牌</param>
        /// <returns></returns>
        ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);

        /// <summary>
        /// 获取所有实体,不推荐使用该方法
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 获取所有实体,不推荐使用该方法
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include">包含导航属性的函数</param>
        /// <param name="disableTracking"></param>
        /// <param name="ignoreQueryFilterts"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true
            , bool ignoreQueryFilterts = false);

        /// <summary>
        /// 获取所有实体,不推荐使用该方法
        /// </summary>
        /// <returns></returns>
        Task<IList<TEntity>> GetAllAsync();

        /// <summary>
        /// 获取所有实体,不推荐使用该方法
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <param name="ignoreQueryFilters"></param>
        /// <returns></returns>
        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , bool disableTracking = true
            , bool ignoreQueryFilters = false);

        /// <summary>
        /// 基于谓词条件函数,获取计数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 基于谓词条件函数,异步获取计数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 基于谓词条件函数,获取长整类型的计数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 基于谓词条件函数,异步获取长整型的计数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        T Max<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null);

        /// <summary>
        /// 异步获取最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        Task<T> MaxAsync<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null);

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        T Min<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null);

        /// <summary>
        /// 异步获取最小值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name=""></param>
        /// <returns></returns>
        Task<T> MinAsync<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null);

        /// <summary>
        /// 获取均值
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        decimal Average(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null);

        /// <summary>
        /// 异步获取均值
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        Task<decimal> AverageAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null);

        /// <summary>
        /// 获取总和
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        decimal Sum(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null);

        /// <summary>
        /// 异步获取总和
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        Task<decimal> SumAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> selector = null);

        /// <summary>
        /// 异步判定是否存在
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> selector = null);

        /// <summary>
        /// 同步插入一行实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// 同步插入一组实体数据
        /// </summary>
        /// <param name="entities"></param>
        void Insert(params TEntity[] entities);

        /// <summary>
        /// 同步插入一组实体数据
        /// </summary>
        /// <param name="entities"></param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// 异步插入新的实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 异步插入一组实体数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task InsertAsync(params TEntity[] entities);

        /// <summary>
        /// 异步插入一组实体数据
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 更新特定实体数据
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// 更新特地实体数据
        /// </summary>
        /// <param name="entities"></param>
        void Update(params TEntity[] entities);

        /// <summary>
        /// 更新特定实体数据
        /// </summary>
        /// <param name="entities"></param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// 删除实体数据通过主键
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);

        /// <summary>
        /// 删除特定实体数据
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// 删除特定实体数据
        /// </summary>
        /// <param name="entities"></param>
        void Delete(params TEntity[] entities);

        /// <summary>
        /// 删除特定实体数据
        /// </summary>
        /// <param name="entities"></param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新实体状态
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="state"></param>
        void ChangeEntityState(TEntity entity, EntityState state);
    }
}
