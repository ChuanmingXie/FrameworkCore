/*****************************************************************************
*项目名称:FrameworkCore.Service
*项目描述:
*类 名 称:GroupService
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:03:21
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Service
{
    using FrameworkCore.Shared.DataInterfaces;
    using FrameworkCore.Shared.Dto;
    using FrameworkCore.Shared.HttpContact;
    using FrameworkCore.Shared.HttpContact.Request;
    using RestSharp;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GroupService : BaseService<GroupDto>, IGroupRepository
    {
        public async Task<BaseResponse<GroupDataDto>> GetGroupAsync(int id)
        {
            return await new BaseServiceRequest()
                .GetRequest<BaseResponse<GroupDataDto>>(new GroupInfoRequest()
                {
                    id = id
                }, Method.GET);
        }

        public async Task<BaseResponse<List<MenuModuleGroupDto>>> GetMenuModuleListAsync()
        {
            return await new BaseServiceRequest()
                .GetRequest<BaseResponse<List<MenuModuleGroupDto>>>(new GroupModuleRequest(), Method.GET);
        }

        public async Task<BaseResponse> SaveGroupAsync(GroupDataDto group)
        {
            return await new BaseServiceRequest()
                .GetRequest<BaseResponse>(new GroupSaveRequest()
                {
                    groupDto = group
                }, Method.POST);
        }
    }
}
