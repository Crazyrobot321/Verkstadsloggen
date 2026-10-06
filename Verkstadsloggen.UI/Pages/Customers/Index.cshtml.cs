using Microsoft.AspNetCore.Mvc.RazorPages;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Infrastructure.Interface;

namespace Verkstadsloggen.UI.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly IPersonRepository _personRepository;

        public IndexModel(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public List<Customer> customers { get; set; } = new();

        public async Task OnGetAsync(string? regnr)
        {
            if (!string.IsNullOrEmpty(regnr))
            {
                var customer = await _personRepository.GetByLicensePlateAsync(regnr);

                if (customer != null)
                {
                    customers = new List<Customer> { customer };
                }
                else
                {
                    customers = new List<Customer>();
                }
            }
            else
            {
                customers = await _personRepository.GetAllPeopleAsync();
            }
        }
    }
}
