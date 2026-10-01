using Verkstadsloggen.Domain.Models;

namespace Verkstadsloggen.Infrastructure.Interface
{
    public interface IJobRepository
    {
        Task AddJobAsync(Job job);
        Task<List<Job>> GetAllJobsAsync();
<<<<<<< HEAD
        Task<Job?> GetJobByIdAsync(Guid id);
        Task<Job> UpdateJobByIdAsync(Job job);
=======
        Task<Job> GetJobByIdAsync(Guid id);
        Task AddCommentAsync(Comment comment);
>>>>>>> origin/main
    }
}