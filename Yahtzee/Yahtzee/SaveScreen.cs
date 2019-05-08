using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

//Make saves seperate files

namespace Yahtzee
{
    public partial class SaveScreen : Form
    {
        int saves = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Saves\").Count();
        public SaveScreen()
        {
            InitializeComponent();
            SaveUpdate();
        }

        private void SaveUpdate()
        {
            string[] saveGames = new string[21];
            int count = 0;
            saves = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Saves\").Count();
            foreach (string fileLocation in Directory.EnumerateFiles(Directory.GetCurrentDirectory() + @"\Saves\"))
            {
                string fileName = fileLocation.Substring(Directory.GetCurrentDirectory().Length + 7);
                fileName = fileName.Substring(0, fileName.Length - 4);
                if (saveGames.Contains(fileName) == false)
                {
                    saveGames[count] = fileName;
                    count++;
                }
                if ((fileName != "temp") && (saves > 1))
                {
                    OverWriteSaveButton.Enabled = true;
                    comboBox1.Enabled = true;
                    if (comboBox1.Items.Contains(fileName) == false)
                    {
                        comboBox1.Items.Add(fileName);
                        comboBox1.Update();
                        DeleteButton.Enabled = true;
                    }
                }
                else if (saves == 1)
                {
                    OverWriteSaveButton.Enabled = false;
                    comboBox1.Enabled = false;
                    DeleteButton.Enabled = false;
                }
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + @"\Saves\temp.txt"))
            {
                File.Delete(Directory.GetCurrentDirectory() + @"\Saves\temp.txt");
            }
            Close();
        }

        private void NewSaveButton_Click(object sender, EventArgs e)
        {
            if (saves > 0)
            {
                OverWriteSaveButton.Enabled = true;
                comboBox1.Enabled = true;
                DeleteButton.Enabled = true;
            }
            else if (saves == 0)
            {
                OverWriteSaveButton.Enabled = false;
                comboBox1.Enabled = false;
                DeleteButton.Enabled = false;
            }

            if (saves <= 20)
            {
                if ((textBox1.Text != "") && (File.Exists(Directory.GetCurrentDirectory() + @"\Saves\" + textBox1.Text + ".txt") == false))
                {
                    File.Create(Directory.GetCurrentDirectory() + @"\Saves\" + textBox1.Text + ".txt").Close();
                    File.Copy(Directory.GetCurrentDirectory() + @"\Saves\temp.txt", Directory.GetCurrentDirectory() + @"\Backups\temp.txt");
                    File.Replace(Directory.GetCurrentDirectory() + @"\Saves\temp.txt", Directory.GetCurrentDirectory() + @"\Saves\" + textBox1.Text + ".txt", null);
                    File.Move(Directory.GetCurrentDirectory() + @"\Backups\temp.txt", Directory.GetCurrentDirectory() + @"\Saves\temp.txt");
                    MessageBox.Show("File Saved", "Saved", MessageBoxButtons.OK);
                    textBox1.Clear();
                }
                else if (File.Exists(Directory.GetCurrentDirectory() + @"\Saves\" + textBox1.Text + ".txt"))
                {
                    MessageBox.Show("Save File with that name already exists\nPlease enter a different name", "Save Name Error", MessageBoxButtons.OK);
                }
            }
            SaveUpdate();
        }

        private void OverWriteSaveButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                DialogResult overWrite = MessageBox.Show($"Are you sure you want to Overwrite \"{comboBox1.Text}\"?\nThis cannot be undone", "Overwrite Save", MessageBoxButtons.OKCancel);
                if (overWrite == DialogResult.OK)
                {
                    File.Copy(Directory.GetCurrentDirectory() + @"\Saves\temp.txt", Directory.GetCurrentDirectory() + @"\temp.txt");
                    File.Replace(Directory.GetCurrentDirectory() + @"\Saves\temp.txt", Directory.GetCurrentDirectory() + @"\Saves\" + comboBox1.Text + ".txt", Directory.GetCurrentDirectory() + @"\Backups\" + comboBox1.Text + "Backup.txt");
                    File.Move(Directory.GetCurrentDirectory() + @"\temp.txt", Directory.GetCurrentDirectory() + @"\Saves\temp.txt");
                    MessageBox.Show("File Overwritten\nSave Complete", "Saved", MessageBoxButtons.OK);
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                DialogResult delete = MessageBox.Show($"Are you sure you want to Delete \"{comboBox1.Text}\"?\nThis cannot be undone", "Delete Save", MessageBoxButtons.OKCancel);
                if (delete == DialogResult.OK)
                {
                    File.Delete(Directory.GetCurrentDirectory() + @"\Saves\" + comboBox1.Text + ".txt");
                    comboBox1.Items.Remove(comboBox1.Text);
                    comboBox1.Update();
                    MessageBox.Show("File Deleted\nRemoval Complete", "Deleted", MessageBoxButtons.OK);
                    comboBox1.Text = "";
                    SaveUpdate();
                }
            }
        }
    }
}
