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

    public interface IBasicManager
    {
        Task<ApiResponse> GetAll(QueryParameters param);

        Task<ApiResponse> Add(BasicDto param);

        Task<ApiResponse> Delete(int id);

        Task<ApiResponse> Save(BasicDto param);
    }
}
