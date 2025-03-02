using Business.Models;
using ResponseModel.Models;
namespace Business.Interfaces
{
    public interface ICustomerService
    {
        Task<Result<IEnumerable<Customer?>>> GetCustomersAsync();
    }
}