-- Створення бази даних
CREATE DATABASE Shop25;
GO

-- Використання бази
USE Shop25;
GO

-- Таблиця міст
CREATE TABLE Cities (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50) NOT NULL UNIQUE
);

-- Таблиця постачальників
CREATE TABLE Suppliers (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    CityId INT FOREIGN KEY REFERENCES Cities(Id)
);

-- Таблиця категорій
CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50) NOT NULL UNIQUE
);

-- Таблиця продуктів
CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Price MONEY NOT NULL CHECK (Price > 0),
    CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
    SupplierId INT FOREIGN KEY REFERENCES Suppliers(Id)
);

-- Заповнення таблиці Cities
INSERT INTO Cities (Name) VALUES
('Kyiv'), --1
('Lviv'), --2
('Odesa'),
('Kharkiv');

INSERT INTO Cities (Name) VALUES
('Lutsk'); --id 5

-- Заповнення таблиці Suppliers
INSERT INTO Suppliers (Name, CityId) VALUES
('Fresh Fruits Ltd.', 1),
('Meat & More', 2),
('Green Veggies Co.', 3),
('Seafood Market', 4),
('Local Farmer Group', NULL); --5  Постачальник без міста, також це постачальник, у якого не буде продуктів

INSERT INTO Suppliers (Name, CityId) VALUES
('Fresh Meat Ltd.', 1); -- 6 постачальник, у якого не буде продуктів

-- Заповнення таблиці Categories
INSERT INTO Categories (Name) VALUES
('Fruits'),
('Vegetables'),
('Meat'),
('Seafood'),
('Bakery'); -- 5

INSERT INTO Categories (Name) VALUES
('Drinks'); --  категорія без товарів

-- Заповнення таблиці Products
INSERT INTO Products (Name, Price, CategoryId, SupplierId) VALUES
('Apple', 1.50, 1, 1),
('Banana', 1.20, 1, 1),
('Carrot', 0.80, 2, 3),
('Beef Steak', 8.00, 3, 2),
('Bread', 1.00, 5, NULL),          -- Продукт без постачальника
('Fish Fillet', 5.50, 4, 4),
('Tomato', 1.30, 2, 3),
('Lettuce', 1.10, 2, NULL);        -- Ще один без постачальника

INSERT INTO Products (Name, Price, CategoryId, SupplierId) VALUES
('Orange', 1.40, 1, 1),
('Cucumber', 1.00, 2, 3),
('Chicken Breast', 6.20, 3, 2),
('Shrimp', 7.80, 4, 4),
('Baguette', 1.20, 5, NULL),
('Pear', 1.60, 1, 1),
('Pumpkin', 2.30, 2, 3),
('Lamb Chops', 9.00, 3, 2),
('Salmon', 10.50, 4, 4),
('Croissant', 1.50, 5, NULL);