/*****************************************************************************
*项目名称:FrameworkCore.ViewModel
*项目描述:
*类 名 称:GroupViewModel
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:20:45
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/


using FrameworkCore.Shared.Common;
using FrameworkCore.Shared.DataInterfaces;
using FrameworkCore.Shared.Dto;
using FrameworkCore.ViewModel.Interfaces;
using FrameworkCore.Shared.Common.Query;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using FrameworkCore.ViewModel.Common;
using FrameworkCore.Shared.DataModel;

namespace FrameworkCore.ViewModel
{
    /// <summary>
    /// 部门管理
    /// </summary>
    public class GroupViewModel : BaseRepository<GroupDto>, IGroupViewModel
    {
        private readonly IUserRepository userRepository;
        private readonly IGroupRepository groupRepository;

        #region Properties

        private int selectCardIndex = 0;
        /// <summary>
        /// 切换检索用户列表的页索引
        /// </summary>
        public int SelectCardIndex
        {
            get { return selectCardIndex; }
            set { selectCardIndex = value; OnPropertyChanged(); }
        }

        private string userSearch = string.Empty;
        /// <summary>
        /// 检索用户条件
        /// </summary>
        public string UserSearch
        {
            get { return userSearch; }
            set { userSearch = value; OnPropertyChanged(); }
        }

        public GroupDataDto groupDto;
        /// <summary>
        /// 操作实体
        /// </summary>
        public GroupDataDto GroupDto
        {
            get { return groupDto; }
            set { groupDto = value; OnPropertyChanged(); }
        }

        private ObservableCollection<UserDto> gridUserModeList;
        /// <summary>
        /// 所有的用户列表
        /// </summary>
        public ObservableCollection<UserDto> GridUserModeList
        {
            get { return gridUserModeList; }
            set { gridUserModeList = value; OnPropertyChanged(); }
        }

        public ObservableCollection<MenuModuleGroupDto> menuModules;
        /// <summary>
        /// 所有菜单模块及对应的功能
        /// </summary>
        public ObservableCollection<MenuModuleGroupDto> MenuModules
        {
            get { return menuModules; }
            set { menuModules = value; OnPropertyChanged(); }
        }

        #endregion

        #region Constructor

        public GroupViewModel(IGroupRepository repository) : base(repository)
        {
            userRepository = NetCoreProvider.Resolve<IUserRepository>();
            AddUserCommand = new RelayCommand<UserDto>(arg =>
            {
                if (arg == null) return;
                var user = GroupDto.GroupUsers?.FirstOrDefault(t => t.Account == arg.Account);
                if (user == null)
                {
                    GroupDto.GroupUsers?.Add(new GroupUserDto()
                    {
                        Account = arg.Account
                    });
                }
            });
            DelUserCommand = new RelayCommand<GroupUserDto>(arg =>
            {
                if (arg == null) return;
                var user = GroupDto.GroupUsers?.FirstOrDefault(t => t.Account == arg.Account);
                if (user != null) GroupDto.GroupUsers?.Remove(user);
            });
            groupRepository = repository;
        }

        #endregion

        #region Command

        public RelayCommand<UserDto> AddUserCommand { get; private set; }

        public RelayCommand<GroupUserDto> DelUserCommand { get; private set; }

        #endregion

        #region Method

        /// <summary>
        /// 获取用户列表
        /// </summary>
        private async void GetUserData()
        {
            var result = await userRepository.GetAllListAsync(new QueryParameters()
            {
                PageIndex = 0,
                PageSize = 30,
                Search = UserSearch
            });
            GridUserModeList = new ObservableCollection<UserDto>();
            if (result.SatusCode == 200)
                GridUserModeList = new ObservableCollection<UserDto>(result.Result.Items?.ToList());
            SelectCardIndex = 1;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        private void AddAllUser()
        {
            for (int i = 0; i < GridUserModeList.Count; i++)
            {
                var arg = GridUserModeList[i];
                if (arg.IsChecked)
                {
                    var user = GroupDto.GroupUsers?.FirstOrDefault(t => t.Account == arg.Account);
                    if (user == null) GroupDto.GroupUsers?.Add(new GroupUserDto() { Account = arg.Account });
                }

            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        private void DeleteAllUser()
        {
            for (int i = GroupDto.GroupUsers.Count - 1; i >= 0; i--)
            {
                var arg = GroupDto.GroupUsers[i];
                if (arg.IsChecked)
                    GroupDto.GroupUsers.Remove(arg);
            }
        }

        /// <summary>
        /// 刷新菜单列表
        /// </summary>
        /// <returns></returns>
        private async Task UpdateMenuModules()
        {
            if (MenuModules != null && MenuModules.Count > 0)
            {
                for (int i = 0; i < MenuModules.Count; i++)
                {
                    var menu = MenuModules[i].Modules;
                    for (int j = 0; j < menu.Count; j++)
                    {
                        menu[j].IsChecked = false;
                    }
                }
            }
            var menuList = await groupRepository.GetMenuModuleListAsync();
            if (menuList.SatusCode == 200)
                MenuModules = new ObservableCollection<MenuModuleGroupDto>(menuList.Result);
        }

        #endregion

        #region Override

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override async Task Execute(string arg)
        {
            switch (arg)
            {
                case "添加用户": GetUserData(); break;
                case "选中所有功能": break;
                case "返回上一页": selectCardIndex = 0; break;
                case "添加所有选中项": AddAllUser(); break;
                case "删除所有选中项": DeleteAllUser(); break;
            }
            await base.Execute(arg);
        }

        /// <summary>
        /// 添加模块数据
        /// </summary>
        public override async void AddAsync()
        {
            GroupDto = new GroupDataDto();
            await UpdateMenuModules();
            base.AddAsync();
        }

        /// <summary>
        /// 更新模块数据
        /// </summary>
        public override async void UpdateAsync()
        {
            if (GridModel == null) return;
            await UpdateMenuModules();
            var group = await groupRepository.GetGroupAsync(GridModel.Id);
            if (group.SatusCode != 200)
            {
                Msg.Warning(group.Message);
            }

            group.Result?.GroupFuncs?.ForEach(f =>
            {
                for (int i = 0; i < MenuModules.Count; i++)
                {
                    var menu = MenuModules[i];
                    if (menu.MenuCode == f.MenuCode)
                    {
                        for (int j = 0; j < menu.Modules.Count; j++)
                        {
                            if ((f.Auth & menu.Modules[j].Value) == menu.Modules[j].Value)
                                menu.Modules[j].IsChecked = true;
                        }
                    }
                }
            });
            GroupDto = group.Result;
            this.CreateDefaultCommand();
            SelectPageIndex = 1;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public override async Task SaveAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(GroupDto.Group.GroupCode) ||
                    string.IsNullOrWhiteSpace(GroupDto.Group.GroupName))
                {
                    Msg.Warning("组代码和名称为必填项");
                    return;
                }

                /* 把选择的功能对应的权限保存到提交的参数当中 */
                GroupDto.GroupFuncs = new List<GroupFunc>();
                for (int i = 0; i < MenuModules.Count; i++)
                {
                    var menu = MenuModules[i];
                    int value = menu.Modules.Where(t => t.IsChecked).Sum(t => t.Value);
                    if (value > 0)
                    {
                        GroupDto.GroupFuncs.Add(new GroupFunc()
                        {
                            MenuCode = menu.MenuCode,
                            Auth = value
                        });
                    }
                }
                var result = await groupRepository.SaveGroupAsync(GroupDto);
                if (result.StatusCode != 200)
                {
                    Msg.Error(result.Message);
                    return;
                }
                await GetPageData(0);
                InitPermissions(this.AuthValue);
                SelectPageIndex = 0;
            }
            catch (Exception ex)
            {
                Msg.Error(ex.Message);
            }
        }

        #endregion
    }
}
