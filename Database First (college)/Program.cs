// See https://aka.ms/new-console-template for more information
// dotnet ef dbcontext scaffold "Server=(localdb)\MSSQLLocalDB;Database=College;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
using Database_First__college_.Models;

Console.WriteLine("_______Datbase First____ (localdb College)");

var db = new CollegeContext();

var groups = db.Groups.ToList();
fores