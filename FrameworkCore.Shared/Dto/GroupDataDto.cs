/*****************************************************************************
*项目名称:FrameworkCore.Shared.Dto
*项目描述:
*类 名 称:GroupDataDto
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:25:35
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.Dto
{
    using FrameworkCore.Shared.DataInterfaces;
    using FrameworkCore.Shared.DataModel;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class GroupDataDto : ViewModelBase
    {
        public Group Group { get; set; }

        public GroupDataDto()
        {
            Group = new Group();
            GroupFuncs = new List<GroupFunc>();
            GroupUsers = new ObservableCollection<GroupUserDto>();
        }

        private ObservableCollection<GroupUserDto> groupUsers;

        /// <summary>
        /// 组所包含的用户
        /// </summary>
        public ObservableCollection<GroupUserDto> GroupUsers
        {
            get { return GroupUsers; }
            set
            {
                groupUsers = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 组件包含的模块清单
        /// </summary>
        public List<GroupFunc> GroupFuncs { get; set; }
    }
}
