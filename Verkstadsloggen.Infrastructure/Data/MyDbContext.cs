using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Verkstadsloggen.Domain.Models;

namespace Verkstadsloggen.Infrastructure.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }
    }
}
