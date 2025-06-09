
// Program.cs
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

var db = new UniversityContext();
db.Database.EnsureDeleted();
db.Database.EnsureCreated();
DbSeeder.Seed(db);

while (true)
{
    Console.WriteLine("\n=== Меню ===");
    Console.WriteLine("1. Вивести факультети");
    Console.WriteLine("2. Вивести групи з факультетами");
    Console.WriteLine("3. Вивести студентів");
    Console.WriteLine("4. Вивести студентів з групами і факультетами");
    Console.WriteLine("5. Пошук студентів за ім’ям");
    Console.WriteLine("6. Пошук студентів за роком вступу");
    Console.WriteLine("0. Вийти");
    Console.Write("Ваш вибір: ");

    var choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            db.Faculties.ToList().ForEach(f => Console.WriteLine(f.Name));
            break;
        case "2":
            foreach (var g in db.Groups.Include(g => g.Faculty))
                Console.WriteLine($"Група: {g.Name}, Факультет: {g.Faculty.Name}");
            break;
        case "3":
            db.Students.ToList().ForEach(s => Console.WriteLine(s.FullName));
            break;
        case "4":
            foreach (var s in db.Students.Include(s => s.Group).ThenInclude(g => g.Faculty))
                Console.WriteLine($"{s.FullName} ({s.EnrollmentYear}) - {s.Group.Name}, {s.Group.Faculty.Name}");
            break;
        case "5":
            Console.Write("Введіть частину імені: ");
            var part = Console.ReadLine();
            var byName = db.Students.Where(s => s.FullName.Contains(part)).ToList();
            byName.ForEach(s => Console.WriteLine(s.FullName));
            break;
        case "6":
            Console.Write("Введіть рік вступу: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                var byYear = db.Students.Where(s => s.EnrollmentYear == year).ToList();
                byYear.ForEach(s => Console.WriteLine(s.FullName));
            }
            break;
        case "0": return;
        default: Console.WriteLine("Невірна опція"); break;
    }
}

// Models.cs
public class Faculty
{
    public int Id { get; set; }
    [Required, StringLength(50, MinimumLength = 2)]
    public string Name { get; set; }
    public List<Group> Groups { get; set; } = new();
}

public class Group
{
    public int Id { get; set; }
    [Required, StringLength(20, MinimumLength = 2)]
    public string Name { get; set; }
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; }
    public List<Student> Students { get; set; } = new();
}

public class Student
{
    public int Id { get; set; }
    [Required, StringLength(100, MinimumLength = 5)]
    public string FullName { get; set; }
    [Range(2022, 2100)]
    public int EnrollmentYear { get; set; }
    public int GroupId { get; set; }
    public Group Group { get; set; }
}

// DbContext
public class UniversityContext : DbContext
{
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=university.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Faculty>()
            .HasMany(f => f.Groups)
            .WithOne(g => g.Faculty)
            .HasForeignKey(g => g.FacultyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Group>()
            .HasMany(g => g.Students)
            .WithOne(s => s.Group)
            .HasForeignKey(s => s.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

// DbSeeder
public static class DbSeeder
{
    public static void Seed(UniversityContext db)
    {
        if (db.Faculties.Any()) return;

        var faculty1 = new Faculty { Name = "IT" };
        var faculty2 = new Faculty { Name = "Economics" };

        var group1 = new Group { Name = "Dev-11", Faculty = faculty1 };
        var group2 = new Group { Name = "QA-21", Faculty = faculty1 };
        var group3 = new Group { Name = "Fin-31", Faculty = faculty2 };
        var group4 = new Group { Name = "Fin-32", Faculty = faculty2 };

        group1.Students.AddRange(new[]
        {
            new Student { FullName = "Іван Петренко", EnrollmentYear = 2023 },
            new Student { FullName = "Марія Коваленко", EnrollmentYear = 2024 },
            new Student { FullName = "Сергій Шевченко", EnrollmentYear = 2023 }
        });

        group2.Students.AddRange(new[]
        {
            new Student { FullName = "Олег Сидоренко", EnrollmentYear = 2024 },
            new Student { FullName = "Анна Лисенко", EnrollmentYear = 2022 },
            new Student { FullName = "Богдан Ющенко", EnrollmentYear = 2024 }
        });

        group3.Students.AddRange(new[]
        {
            new Student { FullName = "Андрій Іванов", EnrollmentYear = 2023 },
            new Student { FullName = "Оксана Бондар", EnrollmentYear = 2022 },
            new Student { FullName = "Максим Нечипорук", EnrollmentYear = 2023 }
        });

        group4.Students.AddRange(new[]
        {
            new Student { FullName = "Ірина Кравець", EnrollmentYear = 2023 },
            new Student { FullName = "Петро Луценко", EnrollmentYear = 2022 },
            new Student { FullName = "Дарина Мельник", EnrollmentYear = 2024 }
        });

        db.Faculties.AddRange(faculty1, faculty2);
        db.SaveChanges();
    }
}
