using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Verkstadsloggen.Domain.Models;

namespace Verkstadsloggen.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid? PersonId { get; set; }
        public Person? Person { get; set; }

    }
}
