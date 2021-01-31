/*****************************************************************************
*项目名称:FrameworkCore.Shared.HttpContact.Request
*项目描述:
*类 名 称:GroupRequest
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:20:44
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.HttpContact.Request
{
    using FrameworkCore.Shared.Dto;
    using FrameworkCore.Shared.HttpContact;


    /// <summary>
    /// 用户组模板信息请求
    /// </summary>
    public class GroupModuleRequest : BaseRequest
    {
        public override string Route { get => "api/Group/GetMenuModules"; }
    }

    /// <summary>
    /// 组明细数据请求
    /// </summary>
    public class GroupInfoRequest : BaseRequest
    {
        public override string Route { get => "api/Group/GetGroupInfo"; }

        public int id { get; set; }
    }

    /// <summary>
    /// 保存组数据请求
    /// </summary>
    public class GroupSaveRequest : BaseRequest
    {
        public override string Route { get => "api/Group/Save"; }

        public GroupDataDto groupDto { get; set; }

    }
}
