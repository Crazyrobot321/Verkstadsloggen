using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Infrastructure.Interface;

namespace Verkstadsloggen.UI.Pages;

public class PeopleModel : PageModel
{
    private readonly IPersonRepository _personRepository;

    public PeopleModel(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public List<Person> People { get; set; } = new();
    [BindProperty]
    public string FirstName { get; set; } = string.Empty;

    [BindProperty]
    public string LastName { get; set; } = string.Empty;
    
    [BindProperty]
    public string Address { get; set; } = string.Empty;

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string PhoneNumber { get; set; } = string.Empty;

    [BindProperty]
    public string PersonType { get; set; } = "Customer";

    public async Task OnGetAsync()
    {
        People = await _personRepository.GetAllPeopleAsync();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        Person person = PersonType == "Customer" 
            ? new Customer { Address = Address ?? string.Empty }
            : new Mechanic();
        person.FirstName = FirstName;
        person.LastName = LastName;
        person.Email = Email;
        person.PhoneNumber = PhoneNumber;

        await _personRepository.AddPersonAsync(person);

        return RedirectToPage();
    }
}