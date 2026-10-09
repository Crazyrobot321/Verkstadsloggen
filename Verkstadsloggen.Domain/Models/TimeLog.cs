using System;

namespace Verkstadsloggen.Domain.Models
{
    public class TimeLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid JobId { get; set; }
        public Job Job { get; set; } = null!;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
