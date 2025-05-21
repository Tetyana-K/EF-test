using EF_test.Data;
using EF_test.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WinAppUsers
{
    public partial class Form1 : Form
    {
        private readonly AppDbContext _db = new();
        public Form1()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            _db.Users.Add(new User { Name = "Ihor", Email = "ihor@gmail.com" });
            _db.Users.Add(new User { Name = "Illia", Email = "illia@gmail.com" });
            _db.SaveChanges();
            var users = _db.Users.ToList();
            dataGridView1.DataSource = users;

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string email = textBoxEmail.Text;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))
            {
                var user = new User { Name = name, Email = email };
                _db.Users.Add(user);
                _db.SaveChanges();
                dataGridView1.DataSource = null; // Clear the current data source
                dataGridView1.DataSource = _db.Users.ToList(); // Refresh the data source
            }
            else
            {
                MessageBox.Show("Please enter a name and email.");
            }
        }
    }
}
