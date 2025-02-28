
using Business.Factories;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    //Doesn't take in any parameters, returns an IEnum list of customer objects.
    public async Task<IEnumerable<Customer?>> GetCustomersAsync()
    {
        //Get all the customerentities and then sends them to the customer factory to turn them into customer objects and returns them.
        var entities = await _customerRepository.GetAllAsync();
        var customers = entities.Select(CustomerFactory.Create);
        return customers;
    }

}
