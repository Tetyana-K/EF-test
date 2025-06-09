using EF_test.Data;
using EF_test.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WinAppUsers
{
    public partial class Form1 : Form
    {
        private readonly AppDbContext db = new();
        public Form1()
        {
            InitializeComponent();
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;  
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells; // автоматичне налаштування ширини стовпців DataGridView під дані, які відображаються
            LoadUsers();
        }

        private void LoadUsers()
        {
            db.Users.Add(new User { Name = "Ihor", Email = "ihor@gmail.com" });
            db.Users.Add(new User { Name = "Illia", Email = "illia@gmail.com" });
            db.Users.Add(new User { Name = "Ann", Email = "anna@gmail.com" });
            db.Users.Add(new User { Name = "Maria", Email = "maria@gmail.com" });
            db.SaveChanges();

            var users = db.Users.ToList();
            dataGridView1.DataSource = users;

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string email = textBoxEmail.Text;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))
            {
                var user = new User { Name = name, Email = email };
                db.Users.Add(user);
                db.SaveChanges();

                // переналштовуємо джерело даних для DataGridView на нові дані
                dataGridView1.DataSource = null; // щоб очистити поточне джерело даних
                dataGridView1.DataSource = db.Users.ToList(); // налаштовуэи нове джерело даних
            }
            else
            {
                MessageBox.Show("Please enter a name and email.");
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            var index = dataGridView1.CurrentCell.RowIndex;
            if (index < 0)
            {
                MessageBox.Show("Please select a user to edit.");
                return;
            }
            var user = db.Users.ToList()[index];

            string name = textBoxName.Text;
            string email = textBoxEmail.Text;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))
            {
                user.Name = name;
                user.Email = email;
                db.SaveChanges();

                // переналаштовуємо джерело даних для DataGridView на нові дані

                dataGridView1.DataSource = null; // Clear the current data source
                dataGridView1.DataSource = db.Users.ToList(); // Refresh the data source
            }
            else
            {
                MessageBox.Show("Please enter a name and email.");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // перевіряємо, чи є вибрані рядки
            {
                var index = dataGridView1.SelectedRows[0].Index; // отримуємо індекс першого із вибраних рядків
                if (index >= 0 && index < db.Users.Count()) // перевіряємо, чи індекс в межах кількості користувачів
                {
                    var user = db.Users.ToList()[index]; // отримуємо користувача за індексом із контексту бази даних
                    textBoxName.Text = user.Name; // заповнюємо текстове поле іменем користувача
                    textBoxEmail.Text = user.Email; // заповнюємо текстове поле електронною поштою користувача
                }
            }
            else
            {
                textBoxName.Clear();
                textBoxEmail.Clear();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var index = dataGridView1.CurrentCell.RowIndex;
            if (index < 0)
            {
                MessageBox.Show("Please select a user to delete.");
                return;
            }
            var user = db.Users.ToList()[index];
            db.Users.Remove(user); // видаляємо користувача з контексту бази даних
            db.SaveChanges(); // зберігаємо зміни в базі даних

            // переналаштовуємо джерело даних для DataGridView на нові дані
            dataGridView1.DataSource = null; // Clear the current data source
            dataGridView1.DataSource = db.Users.ToList(); // Refresh the data source

        }

        private void buttonSortByName_Click(object sender, EventArgs e)
        {

            var users = db.Users.OrderBy(u => u.Name).ToList(); // отримуємо список користувачів, сортуємо за іменем

            // переналаштовуємо джерело даних для DataGridView на список users (відсортований)
            //dataGridView1.DataSource = null; // Clear the current data source
            dataGridView1.DataSource = users; // поновили джерело даних для DataGridView
        }

        private void buttonSortById_Click(object sender, EventArgs e)
        {
            var users = db.Users.ToList(); // отримуємо список користувачів

            // переналаштовуємо джерело даних для DataGridView на нові дані
            //dataGridView1.DataSource = null; // очищаємо поточне джерело даних
            dataGridView1.DataSource = users; // поновили джерело даних для DataGridView
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var users
        }

        private void buttonFilterByName_Click(object sender, EventArgs e)
        {
            string name = textBoxFilterName.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a name to filter.");
                return;
            }
            var users = db.Users.Where(u => u.Name.StartsWith(name)).ToList(); // фільтруємо користувачів за іменем
            if (users.Count == 0)
            {
                MessageBox.Show("No users found with the specified name.");
                return;
            }
            // переналаштовуємо джерело даних для DataGridView на нові дані
            dataGridView1.DataSource = null; // очищаємо поточне джерело даних
            dataGridView1.DataSource = users; // поновлюємо джерело даних для DataGridView з відфільтрованими користувачами

        }

        private void buttonClearFilters_Click(object sender, EventArgs e)
        {
            var users = db.Users.ToList(); // отримуємо список всіх користувачів
            // переналаштовуємо джерело даних для DataGridView на нові дані
            //dataGridView1.DataSource = null; // очищаємо поточне джерело даних
            dataGridView1.DataSource = users; // поновлюємо джерело даних для DataGridView з усіма користувачами
            textBoxFilterName.Clear(); // очищаємо текстове поле фільтру
        }
    }
}

