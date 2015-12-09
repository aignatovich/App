using System.Collections.Generic;
using App.Models;
using App.Models.EmployeeModels;
using App.Models.ManagingTableModels;
using PagedList;

namespace App.Service.Interfaces
{
    public interface IEmployeeService
    {
        void Add(EmployeeViewModel employee);

        ICollection<EmployeeViewModel> GetAllViewModels();

        EmployeeViewModel GetSingle(int id);

        void Remove(EmployeeViewModel employee);

        void Edit(EmployeeViewModel employee);

        TableData GetIPagedList(ManagingRequest request);

        TableData GetTableData(ManagingRequest request);

        void ApplyAbsence(ManagingDateModel model);

        int CalculatePages(int pageSize, int length);

        IEnumerable<SimplifiedEmployeeViewModel> SimplifyCollection(IEnumerable<EmployeeModel> employees);
    }
}