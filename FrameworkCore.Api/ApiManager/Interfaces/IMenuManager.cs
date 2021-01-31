using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrameworkCore.Api.ApiManager
{
    using FrameworkCore.Shared.HttpContact.Response;
    using FrameworkCore.Shared.Dto;
    using FrameworkCore.Shared.Common.Query;
    using System.Threading.Tasks;

    public interface IMenuManager
    {
        Task<ApiResponse> GetAll(QueryParameters param);

        Task<ApiResponse> Add(MenuDto param);

        Task<ApiResponse> Delete(int id);

        Task<ApiResponse> Save(MenuDto param);
    }
}
