using Many_to_many__junc_table_Student_Project_.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Many_to_many__junc_table_Student_Project_.Data
{
    public class StudentProjectDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<StudentProject> StudentProjects { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use SQL Server LocalDB
            optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EF_StudentProjectDb;Trusted_Connection=True;");
            //.LogTo(Console.WriteLine, LogLevel.Information); // буде виводити  SQL-запити у консоль
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // визначаємо складений первинний ключ (на двох полях) для таблиці Student 
            // ключ складається з двох зовнішніх ключів: StudentId і ProjectId
            // це забезпечує унікальність кожної пари студент-проєкт (тобто один студент не може мати дві оцінки за один і той самий проєкт)
            modelBuilder.Entity<StudentProject>()
                .HasKey(sp => new { sp.StudentId, sp.ProjectId });

            // визначаємо зв'язки між таблицями Student, Project та StudentProject
            // зв'язок між Student та StudentProject -  зв'язок "один до багатьох" між Student та StudentProject
            // один студент може мати багато записів StudentProject (тобто бути учасником багатьох проєктів)
            // Кожен запис StudentProject має одного конкретного студента

            modelBuilder.Entity<StudentProject>()
                .HasOne(sp => sp.Student)
                .WithMany(s => s.StudentProjects)
                .HasForeignKey(sp => sp.StudentId);

            modelBuilder.Entity<StudentProject>()
                .HasOne(sp => sp.Project)
                .WithMany(p => p.StudentProjects)
                .HasForeignKey(sp => sp.ProjectId);
        }
    }
}
