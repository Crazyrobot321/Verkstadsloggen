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
    public void GetJobById_ReturnsCorrectJob()
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
        var job = jobs.FirstOrDefault(j => j.Id == job1.Id);

        // Assert
        Assert.NotNull(job);
        Assert.Equal(job1.Id, job.Id);
        Assert.Equal("Bromsbyte", job.Title);
        Assert.Equal("Byte av bromsar fram", job.Description);
        Assert.Equal(JobStatus.NotStarted, job.Status);
    }

    [Fact]
    public void GetJobById_WhenJobDoesNotExist_ReturnsNull()
    {
        // Arrange
        var job = new Job
        {
            Title = "Bromsbyte",
            Description = "Byte av bromsar fram",
            Status = JobStatus.NotStarted
        };
        var jobs = new List<Job> { job };
        var id = Guid.NewGuid();

        // Act
        var result = jobs.FirstOrDefault(j => j.Id == id);

        // Assert
        Assert.Null(result);
    }
}