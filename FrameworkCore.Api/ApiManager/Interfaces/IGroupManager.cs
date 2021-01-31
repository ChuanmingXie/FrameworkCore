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

    public interface IGroupManager
    {
        Task<ApiResponse> GetAll(QueryParameters param);

        Task<ApiResponse> Delete(int id);

        Task<ApiResponse> Save(GroupDataDto param);

        Task<ApiResponse> GetMenuModuleList();

        Task<ApiResponse> GetGroupData(int id);
    }
}
