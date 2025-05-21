// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
Console.WriteLine("Hello from EF Core (Students)");

/*
 Створити клас для моделі ComputerGame
- Id
- Name
- Genre
- Price
- ReleaseDate
- Rating (0..10)  [Range(0, 10)]

Завантажити необхідні пакети для роботи з EF Core: Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Tools
- Створити клас для контексту бази даних (DbContext) GameDbContext, успадковуючи його від DbContext
 (DbSet<ComputerGame>)

- Створити консольний додаток, який підключається до бази даних SQL Server LocalDB (або MS SQl Server)
- Створити базу даних, якщо вона не існує
- Передбачити додвання нових комп'ютерних ігор в базу даних
- Передбачити видалення комп'ютерних ігор з бази даних  (Remove(....))
- Передбачити редагування комп'ютерних ігор в базі даних (Update(....))

 */
