using System.Collections.Generic;
using System.Linq;
using App.Models;
using App.Models.DatabaseModel;
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
            EmployeeModel editableEmployee = DatabaseModelContainer.Current.EmployeeSet.FirstOrDefault(x => x.Id == employee.Id);
            editableEmployee.Name = employee.Name;
            editableEmployee.Surname = employee.Surname;
            editableEmployee.Position = employee.Position;
            editableEmployee.Email = employee.Email;
        }

        public void Remove(int id)
        {
            EmployeeModel employee = DatabaseModelContainer.Current.EmployeeSet.FirstOrDefault(x => x.Id == id);
            DatabaseModelContainer.Current.EmployeeSet.Remove(employee);
        }

        public ICollection<EmployeeModel> GetAll()
        {
            ICollection<EmployeeModel> employeeList = DatabaseModelContainer.Current.EmployeeSet.ToList();
            return employeeList;
        }

        public IEnumerable<EmployeeModel> GetNextPage(int pageNumber, int pageSize)
        {
            return DatabaseModelContainer.Current.EmployeeSet.OrderBy(x => x.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public EmployeeModel GetSingle(int id)
        {
            EmployeeModel employee = DatabaseModelContainer.Current.EmployeeSet.FirstOrDefault(x => x.Id == id);
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

        public int GetTotalEmployeeCount()
        {
            return DatabaseModelContainer.Current.EmployeeSet.Count();
        }

        public IEnumerable<EmployeeModel> DirectSearch(string name, string surname, int? id, Roles role)
        {
            IEnumerable<EmployeeModel> toTransfer = new List<EmployeeModel>();

            if (id != null)
            {
                toTransfer = !toTransfer.Any() ? DatabaseModelContainer.Current.EmployeeSet.Where(x => x.Id.Equals(id)) : toTransfer.Where(x => x.Id.Equals(id));
            }
            if (role != Roles.All)
            {
                toTransfer = !toTransfer.Any() ? DatabaseModelContainer.Current.EmployeeSet.Where(x => x.Position.ToString().Equals(role.ToString())) : toTransfer.Where(x => x.Position.ToString().Equals(role.ToString()));
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