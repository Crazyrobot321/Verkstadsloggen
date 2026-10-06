using Verkstadsloggen.Domain.Enum;
using Verkstadsloggen.Domain.Models;

namespace Verkstadsloggen.Infrastructure.Interface
{
    public interface IJobRepository
    {
        Task AddJobAsync(Job job);
        Task<List<Job>> GetAllJobsAsync();
        Task<List<Job>> GetJobsByStatusAsync(JobStatus status);
        Task<List<Job>> GetJobsByLicensePlateAsync(string regnr);

        Task<Job?> GetJobByIdAsync(Guid id);
        Task<Job?> UpdateJobByIdAsync(Job job);

        Task AddCommentAsync(Comment comment);
    }
}
