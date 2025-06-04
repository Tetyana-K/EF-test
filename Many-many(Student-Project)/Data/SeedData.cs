using Many_to_many__junc_table_Student_Project_.Data;
using Many_to_many__junc_table_Student_Project_.Models;

namespace Many_many_Student_Project_.Data
{
    public static class SeedData
    {
        public static bool SeedDatabase(StudentProjectDbContext context)
        {
            // Якщо дані вже є — не додаємо
            if (context.Students.Any() || context.Projects.Any())
                return false;

            // Студенти
            var student1 = new Student { Name = "Olesia O.", };
            var student2 = new Student { Name = "Maria V.", };
            var student3 = new Student { Name = "Ihor C." };

            // Проєкти
            var project1 = new Project { Title = "AI Chatbot" };
            var project2 = new Project { Title = "Web API" };

            // Додаємо студентів і проєкти
            context.Students.AddRange(student1, student2, student3);
            context.Projects.AddRange(project1, project2);
            context.SaveChanges();

            // Проміжні записи з оцінками
            var studentProjects = new List<StudentProject>
            {
                new StudentProject { StudentId = student1.Id, ProjectId = project1.Id, Grade = 90 },
                new StudentProject { StudentId = student1.Id, ProjectId = project2.Id, Grade = 85 },
                new StudentProject { StudentId = student2.Id, ProjectId = project1.Id, Grade = 88 },
                new StudentProject { StudentId = student3.Id, ProjectId = project2.Id, Grade = 92 },
            };

            context.StudentProjects.AddRange(studentProjects);
            context.SaveChanges();

            return true; // База даних була успішно заповнена
        }
    }
}
