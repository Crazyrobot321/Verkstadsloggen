using Moq;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Application.Interfaces;
using Verkstadsloggen.Application.Interface;

namespace Verkstadsloggen.Tests;

public class TimeLogMockTests
{
    // Service som använder repository
    public class TimeLogService : ITimeLogService
    {
        private readonly ITimeLogRepository _repository;

        public TimeLogService(ITimeLogRepository repository)
        {
            _repository = repository;
        }

        public async Task AddTimeLogAsync(Guid jobId, DateTime start, DateTime end, string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return;

            var log = new TimeLog
            {
                JobId = jobId,
                StartTime = start,
                EndTime = end,
                Description = description
            };

            await _repository.AddTimeLogAsync(log);
        }

        public Task DeleteTimeLogAsync(Guid id)
        {
            return _repository.DeleteTimeLogAsync(id);
        }

        public Task<TimeLog?> GetLogByIdAsync(Guid id)
        {
            return _repository.GetLogByIdAsync(id);
        }

        public Task<List<TimeLog>> GetLogsForJobAsync(Guid jobId)
        {
            return _repository.GetLogsForJobAsync(jobId);
        }

        public Task UpdateTimeLogAsync(TimeLog log)
        {
            return _repository.UpdateTimeLogAsync(log);
        }
    }

    // ---------------------------------------------------------
    // 1. AddTimeLogAsync skickar korrekt data till repository
    // ---------------------------------------------------------

    [Fact]
    public async Task AddTimeLog_CallsRepositoryWithCorrectData()
    {
        var repoMock = new Mock<ITimeLogRepository>();
        var service = new TimeLogService(repoMock.Object);

        var jobId = Guid.NewGuid();
        var start = DateTime.UtcNow;
        var end = start.AddHours(1);

        TimeLog? savedLog = null;

        repoMock
            .Setup(r => r.AddTimeLogAsync(It.IsAny<TimeLog>()))
            .Callback<TimeLog>(log => savedLog = log)
            .Returns(Task.CompletedTask);

        await service.AddTimeLogAsync(jobId, start, end, "Bytte bromsar");

        Assert.NotNull(savedLog);
        Assert.Equal(jobId, savedLog.JobId);
        Assert.Equal(start, savedLog.StartTime);
        Assert.Equal(end, savedLog.EndTime);
        Assert.Equal("Bytte bromsar", savedLog.Description);
    }

    // ---------------------------------------------------------
    // 2. Tom beskrivning → ingen logg sparas
    // ---------------------------------------------------------

    [Fact]
    public async Task AddTimeLog_WithEmptyDescription_DoesNotSave()
    {
        var repoMock = new Mock<ITimeLogRepository>();
        var service = new TimeLogService(repoMock.Object);

        await service.AddTimeLogAsync(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, "   ");

        repoMock.Verify(r => r.AddTimeLogAsync(It.IsAny<TimeLog>()), Times.Never);
    }

    // ---------------------------------------------------------
    // 3. DeleteTimeLogAsync anropar repository korrekt
    // ---------------------------------------------------------

    [Fact]
    public async Task DeleteTimeLog_CallsRepository()
    {
        var repoMock = new Mock<ITimeLogRepository>();
        var service = new TimeLogService(repoMock.Object);

        var id = Guid.NewGuid();

        await service.DeleteTimeLogAsync(id);

        repoMock.Verify(r => r.DeleteTimeLogAsync(id), Times.Once);
    }
}

