using App.Models;
using App.Models.ManagingTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        IEnumerable<EmployeeModel> GetNextPage(int pageNumber, int pageSize);

        int GetTotalEmployeeCount();
    }
}
