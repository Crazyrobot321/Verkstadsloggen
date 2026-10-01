using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Infrastructure.Interface;

namespace Verkstadsloggen.UI.Pages;

public class JobDetails : PageModel
{
    private readonly IJobRepository _jobRepository;

    public JobDetails(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public Job? Job { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Job = await _jobRepository.GetJobByIdAsync(id);

        if (Job == null)
        {
            return NotFound();
        }

        return Page();
    }
}