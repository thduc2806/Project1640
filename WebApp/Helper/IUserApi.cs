using Project1640.Dto.Common;
using Project1640.Dto.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Helper
{
    public interface IUserApi
    {
        Task<ApiResult<string>> Login(LoginDto request);

    }
}
