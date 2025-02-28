using Business.Models;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(ProjectRegistrationForm form);
        Task<Project?> GetProjectAsync(int id);
        Task<IEnumerable<Project?>> GetProjectsAsync();
        Task<bool> RemoveProjectAsync(int id);
        Task<bool> UpdateProjectAsync(ProjectUpdateForm updateForm);
    }
}