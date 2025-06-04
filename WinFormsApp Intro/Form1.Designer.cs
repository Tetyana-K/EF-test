namespace WinFormsApp_Intro
{
    partial class MyForm
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
            textBoxName = new TextBox();
            buttonHello = new Button();
            SuspendLayout();
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(122, 55);
            textBoxName.Margin = new Padding(4, 5, 4, 5);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(371, 29);
            textBoxName.TabIndex = 0;
            // 
            // buttonHello
            // 
            buttonHello.Location = new Point(122, 137);
            buttonHello.Name = "buttonHello";
            buttonHello.Size = new Size(371, 36);
            buttonHello.TabIndex = 1;
            buttonHello.Text = "Click me";
            buttonHello.UseVisualStyleBackColor = true;
            buttonHello.Click += buttonHello_Click;
            // 
            // MyForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(637, 379);
            Controls.Add(buttonHello);
            Controls.Add(textBoxName);
            Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4, 5, 4, 5);
            Name = "MyForm";
            Text = "Моя форма";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxName;
        private Button buttonHello;
    }
}
