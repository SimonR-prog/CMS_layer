using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    //Takes in a customer form and maps the data into a new customer entity and returns it.
    public static CustomerEntity? Create(CustomerRegistrationForm form) => form == null ? null : new()
    {
        CustomerName = form.CustomerName,
        Email = form.Email
    };

    //Takes in an entity, returns a customer object.
    public static Customer? Create(CustomerEntity entity)
    {
        //If entity is null, return null.
        if (entity == null) 
        { 
            return null; 
        }

        //Maps the entity data into a new customer object.
        var customer = new Customer()
        {
            Id = entity.Id,
            CustomerName = entity.CustomerName,
            Email = entity.Email,
            Projects = new List<Project>()
        };

        //If the entity has projects attached to them, this takes the projects and adds them to a list and returns them together with the customer.
        if (entity.Projects != null) 
        {
            var projects = new List<Project>();

            foreach (var project in entity.Projects)
                projects.Add(new Project
                {
                    Id = project.Id,
                    Description = project.Description,
                });
            customer.Projects = projects;
        }
        return customer;
    }
}
