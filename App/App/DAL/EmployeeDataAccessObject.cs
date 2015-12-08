using App.Models;
using CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Autofac;
using System.Web;
using App.Models.DatabaseModel;
using App.Models.ManagingTableModels;
using System.Linq.Dynamic;

namespace App.DAL
{
    public class EmployeeDataAccessObject : IEmployeeDAO
    {
        IDatabaseContextAccessor dbAccessor;

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
            EmployeeModel editableEmployee = DatabaseModelContainer.Current.EmployeeSet.Where(x => x.Id == employee.Id).FirstOrDefault();
            editableEmployee.Name = employee.Name;
            editableEmployee.Surname = employee.Surname;
            editableEmployee.Position = employee.Position;
            editableEmployee.Email = employee.Email;
        }

        public void Remove(int id)
        {
            EmployeeModel employee = DatabaseModelContainer.Current.EmployeeSet.Where(x => x.Id == id).FirstOrDefault();
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
            EmployeeModel employee = DatabaseModelContainer.Current.EmployeeSet.Where(x => x.Id == id).FirstOrDefault();
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

        public ICollection<EmployeeModel> GetEmployeesByIds(IEnumerable<Int32> ids)
        {
            ICollection<EmployeeModel> employees = new List<EmployeeModel>();

            foreach (Int32 employeeId in ids)
            {
                employees.Add(GetSingle(employeeId));
            }

            return employees;
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
                if (toTransfer.Count() == 0)
                     toTransfer = DatabaseModelContainer.Current.EmployeeSet.Where(x => x.Id.Equals(id));
                else
                    toTransfer = toTransfer.Where(x => x.Id.Equals(id));
            }
            if (role != Roles.All)
            {
                if (toTransfer.Count() == 0)
                    toTransfer = DatabaseModelContainer.Current.EmployeeSet.Where(x => x.Position.ToString().Equals(role.ToString()));
                else
                    toTransfer = toTransfer.Where(x => x.Position.ToString().Equals(role.ToString()));
            }

            if (!String.IsNullOrEmpty(name))
            {
                if (toTransfer.Count() == 0)
                    toTransfer = DatabaseModelContainer.Current.EmployeeSet.Where(x => x.Name.Contains(name));
                else
                    toTransfer = toTransfer.Where(x => x.Name.Contains(name));
            }
            if (!String.IsNullOrEmpty(surname))
            {
                if (toTransfer.Count() == 0)
                    toTransfer = DatabaseModelContainer.Current.EmployeeSet.Where(x => x.Surname.Contains(surname));
                else
                    toTransfer = toTransfer.Where(x => x.Surname.Contains(surname));
            }
        
            return toTransfer;
        }

    }
}