using Many_to_many.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Many_to_many.Data
{
    public class StudentCourseDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
              .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EF_StudentCourseDb;Trusted_Connection=True;");
            //.LogTo(Console.WriteLine, LogLevel.Information); // буде виводити  SQL-запити у консоль
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Student>()
            //    .HasMany(s => s.Courses)
            //    .WithMany(c => c.Students)
            //    .UsingEntity(e => e.ToTable("StudentCourses"));
            
        }
    }
    
}
