
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Models.ResultModels;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<IResult> GetCustomersAsync()
    {
        var entities = await _customerRepository.GetAllAsync();
        var customers = entities.Select(CustomerFactory.Create);
        return Result<IEnumerable<Customer?>>.Ok(customers);
    }

}
