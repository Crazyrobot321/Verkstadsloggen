using Microsoft.AspNetCore.Mvc.RazorPages;
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

    public async Task OnGetAsync()
    {
        People = await _personRepository.GetAllPeopleAsync();
    }
}