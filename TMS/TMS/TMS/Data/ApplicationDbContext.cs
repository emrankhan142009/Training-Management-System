using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMS.Models;

namespace TMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<TMS.Models.Batch> Batch { get; set; }

        public DbSet<TMS.Models.Coordinator> Coordinator { get; set; }

        public DbSet<TMS.Models.Course> Course { get; set; }

        public DbSet<TMS.Models.Instructor> Instructor { get; set; }

        public DbSet<TMS.Models.Performance> Performance { get; set; }

        public DbSet<TMS.Models.Progress> Progress { get; set; }

        public DbSet<TMS.Models.Salary> Salary { get; set; }

        public DbSet<TMS.Models.Tasks> Tasks { get; set; }

        public DbSet<TMS.Models.Trainee> Trainee { get; set; }
    }
}
