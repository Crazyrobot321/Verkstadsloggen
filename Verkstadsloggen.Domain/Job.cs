using System;
using System.Collections.Generic;
using System.Text;

namespace Verkstadsloggen.Domain
{
    public class Job
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }

    }
}
