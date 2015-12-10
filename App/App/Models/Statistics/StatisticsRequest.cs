using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.Statistics
{
    public class StatisticsRequest
    {
        public int? ProjectId { get; set; }

        public int? StartMonth { get; set; }

        public int? Year { get; set; }

        public int? EndMonth { get; set; }

        public int? EndYear { get; set; }

    }
}