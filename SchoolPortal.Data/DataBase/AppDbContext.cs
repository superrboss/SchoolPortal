using Microsoft.EntityFrameworkCore;
using SchoolPortal.Data.Interface;
using SchoolPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Instructor> Instructors => Set<Instructor>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Owned entities
            modelBuilder.Entity<Student>()
                .OwnsOne(s => s.Address);

            modelBuilder.Entity<Instructor>()
                .OwnsOne(i => i.OfficeAssignment);

            // Enums
            modelBuilder.Entity<Student>()
                .Property(s => s.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.Grade)
                .HasConversion<string>();
            modelBuilder.Entity<Student>()
           .Property(s => s.RowVersion)
           .IsRowVersion();

            modelBuilder.Entity<Course>()
          .Property(c => c.RowVersion)
          .IsRowVersion();

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<Department>()
                .Property(d => d.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<Instructor>()
                .Property(i => i.RowVersion)
                .IsRowVersion();
            // Soft Delete global filter
            modelBuilder.Entity<Student>().HasQueryFilter(s => !s.IsDeleted);
            modelBuilder.Entity<Course>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Enrollment>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Department>().HasQueryFilter(d => !d.IsDeleted);
            modelBuilder.Entity<Instructor>().HasQueryFilter(i => !i.IsDeleted);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable && (
                            e.State == EntityState.Added ||
                            e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var now = DateTime.UtcNow;
                var user = "System"; // Replace with actual user context if available

                if (entry.State == EntityState.Added)
                {
                    ((IAuditable)entry.Entity).CreatedAt = now;
                    ((IAuditable)entry.Entity).CreatedBy = user;
                }
                else
                {
                    ((IAuditable)entry.Entity).UpdatedAt = now;
                    ((IAuditable)entry.Entity).UpdatedBy = user;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
