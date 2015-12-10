using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Models.Statistics;
using App.Service.Interfaces;

namespace App.Controllers
{
    public class StatisticsController : Controller
    {
        private IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        [Authorize]
        public ActionResult EmployeeAbsenceHist()
        {
            return View();
        }

        [HttpGet]
        public string GetData(StatisticsRequest request)
        {
            return statisticsService.FormHistResponse(request);
        }

    }
}