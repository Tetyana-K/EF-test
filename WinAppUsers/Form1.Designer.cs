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
            buttonDelete = new Button();
            buttonSortByName = new Button();
            buttonSortById = new Button();
            textBoxFilterName = new TextBox();
            buttonFilterByName = new Button();
            buttonClearFilters = new Button();
            groupBox3 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(29, 21);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(272, 256);
            dataGridView1.TabIndex = 0;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBoxEmail);
            groupBox1.Controls.Add(textBoxName);
            groupBox1.Location = new Point(331, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(289, 137);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "User";
            // 
            // textBoxEmail
            // 
            textBoxEmail.Location = new Point(36, 82);
            textBoxEmail.Name = "textBoxEmail";
            textBoxEmail.Size = new Size(206, 23);
            textBoxEmail.TabIndex = 3;
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(36, 37);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(206, 23);
            textBoxName.TabIndex = 2;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(331, 171);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(75, 23);
            buttonAdd.TabIndex = 2;
            buttonAdd.Text = "Add User";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Location = new Point(438, 171);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(75, 23);
            buttonEdit.TabIndex = 3;
            buttonEdit.Text = "Edit User";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.ForeColor = Color.DarkRed;
            buttonDelete.Location = new Point(545, 171);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(75, 23);
            buttonDelete.TabIndex = 4;
            buttonDelete.Text = "Delete User";
            buttonDelete.TextAlign = ContentAlignment.MiddleRight;
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonSortByName
            // 
            buttonSortByName.Location = new Point(29, 302);
            buttonSortByName.Name = "buttonSortByName";
            buttonSortByName.Size = new Size(99, 23);
            buttonSortByName.TabIndex = 5;
            buttonSortByName.Text = "Sort By Name";
            buttonSortByName.TextAlign = ContentAlignment.MiddleRight;
            buttonSortByName.UseVisualStyleBackColor = true;
            buttonSortByName.Click += buttonSortByName_Click;
            // 
            // buttonSortById
            // 
            buttonSortById.Location = new Point(202, 302);
            buttonSortById.Name = "buttonSortById";
            buttonSortById.Size = new Size(99, 23);
            buttonSortById.TabIndex = 6;
            buttonSortById.Text = "Sort By Id";
            buttonSortById.UseVisualStyleBackColor = true;
            buttonSortById.Click += buttonSortById_Click;
            // 
            // textBoxFilterName
            // 
            textBoxFilterName.Location = new Point(36, 22);
            textBoxFilterName.Name = "textBoxFilterName";
            textBoxFilterName.Size = new Size(206, 23);
            textBoxFilterName.TabIndex = 7;
            // 
            // buttonFilterByName
            // 
            buttonFilterByName.Location = new Point(36, 70);
            buttonFilterByName.Name = "buttonFilterByName";
            buttonFilterByName.Size = new Size(99, 23);
            buttonFilterByName.TabIndex = 8;
            buttonFilterByName.Text = "Filter By Name";
            buttonFilterByName.TextAlign = ContentAlignment.MiddleRight;
            buttonFilterByName.UseVisualStyleBackColor = true;
            buttonFilterByName.Click += buttonFilterByName_Click;
            // 
            // buttonClearFilters
            // 
            buttonClearFilters.DialogResult = DialogResult.Cancel;
            buttonClearFilters.Location = new Point(143, 70);
            buttonClearFilters.Name = "buttonClearFilters";
            buttonClearFilters.Size = new Size(99, 23);
            buttonClearFilters.TabIndex = 9;
            buttonClearFilters.Text = "Clear filters";
            buttonClearFilters.UseVisualStyleBackColor = true;
            buttonClearFilters.Click += buttonClearFilters_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(textBoxFilterName);
            groupBox3.Controls.Add(buttonClearFilters);
            groupBox3.Controls.Add(buttonFilterByName);
            groupBox3.Location = new Point(340, 232);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(280, 118);
            groupBox3.TabIndex = 10;
            groupBox3.TabStop = false;
            groupBox3.Text = "Filter";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 362);
            Controls.Add(groupBox3);
            Controls.Add(buttonSortById);
            Controls.Add(buttonSortByName);
            Controls.Add(buttonDelete);
            Controls.Add(buttonEdit);
            Controls.Add(buttonAdd);
            Controls.Add(groupBox1);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Users (EfCore)";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private TextBox textBoxEmail;
        private TextBox textBoxName;
        private Button buttonAdd;
        private Button buttonEdit;
        private Button buttonDelete;
        private Button buttonSortByName;
        private Button buttonSortById;
        private TextBox textBoxFilterName;
        private Button buttonFilterByName;
        private Button buttonClearFilters;
        private GroupBox groupBox3;
    }
}
