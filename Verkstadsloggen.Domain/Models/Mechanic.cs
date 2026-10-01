using System;
using System.Collections.Generic;
using System.Text;

namespace Verkstadsloggen.Domain.Models
{
    public class Mechanic : Person
    {
        public List<Job> AssignedJobs { get; set; } = new();

    }
}
