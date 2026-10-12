using Verkstadsloggen.Domain.Models;

namespace Verkstadsloggen.Application.Interface
{
    public interface IPersonService
    {
        Task<Customer> CreateCustomerAsync(string firstName, string lastName, string email, string phoneNumber);
        Task<bool> DeletePersonAsync(Guid id);
        Task<List<Person>> GetAllPeopleAsync();
        Task<Person?> GetPersonAsync(Guid id);
        Task<Person?> UpdateContactInfoAsync(Guid id, string firstName, string lastName, string email, string phoneNumber);
    }
}