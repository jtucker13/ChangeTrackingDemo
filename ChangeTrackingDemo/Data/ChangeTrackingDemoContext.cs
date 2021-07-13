using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChangeTrackingDemo.Models;

namespace ChangeTrackingDemo.Data
{
    public class ChangeTrackingDemoContext : DbContext
    {
        public ChangeTrackingDemoContext (DbContextOptions<ChangeTrackingDemoContext> options)
            : base(options)
        {
        }

        public DbSet<ChangeTrackingDemo.Models.Record> Record { get; set; }

        public DbSet<ChangeTrackingDemo.Models.Student> Student { get; set; }
    }
}
