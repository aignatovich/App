using System;
using System.Collections.Generic;
using App.Models;
using App.Models.EmployeeModels;
using App.Models.JqGridObjects;
using App.Models.ManagingTableModels;

namespace App.DAL
{
    public interface IEmployeeDAO
    {
        void Add(EmployeeModel employee);

        void Edit(EmployeeModel employee);

        void Remove(int id);

        ICollection<EmployeeModel> GetAll();

        EmployeeModel GetSingle(int id);

        bool Exists(EmployeeModel employee);

        ICollection<EmployeeModel> GetEmployeesByIds(IEnumerable<Int32> ids);

        IEnumerable<EmployeeModel> DirectSearch(string name, string surname, int? id, Roles role);

        PagingQueryResult GetNextPage(TableRequest request, int pageSize);
    }
}
