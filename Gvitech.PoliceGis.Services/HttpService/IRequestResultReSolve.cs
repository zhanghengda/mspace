using Mmc.Mspace.Models.HttpResult;
using System;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.HttpService
{
    public interface IRequestResultReSolve
    {
        HttpResult<T> ReSolveRequestResult<T>(string requestResult, Dictionary<string, Type> keys) where T : class, new();
    }
}