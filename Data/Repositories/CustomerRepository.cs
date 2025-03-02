using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : BaseRepository<CustomerEntity>(context), ICustomerRepository
{
    public override async Task<CustomerEntity?> GetAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        try
        {
            var entity = await _db
            .Include(x => x.Projects)
            .FirstOrDefaultAsync(expression);
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return null;
        }
    }
}