// See https://aka.ms/new-console-template for more information
using Many_to_many.Data;
using Many_to_many.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

Console.WriteLine("__________Many-to-Many Example (students and courses)____\n");

using var db = new StudentCourseDbContext();
db.Database.EnsureDeleted();
db.Database.EnsureCreated();

SeedData.SeedDatabase(db);
Console.WriteLine("Database seeded with initial data.\n");

var students = db.Students.Include(s => s.Courses).ToList();
Console.WriteLine("__________Students and their courses:");
foreach (var student in students)
{
    Console.WriteLine($"Student: {student.Name}, BirthDate: {student.BirthDate.ToShortDateString()}");
    foreach (var course in student.Courses)
    {
        Console.WriteLine($"  - Course: {course.Name}, Description: {course.Description}");
    }
}
Console.WriteLine("\n_________Trying to a course with validation errors (end date before start date, short name, ...)");
var badCourse = new Course
{
    Name = "Te_1", // name
    Description = "Invalid because EndDate < StartDate",
    StartDate = new DateTime(2025, 10, 1),
    EndDate = new DateTime(2025, 5, 1) // Помилка  задання лати
};

AddCourse(badCourse);

var mathCourse = new Course
{
    Name = "Mathematics", // name
    Description = "Mathematics (linear algebra)",
    StartDate = new DateTime(2025, 9, 1),
    EndDate = new DateTime(2025, 12, 22) 
};

AddCourse(mathCourse);

//Console.WriteLine("\n_________Delete course with Id = 1");
//DeleteCourse(1);
//PrintStudentsAndCourses();

//Console.WriteLine("\n_________Delete student with Id = 1");
//DeleteStudent(1);
//PrintStudentsAndCourses();

Console.WriteLine("\n_____Deleteing course with Id = 1 from student with Id = 2");
UnenrollStudentFromCourse (1, 2); // видаляємо курс з Id=2 у студента з Id=1
PrintStudentsAndCourses();

Console.WriteLine($"\n_____Ennrollment student  with Id = 1  to course with Id = 4");
EnrollStudentInCourse(1, 4);
PrintStudentsAndCourses();


void AddCourse(Course course)
{
    if (course == null)
    {
        Console.WriteLine("Course cannot be null.");
        return;
    }

    // створюємо контекст валідації
    var context = new ValidationContext(course);
    // створюємо список для зберігання результатів валідації
    var validationResults = new List<ValidationResult>();

    // перевіряємо чи  об'єкт валідний
    if (!Validator.TryValidateObject(course, context, validationResults, validateAllProperties: true))
    {
        // якщо хочемо потім побачити усі помилки валідації, якщо закоментувати рядки 56-61, то буде виведено лише першу помилку
        var iValidatable = course as IValidatableObject;
        // if (iValidatable != null)
        {
            var customResults = iValidatable.Validate(context);
            validationResults.AddRange(customResults);
        }
        // виводимо помилки валідації
        foreach (var validationResult in validationResults)
        {
            Console.WriteLine($"Validation error: {validationResult.ErrorMessage}");

        }
        Console.WriteLine($"Course '{course.Name}' not added.");
    }
    else
    {
        // інакше - все добре, пишемо курс у БД
        db.Courses.Add(course);
        db.SaveChanges();
    }
}

void DeleteCourse(int courseId)
{
    var course = db.Courses.FirstOrDefault(c => c.Id == courseId);
    if (course != null)
    {
        db.Courses.Remove(course);
        db.SaveChanges();
        Console.WriteLine($"Course '{course.Name}' deleted successfully.");
    }
    else
    {
        Console.WriteLine("Course not found.");
    }
}

void DeleteStudent(int studentId)
{

    var student = db.Students.FirstOrDefault(s => s.Id == studentId);
    if (student != null)
    {
        db.Students.Remove(student);
        db.SaveChanges();
        Console.WriteLine($"Student '{student.Name}' deleted successfully.");
    }
    else
    {
        Console.WriteLine("Student not found.");
    }
}

void UnenrollStudentFromCourse (int studentId, int courseId)
{
    // завантажуємо студента з його курсами
    var student = db.Students
                    .Include(s => s.Courses)
                    .FirstOrDefault(s => s.Id == studentId);

    if (student == null)
    {
        Console.WriteLine($"Student with Id={studentId} not found.");
        return;
    }

    // знаходимо цей курс у списку курсів студента
    var courseToRemove = student.Courses.FirstOrDefault(c => c.Id == courseId);

    if (courseToRemove == null)
    {
        Console.WriteLine($"Course with Id={courseId} not found for this student.");
        return;
    }

    // видаляємо зв’язок (курс зі списку курсів студента), тобто видаляємо курс зі списку курсів студента
    student.Courses.Remove(courseToRemove);

    db.SaveChanges();

    Console.WriteLine($"Course '{courseToRemove.Name}' removed from student '{student.Name}'.");
}

void EnrollStudentInCourse(int studentId, int courseId)
{
    var student = db.Students
                    .Include(s => s.Courses)
                    .FirstOrDefault(s => s.Id == studentId);

    if (student == null)
    {
        Console.WriteLine($"Student with Id={studentId} not found.");
        return;
    }

    var course = db.Courses.Find(courseId);// FirstOrDefault(c => c.Id == courseId);
    if (course == null)
    {
        Console.WriteLine($"Course with Id={courseId} not found.");
        return;
    }

    if (student.Courses.Any(c => c.Id == courseId))
    {
        Console.WriteLine($"Student '{student.Name}' is already enrolled in course '{course.Name}'.");
        return;
    }

    student.Courses.Add(course);
    db.SaveChanges();

    Console.WriteLine($"Student '{student.Name}' successfully enrolled in course '{course.Name}'.");
}


void PrintStudentsAndCourses()
{
    // очищаємо трекер змін, щоб уникнути помилок при повторному виконанні
    db.ChangeTracker.Clear();
    //  отримуємо актуальний список всіх студентів з їх курсами
    var students = db.Students.Include(s => s.Courses).ToList();

    Console.WriteLine("__________Students and their courses:");
    foreach (var student in students)
    {
        Console.WriteLine($"Student: {student.Name}, BirthDate: {student.BirthDate.ToShortDateString()}");
        foreach (var course in student.Courses)
        {
            Console.WriteLine($"  - Course: {course.Name}, Description: {course.Description}");
        }
    }
}


