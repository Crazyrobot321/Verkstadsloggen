using Verkstadsloggen.Application.Interface;
using Verkstadsloggen.Application.Interfaces;
using Verkstadsloggen.Domain.Models;

namespace Verkstadsloggen.Application
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person?> GetPersonAsync(Guid id)
        {
            return await _personRepository.GetByIdAsync(id);
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            return await _personRepository.GetAllPeopleAsync();
        }

        public async Task<Customer> CreateCustomerAsync(string firstName, string lastName, string email, string phoneNumber)
        {
            var customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };

            await _personRepository.AddPersonAsync(customer);
            return customer;
        }

        public async Task<Person?> UpdateContactInfoAsync(Guid id, string firstName, string lastName, string email, string phoneNumber)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null) return null;

            person.FirstName = firstName;
            person.LastName = lastName;
            person.Email = email;
            person.PhoneNumber = phoneNumber;

            await _personRepository.SaveChangesAsync();
            return person;
        }

        public async Task<bool> DeletePersonAsync(Guid id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null) return false;

            await _personRepository.DeletePersonAsync(person);
            return true;
        }
    }
}