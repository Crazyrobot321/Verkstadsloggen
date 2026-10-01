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

        public async Task AddPersonAsync(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            return await _context.People.ToListAsync();
        }
    }
}