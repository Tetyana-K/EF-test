namespace WinAppUsers
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            groupBox1 = new GroupBox();
            textBoxEmail = new TextBox();
            textBoxName = new TextBox();
            buttonAdd = new Button();
            buttonEdit = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(124, 30);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(335, 238);
            dataGridView1.TabIndex = 0;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBoxEmail);
            groupBox1.Controls.Add(textBoxName);
            groupBox1.Location = new Point(124, 292);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(212, 129);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "User";
            // 
            // textBoxEmail
            // 
            textBoxEmail.Location = new Point(52, 67);
            textBoxEmail.Name = "textBoxEmail";
            textBoxEmail.Size = new Size(100, 23);
            textBoxEmail.TabIndex = 3;
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(52, 22);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(100, 23);
            textBoxName.TabIndex = 2;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(384, 313);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(75, 23);
            buttonAdd.TabIndex = 2;
            buttonAdd.Text = "Add User";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Location = new Point(384, 359);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(75, 23);
            buttonEdit.TabIndex = 3;
            buttonEdit.Text = "Edit User";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(598, 450);
            Controls.Add(buttonEdit);
            Controls.Add(buttonAdd);
            Controls.Add(groupBox1);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private TextBox textBoxEmail;
        private TextBox textBoxName;
        private Button buttonAdd;
        private Button buttonEdit;
    }
}
