using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    // Factory with three different create methods which will run depending on which type of object gets sent to it.
    public static ProjectEntity? Create(ProjectRegistrationForm registrationForm) => new ProjectEntity
    {
        ProjectName = registrationForm.ProjectName,
        Description = registrationForm.Description,
        CustomerId = registrationForm.CustomerId,
    };
    public static ProjectEntity? Create(ProjectUpdateForm updateForm) => new ProjectEntity
    {
        Id = updateForm.Id,
        ProjectName = updateForm.ProjectName,
        Description = updateForm.Description,
        CustomerId = updateForm.CustomerId
    };
    public static Project? Create(ProjectEntity entity)
    {
        if (entity == null)
        {
            return null;
        }

        var project = new Project
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName,
            Description = entity.Description,
        };

        if (entity.Customer != null)
        {
            project.Customer = new Customer
            {
                Id = entity.Customer.Id,
                CustomerName = entity.Customer.CustomerName,
                Email = entity.Customer.Email,
            };
        }
        return project;
    }
}
