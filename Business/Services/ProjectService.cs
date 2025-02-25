using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, ICustomerRepository customerRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;


    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        try
        {
            if (!await _customerRepository.ExistsAsync(customer => customer.Id == form.CustomerId))
            {
                return false;
            }
            var projectEntity = ProjectFactory.Create(form);
            if (projectEntity == null)
            {
                return false;
            }
            bool result = await _projectRepository.AddAsync(projectEntity);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<IEnumerable<Project?>> GetProjectsAsync()
    {
        try
        {   
            var entities = await _projectRepository.GetAllAsync();
            var projects = entities.Select(ProjectFactory.Create);
            return projects;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new List<Project>();
        }
    }
    public async Task<bool> UpdateProjectAsync(Project form)
    {
        try
        {
            if (!await _projectRepository.ExistsAsync(project => project.Id == form.Id))
            {
                return false;
            }
            var project = await _projectRepository.GetAsync(project => project.Id == form.Id);
            if (project == null)
            {
                return false;
            }

            project.Description = form.Description;
            project.ProjectName = form.ProjectName;

            bool result = await _projectRepository.UpdateAsync(project);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }
    public async Task<bool> RemoveProjectAsync(int id)
    {
        try
        {
            if (!await _projectRepository.ExistsAsync(project => project.Id == id))
            {
                return false;
            }
            var project = await _projectRepository.GetAsync(project => project.Id == id);
            if (project == null)
            {
                return false;
            }
            bool result = await _projectRepository.RemoveAsync(project);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }
    public async Task<Project?> GetProjectAsync(int id)
    {
        try
        {
            //Gets the projectentity from its id and uses factory to turn it into a project and returns it.
            var projectEntity = await _projectRepository.GetAsync(project => project.Id == id);
            if (projectEntity != null)
            {
                return ProjectFactory.Create(projectEntity);
            }
            return null;
        }
        catch (Exception ex) 
        {
            Debug.WriteLine(ex);
            return null;
        }
    }
}
