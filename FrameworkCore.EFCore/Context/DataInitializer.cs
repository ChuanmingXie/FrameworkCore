
/*****************************************************************************
*项目名称:FrameworkCore.EFCore.Context
*项目描述:
*类 名 称:Data
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 0:02:58
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
    using FrameworkCore.Shared.DataInterfaces;
    using FrameworkCore.Shared.DataModel;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class DataInitializer : IDataInitializer
    {
        private readonly ILogger<DataInitializer> logger;
        private readonly EntityContext context;

        public DataInitializer(ILogger<DataInitializer> logger, EntityContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        /// <summary>
        /// 创建数据库结构
        /// </summary>
        /// <returns></returns>
        public async Task InitSampleDataAsync()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            await this.CreateSampleDataAsync();
        }

        /// <summary>
        /// 填入测试数据
        /// </summary>
        /// <returns></returns>
        private async Task CreateSampleDataAsync()
        {
            if (!context.Users.Any() && !context.Menus.Any() && !context.AuthItems.Any())
            {
                context.Users.AddRange(
                    new User() { Account = "Admin", UserName = "张三鱼", Address = "naite", Telephone = "18282828282", Password = "123", CreateTime = DateTime.Now, FlagAdmin = 1 },
                    new User() { Account = "Engineer", UserName = "李大四", Address = "zhuhaichao", Telephone = "18282828282", Password = "123", CreateTime = DateTime.Now, FlagAdmin = 1 },
                    new User() { Account = "Opeations", UserName = "王二小", Address = "huilihua", Telephone = "18282828282", Password = "123", CreateTime = DateTime.Now, FlagAdmin = 1 }
                    );
                context.Menus.AddRange(
                    new Menu() { MenuCode = "1001", MenuName = "用户管理", MenuCaption = "AccountBox", MenuNameSpcace = "UserCenter", MenuAuth = 7 },
                    new Menu() { MenuCode = "1002", MenuName = "权限管理", MenuCaption = "Group", MenuNameSpcace = "GroupCenter", MenuAuth = 7 },
                    new Menu() { MenuCode = "1003", MenuName = "制图操作", MenuCaption = "TelevisionGuide", MenuNameSpcace = "DashboardCenter", MenuAuth = 8 },
                    new Menu() { MenuCode = "1004", MenuName = "个性化", MenuCaption = "Palette", MenuNameSpcace = "SkinCenter", MenuAuth = 8 },
                    new Menu() { MenuCode = "1005", MenuName = "菜单管理", MenuCaption = "Menu", MenuNameSpcace = "MenuCenter", MenuAuth = 7 }
                    );
                context.AuthItems.AddRange(
                    new AuthItem() { AuthColor = "#0080FF", AuthKind = "PlaylistPlus", AuthName = "添加", AuthValue = 1 },
                    new AuthItem() { AuthColor = "#28CBA3", AuthKind = "PlaylistPlay", AuthName = "修改", AuthValue = 2 },
                    new AuthItem() { AuthColor = "#FF5370", AuthKind = "PlaylistRemove", AuthName = "删除", AuthValue = 4 },
                    new AuthItem() { AuthColor = "#FF5370", AuthKind = "FileDocumentBoxSearchOutline", AuthName = "查看", AuthValue = 8 },
                    new AuthItem() { AuthColor = "#FF5370", AuthKind = "LocalPrintShop", AuthName = "打印", AuthValue = 16 },
                    new AuthItem() { AuthColor = "#FF5370", AuthKind = "UploadOutline", AuthName = "导入", AuthValue = 32 },
                    new AuthItem() { AuthColor = "#FF5370", AuthKind = "DownloadOutline", AuthName = "导出", AuthValue = 64 }
                    );
                await context.SaveChangesAsync();
            }
        }
    }
}
