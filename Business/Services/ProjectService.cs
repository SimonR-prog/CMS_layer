using Business.Factories;
using Business.Interfaces;
using Business.Models;
using ResponseModel.Interfaces;
using ResponseModel.Models;
using System.Diagnostics;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, ICustomerRepository customerRepository) : IProjectService
{
    //Initializes some private fields.
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<Result> CreateProjectAsync(ProjectRegistrationForm registrationForm)
    {
        try
        {   
            if (!await _customerRepository.ExistsAsync(customer => customer.Id == registrationForm.CustomerId))
            {
                return Result.AlreadyExists("Customer already exists.");
            }
            //Sends form to the factory and adds the resulting entity into var.
            var projectEntity = ProjectFactory.Create(registrationForm);
            //If entity null, return notFound.
            if (projectEntity == null)
            {
                return Result.NotFound("Project entity is null.");
            }

            //Sends entity to the adding method in the repo and adds the return into the bool and returns it.
            if (await _projectRepository.AddAsync(projectEntity))
               { 
                return Result.Ok(); 
            }
            return Result.Error("Something went wrong.");
        }
        //If something in the try doesn't work, send message to debug and return false.
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return Result.Error(ex.ToString());
        }
    }

    //Doesn't take in any parameters, returns an IEnum list of project objects. 
    public async Task<Result<IEnumerable<Project?>>> GetProjectsAsync()
    {
        try
        {   
            //Gets all the entities.
            var entities = await _projectRepository.GetAllAsync();
            //Turns all the entities into projects with the help of the factory and then returns them as IEnum. Select sends one by one.
            var projects = entities.Select(ProjectFactory.Create);
            return Result<IEnumerable<Project?>>.Ok(projects);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            //Returns a new empty list if anything goes wrong in the try.
            return Result<IEnumerable<Project?>>.Error(Enumerable.Empty<Project?>(), ex.Message);
        }
    }
    
    public async Task<Result> UpdateProjectAsync(ProjectUpdateForm updateForm)
    {
        try
        {   
            //Sends updateForm to factory.
            var entity = ProjectFactory.Create(updateForm);
            if (entity == null)
            {
                return Result.NotFound("Project is null.");
            }
            //Sends entity to the update in repository.
            await _projectRepository.UpdateAsync(entity);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return Result.Error("Error when updating data in project.");
        }
    }
    public async Task<Result> RemoveProjectAsync(int id)
    {
        try
        {
            //If project doesn't exist, return false.
            if (!await _projectRepository.ExistsAsync(project => project.Id == id))
            {
                return Result.NotFound("Project doesn't exist.");
            }
            //Get the project.
            var project = await _projectRepository.GetAsync(project => project.Id == id);
            //If project is null, return false.
            if (project == null)
            {
                return Result.NotFound("Project entity is null.");
            }
            //Removes project and returns ok.
            await _projectRepository.RemoveAsync(project);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return Result.Error("Error when trying to remove project.");
        }
    }
    public async Task<Result> GetProjectAsync(int id)
    {
        try
        {
            //If it doesn't exist return not found.
            if (!await _projectRepository.ExistsAsync(project => project.Id == id))
            {
                return Result.NotFound("Project doesn't exist.");
            }
            //Gets the projectentity from its id.
            var projectEntity = await _projectRepository.GetAsync(project => project.Id == id);
            if (projectEntity == null)
            {
                return Result.Error("Project not found.");
            }
            //Returns the resulting project from sending the entity to the factory.
            var project = ProjectFactory.Create(projectEntity);
            return Result<Project?>.Ok(project);
        }
        catch (Exception ex) 
        {
            Debug.WriteLine(ex);
            return Result.Error("Error when trying to get project.");
        }
    }
}