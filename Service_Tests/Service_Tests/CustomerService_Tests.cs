using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Tests.TestData;
namespace Tests.Service_Test;

public class CustomerService_Tests
{
    private readonly DataContext _context;
    private readonly ICustomerService _customerService;

    public CustomerService_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new DataContext(options);
        _customerService = new CustomerService(new CustomerRepository(_context));
        _context.Database.EnsureCreated();
    }



    [Fact]
    public async Task GetCustomersAsync_ShouldReturnIEnumerableOfCustomers()
    {
        //Arrange
        _context.Customers.AddRange(Test_Data_Service.CustomerEntity_Valid_TestData);
        await _context.SaveChangesAsync();

        //Act
        var result = await _customerService.GetCustomersAsync();

        //Assert
        Assert.NotNull(result.Content);
        Assert.Equal(result.Content.Count(), Test_Data_Service.CustomerEntity_Valid_TestData.Count());
    }
    [Fact]
    public async Task GetCustomersAsync_ShouldReturnEmptyEnumerableOfCustomers()
    {
        //Arrange


        //Act
        var result = await _customerService.GetCustomersAsync();

        //Assert
        Assert.NotNull(result.Content);
        Assert.Empty(result.Content);
    }

}
