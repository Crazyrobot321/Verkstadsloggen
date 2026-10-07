
using Verkstadsloggen.Domain.Models;

namespace Verkstadloggen.Tests;

public class TimeLogTests
{
    [Fact]
    public void TimeLog_GetsId()
    {
        var log = new TimeLog();
        Assert.NotEqual(Guid.Empty, log.Id);
    }

    [Fact]
    public void TimeLog_DefaultDescriptionIsEmpty()
    {
        var log = new TimeLog();
        Assert.Equal(string.Empty, log.Description);
    }

    [Fact]
    public void TimeLog_CanSetDescription()
    {
        var log = new TimeLog { Description = "Bytte bromsar" };
        Assert.Equal("Bytte bromsar", log.Description);
    }

    [Fact]
    public void TimeLog_CanSetStartTime()
    {
        var time = DateTime.UtcNow;
        var log = new TimeLog { StartTime = time };
        Assert.Equal(time, log.StartTime);
    }

    [Fact]
    public void TimeLog_CanSetEndTime()
    {
        var time = DateTime.UtcNow.AddHours(1);
        var log = new TimeLog { EndTime = time };
        Assert.Equal(time, log.EndTime);
    }

    [Fact]
    public void TimeLog_IsLinkedToCorrectJob()
    {
        var job = new Job { Title = "Testjobb" };
        var log = new TimeLog
        {
            Job = job,
            JobId = job.Id
        };

        Assert.Equal(job.Id, log.JobId);
        Assert.Equal(job, log.Job);
    }
}
