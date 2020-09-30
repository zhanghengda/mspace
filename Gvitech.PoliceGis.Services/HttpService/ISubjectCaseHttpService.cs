using Mmc.Mspace.Models.Case;
using System;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.HttpService
{
    public interface ISubjectCaseHttpService
    {
        List<SubjectCaseInfo> GetSubjectCaseInfos(DateTime startTime, DateTime endTime, int curPage, int pageCapacity);
    }
}