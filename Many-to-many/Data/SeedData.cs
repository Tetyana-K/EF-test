using Many_to_many.Models;

namespace Many_to_many.Data
{
    public static class SeedData
    {
        public static bool SeedDatabase(StudentCourseDbContext context)
        {
            if (context.Students.Any() || context.Courses.Any())
                return false; // якщо база даних вже містить дані

            // створення студентів
            var student1 = new Student { Name = "Mark M.", BirthDate = new DateTime(2007, 1, 1) };
            var student2 = new Student { Name = "Nastia N.", BirthDate = new DateTime(2007, 2, 28) };
            var student3 = new Student { Name = "Danylo D.", BirthDate = new DateTime(2006, 3, 22) };

            // додавання студентів
            context.Students.AddRange(student1, student2, student3);

            // створення курсів
            var course1 = new Course 
            {
                Name = "Intro to Programming",
                Description = "Introduction to Programming (C++)",
                StartDate = new DateTime(2024, 2, 1),
                EndDate = new DateTime(2024, 12, 25) 
            };
            var course2 = new Course 
            {
                Name = "Intro to Web Development",
                Description = "Front-end and back-end web technologies: HTML, CSS",
                StartDate = new DateTime(2023, 2, 1),
                EndDate = new DateTime(2025, 6, 28) 
            };
            var course3 = new Course
            {
                Name = "Databases and SQL",
                Description = "Relational databases, SQL queries, EF Core.",
                StartDate = new DateTime(2025, 9, 1),
                EndDate = new DateTime(2025, 11, 30)
            };

            // додали курси
            context.Courses.AddRange(course1, course2);

            // зв'язування студентів з курсами
            student1.Courses.Add(course1);
            student1.Courses.Add(course2);
            
            student2.Courses.Add(course2);
            student2.Courses.Add(course3);

            student3.Courses.Add(course1);
            student3.Courses.Add(course2);
            student3.Courses.Add(course3);

            context.SaveChanges();
            return true;
        }
    }
}
