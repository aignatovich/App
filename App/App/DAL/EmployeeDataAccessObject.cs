using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using App.Models;
using App.Models.DatabaseModel;
using App.Models.EmployeeModels;
using App.Models.JqGridObjects;
using App.Models.ManagingTableModels;
using CodeFirst;

namespace App.DAL
{
    public class EmployeeDataAccessObject : IEmployeeDAO
    {
        private IDatabaseContextAccessor dbAccessor;

        public EmployeeDataAccessObject(IDatabaseContextAccessor dbContextAccessor)
        {
            dbAccessor = dbContextAccessor;
        }

        public void Add(EmployeeModel employee)
        {
            dbAccessor.GetEmployeeSet().Add(employee);
        }

        public void Edit(EmployeeModel employee)
        {
            var editableEmployee = DatabaseModelContainer.Current.EmployeeSet.FirstOrDefault(x => x.Id == employee.Id);
            editableEmployee.Name = employee.Name;
            editableEmployee.Surname = employee.Surname;
            editableEmployee.Position = employee.Position;
            editableEmployee.Email = employee.Email;
        }

        public void Remove(int id)
        {
            var employee = DatabaseModelContainer.Current.EmployeeSet.FirstOrDefault(x => x.Id == id);
            DatabaseModelContainer.Current.EmployeeSet.Remove(employee);
        }

        public ICollection<EmployeeModel> GetAll()
        {
            ICollection<EmployeeModel> employeeList = DatabaseModelContainer.Current.EmployeeSet.ToList();
            return employeeList;
        }

        public PagingQueryResult GetNextPage(TableRequest request, int pageSize)
        {
            var projectId = request.ProjectId;
            var id = request.Id;
            var role = request.Role;
            var name = request.Name;
            var surname = request.Surname;
            var property = request.SortingProperty;
            var sortingOrder = request.SortOrder;
            var pageNumber = request.Page;

            var result = DirectSearch(name, surname, id, role, projectId)
                .OrderBy(property + (sortingOrder.Equals(SortEnum.asc) ? " descending" : ""))
                .Skip((pageNumber - 1)*pageSize)
                .Take(pageSize);

            return new PagingQueryResult()
            {
                Employees = result,
                ResultQuantity = result.Count()
            };
        }

        public EmployeeModel GetSingle(int id)
        {
            var employee = DatabaseModelContainer.Current.EmployeeSet.FirstOrDefault(x => x.Id == id);
            return employee;
        }

        public  bool Exists(EmployeeModel employee)
        {
            return (DatabaseModelContainer.Current.EmployeeSet.Any(x =>
                               x.Name.Equals(employee.Name) &&
                               x.Surname.Equals(employee.Surname) &&
                               x.Position.ToString().Equals(employee.Position.ToString()))) ||
                               (employee.Name == null ||
                               employee.Surname == null);
        }

        public ICollection<EmployeeModel> GetEmployeesByIds(IEnumerable<int> ids)
        {
            return ids.Select(GetSingle).ToList();
        }

        public IEnumerable<EmployeeModel> DirectSearch(string name, string surname, int? id, Roles role, int? projectId)
        {
            IEnumerable<EmployeeModel> toTransfer = new List<EmployeeModel>();

            toTransfer =
                DatabaseModelContainer.Current.EmployeeSet.Where(
                    x =>
                        (id == null || x.Id == id) &&
                        (role == Roles.All || x.Position == role) &&
                        (string.IsNullOrEmpty(name) || x.Name.Contains(name)) &&
                        (string.IsNullOrEmpty(surname) || x.Surname.Contains(surname))).
                        Where(x => (projectId == null || x.ActualProjects.Any(y => y.Id == projectId)));

            return toTransfer;
        }
    }
}