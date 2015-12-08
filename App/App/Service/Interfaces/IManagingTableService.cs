using App.Models;
using App.Models.ManagingTableModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Interfaces
{
    public interface IManagingTableService
    {
        TableData CreateTable(IPagedList<EmployeeViewModel> employees, ManagingRequest request);
    }
}
