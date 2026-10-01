using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Verkstadsloggen.Domain.Enum;
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
    [BindProperty]
    public JobStatus NewStatus { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Job = await _jobRepository.GetJobByIdAsync(id);

        if (Job == null)
        {
            return NotFound();
        }
        NewStatus = Job.Status;
        return Page();
    }

    public async Task<IActionResult> OnPostUpdateStatusAsync(Guid id)
    {
        Job = await _jobRepository.GetJobByIdAsync(id);
        if (Job == null)
        {
            return NotFound();
        }
        Job.Status = NewStatus;
        await _jobRepository.UpdateJobByIdAsync(Job);
        return RedirectToPage("/Jobs");
    }
}