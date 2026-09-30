using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Verkstadsloggen.Domain.Enum;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Infrastructure.Data;
using Verkstadsloggen.Infrastructure.Interface;

namespace Verkstadsloggen.Infrastructure
{
    public class JobRepository : IJobRepository
    {
        private readonly MyDbContext _context;
        public JobRepository(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public async Task AddJobAsync(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Job>> GetAllJobsAsync()
        {
            return await _context.Jobs.ToListAsync();
        }
        public async Task<List<Job>> GetJobsByStatusAsync(JobStatus status)
        {
            return await _context.Jobs
                .Where(j => j.Status == status)
                .ToListAsync();
        }


        public async Task<Job> GetJobByIdAsync(Guid id)
        {
            return await _context.Jobs.FindAsync(id);
        }
    }
}
