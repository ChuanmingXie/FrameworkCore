/*****************************************************************************
*项目名称:FrameworkCore.Service
*项目描述:
*类 名 称:UserService
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:03:54
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
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FrameworkCore.Shared.DataModel;
    using FrameworkCore.Shared.Dto;
    using FrameworkCore.Shared.HttpContact;
    using FrameworkCore.Shared.HttpContact.Request;
    using FrameworkCore.Shared.DataInterfaces;
    using RestSharp;

    public class UserService : BaseService<UserDto>, IUserRepository
    {
        public async Task<BaseResponse<List<AuthItem>>> GetAuthListAsync()
        {
            return await new BaseServiceRequest()
                .GetRequest<BaseResponse<List<AuthItem>>>(new AuthItemRequest(), Method.GET);
        }

        public async Task<BaseResponse> GetUserPermByAccountAsync(string account)
        {
            return await new BaseServiceRequest()
                .GetRequest<BaseResponse>(new UserPermRequest()
                {
                    account = account
                }, Method.GET);
        }

        public async Task<BaseResponse<UserInfoDto>> LoginAsync(string account, string password)
        {
            return await new BaseServiceRequest()
                .GetRequest<BaseResponse<UserInfoDto>>(new UserLoginRequest()
                {
                    Parameter = new LoginDto()
                    {
                        Account = account,
                        PassWord = password
                    }
                }, Method.POST);
        }
    }
}
