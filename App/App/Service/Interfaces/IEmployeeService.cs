using App.Models;
using App.Models.EmployeeModels;
using App.Models.JqGridObjects;
using App.Models.ManagingTableModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace App.Service.Interfaces
{
    public interface IEmployeeService
    {
        void Add(EmployeeViewModel employee);

        ICollection<EmployeeViewModel> GetAllViewModels();

        EmployeeViewModel GetSingle(int id);

        void Remove(EmployeeViewModel employee);

        void Edit(EmployeeViewModel employee);

        IPagedList<EmployeeViewModel> GetIPagedList(ManagingRequest request);

        TableData GetTableData(ManagingRequest request);

        void ApplyAbsence(ManagingDateModel model);

        int CalculatePages(int pageSize, int length);

        IEnumerable<SimplifiedEmployeeViewModel> SimplifyCollection(IEnumerable<EmployeeModel> employees);
    }
}