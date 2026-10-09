using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Verkstadsloggen.Application.Interface;
using Verkstadsloggen.Application.Interfaces;
using Verkstadsloggen.Domain.Models;

namespace Verkstadsloggen.Application
{
    public class TimeLogService : ITimeLogService
    {
        private readonly ITimeLogRepository _timeLogRepository;

        public TimeLogService(ITimeLogRepository timeLogRepository)
        {
            _timeLogRepository = timeLogRepository;
        }

        public async Task AddTimeLogAsync(Guid jobId, DateTime start, DateTime end, string description)
        {
            var log = new TimeLog
            {
                JobId = jobId,
                StartTime = start,
                EndTime = end,
                Description = description
            };

            await _timeLogRepository.AddTimeLogAsync(log);
        }

        public async Task<List<TimeLog>> GetLogsForJobAsync(Guid jobId)
        {
            return await _timeLogRepository.GetLogsForJobAsync(jobId);
        }

        public async Task<TimeLog?> GetLogByIdAsync(Guid id)
        {
            return await _timeLogRepository.GetLogByIdAsync(id);
        }

        public async Task UpdateTimeLogAsync(TimeLog log)
        {
            await _timeLogRepository.UpdateTimeLogAsync(log);
        }

        public async Task DeleteTimeLogAsync(Guid id)
        {
            await _timeLogRepository.DeleteTimeLogAsync(id);
        }
    }
}
