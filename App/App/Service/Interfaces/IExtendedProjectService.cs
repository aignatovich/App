using App.Models;

namespace App.Service.Interfaces
{
    public interface IExtendedProjectService
    {
        ExtendedProjectViewModel Create(int projectId);
    }
}
