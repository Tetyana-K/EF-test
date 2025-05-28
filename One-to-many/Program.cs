// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using One_to_many.Data;
using One_to_many.Models;
using System.Linq;

Console.WriteLine("One-to-many relation (EF Core)");

using var db = new MenuDbContext();

db.Database.EnsureDeleted(); // видалити базу даних, якщо вона існує
db.Database.EnsureCreated(); // створити базу даних, якщо вона не існує

var hotMenu = new Menu
{
    Name = "Hot dishes",
    Dishes = new List<Dish>
    {
        new Dish { Name = "Borsch", Description = "Traditional Ukrainian soup", Price = 70.0m },
        new Dish { Name = "Varenyky", Description = "Dumplings with potatoes", Price = 90.0m },
        new Dish { Name = "Chicken Kiev", Description = "Breaded chicken with garlic butter", Price = 120.0m }
    }
};

var saladMenu = new Menu
{
    Name = "Salads",
    Dishes = new List<Dish>
    {
        new Dish { Name = "Olivier", Description = "Russian salad with vegetables and meat", Price = 50.0m },
        new Dish { Name = "Caesar", Description = "Salad with chicken and croutons", Price = 80.0m },
        new Dish { Name = "Greek", Description = "Salad with feta cheese and olives", Price = 60.0m }
    }
};

var desertMenu = new Menu
{
    Name = "Desserts",
    Dishes = new List<Dish>
    {
        new Dish { Name = "Cheesecake", Description = "Creamy cheesecake with berries", Price = 100.0m },
        new Dish { Name = "Apple Pie", Description = "Homemade apple pie with ice cream", Price = 80.0m },
        new Dish { Name = "Chocolate Mousse", Description = "Rich chocolate mousse", Price = 90.0m }
    }
};

db.Menus.Add(hotMenu);
db.Menus.Add(saladMenu);
db.Menus.Add(desertMenu);

db.SaveChanges();

Console.WriteLine("Menus and their dishes:");
foreach (var m in db.Menus.Include(d => d.Dishes)) //eager loading (жадібне завантаження) Include(d => d.Dishes) — завантажує навігаційну властивість страви
{
    PrintMenu(m);
}
Console.WriteLine("_____________________________________");

Console.WriteLine("Add new dish to menu #1 (Hot menu)");
AddDishToMenu(1, new Dish { Name = "Baked Potato", Description = "Potato baked with herbs", Price = 50.0m });
PrintMenu(hotMenu);
Console.WriteLine("_____________________________________");

Console.WriteLine("Remove  dish from menu #1 (Hot menu)");
RemoveDishFromMenu(1, 4); // видаляємо страву з Id 4 ()
PrintMenu(hotMenu);

Console.WriteLine("_____________________________________");
RemoveMenu(2); // видаляємо меню з Id 2 (Salads)
PrintMenuNames();
Console.WriteLine("_____________________________________");

Console.WriteLine("Create new menu 'Drinks'");
int drinkMenuId = CreateMenu("Drinks");
Console.WriteLine("_____________________________________");

Console.WriteLine("Print all menu names:");
PrintMenuNames();

AddDishToMenu(drinkMenuId, new Dish { Name = "Coca-Cola", Description = "Refreshing cola drink", Price = 30.0m });
PrintMenuById(drinkMenuId);
Console.WriteLine("____________________________________");

Console.WriteLine("Update dish price for 'Borsch' (Id 1) to 75.0");
UpdateDishPrice(1, 75.0m);
PrintDish(db.Dishes.Find(1));
Console.WriteLine("____________________________________");

Console.WriteLine("Update dish name, description and price for 'Varenyky' (Id 2)");
UpdateDishNamPriceDescription(2, "Varenyky with cheese", "Dumplings with cheese filling", 95.0m);
PrintDish(db.Dishes.Find(2));
Console.WriteLine("____________________________________");

Console.WriteLine("Update dish by for 'Chicken Kiev' (Id 3)");
UpdateDish(new Dish { Id = 3, Name = "Chicken Kiev with herbs", Description = "Breaded chicken with garlic and herbs", Price = 130.0m });
PrintDish(db.Dishes.Find(3));

void PrintMenuNames()
{
    Console.WriteLine("Menu names in resturant:");
    var menus = db.Menus.Select(m => m.Name);
    foreach (var menuName in menus)
    {
        Console.WriteLine($"\t-{menuName}");
    }
}

void PrintMenu(Menu menu)
{
    Console.WriteLine($">>>>> Menu: {menu.Name}");
    foreach (var dish in menu.Dishes)
    {
        PrintDish(dish);
    }
}

void PrintMenuById(int menuId)
{
    var menu =  db.Menus.Find(menuId);
    if (menu != null)
    {
        Console.WriteLine($">>>>> Menu: {menu.Name}");
        foreach (var dish in menu.Dishes)
        {
            PrintDish(dish);
        }
    }
    else
    {
        Console.WriteLine($"Menu with Id {menuId} not found");
    }
}
void PrintDish(Dish dish)
{
    Console.WriteLine($"\t{dish.Name} ({dish.Description}) {dish.Price}");
}

void AddDishToMenu(int menuId, Dish dish)
{
    var menu = db.Menus.Include(m => m.Dishes).FirstOrDefault(m => m.Id == menuId);
    if (menu == null)
    {
        Console.WriteLine($"Menu with Id {menuId} not found");
        return;
    }
    menu.Dishes.Add(dish); // додаємо страву до меню   через навігаційну властивість Dishes
}

void RemoveDishFromMenu(int menuId, int dishId)
{
    var menu = db.Menus.Include(m => m.Dishes).FirstOrDefault(m => m.Id == menuId);
    if (menu == null)
    {
        Console.WriteLine($"Menu with Id {menuId} not found");
        return;
    }
    var dish = menu.Dishes.FirstOrDefault(d => d.Id == dishId);
    //var dish = db.Dishes.Find(dishId); // шукаємо страву за Id
    if (dish == null)
    {
        Console.WriteLine($"Dish with Id {dishId} not found in Menu {menu.Name}");
        return;
    }
    menu.Dishes.Remove(dish);
    db.SaveChanges();
}

void RemoveMenu(int menuId)
{
    var menu = db.Menus.Find(menuId); // шукаємо меню за Id, Find - метод із DbSet, який шукає об'єкт за первинним ключем (Id)
    if (menu != null)
    {
        db.Menus.Remove(menu);
        db.SaveChanges();
        Console.WriteLine($"Menu with id: {menuId} is deleted");
    }
    else 
    {
        Console.WriteLine($"Not found menu with id : {menuId}"); 
    }
}

int CreateMenu(string menuName)
{
    var menu = new Menu { Name = menuName };
    db.Menus.Add(menu);
    db.SaveChanges();
    Console.WriteLine($"Menu '{menuName}' created with Id: {menu.Id}");
    return menu.Id;
}

void UpdateDishPrice(int dishId, decimal newPrice)
{
    var dish = db.Dishes.Find(dishId);
    if (dish != null)
    {
        dish.Price = newPrice;
        db.SaveChanges();
        Console.WriteLine($"Dish {dish.Name} price updated to {newPrice}");
    }
    else
    {
        Console.WriteLine($"Dish with Id {dishId} not found");
    }
}

void UpdateDishNamPriceDescription(int dishId, string newName, string newDescription,  decimal newPrice)
{
    var dish = db.Dishes.Find(dishId);
    if (dish != null)
    {
        dish.Name = newName;
        dish.Description = newDescription;
        dish.Price = newPrice;
        db.SaveChanges();
        Console.WriteLine($"Dish {dish.Id} updated: {dish.Name}, {dish.Description}, {dish.Price}");
    }
    else
    {
        Console.WriteLine($"Dish with Id {dishId} not found");
    }
}

void UpdateDish(Dish dish) // !!! поновлення повного обʼєкта на основі об'єкта dish, який містить Id існуючої страви
{
    var existingDish = db.Dishes.Find(dish.Id);
    if (existingDish != null)
    {
        existingDish.Name = dish.Name;
        existingDish.Description = dish.Description;
        existingDish.Price = dish.Price;
        db.SaveChanges();
        Console.WriteLine($"Dish {existingDish.Id} updated: {existingDish.Name}, {existingDish.Description}, {existingDish.Price}");
    }
    else
    {
        Console.WriteLine($"Dish with Id {dish.Id} not found");
    }
}

