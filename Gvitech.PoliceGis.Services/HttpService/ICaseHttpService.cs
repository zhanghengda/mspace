using Mmc.Mspace.Models.Case;
using System;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.HttpService
{
    internal interface ICaseHttpService
    {
        List<CaseInfo> GetCaseInfos(DateTime startTime, DateTime endTime, int curPage, int pageCapacity);
    }
}