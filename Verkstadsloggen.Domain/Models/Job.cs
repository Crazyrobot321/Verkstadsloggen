using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Verkstadsloggen.Domain.Enum;

namespace Verkstadsloggen.Domain.Models
{
    public class Job
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public JobStatus Status { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public string Notes { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string LicensePlate { get; set; } = string.Empty;

        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public Guid MechanicId { get; set; }
        public Mechanic? Mechanic { get; set; }

        public List<TimeLog> TimeLogs { get; set; } = new();

    }
}