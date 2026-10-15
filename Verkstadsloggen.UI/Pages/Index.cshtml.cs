using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Verkstadsloggen.Application.Interfaces;
using Verkstadsloggen.Domain.Enum;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Infrastructure.Identity;

namespace Verkstadsloggen.UI.Pages;

[Authorize]
public class DashboardModel : PageModel
{
    private readonly IJobRepository _jobRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public DashboardModel(IJobRepository jobRepository, UserManager<ApplicationUser> userManager)
    {
        _jobRepository = jobRepository;
        _userManager = userManager;
    }

    public record MechanicLoad(string Name, int NotStarted, int InProgress, int Completed)
    {
        public int Total => NotStarted + InProgress + Completed;
    }

    public List<Job> Jobs { get; set; } = new();
    public List<Job> Latest { get; set; } = new();
    public List<MechanicLoad> Loads { get; set; } = new();
    public int NotStarted { get; set; }
    public int InProgress { get; set; }
    public int Completed { get; set; }

    public async Task OnGetAsync()
    {
        var allJobs = await _jobRepository.GetAllJobsAsync();

        if (User.IsInRole("Admin"))
        {
            Jobs = allJobs;
        }
        else if (User.IsInRole("Mechanic"))
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.PersonId != null)
                Jobs = allJobs.Where(j => j.MechanicId == user.PersonId).ToList();
        }

        NotStarted = Jobs.Count(j => j.Status == JobStatus.NotStarted);
        InProgress = Jobs.Count(j => j.Status == JobStatus.InProgress);
        Completed = Jobs.Count(j => j.Status == JobStatus.Completed);

        Latest = Jobs.OrderByDescending(j => j.CreatedAt).Take(5).ToList();

        Loads = Jobs
            .GroupBy(j => j.Mechanic != null ? $"{j.Mechanic.FirstName} {j.Mechanic.LastName}" : "Ingen mekaniker")
            .Select(g => new MechanicLoad(
                g.Key,
                g.Count(j => j.Status == JobStatus.NotStarted),
                g.Count(j => j.Status == JobStatus.InProgress),
                g.Count(j => j.Status == JobStatus.Completed)))
            .OrderByDescending(l => l.Total)
            .ToList();
    }
}