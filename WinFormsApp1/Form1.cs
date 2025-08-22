using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
     

       
            CarDbContext _db = new();

            DataGridView dgv = new() { Dock = DockStyle.Top, Height = 250, AutoGenerateColumns = true };
            TextBox txtId = new() { ReadOnly = true, Width = 50 };
            TextBox txtBrand = new() { Width = 150 };
            TextBox txtModel = new() { Width = 150 };
            TextBox txtYear = new() { Width = 60 };
            TextBox txtColor = new() { Width = 100 };

            TextBox txtFilterBrand = new() { Width = 100, PlaceholderText = "Фільтр бренд" };
            TextBox txtFilterColor = new() { Width = 100, PlaceholderText = "Фільтр колір" };
            TextBox txtFilterYear = new() { Width = 60, PlaceholderText = "Фільтр рік" };

            Button btnAdd = new() { Text = "Додати" };
            Button btnUpdate = new() { Text = "Оновити" };
            Button btnDelete = new() { Text = "Видалити" };
            Button btnRefresh = new() { Text = "Оновити список" };
            Button btnSortBrand = new() { Text = "Сортувати за брендом" };
            Button btnFilterBrand = new() { Text = "Фільтр за брендом" };
            Button btnFilterColor = new() { Text = "Фільтр за кольором" };
            Button btnFilterYear = new() { Text = "Фільтр за роком" };
            Button btnClearFilters = new() { Text = "Скинути фільтри" };

            public Form1()
            {
                Text = "Car Manager";
                Width = 850;
                Height = 450;

                var panelInputs = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(5) };
                panelInputs.Controls.AddRange(new Control[]
                {
                new System.Windows.Forms.Label(){ Text="Id", Width=20, TextAlign=System.Drawing.ContentAlignment.MiddleRight }, txtId,
                new System.Windows.Forms.Label(){ Text="Brand", Width=40, TextAlign=System.Drawing.ContentAlignment.MiddleRight }, txtBrand,
                new System.Windows.Forms.Label(){ Text="Model", Width=40, TextAlign=System.Drawing.ContentAlignment.MiddleRight }, txtModel,
                new System.Windows.Forms.Label(){ Text="Year", Width=35, TextAlign=System.Drawing.ContentAlignment.MiddleRight }, txtYear,
                new System.Windows.Forms.Label(){ Text="Color", Width=40, TextAlign=System.Drawing.ContentAlignment.MiddleRight }, txtColor
                });

                var panelButtons = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 35, Padding = new Padding(5) };
                panelButtons.Controls.AddRange(new Control[] { btnAdd, btnUpdate, btnDelete, btnRefresh, btnSortBrand });

                var panelFilters = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 35, Padding = new Padding(5) };
                panelFilters.Controls.AddRange(new Control[]
                {
                txtFilterBrand, btnFilterBrand,
                txtFilterColor, btnFilterColor,
                txtFilterYear, btnFilterYear,
                btnClearFilters
                });

                Controls.AddRange(new Control[] { dgv, panelInputs, panelButtons, panelFilters });

                Load += (s, e) => { _db.Database.EnsureCreated(); SeedData(); LoadData(); };

                dgv.SelectionChanged += Dgv_SelectionChanged;

                btnAdd.Click += BtnAdd_Click;
                btnUpdate.Click += BtnUpdate_Click;
                btnDelete.Click += BtnDelete_Click;
                btnRefresh.Click += (s, e) => LoadData();
                btnSortBrand.Click += (s, e) =>
                    dgv.DataSource = _db.Cars.OrderBy(c => c.Brand).AsNoTracking().ToList();

                btnFilterBrand.Click += (s, e) =>
                    dgv.DataSource = _db.Cars.Where(c => c.Brand.Contains(txtFilterBrand.Text)).AsNoTracking().ToList();

                btnFilterColor.Click += (s, e) =>
                    dgv.DataSource = _db.Cars.Where(c => c.Color.Contains(txtFilterColor.Text)).AsNoTracking().ToList();

                btnFilterYear.Click += (s, e) =>
                {
                    if (int.TryParse(txtFilterYear.Text, out int y))
                        dgv.DataSource = _db.Cars.Where(c => c.Year == y).AsNoTracking().ToList();
                    else
                        MessageBox.Show("Невірний формат року");
                };

                btnClearFilters.Click += (s, e) =>
                {
                    txtFilterBrand.Text = "";
                    txtFilterColor.Text = "";
                    txtFilterYear.Text = "";
                    LoadData();
                };
            }

            void SeedData()
            {
                if (!_db.Cars.Any())
                {
                    _db.Cars.AddRange(new[]
                    {
                    new Car { Brand="Toyota", Model="Corolla", Year=2015, Color="White" },
                    new Car { Brand="Ford", Model="Focus", Year=2018, Color="Black" },
                    new Car { Brand="BMW", Model="X5", Year=2020, Color="Blue" }
                });
                    _db.SaveChanges();
                }
            }

            void LoadData()
            {
                dgv.DataSource = _db.Cars.AsNoTracking().ToList();
                ClearInputs();
            }

            void ClearInputs()
            {
                txtId.Text = "";
                txtBrand.Text = "";
                txtModel.Text = "";
                txtYear.Text = "";
                txtColor.Text = "";
            }

            void Dgv_SelectionChanged(object? sender, EventArgs e)
            {
                if (dgv.CurrentRow?.DataBoundItem is not Car car) return;
                txtId.Text = car.Id.ToString();
                txtBrand.Text = car.Brand;
                txtModel.Text = car.Model;
                txtYear.Text = car.Year.ToString();
                txtColor.Text = car.Color;
            }

            void BtnAdd_Click(object sender, EventArgs e)
            {
                if (!int.TryParse(txtYear.Text, out int year))
                {
                    MessageBox.Show("Невірний формат року");
                    return;
                }
                var car = new Car
                {
                    Brand = txtBrand.Text.Trim(),
                    Model = txtModel.Text.Trim(),
                    Year = year,
                    Color = txtColor.Text.Trim()
                };
                _db.Cars.Add(car);
                _db.SaveChanges();
                LoadData();
            }

            void BtnUpdate_Click(object sender, EventArgs e)
            {
                if (!int.TryParse(txtId.Text, out int id)) return;
                var car = _db.Cars.Find(id);
                if (car == null) return;
                if (!int.TryParse(txtYear.Text, out int year))
                {
                    MessageBox.Show("Невірний формат року");
                    return;
                }
                car.Brand = txtBrand.Text.Trim();
                car.Model = txtModel.Text.Trim();
                car.Year = year;
                car.Color = txtColor.Text.Trim();
                _db.SaveChanges();
                LoadData();
            }

            void BtnDelete_Click(object sender, EventArgs e)
            {
                if (!int.TryParse(txtId.Text, out int id)) return;
                var car = _db.Cars.Find(id);
                if (car == null) return;
                _db.Cars.Remove(car);
                _db.SaveChanges();
                LoadData();
            }
        }

    
}
public class Car
{
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Brand { get; set; } = null!;
    [Required, MaxLength(100)]
    public string Model { get; set; } = null!;
    public int Year { get; set; }
    [Required, MaxLength(50)]
    public string Color { get; set; } = null!;
}

public class CarDbContext : DbContext
{
    public DbSet<Car> Cars => Set<Car>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CarDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Brand).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Model).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Color).IsRequired().HasMaxLength(50);
        });
    }
}