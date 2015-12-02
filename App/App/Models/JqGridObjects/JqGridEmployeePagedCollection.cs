using App.Models.EmployeeModels;
using App.Service;
using App.Service.Interfaces;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;
using static App.Util.AutofacConfig;
using Newtonsoft.Json.Linq;

namespace App.Models.JqGridObjects
{
    public class JqGridEmployeePagedCollection
    {
        private const int jqGridPageSize = 100;
        private const string SortingOrderDesc = "desc";
        private const string SortingOrderAsc = "asc";

        public int Page { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public string SortColumn { get; set; }

        public string SortOrder { get; set; }


        public IEnumerable<SimplifiedEmployeeViewModel> Employees { get; set; }

        public static JqGridEmployeePagedCollection Create(bool search, int page, int rows, string sortingProperty, string sortingOrder, string filters)
        {
            IEmployeeService employeeService = Container.Resolve<IEmployeeService>();
            List<SimplifiedEmployeeViewModel> employees = employeeService.GetAllSimplified().ToList();

            if (!search)
            {
                int startIndex = (page - 1) * jqGridPageSize;
                int endIndex = jqGridPageSize < (employees.Count - (page - 1) * jqGridPageSize) ? jqGridPageSize : (employees.Count - (page - 1) * jqGridPageSize);

                IEnumerable<SimplifiedEmployeeViewModel> toTransfer = employees.GetRange(startIndex, endIndex);
                if (sortingOrder.Equals(SortingOrderAsc))
                {
                    toTransfer = toTransfer.AsEnumerable().OrderBy(sortingProperty).Reverse();
                }
                else
                {
                    toTransfer = toTransfer.AsEnumerable().OrderBy(sortingProperty);
                }

                return new JqGridEmployeePagedCollection()
                {
                    Employees = toTransfer,
                    Page = page,
                    TotalPages = employeeService.CalculateTotalPages(jqGridPageSize),
                    SortColumn = sortingProperty,
                    SortOrder = sortingOrder,
                    TotalRecords = employees.Count
                };
            }

            else
            {
                IEnumerable<SimplifiedEmployeeViewModel> toTransfer = employees;

                JObject appliedFilters = JObject.Parse(filters);
                JEnumerable<JToken> tokens = appliedFilters.Children();

                string Name = "";
                string Surname = "";
                string Role = "";
                int? Id = null;
                int index = 0;

                foreach (JToken token in tokens)
                {
                    if (index != 0)
                    {
                        for (int j = 0; j < token.First.Count(); j++)
                        {
                            JToken currentToken = token.First[j];
                            if (currentToken.Value<string>("field").Equals("Name"))
                            {
                                Name = currentToken["data"].ToString();
                            }
                            if (currentToken.Value<string>("field").Equals("Surname"))
                            {
                                Surname = currentToken["data"].ToString();
                            }
                            if (currentToken.Value<string>("field").Equals("PositionValue"))
                            {
                                Role = (Enum.Parse(typeof(Roles), currentToken["data"].ToString())).ToString();
                            }
                            if (currentToken.Value<string>("field").Equals("Id"))
                            {
                                Id = Convert.ToInt32(currentToken["data"].ToString());
                            }
                        }
                    }
                    index++;
                }

                toTransfer =  DirectSearch(toTransfer, Name, Surname, Id, Role);
                int startIndex = (page - 1) * jqGridPageSize;
                int endIndex = jqGridPageSize < (toTransfer.Count() - (page - 1) * jqGridPageSize) ? jqGridPageSize : (toTransfer.Count() - (page - 1) * jqGridPageSize);

                return new JqGridEmployeePagedCollection()
                {
                    Employees = toTransfer.ToList().GetRange(startIndex,endIndex),
                    Page = page,
                    TotalPages = employeeService.CalculatePages(jqGridPageSize, toTransfer.Count()),
                    SortColumn = sortingProperty,
                    SortOrder = sortingOrder,
                    TotalRecords = toTransfer.Count()
                };
            }
        }

        private static ICollection<SimplifiedEmployeeViewModel> DirectSearch(IEnumerable<SimplifiedEmployeeViewModel> employees, string name, string surname, int? id, string role)
        {
            IEnumerable<SimplifiedEmployeeViewModel> toTransfer = employees;

            if (id != null)
            {
                toTransfer = toTransfer.Where(x => x.Id.Equals(id)).ToList();
            }
            if (role != "")
            {
                toTransfer = toTransfer.Where(x => x.PositionValue.Equals(role)).ToList();
            }

            if (name != "")
            {
                toTransfer = toTransfer.Where(x => x.Name.Contains(name)).ToList();
            }
            if (surname != "")
            {
                toTransfer = toTransfer.Where(x => x.Surname.Contains(surname)).ToList();
            }
                   
            return toTransfer.ToList();
        }


    }

}