using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrameworkCore.Api.ApiManager
{

    using FrameworkCore.Shared.Common.Query;
    using FrameworkCore.Shared.Dto;
    using FrameworkCore.Shared.HttpContact.Response;
    using System.Threading.Tasks;

    public interface IUserManager
    {
        Task<ApiResponse> Login(LoginDto param);

        Task<ApiResponse> GetAll(QueryParameters param);

        Task<ApiResponse> Get(int id);

        Task<ApiResponse> Add(UserDto param);

        Task<ApiResponse> Delete(int id);

        Task<ApiResponse> Save(UserDto param);
    }
}
