using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using App.Models;
using App.Models.DatabaseModel;
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

        public IEnumerable<EmployeeModel> GetNextPage(int pageNumber, int pageSize, int? projectId,
            SortEnum sortingOrder, string property)
        {

            return sortingOrder.Equals(SortEnum.desc)
                ? projectId == null
                    ? DatabaseModelContainer.Current.EmployeeSet.OrderBy(property)
                        .Skip((pageNumber - 1)*pageSize)
                        .Take(pageSize)
                    : DatabaseModelContainer.Current.ProjectSet.FirstOrDefault(x => x.Id == projectId).CurrentEmployees.
                        OrderBy(property).Skip((pageNumber - 1)*pageSize).Take(pageSize)
                : projectId == null
                    ? DatabaseModelContainer.Current.EmployeeSet.
                        OrderBy(property + (sortingOrder.Equals(SortEnum.asc) ? " descending" : ""))
                        .Skip((pageNumber - 1)*pageSize)
                        .Take(pageSize)
                    : DatabaseModelContainer.Current.ProjectSet.FirstOrDefault(x => x.Id == projectId).CurrentEmployees.
                        OrderBy(property + (sortingOrder.Equals(SortEnum.asc) ? " descending" : ""))
                        .Skip((pageNumber - 1)*pageSize)
                        .Take(pageSize);
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

        public IEnumerable<EmployeeModel> DirectSearch(string name, string surname, int? id, Roles role)
        {
            IEnumerable<EmployeeModel> toTransfer = new List<EmployeeModel>();

            if (id != null)
            {
                toTransfer = !toTransfer.Any() ? DatabaseModelContainer.Current.EmployeeSet.Where(x => x.Id == id) : toTransfer.Where(x => x.Id == id);
            }
            if (role != Roles.All)
            {
                toTransfer = !toTransfer.Any() ? DatabaseModelContainer.Current.EmployeeSet.Where(x => x.Position.ToString().Equals(role.ToString())) : toTransfer.Where(x => x.Position.Equals(role));
            }

            if (!string.IsNullOrEmpty(name))
            {
                toTransfer = !toTransfer.Any() ? DatabaseModelContainer.Current.EmployeeSet.Where(x => x.Name.Contains(name)) : toTransfer.Where(x => x.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(surname))
            {
                toTransfer = !toTransfer.Any() ? DatabaseModelContainer.Current.EmployeeSet.Where(x => x.Surname.Contains(surname)) : toTransfer.Where(x => x.Surname.Contains(surname));
            }
        
            return toTransfer;
        }

    }
}