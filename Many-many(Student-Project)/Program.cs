using Many_many_Student_Project_.Data;
using Many_to_many__junc_table_Student_Project_.Data;
using Many_to_many__junc_table_Student_Project_.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

Console.WriteLine("Many to many (explicit junction table)");

using var db = new StudentProjectDbContext();
db.Database.EnsureDeleted();
db.Database.EnsureCreated();

if (SeedData.SeedDatabase(db))
{
    Console.WriteLine("Database seeded successfully.");
}
else
{
    Console.WriteLine("Database already contains data.");
}


Console.WriteLine("\nAdding new students and projects...");
AddStudent("Sergii M.");
AddProject("Mobile App Development");

PrintStudents();
PrintProjects();

Console.WriteLine("\nAssigning students to projects with grades...");
AssignStudentProject(4, 2, 97); // Assigning Sergii M. to Web API with grade 97
PrintStudentsWithProjects();

Console.WriteLine("\nUpdating student project grade...");
UpdateStudentProjectGrade(4, 2, 100); // Updating Sergii M.'s grade for Web API to 100
PrintStudentsWithProjects();

RemoveStudent(4); // Removing Sergii M. from the database
PrintStudentsWithProjects();
PrintProjects();

void AddStudent(string name)
{
    var student = new Student { Name = name };
    db.Students.Add(student);
    db.SaveChanges();
    Console.WriteLine($"Added student: {name}");
}

void AddProject(string title)
{
    var project = new Project { Title = title };
    db.Projects.Add(project);
    db.SaveChanges();
    Console.WriteLine($"Added project: {title}");
}

void AssignStudentProject(int studentId, int projectId, int grade)
{
    var student = db.Students.Find(studentId);
    if (student == null)
    {
        Console.WriteLine($"Student with Id={studentId} not found.");
        return;
    }
    var project = db.Projects.Find(projectId);
    if (project == null)
    {
        Console.WriteLine($"Project with Id={projectId} not found.");
        return;
    }

    var existingAssignment = db.StudentProjects.Find(studentId, projectId);
    //.FirstOrDefault(sp => sp.StudentId == studentId && sp.ProjectId == projectId);
    if (existingAssignment != null)
    {
        Console.WriteLine($"StudentProject with StudentId={studentId} and ProjectId={projectId} already exists.");
        return;
    }

    var studentProject = new StudentProject
    {
        StudentId = studentId,
        ProjectId = projectId,
        Grade = grade
    };

    db.StudentProjects.Add(studentProject);
    db.SaveChanges();

    Console.WriteLine($"Added StudentProject: StudentId={studentId}, ProjectId={projectId}, Grade={grade}");
}

void PrintStudents()
{
    var students = db.Students.ToList();
    if (students.Count == 0)
    {
        Console.WriteLine("No students found");
        return;
    }

    Console.WriteLine("__________Students:");
    foreach (var student in students)
    {
        Console.WriteLine($"Id: {student.Id}, Name: {student.Name}");
    }
}

void PrintProjects()
{
    var projects = db.Projects.ToList();
    if (projects.Count == 0)
    {
        Console.WriteLine("No projects found");
        return;
    }

    Console.WriteLine("_______Projects:");
    foreach (var project in projects)
    {
        Console.WriteLine($"Id: {project.Id}, Title: {project.Title}");
    }
}

void PrintStudentsWithProjects()
{
    var students= db.Students
        .Include(s => s.StudentProjects)
        .ThenInclude(sp => sp.Project)
        .ToList();

    if (students.Count == 0)
    {
        Console.WriteLine("No students found.");
        return;
    }

    Console.WriteLine("_______Student-Project Assignments:");
    foreach (var s in students)
    {
        Console.WriteLine($"Student: {s.Name}");
        foreach(var sp in s.StudentProjects)
        {
            Console.WriteLine($"\tProject: {sp.Project.Title}, Grade: {sp.Grade}");
        }
    }
}

void UpdateStudentProjectGrade(int studentId, int projectId, int newGrade)
{
    var studentProject = db.StudentProjects
        .FirstOrDefault(sp => sp.StudentId == studentId && sp.ProjectId == projectId);

    if (studentProject == null)
    {
        Console.WriteLine($"StudentProject with StudentId={studentId} and ProjectId={projectId} not found");
        return;
    }

    studentProject.Grade = newGrade;
    db.SaveChanges();

    Console.WriteLine($"Updated grade for StudentProject: StudentId={studentId}, ProjectId={projectId}, New Grade={newGrade}");
}

void RemoveStudent(int studentId)
{
    var student = db.Students.Find(studentId);
    if (student == null)
    {
        Console.WriteLine($"Student with Id={studentId} not found.");
        return;
    }

    // видаляємо всі зв'язки студента з проєктами, але  у нас каскадене видалення, тому видалення студентських проєктів відбудеться автоматично
    //var studentProjects = db.StudentProjects.Where(sp => sp.StudentId == studentId).ToList();
    //db.StudentProjects.RemoveRange(studentProjects);

    // видаляємо студента
    db.Students.Remove(student);
    db.SaveChanges();

    Console.WriteLine($"Student '{student.Name}' removed successfully.");
}