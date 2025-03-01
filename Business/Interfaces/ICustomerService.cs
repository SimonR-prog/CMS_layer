using ResponseModel.Interfaces;
namespace Business.Interfaces
{
    public interface ICustomerService
    {
        Task<IResult> GetCustomersAsync();
    }
}