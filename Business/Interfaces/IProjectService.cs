using Business.Models;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<IResult> CreateProjectAsync(ProjectRegistrationForm registrationForm);
        Task<IResult> GetProjectAsync(int id);
        Task<IResult> GetProjectsAsync();
        Task<IResult> UpdateProjectAsync(ProjectUpdateForm updateForm);
        Task<IResult> RemoveProjectAsync(int id);
    }
}