using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Application.Interfaces;

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
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = string.Empty;

    [BindProperty]
    public string Address { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [BindProperty]
    public string PersonType { get; set; } = "Customer";

    public async Task OnGetAsync()
    {
        People = await _personRepository.GetAllPeopleAsync();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (PersonType == "Customer" && string.IsNullOrWhiteSpace(Address))
            ModelState.AddModelError(nameof(Address), "Address is required for customers.");

        if (!ModelState.IsValid)
        {
            People = await _personRepository.GetAllPeopleAsync();
            return Page();
        }

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