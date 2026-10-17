using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moq;
using Verkstadsloggen.Application.Interface;
using Verkstadsloggen.Application.Interfaces;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Infrastructure;
using Verkstadsloggen.Infrastructure.Data;
using Verkstadsloggen.Infrastructure.Repository;
using Verkstadsloggen.UI.Pages;

namespace Verkstadsloggen.Tests;

public class CommentTests
{
    [Fact]
    public async Task AddComment_WithValidText_SavesComment()
    {
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var context = new MyDbContext(options);

        var job = new Job { Title = "Bromsbyte", Description = "Byte av bromsar" };
        context.Jobs.Add(job);
        await context.SaveChangesAsync();

        var repository = new JobRepository(context);
        var timeLogServiceMock = new Mock<ITimeLogService>();

        var page = new JobDetails(repository, timeLogServiceMock.Object)
        {
            NewComment = "Kunden kontaktad"
        };

        var result = await page.OnPostAddCommentAsync(job.Id);

        Assert.IsType<RedirectToPageResult>(result);

        var savedComment = await context.Comments.SingleAsync();
        Assert.Equal("Kunden kontaktad", savedComment.Text);
        Assert.Equal(job.Id, savedComment.JobId);
    }

    [Fact]
    public async Task AddComment_WithWhitespace_DoesNotSaveComment()
    {
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var context = new MyDbContext(options);

        var job = new Job { Title = "Bromsbyte", Description = "Byte av bromsar" };
        context.Jobs.Add(job);
        await context.SaveChangesAsync();

        var repository = new JobRepository(context);
        var timeLogServiceMock = new Mock<ITimeLogService>();

        var page = new JobDetails(repository, timeLogServiceMock.Object)
        {
            NewComment = "   "
        };

        var result = await page.OnPostAddCommentAsync(job.Id);

        Assert.IsType<PageResult>(result);
        Assert.Empty(await context.Comments.ToListAsync());
    }

    [Fact]
    public async Task AddComment_WhenJobDoesNotExist_ReturnsNotFound()
    {
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var context = new MyDbContext(options);

        var repository = new JobRepository(context);
        var timeLogServiceMock = new Mock<ITimeLogService>();

        var page = new JobDetails(repository, timeLogServiceMock.Object)
        {
            NewComment = "Kunden kontaktad"
        };

        var jobId = Guid.NewGuid();

        var result = await page.OnPostAddCommentAsync(jobId);

        Assert.IsType<NotFoundResult>(result);
        Assert.Empty(await context.Comments.ToListAsync());
    }
}
