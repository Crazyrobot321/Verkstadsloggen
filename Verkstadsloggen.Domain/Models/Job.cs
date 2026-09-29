using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Verkstadsloggen.Domain.Models
{
    public class Job
    {
        public enum StatusEnum { NotStarted, InProgress, Completed }
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public StatusEnum Status { get; set; }

    }
}
