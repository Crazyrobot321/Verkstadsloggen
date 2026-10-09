using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Application.Interfaces;

namespace Verkstadsloggen.UI.Pages
{
    public class JobsModel : PageModel
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMechanicRepository _mechanicRepository;
        private readonly IPersonRepository _personRepository;

        public JobsModel(IJobRepository jobRepository, IMechanicRepository mechanicRepository, IPersonRepository personRepository)
        {
            _jobRepository = jobRepository;
            _mechanicRepository = mechanicRepository;
            _personRepository = personRepository;
        }

        public List<Job> jobs { get; set; } = new();
        public List<Mechanic> mechanics { get; set; } = new();
        public List<Customer> customers { get; set; } = new();

        [BindProperty]
        [Required]
        public string Title { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        public string Description { get; set; } = string.Empty;

        [BindProperty]
        public Guid? MechanicId { get; set; }
        [BindProperty]
        public Guid? CustomerId { get; set; }

        public async Task OnGetAsync()
        {
            await LoadAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadAsync();
                return Page();
            }

            var job = new Job
            {
                Title = Title,
                Description = Description,
                Status = Domain.Enum.JobStatus.NotStarted, // Set default status
                CustomerId = CustomerId,
                MechanicId = MechanicId
            };

            await _jobRepository.AddJobAsync(job);
            return RedirectToPage();
        }

        private async Task LoadAsync()
        {
            jobs = await _jobRepository.GetAllJobsAsync();
            mechanics = await _mechanicRepository.GetAllMechanicsAsync();
            customers = await _personRepository.GetAllCustomersAsync();
        }
    }
}