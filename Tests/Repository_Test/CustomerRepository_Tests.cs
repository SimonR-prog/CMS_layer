using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tests.TestData;

namespace Tests.Repositories;

public class CustomerRepository_Tests
{
    private readonly DataContext _context;
    private readonly ICustomerRepository _customerRepository;

    public CustomerRepository_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new DataContext(options);
        _customerRepository = new CustomerRepository(_context);
        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task AddAsync_ShouldReturnTrue_IfValidCustomerEntity()
    {
        //Arrange
        var entity = Test_Data.CustomerEntity_Valid_TestData[0];
        await _context.SaveChangesAsync();

        //Act
        var result = await _customerRepository.AddAsync(entity);

        //Assert
        Assert.True(result);
    }
    [Fact]
    public async Task AddAsync_ShouldReturnFalse_IfInvalidCustomerEntity()
    {
        //Arrange
        var entity = new CustomerEntity { CustomerName = "Invalid test" };

        //Act
        var result = await _customerRepository.AddAsync(entity);

        //Assert
        Assert.False(result);
    }


    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_IfCustomerExists()
    {
        //Arrange 
        _context.Customers.AddRange(Test_Data.CustomerEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        var validCustomerId = 1;

        //Act
        var result = await _customerRepository.ExistsAsync(customer => customer.Id == validCustomerId);

        //Assert
        Assert.True(result);

    }
    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_IfCustomerDoesNotExist()
    {
        //Arrange 
        _context.Customers.AddRange(Test_Data.CustomerEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        var invalidCustomerId = 666;

        //Act
        var result = await _customerRepository.ExistsAsync(customer => customer.Id == invalidCustomerId);

        //Assert
        Assert.False(result);

    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnIEnumerable_IfDoesExists()
    {
        //Arrange 
        _context.Customers.AddRange(Test_Data.CustomerEntity_Valid_TestData);
        await _context.SaveChangesAsync();

        //Act
        var result = await _customerRepository.GetAllAsync();

        //Assert
        Assert.Equal(Test_Data.CustomerEntity_Valid_TestData.Count(), result.Count());

    }
    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyIEnumerable_IfNotExists()
    {
        //Arrange 

        //Act
        var result = await _customerRepository.GetAllAsync();

        //Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnEntity_IfExists()
    {
        //Arrange 
        _context.Customers.AddRange(Test_Data.CustomerEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        var validCustomerId = 1;

        //Act
        var result = await _customerRepository.GetAsync(customer => customer.Id == validCustomerId);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.Id == validCustomerId);

    }
    [Fact]
    public async Task GetAsync_ShouldReturnNull_IfNotExist()
    {
        //Arrange 
        var validCustomerId = 1;

        //Act
        var result = await _customerRepository.GetAsync(customer => customer.Id == validCustomerId);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task RemoveAsync_ShouldReturnTrue_IfRemoved()
    {
        //Arrange 
        _context.Customers.AddRange(Test_Data.CustomerEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        var validCustomer = Test_Data.CustomerEntity_Valid_TestData[0];

        //Act
        var result = await _customerRepository.RemoveAsync(validCustomer);

        //Assert
        Assert.True(result);
    }
    [Fact]
    public async Task RemoveAsync_ShouldReturnFalse_IfNotRemoved()
    {
        //Arrange 
        _context.Customers.AddRange(Test_Data.CustomerEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        var invalidCustomer = Test_Data.CustomerEntity_Invalid_TestData[0];

        //Act
        var result = await _customerRepository.RemoveAsync(invalidCustomer);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue_IfUpdated()
    {
        //Arrange
        _context.Customers.AddRange(Test_Data.CustomerEntity_Valid_TestData);
        await _context.SaveChangesAsync();
        var updatedCustomer = new CustomerEntity { Id = 1, CustomerName = "UpdatedNameNackademin", Email = "Nackademin@email.com" };

        //Act
        var result = await _customerRepository.UpdateAsync(updatedCustomer);

        //Assert
        Assert.Equal(updatedCustomer.Id, result.Id);
        Assert.True(updatedCustomer.CustomerName == result.CustomerName);
        Assert.False(updatedCustomer.CustomerName == Test_Data.CustomerEntity_Valid_TestData[0].CustomerName);
        Assert.True(result.CustomerName == "UpdatedNameNackademin");

    }
    [Fact]
    public async Task UpdateAsync_ShouldReturnNull_IfEntityNull()
    {
        //Arrange
        _context.Customers.AddRange(Test_Data.CustomerEntity_Valid_TestData);
        await _context.SaveChangesAsync();

        //Act
        var result = await _customerRepository.UpdateAsync(null);

        //Assert
        Assert.Null(result);
    }
}