using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using ResponseModel.Models;
using Tests.TestData;

namespace Service_Tests.Service_Tests;

public class ProjectService_Tests
{
    private readonly DataContext _context;
    private readonly IProjectService _projectService;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProjectRepository _projectRepository;

    public ProjectService_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new DataContext(options);
        _customerRepository = new CustomerRepository(_context);
        _projectRepository = new ProjectRepository(_context);
        _projectService = new ProjectService(_projectRepository, _customerRepository);
        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task CreateProjectAsync_ShouldReturnTrue_IfValidProjectForm()
    {
        //Arrange
        _context.Customers.AddRange(Test_Data_Service.CustomerEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        var validProjectForm = Test_Data_Service.ProjectRegistrationForms_Valid_TestData[0];

        //Act
        var result = await _projectService.CreateProjectAsync(validProjectForm);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
    }
    [Fact]
    public async Task CreateProjectAsync_ShouldReturnFalse_IfInvalidProjectForm()
    {
        //Arrange
        _context.Customers.AddRange(Test_Data_Service.CustomerEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        var invalidProjectForm = Test_Data_Service.ProjectRegistrationForms_Invalid_TestData[0];

        //Act
        var result = await _projectService.CreateProjectAsync(invalidProjectForm);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task GetProjectsAsync_ShouldReturnIEnumerableListOfProjects()
    {
        //Arrange
        _context.Customers.AddRange(Test_Data_Service.CustomerEntity_Valid_TestData);
        _context.Projects.AddRange(Test_Data_Service.ProjectEntity_Valid_TestData);
        await _context.SaveChangesAsync();

        //Act
        var result = await _projectService.GetProjectsAsync();

        //Assert
        Assert.NotNull(result.Content);
        Assert.True(result.Success);
        Assert.Equal(result.Content.Count(), Test_Data_Service.ProjectEntity_Valid_TestData.Count());
    }
    [Fact]
    public async Task GetProjectAsync_ShouldReturnEmptyEnumerable()
    {
        //Arrange

        //Act
        var result = await _projectService.GetProjectsAsync();

        //Assert
        Assert.NotNull(result.Content);
        Assert.Empty(result.Content);
    }
    
    [Fact]
    public async Task UpdateProjectAsync_ShouldReturnTrue()
    {
        //Arrange
        _context.Customers.AddRange(Test_Data_Service.CustomerEntity_Valid_TestData);
        _context.Projects.AddRange(Test_Data_Service.ProjectEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        var updatedProject = new ProjectUpdateForm { Id = 1, ProjectName = "UpdatedNameNackademin", CustomerId = 1, Description = "" };

        //Act
        var result = await _projectService.UpdateProjectAsync(updatedProject);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task UpdateProjectAsync_ShouldReturnFalse()
    {
        //Arrange

        //Act
        var result = await _projectService.UpdateProjectAsync(null!);

        //Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task RemoveProjectAsync_ShouldRemoveIfSuccessful()
    {
        //Arrange
        _context.Customers.AddRange(Test_Data_Service.CustomerEntity_Valid_TestData);
        _context.Projects.AddRange(Test_Data_Service.ProjectEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        int removeId = 1;

        //Act
        await _projectService.RemoveProjectAsync(removeId);
        var result = await _projectService.GetProjectsAsync();

        //Assert
        Assert.NotNull(result.Content);
        Assert.True(result.Content.Count() < Test_Data_Service.ProjectEntity_Valid_TestData.Count());
    }
    [Fact]
    public async Task RemoveProjectAsync_ShouldReturnFalseIfGivenInvalidId()
    {
        //Arrange
        _context.Customers.AddRange(Test_Data_Service.CustomerEntity_Valid_TestData);
        _context.Projects.AddRange(Test_Data_Service.ProjectEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        int removeInvalidId = 99;

        //Act
        await _projectService.RemoveProjectAsync(removeInvalidId);
        var result = await _projectService.GetProjectsAsync();

        //Assert
        Assert.NotNull(result.Content);
        Assert.True(result.Content.Count() == Test_Data_Service.ProjectEntity_Valid_TestData.Count());
    }

    [Fact]
    public async Task GetProjectAsync_ShouldReturnTrue()
    {
        //Arrange
        _context.Customers.AddRange(Test_Data_Service.CustomerEntity_Valid_TestData);
        _context.Projects.AddRange(Test_Data_Service.ProjectEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        int validId = 1;

        //Act
        var result = await _projectService.GetProjectAsync(validId);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task GetProjectAsync_ShouldReturnFalse()
    {
        //Arrange
        _context.Customers.AddRange(Test_Data_Service.CustomerEntity_Valid_TestData);
        _context.Projects.AddRange(Test_Data_Service.ProjectEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        int invalidId = 99;

        //Act
        var result = await _projectService.GetProjectAsync(invalidId);

        //Assert
        Assert.False(result.Success);
    }
}
