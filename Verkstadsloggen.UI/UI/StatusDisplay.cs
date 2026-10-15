using Verkstadsloggen.Domain.Enum;

namespace Verkstadsloggen.UI;

public static class StatusDisplay
{
    public static string Label(JobStatus status) => status switch
    {
        JobStatus.NotStarted => "Not Started",
        JobStatus.InProgress => "In Progress",
        JobStatus.Completed => "Completed",
        _ => status.ToString()
    };

    public static string Badge(JobStatus status) => status switch
    {
        JobStatus.NotStarted => "bg-secondary",
        JobStatus.InProgress => "bg-warning text-dark",
        JobStatus.Completed => "bg-success",
        _ => "bg-light text-dark"
    };
}