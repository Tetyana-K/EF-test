// ВИКОНАЛИ ПОПЕРЕДНЬО КОМАНДУ ДЛЯ СТВОРЕННЯ МОДЕЛЕЙ З БАЗИ ДАНИХ College ---> було автоматично створено моделі та контекст CollegeContext.cs
// dotnet ef dbcontext scaffold "Server=(localdb)\MSSQLLocalDB;Database=College;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models

using Database_First__college_.Models;
using Microsoft.EntityFrameworkCore;

/*Lazy Loading - це механізм, який дозволяє завантажувати навігаційні властивості (зв'язки між сутностями) лише тоді, коли до них звертаються вперше.
 Це дозволяє зменшити кількість запитів до бази даних і підвищити продуктивність, оскільки дані завантажуються лише тоді, коли вони дійсно потрібні.
 Але Lazy Loading може призвести до проблем з продуктивністю, якщо ви не контролюєте, коли і як завантажуються навігаційні властивості, оскільки це може призвести до великої кількості запитів до бази даних (N+1 проблема).

 Для використання Lazy Loading у EF Core потрібно:
 1. Встановити NuGet пакет Microsoft.EntityFrameworkCore.Proxies
 2. Використовувати метод UseLazyLoadingProxies() у методі OnConfiguring() класу DbContext
*/

Console.WriteLine("_______Database First____ (localdb College) and Lazy Loading");

var db = new CollegeContext(); // у контексті налаштовано LazyLoading, навігаційні властивості у моделіх позначені як віртуальні
// для Lazy Loading потрібно встановити NuGet пакет Microsoft.EntityFrameworkCore.Proxies
// dotnet add package Microsoft.EntityFrameworkCore.Proxies

var groups = db.Groups/*.Include(x=>x.Department)*/.ToList(); // якщо у нас Lazy Loading, то навігаційні властивості будуть завантажені АВТОМАТИЧНО при першому доступі до них
Console.WriteLine("Groups:");
foreach (var group in groups)
{
    // group.Department?.Name - можемо тягнути назву департаменту, бо маємо Lazy Loading, але якщо не буде департаменту, то отримаємо null
    Console.WriteLine($"{group.Id}: {group.Name} - {group.DepartmentId} {group.Department?.Name}"); // group.Department?.Name - ось тут спрацбовує автоматичне витягування назви групи через навігаційну властивість
}

var students = db.Students.ToList(); // не використовуємо Include(), бо маємо Lazy Loading
Console.WriteLine("\nStudents:");
foreach (var student in students)
{
    Console.WriteLine($"{student.Id,3}: {student.FirstName} {student.LastName}\t(GroupId: {student.GroupId}) {student.Group?.Name}");
}