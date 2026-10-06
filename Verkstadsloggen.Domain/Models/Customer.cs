using System;
using System.Collections.Generic;
using System.Text;

namespace Verkstadsloggen.Domain.Models
{
    public class Customer : Person
    {
        public string Address { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;

        public List<Job> Jobs { get; set; } = new();
    }
}