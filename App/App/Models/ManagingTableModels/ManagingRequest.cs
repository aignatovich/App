using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class ManagingRequest
    {
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? Sort { get; set; }
        public int? Page { get; set; }
        
        public int? ProjectId { get; set; }
    }
}