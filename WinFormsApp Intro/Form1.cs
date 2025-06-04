namespace WinFormsApp_Intro
{
    public partial class MyForm : Form
    {
        string name = "Tania";
        public MyForm()
        {
            InitializeComponent();
        }

        // Обробник події кнопки "Hello"
        private void buttonHello_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Привіт, {name}!", "Привітання", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
