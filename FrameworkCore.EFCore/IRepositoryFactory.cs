/*****************************************************************************
*项目名称:FrameworkCore.EFCore
*项目描述:
*类 名 称:IRepositoryFactory
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 0:04:45
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
    public interface IRepositoryFactory
    {
        /// <summary>
        /// 获取特定的存储库数据
        /// </summary>
        /// <typeparam name="TEntity">泛型实体类型</typeparam>
        /// <param name="hasCustomRepository">是否提供特定存储库</param>
        /// <returns></returns>
        IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class;
    }
}
