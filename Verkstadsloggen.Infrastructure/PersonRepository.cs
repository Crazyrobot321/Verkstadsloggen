using Microsoft.EntityFrameworkCore;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Infrastructure.Data;
using Verkstadsloggen.Infrastructure.Interface;

namespace Verkstadsloggen.Infrastructure
{
    public class PersonRepository : IPersonRepository
    {
        private readonly MyDbContext _context;

        public PersonRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task AddPersonAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(); 
        }

        public async Task<List<Customer>> GetAllPeopleAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByLicensePlateAsync(string regnr)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.LicensePlate == regnr);
        }
    }
}
