using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Infrastructure.Interface;

namespace Verkstadsloggen.UI.Pages
{
    public class JobsModel : PageModel
    {
        private readonly IJobRepository _jobRepository;

        public JobsModel(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public List<Job> jobs { get; set; } = new();

        [BindProperty]
        [Required]
        public string Title { get; set; } = string.Empty;
        [BindProperty]
        [Required]
        public string Description { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            jobs = await _jobRepository.GetAllJobsAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var job = new Job
            {
                Title = Title,
                Description = Description,
                Status = Domain.Enum.JobStatus.NotStarted // Set default status
            };
            await _jobRepository.AddJobAsync(job);
            return RedirectToPage();
        }
    }
}
