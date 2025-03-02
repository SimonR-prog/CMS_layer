using Business.Factories;
using Business.Interfaces;
using Business.Models;
using ResponseModel.Models;
using System.Diagnostics;

namespace Business.Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<Result<IEnumerable<Customer?>>> GetCustomersAsync()
        {
            try
            {
                var entities = await _customerRepository.GetAllAsync();
                var customers = entities.Select(CustomerFactory.Create);
                return Result<IEnumerable<Customer?>>.Ok(customers);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                //Got help from chatgpt for this line. It wanted me to return null first, but I asked to change it to an empty list instead.
                return Result<IEnumerable<Customer?>>.Error(Enumerable.Empty<Customer?>(), ex.Message);
            }
        }
    }
}
