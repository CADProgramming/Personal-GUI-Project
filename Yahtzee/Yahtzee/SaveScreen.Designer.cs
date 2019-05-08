namespace Yahtzee
{
    partial class SaveScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.OverWriteSaveButton = new System.Windows.Forms.Button();
            this.NewSaveButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SaveNewLabel = new System.Windows.Forms.Label();
            this.OverWriteSaveLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.BackButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33777F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33778F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66222F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66222F));
            this.tableLayoutPanel1.Controls.Add(this.OverWriteSaveButton, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.NewSaveButton, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.SaveNewLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.OverWriteSaveLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.TitleLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.BackButton, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.DeleteButton, 3, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // OverWriteSaveButton
            // 
            this.OverWriteSaveButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.OverWriteSaveButton.Location = new System.Drawing.Point(535, 274);
            this.OverWriteSaveButton.Name = "OverWriteSaveButton";
            this.OverWriteSaveButton.Size = new System.Drawing.Size(100, 28);
            this.OverWriteSaveButton.TabIndex = 1;
            this.OverWriteSaveButton.Text = "Save";
            this.OverWriteSaveButton.UseVisualStyleBackColor = true;
            this.OverWriteSaveButton.Click += new System.EventHandler(this.OverWriteSaveButton_Click);
            // 
            // NewSaveButton
            // 
            this.NewSaveButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.NewSaveButton.Location = new System.Drawing.Point(535, 146);
            this.NewSaveButton.Name = "NewSaveButton";
            this.NewSaveButton.Size = new System.Drawing.Size(100, 28);
            this.NewSaveButton.TabIndex = 2;
            this.NewSaveButton.Text = "Save";
            this.NewSaveButton.UseVisualStyleBackColor = true;
            this.NewSaveButton.Click += new System.EventHandler(this.NewSaveButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.textBox1.Location = new System.Drawing.Point(299, 147);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 26);
            this.textBox1.TabIndex = 3;
            // 
            // SaveNewLabel
            // 
            this.SaveNewLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SaveNewLabel.AutoSize = true;
            this.SaveNewLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.SaveNewLabel.Location = new System.Drawing.Point(166, 149);
            this.SaveNewLabel.Name = "SaveNewLabel";
            this.SaveNewLabel.Size = new System.Drawing.Size(97, 22);
            this.SaveNewLabel.TabIndex = 5;
            this.SaveNewLabel.Text = "Save New:";
            // 
            // OverWriteSaveLabel
            // 
            this.OverWriteSaveLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.OverWriteSaveLabel.AutoSize = true;
            this.OverWriteSaveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.OverWriteSaveLabel.Location = new System.Drawing.Point(139, 277);
            this.OverWriteSaveLabel.Name = "OverWriteSaveLabel";
            this.OverWriteSaveLabel.Size = new System.Drawing.Size(124, 22);
            this.OverWriteSaveLabel.TabIndex = 6;
            this.OverWriteSaveLabel.Text = "Save Existing:";
            // 
            // TitleLabel
            // 
            this.TitleLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.TitleLabel.Location = new System.Drawing.Point(373, 20);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(52, 24);
            this.TitleLabel.TabIndex = 7;
            this.TitleLabel.Text = "Save";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(299, 274);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(200, 28);
            this.comboBox1.TabIndex = 8;
            // 
            // BackButton
            // 
            this.BackButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BackButton.Location = new System.Drawing.Point(349, 403);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(100, 28);
            this.BackButton.TabIndex = 0;
            this.BackButton.Text = "Back";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DeleteButton.Location = new System.Drawing.Point(668, 274);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(100, 28);
            this.DeleteButton.TabIndex = 9;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // SaveScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SaveScreen";
            this.Text = "SaveScreen";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button OverWriteSaveButton;
        private System.Windows.Forms.Button NewSaveButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label SaveNewLabel;
        private System.Windows.Forms.Label OverWriteSaveLabel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button DeleteButton;
    }
}