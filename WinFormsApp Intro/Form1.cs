namespace WinFormsApp_Intro
{
    public partial class MyForm : Form
    {
        string name = "Tania";
        public MyForm()
        {
            InitializeComponent();
        }

        // �������� ��䳿 ������ "Hello"
        private void buttonHello_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"�����, {name}!", "���������", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
