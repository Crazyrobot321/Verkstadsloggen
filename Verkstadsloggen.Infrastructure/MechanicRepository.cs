using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Infrastructure.Data;
using Verkstadsloggen.Infrastructure.Interface;

namespace Verkstadsloggen.Infrastructure
{
    public class MechanicRepository : IMechanicRepository
    {
        private readonly MyDbContext _myDbContext;

        public MechanicRepository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<List<Mechanic>> GetAllMechanicsAsync()
        {
            return await _myDbContext.Set<Mechanic>().OrderBy(m => m.FirstName).ToListAsync();
        }
    }
}
