using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Verkstadsloggen.UI.Pages
{
    [Authorize]
    public class JobsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJobRepository _jobRepository;
        private readonly IMechanicRepository _mechanicRepository;
        private readonly IPersonRepository _personRepository;

        public JobsModel(IJobRepository jobRepository, IMechanicRepository mechanicRepository, IPersonRepository personRepository, UserManager<ApplicationUser> userManager)
        {
            _jobRepository = jobRepository;
            _mechanicRepository = mechanicRepository;
            _personRepository = personRepository;
            _userManager = userManager;
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
        [BindProperty]
        public string LicensePlate { get; set; } = string.Empty;

        public async Task OnGetAsync(string search)
        {
            await LoadAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                jobs = jobs
                    .Where(j => j.LicensePlate != null &&
                                j.LicensePlate.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.IsInRole("Admin"))
            {
                return Forbid();
            }

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
                MechanicId = MechanicId,
                LicensePlate = LicensePlate,
            };

            await _jobRepository.AddJobAsync(job);
            return RedirectToPage();
        }

        private async Task LoadAsync()
        {
            var allJobs = await _jobRepository.GetAllJobsAsync();

            if (User.IsInRole("Admin"))
            {
                jobs = allJobs;
            }
            else if (User.IsInRole("Mechanic"))
            {
                var user = await _userManager.GetUserAsync(User);
                jobs = user?.PersonId is null //check if the user has a PersonId, if not return an empty list 
                    ? new List<Job>()
                    : allJobs.Where(j => j.MechanicId == user.PersonId).ToList();
            }

            mechanics = await _mechanicRepository.GetAllMechanicsAsync();
            customers = await _personRepository.GetAllCustomersAsync();
        }
    }
}