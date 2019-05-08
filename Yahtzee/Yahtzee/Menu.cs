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

namespace Yahtzee
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + @"\Saves\temp.txt"))
            {
                File.Delete(Directory.GetCurrentDirectory() + @"\Saves\temp.txt");
            }
            Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Form SaveScreen = new SaveScreen();
            SaveScreen.Show();
            SaveScreen.Location = Location;
            Close();
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Program.RefToMainMenu.Close();
        }

        private void ButtonMainMenu_Click(object sender, EventArgs e)
        {
            Program.MainMenuPressed = true;
            Program.RefToGame.Close();
            if (Program.CancelCheck == false)
            {
                Program.RefToMainMenu.Show();
                Close();
            }
            else
            {
                Activate();
                Program.CancelCheck = false;
            }
        }
    }
}
