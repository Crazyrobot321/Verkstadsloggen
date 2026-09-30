
using Microsoft.EntityFrameworkCore;
using Verkstadsloggen.Domain.Enum;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Infrastructure;
using Verkstadsloggen.Infrastructure.Data;

namespace Verkstadsloggen.Tests;

public class JobTests
{
    [Fact]
    public void CreateJob_ReturnsCorrectInformation()
    {
        // Arrange
        var title = "Bromsbyte";
        var description = "Byte av bromsar fram";
        var status = "NotStarted";

        // Act
        var job = new Job
        {
            Title = title,
            Description = description,
            Status = JobStatus.NotStarted
        };

        // Assert
        Assert.Equal(title, job.Title);
        Assert.Equal(description, job.Description);
        Assert.NotEqual(Guid.Empty, job.Id);
        Assert.Equal(JobStatus.NotStarted, job.Status);
    }
    [Fact]
    public void ShowAllJobs_ReturnsCorrectInformation()
    {
        // Arrange
        var job1 = new Job
        {
            Title = "Bromsbyte",
            Description = "Byte av bromsar fram",
            Status = JobStatus.NotStarted
        };
        var job2 = new Job
        {
            Title = "Oljebyte",
            Description = "Byte av motorolja",
            Status = JobStatus.InProgress
        };
        var jobs = new List<Job> { job1, job2 };
        // Act
        var allJobs = jobs;
        // Assert
        Assert.Equal(2, allJobs.Count);
        Assert.Contains(job1, allJobs);
        Assert.Contains(job2, allJobs);
    }
    [Fact]
    public async Task GetJobsByStatusAsync_ReturnsOnlyMatchingJobs()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new MyDbContext(options);
        var repo = new JobRepository(context);

        var job1 = new Job { Title = "Bromsbyte", Description = "Byte av bromsar fram", Status = JobStatus.InProgress };
        var job2 = new Job { Title = "Oljebyte", Description = "Byte av motorolja", Status = JobStatus.Completed };
        var job3 = new Job { Title = "Däckbyte", Description = "Byte av vinterdäck", Status = JobStatus.InProgress };

        await repo.AddJobAsync(job1);
        await repo.AddJobAsync(job2);
        await repo.AddJobAsync(job3);

        // Act
        var result = await repo.GetJobsByStatusAsync(JobStatus.InProgress);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(job1, result);
        Assert.Contains(job3, result);
        Assert.DoesNotContain(job2, result);
    }


}