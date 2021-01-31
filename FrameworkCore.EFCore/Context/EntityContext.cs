/*****************************************************************************
*项目名称:FrameworkCore.EFCore.Context
*项目描述:
*类 名 称:DatabaseContext
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 0:01:10
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.EFCore.Context
{
    using FrameworkCore.Shared.DataModel;
    using Microsoft.EntityFrameworkCore;

    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserLog> UserLogs { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<GroupUser> GroupUsers { get; set; }

        public DbSet<GroupFunc> GroupFuncs { get; set; }

        public DbSet<Basic> Basics { get; set; }

        public DbSet<BasicType> BasicTypes { get; set; }

        public DbSet<AuthItem> AuthItems { get; set; }

        public DbSet<UserConfig> UserConfigs { get; set; }
    }
}
