using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using App.Models;
using App.Models.Statistics;
using App.Service.Interfaces;
using Newtonsoft.Json;

namespace App.Service
{
    public class StatisticsService : IStatisticsService
    {
        private IProjectService projectService;
        private IEmployeeService employeeService;

        public StatisticsService(IProjectService projectService, IEmployeeService employeeService)
        {
            this.projectService = projectService;
            this.employeeService = employeeService;
        }

        public string FormHistResponse(StatisticsRequest request)
        {

            var response = new StatisticsResponse
            {
                Labels = GetLabels(request.Year, request.StartMonth, request.EndMonth),
                VacationAbsenceData = GetTotalAbsenceValues(request, Reason.V),
                SickAbsenceData = GetTotalAbsenceValues(request, Reason.S),
                PersonalAbsenceData = GetTotalAbsenceValues(request, Reason.P)
            };
            return JsonConvert.SerializeObject(response);
        }

        private IEnumerable<string> GetLabels(int? yearValue, int? start, int? end)
        {
            var year = yearValue ?? DateTime.Now.Year;
            var startMonth = start ?? DateTime.Now.Month;
            var endMonth = end ?? DateTime.Now.Month;
            var days = new List<string>();

            for (var j = startMonth; j < endMonth + 1; j++)
            {
                {
                    days.Add(new DateTime(year,j,1).ToString("MMMM", new CultureInfo("en-GB")));
                }
            }

            return days;
        }

        private int GetAbsenceValuesInSingleMonth(IEnumerable<EmployeeViewModel> employees, Reason reason, int? _month,
            int? _year, int? _projectId)
        {
            var year = _year ?? DateTime.Now.Year;
            var month = _month ?? DateTime.Now.Month;
            var projectId = _projectId;

            return
                employees.Sum(
                    employee =>
                        employee.AbsenceList.Count(
                            x =>
                                x.Reason.Equals(reason) && x.Month == month && x.Year == year &&
                                (projectId == null || x.ProjectId == projectId)));

        }

        private IEnumerable<int> GetTotalAbsenceValues(StatisticsRequest request, Reason reason)
        {
            var employees = request.ProjectId == null
                ? employeeService.GetAllViewModels()
                : projectService.GetSingle((int) request.ProjectId).CurrentEmployees;

            var totalList = new List<int>();

            for (var currentMonth = request.StartMonth; currentMonth < request.EndMonth + 1; currentMonth++)
            {
                totalList.Add(GetAbsenceValuesInSingleMonth(employees, reason, currentMonth, request.Year,
                    request.ProjectId));
            }
            return totalList;
        }
    }
}