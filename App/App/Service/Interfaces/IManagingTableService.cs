using System.Collections.Generic;
using App.Models;
using App.Models.ManagingTableModels;
using PagedList;

namespace App.Service.Interfaces
{
    public interface IManagingTableService
    {
        TableData CreateTable(IEnumerable<EmployeeViewModel> employees, ManagingRequest request, int page, int pageSize);
    }
}
