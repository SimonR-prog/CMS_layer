using Business.Factories;
using Business.Interfaces;
using Business.Models;
using ResponseModel.Interfaces;
using ResponseModel.Models;

namespace Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        // Correct constructor syntax
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // Doesn't take in any parameters, returns an IEnumerable list of customer objects.
        public async Task<IResult> GetCustomersAsync()
        {
            // Get all the customer entities and then send them to the customer factory to turn them into customer objects.
            var entities = await _customerRepository.GetAllAsync();
            var customers = entities.Select(CustomerFactory.Create);  // Select should work here
            return Result<IEnumerable<Customer?>>.Ok(customers);
        }
    }
}
