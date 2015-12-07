using App.Models.EmployeeModels;
using App.Models.JqGridObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Service.Interfaces
{
    public interface IJqGridService
    {
        JqGridEmployeePagedCollection Create(JqGridRequest request);
    }
}