using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.Statistics
{
    public class StatisticsResponse
    {
        public IEnumerable<string> Labels { get; set; }

        public IEnumerable<int> VacationAbsenceData { get; set; }

        public IEnumerable<int> SickAbsenceData { get; set; }

        public IEnumerable<int> PersonalAbsenceData { get; set; }
    }
}