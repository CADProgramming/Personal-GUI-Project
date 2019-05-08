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
    public partial class ComputerGame : Form
    {
        //Global Variables

        Random rand = new Random();

        int[] dice = new int[5];
        int[] backupDice = new int[5];
        int[] UpperSection = new int[9];
        int[] LowerSection = new int[11];

        int rollCount = 1;
        int roundCount = 1;

        bool load = false;
        bool nullRound = false;

        public ComputerGame(bool NewGame, string SaveName)
        {
            //Initialization
            InitializeComponent();

            Label[] upperScoreLabels = { Pointlabel1, Pointlabel2, Pointlabel3, Pointlabel4, Pointlabel5, Pointlabel6, PointlabelTotal1, PointlabelBonus1, PointlabelTotal2 };
            Label[] lowerScoreLabels = { Label3oK, Label4oK, LabelFH, LabelSS, LabelLS, LabelYz, LabelChnc, LabelBnsYz, LabelTot1, LabelTot2, LabelGTot };

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 65;
            progressBar1.Step = 1;
            progressBar1.Value = 0;

            if ((NewGame == false) && (SaveName != ""))
            {
                load = true;

                string[] diceTemp = File.ReadLines(Directory.GetCurrentDirectory() + @"\Saves\" + SaveName + ".txt").Take(1).First().Split(',');
                for (int i = 0; i < dice.Length; i++)
                {
                    dice[i] = Convert.ToInt32(diceTemp[i]);
                }

                string[] backupDiceTemp = File.ReadLines(Directory.GetCurrentDirectory() + @"\Saves\" + SaveName + ".txt").Skip(1).Take(1).First().Split(',');
                for (int i = 0; i < backupDice.Length; i++)
                {
                    backupDice[i] = Convert.ToInt32(backupDiceTemp[i]);
                }

                string[] UpperSectionTemp = File.ReadLines(Directory.GetCurrentDirectory() + @"\Saves\" + SaveName + ".txt").Skip(2).Take(1).First().Split(',');
                for (int i = 0; i < UpperSection.Length; i++)
                {
                    UpperSection[i] = Convert.ToInt32(UpperSectionTemp[i]);
                }

                string[] LowerSectionTemp = File.ReadLines(Directory.GetCurrentDirectory() + @"\Saves\" + SaveName + ".txt").Skip(3).Take(1).First().Split(',');
                for (int i = 0; i < LowerSection.Length; i++)
                {
                    LowerSection[i] = Convert.ToInt32(LowerSectionTemp[i]);
                }

                rollCount = Convert.ToInt32(File.ReadLines(Directory.GetCurrentDirectory() + @"\Saves\" + SaveName + ".txt").Skip(4).Take(1).First());
                roundCount = Convert.ToInt32(File.ReadLines(Directory.GetCurrentDirectory() + @"\Saves\" + SaveName + ".txt").Skip(5).Take(1).First());

                DiceFrameUpdate();

                for (int i = 0; i < UpperSection.Length; i++)
                {
                    if (UpperSection[i] != -1)
                    {
                        upperScoreLabels[i].Text = Convert.ToString(UpperSection[i]);
                    }
                    else
                    {
                        upperScoreLabels[i].Text = "0";
                    }
                }

                for (int i = 0; i < LowerSection.Length; i++)
                {
                    if ((LowerSection[i] != -1) && (LowerSection[i] != 0))
                    {
                        lowerScoreLabels[i].Text = Convert.ToString(LowerSection[i]);
                    }
                    else if (LowerSection[i] == -1)
                    {
                        lowerScoreLabels[i].Text = "0";
                    }
                    else
                    {
                        lowerScoreLabels[i].Text = "X";
                    }
                }

                progressBar1.Value = UpperSection[6];

                load = false;
            }

            if (NewGame)
            {
                for (int i = 0; i < UpperSection.Length; i++)
                {
                    UpperSection[i] = -1;
                }
                for (int i = 0; i < LowerSection.Length; i++)
                {
                    LowerSection[i] = -1;
                }
            }
        }
        //Exit Confirmation Dialog
        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult choice = MessageBox.Show("Are you sure you want to Quit?\nAll unsaved progress will be lost", "Close Application", MessageBoxButtons.YesNo);
            if (choice == DialogResult.No)
            {
                e.Cancel = true;
                Activate();
            }
        }
        //Close App
        private void Game_FormClosed(object sender, FormClosedEventArgs c)
        {
            if (c.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
        //Roll Dice Button Clicked
        private void RollButton_Click(object sender, EventArgs e)
        {
            rollCount++;
        }

        private void DiceFrameUpdate()
        {
            for (int i = 0; i < dice.Length; i++)
            {
                if ((dice[i] == 0) || (load))
                {
                    if (!load)
                    {
                        dice[i] = rand.Next(1, 7);
                    }
                }
            }
        }

        private void NextRoundButton_Click(object sender, EventArgs e)
        {

        }

        private void EndRollButton_Click(object sender, EventArgs e)
        {

        }

        private void BonusCheck(int tempScore)
        {
            for (int i = 0; i < tempScore; i++)
            {
                if (progressBar1.Value < 65)
                {
                    progressBar1.PerformStep();
                }
            }
            if ((UpperSection[0] != -1) && (UpperSection[1] != -1) && (UpperSection[2] != -1) && (UpperSection[3] != -1) && (UpperSection[4] != -1) && (UpperSection[5] != -1))
            {
                if (UpperSection[6] >= 65)
                {
                    UpperSection[7] = 35;
                    PointlabelBonus1.Text = Convert.ToString(UpperSection[7]);
                }
                if (UpperSection[6] < 65)
                {
                    UpperSection[7] = 0;
                    PointlabelBonus1.Text = Convert.ToString(UpperSection[7]);
                }
                UpperSection[8] = (UpperSection[6] + UpperSection[7]);
                PointlabelTotal2.Text = Convert.ToString(UpperSection[8]);
            }
        }

        private void TotalUpdate(int scoreUp, int scoreLow)
        {
            if (UpperSection[6] == -1)
            {
                UpperSection[6] = 0;
            }
            if (UpperSection[8] == -1)
            {
                UpperSection[8] = 0;
            }
            if (LowerSection[9] == -1)
            {
                LowerSection[9] = 0;
            }
            if (scoreUp != -1)
            {
                UpperSection[6] += scoreUp;
            }
            if (scoreLow != -1)
            {
                LowerSection[9] += scoreLow;
            }
            if (UpperSection[7] <= 0)
            {
                UpperSection[8] = UpperSection[6];
                PointlabelTotal2.Text = Convert.ToString(UpperSection[8]);
            }
            LowerSection[10] = UpperSection[8] + LowerSection[9];
            LowerSection[8] = UpperSection[6];
            PointlabelTotal1.Text = Convert.ToString(UpperSection[6]);
            LabelTot1.Text = Convert.ToString(LowerSection[8]);
            LabelTot2.Text = Convert.ToString(LowerSection[9]);
            LabelGTot.Text = Convert.ToString(LowerSection[10]);
        }

        private void Button1s_Click(object sender, EventArgs e)
        {
            int temp = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                if (dice[i] == 1)
                {
                    temp += 1;
                }
            }
            UpperSection[0] = temp;
            Pointlabel1.Text = Convert.ToString(temp);
            TotalUpdate(UpperSection[0], 0);
            BonusCheck(temp);
            TotalUpdate(0, 0);
        }

        private void Button2s_Click(object sender, EventArgs e)
        {
            int temp = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                if (dice[i] == 2)
                {
                    temp += 2;
                }
            }
            UpperSection[1] = temp;
            Pointlabel2.Text = Convert.ToString(temp);
            TotalUpdate(UpperSection[1], 0);
            BonusCheck(temp);
            TotalUpdate(0, 0);
        }

        private void Button3s_Click(object sender, EventArgs e)
        {
            int temp = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                if (dice[i] == 3)
                {
                    temp += 3;
                }
            }
            UpperSection[2] = temp;
            Pointlabel3.Text = Convert.ToString(temp);
            TotalUpdate(UpperSection[2], 0);
            BonusCheck(temp);
            TotalUpdate(0, 0);
        }

        private void Button4s_Click(object sender, EventArgs e)
        {
            int temp = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                if (dice[i] == 4)
                {
                    temp += 4;
                }
            }
            UpperSection[3] = temp;
            Pointlabel4.Text = Convert.ToString(temp);
            TotalUpdate(UpperSection[3], 0);
            BonusCheck(temp);
            TotalUpdate(0, 0);
        }

        private void Button5s_Click(object sender, EventArgs e)
        {
            int temp = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                if (dice[i] == 5)
                {
                    temp += 5;
                }
            }
            UpperSection[4] = temp;
            Pointlabel5.Text = Convert.ToString(temp);
            TotalUpdate(UpperSection[4], 0);
            BonusCheck(temp);
            TotalUpdate(0, 0);
        }

        private void Button6s_Click(object sender, EventArgs e)
        {
            int temp = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                if (dice[i] == 6)
                {
                    temp += 6;
                }
            }
            UpperSection[5] = temp;
            Pointlabel6.Text = Convert.ToString(temp);
            TotalUpdate(UpperSection[5], 0);
            BonusCheck(temp);
            TotalUpdate(0, 0);
        }

        private void Button3oK_Click(object sender, EventArgs e)
        {
            int temp = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                temp += dice[i];
            }
            
            if (nullRound == false)
            {
                LowerSection[0] = temp;
                Label3oK.Text = Convert.ToString(temp);
            }
            else
            {
                LowerSection[0] = 0;
                Label3oK.Text = "X";
                nullRound = false;
            }
            TotalUpdate(0, LowerSection[0]);
        }

        private void Button4oK_Click(object sender, EventArgs e)
        {
            int temp = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                temp += dice[i];
            }
            
            if (nullRound == false)
            {
                LowerSection[1] = temp;
                Label4oK.Text = Convert.ToString(temp);
            }
            else
            {
                LowerSection[1] = 0;
                Label4oK.Text = "X";
                nullRound = false;
            }
            TotalUpdate(0, LowerSection[1]);
        }

        private void ButtonFH_Click(object sender, EventArgs e)
        {
            
            if (nullRound == false)
            {
                LowerSection[2] = 25;
                LabelFH.Text = "25";
            }
            else
            {
                LowerSection[2] = 0;
                LabelFH.Text = "X";
                nullRound = false;
            }
            TotalUpdate(0, LowerSection[2]);
        }

        private void ButtonSS_Click(object sender, EventArgs e)
        {
            
            if (nullRound == false)
            {
                LowerSection[3] = 30;
                LabelSS.Text = "30";
            }
            else
            {
                LowerSection[3] = 0;
                LabelSS.Text = "X";
                nullRound = false;
            }
            TotalUpdate(0, LowerSection[3]);
        }

        private void ButtonLS_Click(object sender, EventArgs e)
        {
            
            if (nullRound == false)
            {
                LowerSection[4] = 40;
                LabelLS.Text = "40";
            }
            else
            {
                LowerSection[4] = 0;
                LabelLS.Text = "X";
                nullRound = false;
            }
            TotalUpdate(0, LowerSection[4]);
        }

        private void ButtonYz_Click(object sender, EventArgs e)
        {
            
            if (nullRound == false)
            {
                LowerSection[5] = 50;
                LabelYz.Text = "50";
            }
            else
            {
                LowerSection[5] = 0;
                LabelYz.Text = "X";
                nullRound = false;
            }
            TotalUpdate(0, LowerSection[5]);
        }

        private void ButtonChnc_Click(object sender, EventArgs e)
        {
            int temp = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                temp += dice[i];
            }
            
            if (nullRound == false)
            {
                LowerSection[6] = temp;
                LabelChnc.Text = Convert.ToString(temp);
            }
            else
            {
                LowerSection[6] = 0;
                LabelChnc.Text = "X";
                nullRound = false;
            }
            TotalUpdate(0, LowerSection[6]);
        }

        private void ButtonBnsYz_Click(object sender, EventArgs e)
        {
            
            if (nullRound == false)
            {
                LowerSection[7] = 100;
                LabelBnsYz.Text = "100";
            }
            else
            {
                LowerSection[7] = 0;
                LabelBnsYz.Text = "X";
                nullRound = false;
            }
            TotalUpdate(0, LowerSection[7]);
        }

        private void ButtonMenu_Click(object sender, EventArgs e)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + @"\Saves\temp.txt"))
            {
                File.Delete(Directory.GetCurrentDirectory() + @"\Saves\temp.txt");
            }
            else
            {
                File.Create(Directory.GetCurrentDirectory() + @"\Saves\temp.txt").Close();
            }

            using (StreamWriter sw = File.AppendText(Directory.GetCurrentDirectory() + @"\Saves\temp.txt"))
            {
                for (int i = 0; i < dice.Length; i++)
                {
                    sw.Write(dice[i] + ",");
                }
                sw.WriteLine();
                for (int i = 0; i < backupDice.Length; i++)
                {
                    sw.Write(backupDice[i] + ",");
                }
                sw.WriteLine();
                for (int i = 0; i < UpperSection.Length; i++)
                {
                    sw.Write(UpperSection[i] + ",");
                }
                sw.WriteLine();
                for (int i = 0; i < LowerSection.Length; i++)
                {
                    sw.Write(LowerSection[i] + ",");
                }
                sw.WriteLine();
                sw.WriteLine(rollCount);
                sw.WriteLine(roundCount);
                sw.WriteLine(nullRound);
                sw.Close();
            }
            Form Menu = new Menu();
            Menu.Show();
            Point topLeftCorner = new Point(Location.X + ((1190 / 2) - (200 / 2)), Location.Y + ((1000 / 2) - (249 / 2)));
            Menu.Location = topLeftCorner;
        }
    }
}
