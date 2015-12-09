using App.Models.JqGridObjects;

namespace App.Service.Interfaces
{
    public interface IEmployeeTableService
    {
        EmployeePagedCollection Create(TableRequest request);
    }
}