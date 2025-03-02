using Business.Models;
using ResponseModel.Models;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<Result> CreateProjectAsync(ProjectRegistrationForm registrationForm);
        Task<Result> GetProjectAsync(int id);
        Task<Result<IEnumerable<Project?>>> GetProjectsAsync();
        Task<Result> UpdateProjectAsync(ProjectUpdateForm updateForm);
        Task<Result> RemoveProjectAsync(int id);
    }
}