using Mmc.Mspace.Models.PlanShowService;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.PlanShowService
{
    public interface IPlanShowService
    {
        List<Preview> GetPlanShow();

        void LoadData();

        void RemovePlanShow();
    }
}