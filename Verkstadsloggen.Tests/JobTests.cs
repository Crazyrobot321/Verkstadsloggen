
using Verkstadsloggen.Domain.Enum;
using Verkstadsloggen.Domain.Models;

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

    
}