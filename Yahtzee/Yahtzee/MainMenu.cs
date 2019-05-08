using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee
{
    public partial class MainMenu : Form
    {
        bool NewGame = true;
        string SaveName = "";

        public MainMenu()
        {
            InitializeComponent();
            Program.RefToMainMenu = this;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.Fixed3D;
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            Form Game = new Game(NewGame, SaveName);
            Game.Show();
            Point topLeftCorner = new Point(Location.X + (900/2) - (1190/2), Location.Y + (852/2) - (1000/2));
            Game.Location = topLeftCorner;
            Hide();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            Form LoadScreen = new LoadScreen();
            Program.RefToMainMenu = this;
            LoadScreen.Show();
            LoadScreen.Location = Location;
            Hide();
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
