using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Models.ResultModels;
using System.Diagnostics;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, ICustomerRepository customerRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;


    public async Task<IResult> CreateProjectAsync(ProjectRegistrationForm form)
    {
        try
        {
            if (!await _customerRepository.ExistsAsync(customer => customer.Id == form.CustomerId))
            {
                return Result.AlreadyExists("Customer already exists.");
            }
            var projectEntity = ProjectFactory.Create(form);
            if (projectEntity == null)
            {
                return Result.NotFound("Project entity is null.");
            }
            if (await _projectRepository.AddAsync(projectEntity))
               { 
                return Result.Ok(); 
            }
            return Result.Error("Something went wrong.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return Result.Error(ex.ToString());
        }
    }

    public async Task<IResult> GetProjectsAsync()
    {
        try
        {   
            var entities = await _projectRepository.GetAllAsync();
            var projects = entities.Select(ProjectFactory.Create);
            return Result<IEnumerable<Project?>>.Ok(projects);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return Result<List<Project?>>.Error("Problem getting the projects. Might not be any.");
        }
    }
    public async Task<IResult> UpdateProjectAsync(Project form)
    {

        //Maybe need to change this one. Create a new form with a new factory?


        try
        {
            if (!await _projectRepository.ExistsAsync(project => project.Id == form.Id))
            {
                return Result.AlreadyExists("Customer already exists.");
            }
            var project = await _projectRepository.GetAsync(project => project.Id == form.Id);
            if (project == null)
            {
                return Result.NotFound("Project entity is null.");
            }

            project.Description = form.Description;
            project.ProjectName = form.ProjectName;

            bool result = await _projectRepository.UpdateAsync(project);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return Result.Error("Error when updating data in project.");
        }
    }
    public async Task<IResult> RemoveProjectAsync(int id)
    {
        try
        {
            if (!await _projectRepository.ExistsAsync(project => project.Id == id))
            {
                return Result.AlreadyExists("Customer already exists.");
            }
            var project = await _projectRepository.GetAsync(project => project.Id == id);
            if (project == null)
            {
                return Result.NotFound("Project entity is null.");
            }


            //Fix these..
            bool result = await _projectRepository.RemoveAsync(project);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return Result.Error("Error when trying to remove project.");
        }
    }
    public async Task<IResult> GetProjectAsync(int id)
    {
        try
        {
            //Gets the projectentity from its id and uses factory to turn it into a project and returns it.
            var projectEntity = await _projectRepository.GetAsync(project => project.Id == id);
            if (projectEntity != null)
            {
                var project = ProjectFactory.Create(projectEntity);
                return Result<Project?>.Ok(project);
            }
            return Result.NotFound("Project not found.");
        }
        catch (Exception ex) 
        {
            Debug.WriteLine(ex);
            return Result.Error("Error when trying to get project.");
        }
    }
}



//Break out the checks into a seperate valid checker method.