using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrameworkCore.Api.ApiManager
{
    using FrameworkCore.Shared.HttpContact.Response;
    using System.Threading.Tasks;

    public interface IAuthItemManager
    {
        Task<ApiResponse> GeAll();
    }
}
