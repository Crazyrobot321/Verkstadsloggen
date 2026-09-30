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

    }
}
