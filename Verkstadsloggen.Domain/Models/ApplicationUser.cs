using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Verkstadsloggen.Domain.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid? PersonId { get; set; }
        public Person? Person { get; set; }

    }
}
