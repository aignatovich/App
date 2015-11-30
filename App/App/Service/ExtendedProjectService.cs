using App.DAL;
using App.Models;
using App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Service
{
    public class ExtendedProjectService:IExtendedProjectService
    {
        private IProjectService projectService;
        private IEmployeeService employeeService;
        private IEmployeeDAO employeeDataAccessObject;
        private IProjectDAO projectDataAccessObject;

        public ExtendedProjectService(IProjectService projectService, IEmployeeService employeeService, 
                                      IEmployeeDAO employeeDataAccessObject, IProjectDAO projectDataAccessObject)
        {
            this.projectService = projectService;
            this.employeeService = employeeService;
            this.employeeDataAccessObject = employeeDataAccessObject;
            this.projectDataAccessObject = projectDataAccessObject;
        }


        public ExtendedProjectViewModel Create(int projectId)
        {
            ExtendedProjectViewModel toTransfer = new ExtendedProjectViewModel(employeeService.GetAllViewModels(), projectService.GetSingle(projectId));
            return toTransfer;
        }
    }
}