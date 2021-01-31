/*****************************************************************************
*项目名称:FrameworkCore.EFCore
*项目描述:
*类 名 称:CustomRepositoryManagement
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:59:02
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
    using FrameworkCore.EFCore.Context;
    using FrameworkCore.Shared.DataModel;

    public class CustomUserRepository : Repository<User>, IRepository<User>
    {
        public CustomUserRepository(EntityContext dbContext) : base(dbContext) { }
    }

    public class CustomUserLogRepository : Repository<UserLog>, IRepository<UserLog>
    {
        public CustomUserLogRepository(EntityContext dbContext) : base(dbContext) { }
    }

    public class CustomMenuRepository : Repository<Menu>, IRepository<Menu>
    {
        public CustomMenuRepository(EntityContext dbContext) : base(dbContext) { }
    }

    public class CustomGroupRepository : Repository<Group>, IRepository<Group>
    {
        public CustomGroupRepository(EntityContext dbContext) : base(dbContext) { }
    }

    public class CustomBasicRepository : Repository<Basic>, IRepository<Basic>
    {
        public CustomBasicRepository(EntityContext dbContext) : base(dbContext) { }
    }

    public class CustomAuthRepository : Repository<AuthItem>, IRepository<AuthItem>
    {
        public CustomAuthRepository(EntityContext dbContext) : base(dbContext) { }
    }
}
