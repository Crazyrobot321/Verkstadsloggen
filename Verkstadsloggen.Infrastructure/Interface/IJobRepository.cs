using Verkstadsloggen.Domain.Models;

namespace Verkstadsloggen.Infrastructure.Interface
{
    public interface IJobRepository
    {
        Task AddJobAsync(Job job);
        Task<List<Job>> GetAllJobsAsync();
        Task<Job> GetJobByIdAsync(Guid id);
        Task AddCommentAsync(Comment comment);
    }
}