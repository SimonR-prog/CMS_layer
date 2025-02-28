using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, ICustomerRepository customerRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;

    //Takes in a projectregistration form and returns a bool.
    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm registrationForm)
    {
        try
        {   
            //If customer doesn't exist, return null.
            if (!await _customerRepository.ExistsAsync(customer => customer.Id == registrationForm.CustomerId))
            {
                return false;
            }
            //Sends form to the factory and adds the resulting entity into var.
            var projectEntity = ProjectFactory.Create(registrationForm);
            //If entity null, return false.
            if (projectEntity == null)
            {
                return false;
            }
            //Sends entity to the adding method in the repo and adds the return into the bool and returns it.
            bool result = await _projectRepository.AddAsync(projectEntity);
            return result;
        }
        //If something in the try doesn't work, send message to debug and return false.
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    //Doesn't take in any parameters, returns an IEnum list of project objects. 
    public async Task<IEnumerable<Project?>> GetProjectsAsync()
    {
        try
        {   
            //Var entities fetches all the entities.
            var entities = await _projectRepository.GetAllAsync();
            //Var projects turns all the entities into projects with the help of the factory and then returns them.
            var projects = entities.Select(ProjectFactory.Create);
            return projects;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            //Returns a new empty list if anything goes wrong in the try.
            return new List<Project>();
        }
    }
    public async Task<bool> UpdateProjectAsync(ProjectUpdateForm updateForm)
    {
        try
        {
            var entity = ProjectFactory.Create(updateForm);
            if ( entity == null)
            {
                return false;
            }
            var updatedEntity = await _projectRepository.UpdateAsync(entity);
            return true;
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
            //If project doesn't exist, return false.
            if (!await _projectRepository.ExistsAsync(project => project.Id == id))
            {
                return false;
            }
            //Get the project.
            var project = await _projectRepository.GetAsync(project => project.Id == id);
            //If project is null, return false.
            if (project == null)
            {
                return false;
            }
            //Remove the project and return the bool.
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
            //Check if exists?

            //Gets the projectentity from its id and uses factory to turn it into a project and returns it.
            var projectEntity = await _projectRepository.GetAsync(project => project.Id == id);
            if (projectEntity != null)
            {
                //Returns the resulting project from sending the entity to the factory. 
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
