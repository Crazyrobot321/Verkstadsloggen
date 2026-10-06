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
        private readonly IMechanicRepository _mechanicRepository;

        public JobsModel(IJobRepository jobRepository, IMechanicRepository mechanicRepository)
        {
            _jobRepository = jobRepository;
            _mechanicRepository = mechanicRepository;
        }

        public List<Job> jobs { get; set; } = new();
        public List<Mechanic> mechanics { get; set; } = new();

        [BindProperty]
        [Required]
        public string Title { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        public string Description { get; set; } = string.Empty;

        [BindProperty]
        public Guid? MechanicId { get; set; }

        public async Task OnGetAsync()
        {
            jobs = await _jobRepository.GetAllJobsAsync();
            mechanics = await _mechanicRepository.GetAllMechanicsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                mechanics = await _mechanicRepository.GetAllMechanicsAsync();
                jobs = await _jobRepository.GetAllJobsAsync();
                return Page();
            }

            var job = new Job
            {
                Title = Title,
                Description = Description,
                Status = Domain.Enum.JobStatus.NotStarted,
                MechanicId = MechanicId
            };

            await _jobRepository.AddJobAsync(job);
            return RedirectToPage();
        }
    }
}
