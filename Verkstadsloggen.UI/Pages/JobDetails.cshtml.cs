using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Verkstadsloggen.Domain.Enum;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Application.Interfaces;

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

    [BindProperty]
    public string NewComment { get; set; } = string.Empty;

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
    public async Task<IActionResult> OnPostAddCommentAsync(Guid id)
    {
        var job = await _jobRepository.GetJobByIdAsync(id);

        if (job == null)
        {
            return NotFound();
        }
        if (string.IsNullOrWhiteSpace(NewComment))
        {
            return Page();
        }

        var comment = new Comment
        {
            Text = NewComment,
            JobId = job.Id
        };
        await _jobRepository.AddCommentAsync(comment);
        return RedirectToPage(new { id });
    }
}