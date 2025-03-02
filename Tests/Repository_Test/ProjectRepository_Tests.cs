using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Tests.TestData;

namespace Tests.Repositories;

public class ProjectRepository_Tests
{
    private readonly DataContext _context;
    private readonly IProjectRepository _projectRepository;

    public ProjectRepository_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new DataContext(options);
        _projectRepository = new ProjectRepository(_context);
        _context.Database.EnsureCreated();
    }


    [Fact]
    public async Task AddAsync_ShouldReturnTrue_IfValidProjectEntity()
    {
        //Arrange
        var entity = Test_Data.ProjectEntity_Valid_TestData[0];
        await _context.SaveChangesAsync();

        //Act
        var result = await _projectRepository.AddAsync(entity);

        //Assert
        Assert.True(result);
    }
    [Fact]
    public async Task AddAsync_ShouldReturnFalse_IfInvalidProjectEntity()
    {
        //Arrange
        var entity = new ProjectEntity { ProjectName = null };

        //Act
        var result = await _projectRepository.AddAsync(entity);

        //Assert
        Assert.False(result);
    }


    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_IfProjectExists()
    {
        //Arrange 
        _context.Projects.AddRange(Test_Data.ProjectEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        var validProjectId = 3;

        //Act
        var result = await _projectRepository.ExistsAsync(project => project.Id == validProjectId);

        //Assert
        Assert.True(result);

    }
    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_IfProjectDoesNotExist()
    {
        //Arrange 
        _context.Projects.AddRange(Test_Data.ProjectEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        var invalidProjectId = 666;

        //Act
        var result = await _projectRepository.ExistsAsync(project => project.Id == invalidProjectId);

        //Assert
        Assert.False(result);

    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnIEnumerable_IfDoesExists()
    {
        //Arrange 
        _context.Projects.AddRange(Test_Data.ProjectEntity_Valid_TestData);
        _context.Customers.AddRange(Test_Data.CustomerEntity_Valid_TestData);
        await _context.SaveChangesAsync();

        //Act
        var result = await _projectRepository.GetAllAsync();

        //Assert
        Assert.Equal(Test_Data.ProjectEntity_Valid_TestData.Count(), result.Count());

    }
    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyIEnumerable_IfNotExists()
    {
        //Arrange 

        //Act
        var result = await _projectRepository.GetAllAsync();

        //Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnEntity_IfExists()
    {
        //Arrange 
        _context.Projects.AddRange(Test_Data.ProjectEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        var validProjectId = 1;

        //Act
        var result = await _projectRepository.GetAsync(project => project.Id == validProjectId);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.Id == validProjectId);

    }
    [Fact]
    public async Task GetAsync_ShouldReturnNull_IfNotExist()
    {
        //Arrange 
        var validProjectId = 1;

        //Act
        var result = await _projectRepository.GetAsync(project => project.Id == validProjectId);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task RemoveAsync_ShouldReturnTrue_IfRemoved()
    {
        //Arrange 
        _context.Projects.AddRange(Test_Data.ProjectEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        var validProject = Test_Data.ProjectEntity_Valid_TestData[0];

        //Act
        var result = await _projectRepository.RemoveAsync(validProject);

        //Assert
        Assert.True(result);
    }
    [Fact]
    public async Task RemoveAsync_ShouldReturnFalse_IfNotRemoved()
    {
        //Arrange 
        _context.Projects.AddRange(Test_Data.ProjectEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        var invalidProject = Test_Data.ProjectEntity_Invalid_TestData[0];

        //Act
        var result = await _projectRepository.RemoveAsync(invalidProject);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue_IfUpdated()
    {
        //Arrange
        _context.Projects.AddRange(Test_Data.ProjectEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        var updatedProject = new ProjectEntity { Id = 1, ProjectName = "UpdatedNameNackademin" };

        //Act
        var result = await _projectRepository.UpdateAsync(updatedProject);

        //Assert
        Assert.Equal(updatedProject.Id, result.Id);
        Assert.True(updatedProject.ProjectName == result.ProjectName);
        Assert.False(updatedProject.ProjectName == Test_Data.ProjectEntity_Valid_TestData[0].ProjectName);
        Assert.True(result.ProjectName == "UpdatedNameNackademin");

    }
    [Fact]
    public async Task UpdateAsync_ShouldReturnNull_IfEntityNull()
    {
        //Arrange
        _context.Projects.AddRange(Test_Data.ProjectEntity_Valid_TestData);
        await _context.SaveChangesAsync();

        //Act
        var result = await _projectRepository.UpdateAsync(null);

        //Assert
        Assert.Null(result);
    }


}
