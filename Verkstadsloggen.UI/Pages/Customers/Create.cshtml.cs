using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Infrastructure.Interface;

namespace Verkstadsloggen.UI.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly IPersonRepository _personRepo;

        public CreateModel(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }

        [BindProperty]
        public Customer Customer { get; set; } = new Customer();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _personRepo.AddPersonAsync(Customer);

            return RedirectToPage("Index");
        }
    }
}
