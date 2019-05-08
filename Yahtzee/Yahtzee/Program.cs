using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Yahtzee
{
    static class Program
    {
        public static Form RefToMainMenu { get; set; }
        public static Form RefToComputerPoints { get; set; }
        public static Form RefToGame { get; set; }
        public static bool MainMenuPressed { get; set; }
        public static bool CancelCheck { get; set; }
        public static bool ComputerPointsLoad { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MainMenuPressed = false;
            CancelCheck = false;
            ComputerPointsLoad = false;

            if (Directory.Exists(Directory.GetCurrentDirectory() + @"\Saves") == false)
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Saves");
            }
            if (Directory.Exists(Directory.GetCurrentDirectory() + @"\Backups") == false)
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Backups");
            }
            if (Directory.Exists(Directory.GetCurrentDirectory() + @"\AI Score") == false)
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\AI Score");
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenu());
        }
    }
}
