using NetCoreCommon.Dtos;
using NetCoreCommon.ResultConstant;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreBusinessEngine.Contracts
{
    public interface IApplicationUserBusinessEngine
    {
        Task<Result<object>> CreateApplicationUser(ApplicationUserDto model);

    }
}
