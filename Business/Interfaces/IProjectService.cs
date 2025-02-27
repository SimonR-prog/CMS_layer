using Business.Models;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<IResult> CreateProjectAsync(ProjectRegistrationForm form);
        Task<IResult> GetProjectAsync(int id);
        Task<IResult> GetProjectsAsync();
        Task<IResult> UpdateProjectAsync(Project form);
        Task<IResult> RemoveProjectAsync(int id);
    }
}