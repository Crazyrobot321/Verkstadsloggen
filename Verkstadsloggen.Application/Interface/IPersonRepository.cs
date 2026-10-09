using Verkstadsloggen.Domain.Models;

namespace Verkstadsloggen.Application.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person?> GetByIdAsync(Guid id);
        Task<List<Person>> GetAllPeopleAsync();
        Task AddPersonAsync(Person person);

        Task SaveChangesAsync();

        Task DeletePersonAsync(Person person);

        Task<List<Customer>> GetAllCustomersAsync();
    }
}