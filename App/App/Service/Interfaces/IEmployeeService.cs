using App.Models;
using App.Models.ManagingTableModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Service.Interfaces
{
    public interface IEmployeeService
    {
        //

        ICollection<EmployeeModel> GetEmployeesByIds(IEnumerable<Int32> ids);

        void Add(EmployeeViewModel employee);

        ICollection<EmployeeViewModel> GetAllViewModels();

        EmployeeViewModel GetSingle(int id);

        void Remove(EmployeeViewModel employee);

        void Edit(EmployeeViewModel employee);

        IPagedList<EmployeeViewModel> GetAllAsIPagedList(ManagingRequest request);

        IPagedList<EmployeeViewModel> GetAllAsIPagedList(int? monthTransfered, int? yearTransfered, int? page, int? sorting);

        IPagedList<EmployeeViewModel> GetIPagedList(int month, int year, int page, int sortingOrder, int? id);

        TableData GetTableData(ManagingRequest request);

        void ApplyAbsence(ManagingDateModel model);

    }
}