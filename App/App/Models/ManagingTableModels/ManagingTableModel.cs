using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class ManagingTableModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ManagingDateModel AbsenceList { get; set; }
    }
}