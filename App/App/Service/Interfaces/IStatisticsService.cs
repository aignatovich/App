using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Models.Statistics;

namespace App.Service.Interfaces
{
    public interface IStatisticsService
    {
        string FormHistResponse(StatisticsRequest request);
    }
}