using Verkstadsloggen.Domain.Models;

namespace Verkstadsloggen.Application.Interfaces
{
    public interface IPersonRepository
    {
        Task AddPersonAsync(Person person);
        Task<List<Person>> GetAllPeopleAsync();
    }
}