using App.Models;
using App.Models.ManagingTableModels;
using PagedList;

namespace App.Service.Interfaces
{
    public interface IManagingTableService
    {
        TableData CreateTable(IPagedList<EmployeeViewModel> employees, ManagingRequest request);
    }
}
