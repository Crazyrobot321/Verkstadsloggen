using Verkstadsloggen.Domain.Models;

namespace Verkstadsloggen.Infrastructure.Interface
{
    public interface IPersonRepository
    {
        Task AddPersonAsync(Person person);
        Task<List<Person>> GetAllPeopleAsync();
    }
}