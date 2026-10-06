using Verkstadsloggen.Domain.Models;

namespace Verkstadsloggen.Infrastructure.Interface
{
    public interface IPersonRepository
    {
        Task AddPersonAsync(Customer customer);
        Task<List<Customer>> GetAllPeopleAsync();
        Task<Customer?> GetByLicensePlateAsync(string regnr);
    }
}
