using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Tests.Repositories;

public class CustomerRepository_Tests
{
    private readonly DataContext _context;
    private readonly ICustomerRepository _customerRepository;

    public CustomerRepository_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid}")
            .Options;


        _context = new DataContext(options);
        _customerRepository = new CustomerRepository(_context);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnTrue_IfValidCustomerEntity()
    {
        //Arrange
        var entity = new CustomerEntity {  CustomerName = "Test", Email = "test@fuckElonMusk.com" };

        //Act
        bool result = await _customerRepository.AddAsync(entity);

        //Assert
        Assert.True(result);
    }
    [Fact]
    public async Task AddAsync_ShouldReturnFalse_IfInvalidCustomerEntity()
    {
        //Arrange
        var entity = new CustomerEntity { CustomerName = "Test" };

        //Act
        bool result = await _customerRepository.AddAsync(entity);

        //Assert
        Assert.False(result);
    }


    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_IfCustomerExists()
    {

    }
    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_IfCustomerDoesNotExist()
    {

    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnIEnumerable_IfDoesExists()
    {

    }
    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyIEnumerable_IfNotExists()
    {

    }

    [Fact]
    public async Task GetAsync_ShouldReturnEntity_IfExists()
    {

    }
    [Fact]
    public async Task GetAsync_ShouldReturnNull_IfNotExist()
    {

    }

    [Fact]
    public async Task RemoveAsync_ShouldReturnTrue_IfRemoved()
    {

    }
    [Fact]
    public async Task RemoveAsync_ShouldReturnFalse_IfNotRemoved()
    {

    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue_IfUpdated()
    {

    }
    [Fact]
    public async Task UpdateAsync_ShouldReturnFalse_IfNotUpdated()
    {

    }
}