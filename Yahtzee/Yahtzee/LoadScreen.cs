using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee
{
    public partial class LoadScreen : Form
    {
        bool NewGame = true;
        string SaveName = "";
        int saves = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Saves\").Count();

        public LoadScreen()
        {
            InitializeComponent();
            LoadGUIRefresh();
        }

        private void LoadGUIRefresh()
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
                if ((fileName != "temp") && (saves > 0))
                {
                    LoadButton.Enabled = true;
                    comboBox1.Enabled = true;
                    if (comboBox1.Items.Contains(fileName) == false)
                    {
                        comboBox1.Items.Add(fileName);
                        comboBox1.Update();
                        DeleteButton.Enabled = true;
                    }
                }
                else if (saves == 0)
                {
                    LoadButton.Enabled = false;
                    comboBox1.Enabled = false;
                    DeleteButton.Enabled = false;
                }
            }
        }

        private void LoadScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.RefToMainMenu.Show();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                NewGame = false;
                SaveName = comboBox1.Text;
                Form Game = new Game(NewGame, SaveName);
                Game.Show();
                Point topLeftCorner = new Point(Location.X + (900 / 2) - (1190 / 2), Location.Y + (852 / 2) - (1000 / 2));
                Game.Location = topLeftCorner;
                Hide();
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
                    LoadGUIRefresh();
                }
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
