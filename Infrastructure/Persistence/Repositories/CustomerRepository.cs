using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentException(nameof(context));
    }

    public async Task Add(Customer custormer) => await _context.Customers.AddAsync(custormer);

    public async Task<Customer?> GetByIdAsync(CustomerId id)
    {
        return await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
    }
}
