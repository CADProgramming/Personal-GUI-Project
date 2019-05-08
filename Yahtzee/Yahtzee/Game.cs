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

//Save and Load Computer Scores
//After closing computer scores form clicking button causes crash
//Make windows spawn in the right positions using coordinates
//AI Bonus bar doesn't fill completely see testrun image
//End Menu
//Multiplayer???

namespace Yahtzee
{
    public partial class Game : Form
    {
        //Global Variables
       
        Random rand = new Random();

        int[] dice = new int[5];
        int[] backupDice = new int[5];
        int[] UpperSection = new int[9];
        int[] LowerSection = new int[11];

        int rollCount = 1;
        int roundCount = 1;

        bool nullRound = false;
        bool changeDice = true;
        bool load = false;
        bool scoreSet = false;

        Form computerPointsTable = new ComputerPointsTable();

        public Game(bool NewGame, string SaveName)
        {
            //Initialization
            InitializeComponent();

            Program.RefToGame = this;
            Point loc = new Point(Location.X + 1150, Location.Y);
            computerPointsTable.Location = loc;
            computerPointsTable.Hide();

            ButtonColours();

            Label[] upperScoreLabels = { Pointlabel1, Pointlabel2, Pointlabel3, Pointlabel4, Pointlabel5, Pointlabel6, PointlabelTotal1, PointlabelBonus1, PointlabelTotal2 };
            Label[] lowerScoreLabels = { Label3oK, Label4oK, LabelFH, LabelSS, LabelLS, LabelYz, LabelChnc, LabelBnsYz, LabelTot1, LabelTot2, LabelGTot };

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 65;
            progressBar1.Step = 1;
            progressBar1.Value = 0;

            if ((NewGame == false) && (SaveName != ""))
            {
                load = true;
                Program.ComputerPointsLoad = true;

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
                nullRound = Convert.ToBoolean(File.ReadLines(Directory.GetCurrentDirectory() + @"\Saves\" + SaveName + ".txt").Skip(6).Take(1).First());
                changeDice = Convert.ToBoolean(File.ReadLines(Directory.GetCurrentDirectory() + @"\Saves\" + SaveName + ".txt").Skip(7).Take(1).First());
                scoreSet = Convert.ToBoolean(File.ReadLines(Directory.GetCurrentDirectory() + @"\Saves\" + SaveName + ".txt").Skip(8).Take(1).First());

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

                if ((rollCount >= 4) && (scoreSet == false))
                {
                    ScoreButtons();
                }

                if (roundCount == 14)
                {
                    NextRoundButton.Text = "End Game";
                }

                if (UpperSection[6] < progressBar1.Maximum)
                {
                    if (UpperSection[6] != -1)
                    {
                        progressBar1.Value = UpperSection[6];
                    }
                    else
                    {
                        progressBar1.Value = 0;
                    }
                }
                else
                {
                    progressBar1.Value = progressBar1.Maximum;
                }

                load = false;
            }

            axWindowsMediaPlayer1.Ctlenabled = false;
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.Hide();

            if (rollCount == 1)
            {
                RollButton.Enabled = true;
                RollCountLabel.Text = $"Roll #{rollCount}";
            }
            else
            {
                RollButton.Enabled = false;
                RollCountLabel.Text = $"Roll #{rollCount - 1}";
            }

            RoundCountLabel.Text = $"Round #{roundCount}";

            if (load == false)
            {
                EndRollButton.Enabled = false;
            }
            else if (rollCount >= 4)
            {
                EndRollButton.Enabled = false;
            }
            else
            {
                EndRollButton.Enabled = true;
            }

            if (scoreSet)
            {
                NextRoundButton.Enabled = true;
            }
            else
            {
                NextRoundButton.Enabled = false;
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
                Program.CancelCheck = true;
                e.Cancel = true;
                Activate();
            }
        }
        //Close App
        private void Game_FormClosed(object sender, FormClosedEventArgs c)
        {
            Hide();
            if ((c.CloseReason == CloseReason.UserClosing) && (Program.MainMenuPressed == false))
            {
                Application.Exit();
            }
            else
            {
                Program.MainMenuPressed = false;
            }
        }
        //Roll Dice Button Clicked
        private void RollButton_Click(object sender, EventArgs e)
        {
            changeDice = false;
            ButtonMenu.Enabled = false;
            RollButton.Enabled = false;
            EndRollButton.Enabled = false;
            rollCount++;
            if (rollCount != 2)
            {
                RollCountLabel.Text = $"Roll #{rollCount-1}";
            }
            if (rollCount >= 4)
            {
                EndRollButton.Enabled = false;
            }
            //Plays Animation
            axWindowsMediaPlayer1.Show();
            axWindowsMediaPlayer1.settings.rate = 1;
            axWindowsMediaPlayer1.URL = (Directory.GetCurrentDirectory()+ @"\Resources\DiceRollFast.mp4");
        }

        private void ButtonColours()
        {
            Button[] upperButtons = { Button1s, Button2s, Button3s, Button4s, Button5s, Button6s };
            Button[] lowerButtons = { Button3oK, Button4oK, ButtonFH, ButtonSS, ButtonLS, ButtonYz, ButtonChnc, ButtonBnsYz };

            for (int i = 0; i < upperButtons.Length; i++)
            {
                if (upperButtons[i].Enabled == true)
                {
                    if (dice.Contains(i+1))
                    {
                        upperButtons[i].BackColor = Color.FromArgb(200, 255, 200);
                    }
                    else
                    {
                        upperButtons[i].BackColor = Color.FromArgb(255, 200, 150);
                    }
                }
                else
                {
                    upperButtons[i].BackColor = Color.FromArgb(255, 200, 200);
                }
            }

            foreach (Button button in lowerButtons)
            {
                if (button.Enabled == true)
                {
                    if (nullRound == false)
                    {
                        button.BackColor = Color.FromArgb(200, 255, 200);
                    }
                    else
                    {
                        button.BackColor = Color.FromArgb(255, 200, 150);
                    }
                }
                else
                {
                    button.BackColor = Color.FromArgb(255, 200, 200);
                }
            }
        }

        private void DiceFrameUpdate()
        {
            for (int i = 0; i < dice.Length; i++)
            {
                if ((dice[i] == 0) || (load))
                {
                    //Sets up dice values in array
                    //Shows dice values in GUI
                    if (!load)
                    {
                        dice[i] = rand.Next(1, 7);
                    }
                    if ((dice[i] != 0) || (!load))
                    {
                        switch (i + 1)
                        {
                            case 1:
                                pictureBox1.Load(Directory.GetCurrentDirectory() + @"\Resources\" + dice[i] + ".png");
                                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                break;
                            case 2:
                                pictureBox2.Load(Directory.GetCurrentDirectory() + @"\Resources\" + dice[i] + ".png");
                                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                                break;
                            case 3:
                                pictureBox3.Load(Directory.GetCurrentDirectory() + @"\Resources\" + dice[i] + ".png");
                                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                                break;
                            case 4:
                                pictureBox4.Load(Directory.GetCurrentDirectory() + @"\Resources\" + dice[i] + ".png");
                                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                                break;
                            case 5:
                                pictureBox5.Load(Directory.GetCurrentDirectory() + @"\Resources\" + dice[i] + ".png");
                                pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
                                break;
                        }
                    }
                    else if (backupDice[i] != 0)
                    {
                        switch (i + 1)
                        {
                            case 1:
                                pictureBox1.Load(Directory.GetCurrentDirectory() + @"\Resources\return.png");
                                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                break;
                            case 2:
                                pictureBox2.Load(Directory.GetCurrentDirectory() + @"\Resources\return.png");
                                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                                break;
                            case 3:
                                pictureBox3.Load(Directory.GetCurrentDirectory() + @"\Resources\return.png");
                                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                                break;
                            case 4:
                                pictureBox4.Load(Directory.GetCurrentDirectory() + @"\Resources\return.png");
                                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                                break;
                            case 5:
                                pictureBox5.Load(Directory.GetCurrentDirectory() + @"\Resources\return.png");
                                pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
                                break;
                        }
                    }
                }
            }
        }

        //When Video State Changes
        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if ((e.newState == 3) && (AnimationCheck.Checked == false))
            {
                axWindowsMediaPlayer1.URL = null;
                axWindowsMediaPlayer1.Hide();
            }
            //When video ends (8)
            if ((e.newState == 8) || (AnimationCheck.Checked == false))
            {
                changeDice = true;
                if (rollCount < 4)
                {
                    EndRollButton.Enabled = true;
                }
                //Hide the player display the dice in the array
                axWindowsMediaPlayer1.URL = null;
                axWindowsMediaPlayer1.Hide();

                DiceFrameUpdate();
                ButtonMenu.Enabled = true;

                if (rollCount >= 4)
                {
                    ScoreButtons();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if ((pictureBox1.ImageLocation != (Directory.GetCurrentDirectory() + @"\Resources\return.png")) && (pictureBox1.Image != null) && (changeDice) && (rollCount < 4))
            {
                backupDice[0] = dice[0];
                dice[0] = 0;
                pictureBox1.Image.Dispose();
                pictureBox1.Load(Directory.GetCurrentDirectory() + @"\Resources\return.png");
            }
            else if (pictureBox1.ImageLocation == (Directory.GetCurrentDirectory() + @"\Resources\return.png") && (changeDice))
            {
                dice[0] = backupDice[0];
                backupDice[0] = 0;
                pictureBox1.Image.Dispose();
                pictureBox1.Load(Directory.GetCurrentDirectory() + @"\Resources\" + dice[0] + ".png");
            }
            if (dice.Contains(0) && (changeDice) && (rollCount < 4))
            {
                RollButton.Enabled = true;
            }
            else
            {
                RollButton.Enabled = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if ((pictureBox2.ImageLocation != (Directory.GetCurrentDirectory() + @"\Resources\return.png")) && (pictureBox2.Image != null) && (changeDice) && (rollCount < 4))
            {
                backupDice[1] = dice[1];
                dice[1] = 0;
                pictureBox2.Image.Dispose();
                pictureBox2.Load(Directory.GetCurrentDirectory() + @"\Resources\return.png");
            }
            else if (pictureBox2.ImageLocation == (Directory.GetCurrentDirectory() + @"\Resources\return.png") && (changeDice))
            {
                dice[1] = backupDice[1];
                backupDice[1] = 0;
                pictureBox2.Image.Dispose();
                pictureBox2.Load(Directory.GetCurrentDirectory() + @"\Resources\" + dice[1] + ".png");
            }
            if (dice.Contains(0) && (changeDice) && (rollCount < 4))
            {
                RollButton.Enabled = true;
            }
            else
            {
                RollButton.Enabled = false;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if ((pictureBox3.ImageLocation != (Directory.GetCurrentDirectory() + @"\Resources\return.png")) && (pictureBox3.Image != null) && (changeDice) && (rollCount < 4))
            {
                backupDice[2] = dice[2];
                dice[2] = 0;
                pictureBox3.Image.Dispose();
                pictureBox3.Load(Directory.GetCurrentDirectory() + @"\Resources\return.png");
            }
            else if (pictureBox3.ImageLocation == (Directory.GetCurrentDirectory() + @"\Resources\return.png") && (changeDice))
            {
                dice[2] = backupDice[2];
                backupDice[2] = 0;
                pictureBox3.Image.Dispose();
                pictureBox3.Load(Directory.GetCurrentDirectory() + @"\Resources\" + dice[2] + ".png");
            }
            if (dice.Contains(0) && (changeDice) && (rollCount < 4))
            {
                RollButton.Enabled = true;
            }
            else
            {
                RollButton.Enabled = false;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if ((pictureBox4.ImageLocation != (Directory.GetCurrentDirectory() + @"\Resources\return.png")) && (pictureBox4.Image != null) && (changeDice) && (rollCount < 4))
            {
                backupDice[3] = dice[3];
                dice[3] = 0;
                pictureBox4.Image.Dispose();
                pictureBox4.Load(Directory.GetCurrentDirectory() + @"\Resources\return.png");
            }
            else if (pictureBox4.ImageLocation == (Directory.GetCurrentDirectory() + @"\Resources\return.png") && (changeDice))
            {
                dice[3] = backupDice[3];
                backupDice[3] = 0;
                pictureBox4.Image.Dispose();
                pictureBox4.Load(Directory.GetCurrentDirectory() + @"\Resources\" + dice[3] + ".png");
            }
            if (dice.Contains(0) && (changeDice) && (rollCount < 4))
            {
                RollButton.Enabled = true;
            }
            else
            {
                RollButton.Enabled = false;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if ((pictureBox5.ImageLocation != (Directory.GetCurrentDirectory() + @"\Resources\return.png")) && (pictureBox5.Image != null) && (changeDice) && (rollCount < 4))
            {
                backupDice[4] = dice[4];
                dice[4] = 0;
                pictureBox5.Image.Dispose();
                pictureBox5.Load(Directory.GetCurrentDirectory() + @"\Resources\return.png");
            }
            else if (pictureBox5.ImageLocation == (Directory.GetCurrentDirectory() + @"\Resources\return.png") && (changeDice))
            {
                dice[4] = backupDice[4];
                backupDice[4] = 0;
                pictureBox5.Image.Dispose();
                pictureBox5.Load(Directory.GetCurrentDirectory() + @"\Resources\" + dice[4] + ".png");
            }
            if (dice.Contains(0) && (changeDice) && (rollCount < 4))
            {
                RollButton.Enabled = true;
            }
            else
            {
                RollButton.Enabled = false;
            }
        }

        private void NextRoundButton_Click(object sender, EventArgs e)
        {
            ComputersTurn();
            ComputerDataTransfer();
            if (computerPointsTable.BackColor == Color.FromArgb(255, 255, 210))
            {
                computerPointsTable.BackColor = Color.FromArgb(255, 255, 211);
            }
            else
            {
                computerPointsTable.BackColor = Color.FromArgb(255, 255, 210);
            }
            nullRound = false;
            scoreSet = false;
            NextRoundButton.Enabled = false;
            if (roundCount == 13)
            {
                NextRoundButton.Text = "End Game";
            }
            if (roundCount == 14)
            {
                //End Game Code Here
            }
            //Resets rolls
            RollButton.Enabled = true;
            rollCount = 1;
            roundCount++;
            RollCountLabel.Text = $"Roll #{rollCount}";
            RoundCountLabel.Text = $"Round #{roundCount}";
            //Resets Dice Array
            for (int i = 0; i < dice.Length; i++)
            {
                dice[i] = 0;
            }
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            pictureBox5.Image = null;
        }

        private void EndRollButton_Click(object sender, EventArgs e)
        {
            rollCount = 4;
            RollButton.Enabled = false;
            EndRollButton.Enabled = false;
            RollCountLabel.Text = $"Roll #{rollCount-1}";
            ScoreButtons();

        }

        private void ScoreButtons()
        {
            int[] numCount = new int[6];

            if (UpperSection[0] == -1)
            {
                Button1s.Enabled = true;
            }
            if (UpperSection[1] == -1)
            {
                Button2s.Enabled = true;
            }
            if (UpperSection[2] == -1)
            {
                Button3s.Enabled = true;
            }
            if (UpperSection[3] == -1)
            {
                Button4s.Enabled = true;
            }
            if (UpperSection[4] == -1)
            {
                Button5s.Enabled = true;
            }
            if (UpperSection[5] == -1)
            {
                Button6s.Enabled = true;
            }
            if (LowerSection[6] == -1)
            {
                ButtonChnc.Enabled = true;
            }
            for (int i = 0; i < dice.Length; i++)
            {
                for (int n = 1; n <= numCount.Length; n++)
                {
                    if (dice[i] == n)
                    {
                        numCount[n-1]++;
                    }
                }
            }
            for (int i = 0; i < numCount.Length; i++)
            {
                if (numCount[i] == 5)
                {
                    if (LowerSection[5] == -1)
                    {
                        ButtonYz.Enabled = true;
                    }
                    else
                    {
                        ButtonBnsYz.Enabled = true;
                    }
                }
                if ((numCount[i] == 4) && (LowerSection[1] == -1))
                {
                    Button4oK.Enabled = true;
                }
                if ((numCount[i] == 3) && (LowerSection[0] == -1))
                {
                    Button3oK.Enabled = true;
                }
            }
            if (numCount.Contains(2) && numCount.Contains(3) && (LowerSection[2] == -1))
            {
                ButtonFH.Enabled = true;
            }
            if ((((numCount[0] >= 1)&&(numCount[1] >= 1)&&(numCount[2] >= 1)&&(numCount[3] >= 1)) || ((numCount[1] >= 1) && (numCount[2] >= 1) && (numCount[3] >= 1) && (numCount[4] >= 1)) || ((numCount[2] >= 1) && (numCount[3] >= 1) && (numCount[4] >= 1) && (numCount[5] >= 1))) && (LowerSection[3] == -1))
            {
                ButtonSS.Enabled = true;
            }
            if ((((numCount[0] == 1) && (numCount[1] == 1) && (numCount[2] == 1) && (numCount[3] == 1) && (numCount[4] == 1)) || ((numCount[1] == 1) && (numCount[2] == 1) && (numCount[3] == 1) && (numCount[4] == 1) && (numCount[5] == 1))) && (LowerSection[4] == -1))
            {
                ButtonLS.Enabled = true;
            }
            NoCategory();
            ButtonColours();
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
                LowerSection[8] = UpperSection[8];
                LabelTot1.Text = Convert.ToString(LowerSection[8]);
                TotalUpdate(0, 0);
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
                LowerSection[8] = UpperSection[6];
                LabelTot1.Text = Convert.ToString(LowerSection[8]);
                PointlabelTotal2.Text = Convert.ToString(UpperSection[8]);
            }
            else
            {
                LowerSection[8] = UpperSection[6] + UpperSection[7];
                LabelTot1.Text = Convert.ToString(LowerSection[8]);
            }
            LowerSection[10] = UpperSection[8] + LowerSection[9];
            PointlabelTotal1.Text = Convert.ToString(UpperSection[6]);
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
            NextRoundButton.Enabled = true;
            scoreSet = true;
            DisableButtons();
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
            NextRoundButton.Enabled = true;
            scoreSet = true;
            DisableButtons();
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
            NextRoundButton.Enabled = true;
            scoreSet = true;
            DisableButtons();
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
            NextRoundButton.Enabled = true;
            scoreSet = true;
            DisableButtons();
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
            NextRoundButton.Enabled = true;
            scoreSet = true;
            DisableButtons();
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
            NextRoundButton.Enabled = true;
            scoreSet = true;
            DisableButtons();
            TotalUpdate(UpperSection[5], 0);
            BonusCheck(temp);
            TotalUpdate(0, 0);
        }

        private void NoCategory()
        {
            Button[] buttonList = { Button3oK, Button4oK, ButtonFH, ButtonSS, ButtonLS, ButtonYz, ButtonChnc, ButtonBnsYz };
            int unSet = 0;
            int buttonCount = 0;

            if (roundCount != 15)
            {
                for (int i = 0; i < LowerSection.Length-3; i++)
                {
                    if (LowerSection[i] == -1)
                    {
                        unSet++;
                    }
                }
                foreach (Button button in buttonList)
                {
                    if (button.Enabled == true)
                    {
                        buttonCount++;
                    }
                }
                if ((unSet > 0) && (buttonCount == 0))
                {
                    for (int i = 0; i < buttonList.Length; i++)
                    {
                        if (LowerSection[i] == -1)
                        {
                            buttonList[i].Enabled = true;
                            nullRound = true;
                        }
                    }
                }
            }
        }

        private void Button3oK_Click(object sender, EventArgs e)
        {
            int temp = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                temp += dice[i];
            }
            DisableButtons();
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
            NextRoundButton.Enabled = true;
            scoreSet = true;
            TotalUpdate(0, LowerSection[0]);
        }

        private void Button4oK_Click(object sender, EventArgs e)
        {
            int temp = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                temp += dice[i];
            }
            DisableButtons();
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
            NextRoundButton.Enabled = true;
            scoreSet = true;
            TotalUpdate(0, LowerSection[1]);
        }

        private void ButtonFH_Click(object sender, EventArgs e)
        {
            DisableButtons();
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
            NextRoundButton.Enabled = true;
            scoreSet = true;
            TotalUpdate(0, LowerSection[2]);
        }

        private void ButtonSS_Click(object sender, EventArgs e)
        {
            DisableButtons();
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
            NextRoundButton.Enabled = true;
            scoreSet = true;
            TotalUpdate(0, LowerSection[3]);
        }

        private void ButtonLS_Click(object sender, EventArgs e)
        {
            DisableButtons();
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
            NextRoundButton.Enabled = true;
            scoreSet = true;
            TotalUpdate(0, LowerSection[4]);
        }

        private void ButtonYz_Click(object sender, EventArgs e)
        {
            DisableButtons();
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
            NextRoundButton.Enabled = true;
            scoreSet = true;
            TotalUpdate(0, LowerSection[5]);
        }

        private void ButtonChnc_Click(object sender, EventArgs e)
        {
            int temp = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                temp += dice[i];
            }
            DisableButtons();
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
            NextRoundButton.Enabled = true;
            scoreSet = true;
            TotalUpdate(0, LowerSection[6]);
        }

        private void ButtonBnsYz_Click(object sender, EventArgs e)
        {
            DisableButtons();
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
            NextRoundButton.Enabled = true;
            scoreSet = true;
            TotalUpdate(0, LowerSection[7]);
        }

        private void DisableButtons()
        {
            Button1s.Enabled = false;
            Button2s.Enabled = false;
            Button3s.Enabled = false;
            Button4s.Enabled = false;
            Button5s.Enabled = false;
            Button6s.Enabled = false;
            Button3oK.Enabled = false;
            Button4oK.Enabled = false;
            ButtonSS.Enabled = false;
            ButtonLS.Enabled = false;
            ButtonFH.Enabled = false;
            ButtonYz.Enabled = false;
            ButtonChnc.Enabled = false;
            ButtonBnsYz.Enabled = false;
            ButtonColours();
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
                    sw.Write(dice[i]+",");
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
                sw.WriteLine(changeDice);
                sw.WriteLine(scoreSet);
                for (int i = 0; i < computerUpperSection.Length; i++)
                {
                    sw.Write(computerUpperSection[i] + ",");
                }
                sw.WriteLine();
                for (int i = 0; i < computerLowerSection.Length; i++)
                {
                    sw.Write(computerLowerSection[i] + ",");
                }
                sw.Close();
            }
            Form Menu = new Menu();
            Menu.Show();
            Point topLeftCorner = new Point(Location.X+((1190/2) - (200/2)), Location.Y+((1000/2) - (249/2)));
            Menu.Location = topLeftCorner;
        }

        int[] computerDice = new int[5];
        int[] computerUpperSection = new int[9];
        int[] computerLowerSection = new int[11];
        int[] computerNumberCount = new int[6];

        int computerRollCount = 0;

        private void ComputersTurn()
        {
            Random rand = new Random();

            computerRollCount = 0;

            for (int i = 0; i < computerDice.Length; i++)
            {
                computerDice[i] = 0;
            }

            if (computerUpperSection[0] == 0)
            {
                for (int i = 0; i < computerUpperSection.Length; i++)
                {
                    computerUpperSection[i] = -1;
                }
            }
            if (computerLowerSection[0] == 0)
            {
                for (int i = 0; i < computerLowerSection.Length; i++)
                {
                    computerLowerSection[i] = -1;
                }
            }

            while (computerRollCount < 4)
            {
                for (int i = 0; i < computerNumberCount.Length; i++)
                {
                    computerNumberCount[i] = 0;
                }
                for (int i = 0; i < computerDice.Length; i++)
                {
                    if (computerDice[i] == 0)
                    {
                        computerDice[i] = rand.Next(1, 7);
                    }
                }
                for (int i = 0; i < computerDice.Length; i++)
                {
                    switch (computerDice[i])
                    {
                        case 1:
                            computerNumberCount[0]++;
                            break;
                        case 2:
                            computerNumberCount[1]++;
                            break;
                        case 3:
                            computerNumberCount[2]++;
                            break;
                        case 4:
                            computerNumberCount[3]++;
                            break;
                        case 5:
                            computerNumberCount[4]++;
                            break;
                        case 6:
                            computerNumberCount[5]++;
                            break;
                    }
                }

                if (((computerNumberCount.Contains(2)) && (computerNumberCount.Contains(3))) && (computerLowerSection[2] == -1))
                {
                    FullHouse();
                }

                if ((((computerNumberCount.Contains(2)) && (computerNumberCount.Contains(3))) == false || computerLowerSection[2] != -1) && (computerNumberCount.Contains(2) || computerNumberCount.Contains(3) || computerNumberCount.Contains(4)) && (((computerNumberCount[0] == 1) && (computerNumberCount[1] == 1) && (computerNumberCount[2] == 1)) == false) && (((computerNumberCount[1] == 1) && (computerNumberCount[2] == 1) && (computerNumberCount[3] == 1)) == false) && (((computerNumberCount[2] == 1) && (computerNumberCount[3] == 1) && (computerNumberCount[4] == 1)) == false) && (((computerNumberCount[3] == 1) && (computerNumberCount[4] == 1) && (computerNumberCount[5] == 1)) == false))
                {
                    Numbers();
                }

                if (((computerNumberCount[0] == 1) && (computerNumberCount[1] == 1) && (computerNumberCount[2] == 1)) || ((computerNumberCount[1] == 1) && (computerNumberCount[2] == 1) && (computerNumberCount[3] == 1)) || ((computerNumberCount[2] == 1) && (computerNumberCount[3] == 1) && (computerNumberCount[4] == 1)) || ((computerNumberCount[3] == 1) && (computerNumberCount[4] == 1) && (computerNumberCount[5] == 1)))
                {
                    Straight();
                }

                if (computerNumberCount.Contains(5))
                {
                    Yahtzee();
                }

                if (computerRollCount == 3)
                {
                    int totUp = 0;
                    int totLow = 0;

                    for (int i = 0; i < 6; i++)
                    {
                        if ((computerUpperSection[i] != -1) && (computerUpperSection[i] != -2))
                        {
                            totUp += computerUpperSection[i];
                        }
                    }
                    if (totUp >= 65)
                    {
                        computerUpperSection[7] = 35;
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        if ((computerLowerSection[i] != -1) && (computerLowerSection[i] != -2))
                        {
                            totLow += computerLowerSection[i];
                        }
                    }
                    computerUpperSection[6] = totUp;
                    if (computerUpperSection[7] != -1)
                    {
                        computerUpperSection[8] = computerUpperSection[6] + computerUpperSection[7];
                    }
                    else
                    {
                        computerUpperSection[8] = computerUpperSection[6];
                    }
                    computerLowerSection[8] = computerUpperSection[8];
                    computerLowerSection[9] = totLow;
                    computerLowerSection[10] = totLow + totUp;

                    string temp = "";
                    string temp2 = "";
                    string temp3 = "";

                    for (int i = 0; i < computerDice.Length; i++)
                    {
                        temp += Convert.ToString(computerDice[i]) + ", ";
                    }
                    temp += "  ";
                    for (int i = 0; i < computerUpperSection.Length; i++)
                    {
                        if (computerUpperSection[i] == -1)
                        {
                            temp += "0, ";
                        }
                        else if (computerUpperSection[i] == -2)
                        {
                            temp += "X, ";
                        }
                        else
                        {
                            temp += Convert.ToString(computerUpperSection[i]) + ", ";
                        }
                    }
                    temp += "  ";
                    for (int i = 0; i < computerLowerSection.Length; i++)
                    {
                        if (computerLowerSection[i] == -1)
                        {
                            temp += "0, ";
                        }
                        else if (computerLowerSection[i] == -2)
                        {
                            temp += "X, ";
                        }
                        else
                        {
                            temp += Convert.ToString(computerLowerSection[i]) + ", ";
                        }
                    }
                    temp += "  ";

                    MessageBox.Show($"{temp}\n{temp2}\n{temp3}", "test", MessageBoxButtons.OK);
                }

                computerRollCount++;
            }

        }

        public void Numbers()
        {
            int goal = 0;

            for (int i = 5; i > 0; i--)
            {
                if (computerNumberCount[i] >= 2)
                {
                    goal = i + 1;
                    break;
                }
            }
            if (computerRollCount < 3)
            {
                for (int i = 0; i < computerDice.Length; i++)
                {
                    if (computerDice[i] != goal)
                    {
                        computerDice[i] = 0;
                    }
                }
            }
            if (computerRollCount == 3)
            {
                int count = 0;

                for (int i = 0; i < computerDice.Length; i++)
                {
                    if (computerDice[i] == goal)
                    {
                        count++;
                    }
                }

                if (computerUpperSection[goal - 1] == -1)
                {
                    computerUpperSection[goal - 1] = count * goal;
                }
                else if ((count == 3) && (computerLowerSection[0] == -1))
                {
                    int temp = 0;

                    for (int i = 0; i < computerDice.Length; i++)
                    {
                        temp += computerDice[i];
                    }

                    computerLowerSection[0] = temp;
                }
                else if ((count == 4) && (computerLowerSection[1] == -1))
                {
                    int temp = 0;

                    for (int i = 0; i < computerDice.Length; i++)
                    {
                        temp += computerDice[i];
                    }

                    computerLowerSection[1] = temp;
                }
                else if (computerLowerSection[6] == -1)
                {
                    int temp = 0;

                    for (int i = 0; i < computerDice.Length; i++)
                    {
                        temp += computerDice[i];
                    }

                    computerLowerSection[6] = temp;
                }
                else
                {
                    bool categoryChosen = false;
                    for (int i = 0; i < computerUpperSection.Length - 3; i++)
                    {
                        if ((computerUpperSection[i] == -1) && (categoryChosen == false))
                        {
                            computerUpperSection[i] = -2;
                            categoryChosen = true;
                        }
                    }
                    if (categoryChosen == false)
                    {
                        for (int i = 0; i < computerLowerSection.Length - 3; i++)
                        {
                            if ((computerLowerSection[i] == -1) && (categoryChosen == false))
                            {
                                computerLowerSection[i] = -2;
                                categoryChosen = true;
                            }
                        }
                    }
                }
            }
        }

        public void DiceReset(int one, int two, int three, int four, int five, int six)
        {
            if (computerNumberCount[0] > one)
            {
                for (int i = 0; i < computerNumberCount[0] - 1; i++)
                {
                    computerDice[Array.BinarySearch(computerDice, 1)] = 0;
                }
            }
            if (computerNumberCount[1] > two)
            {
                for (int i = 0; i < computerNumberCount[1] - 1; i++)
                {
                    computerDice[Array.BinarySearch(computerDice, 2)] = 0;
                }
            }
            if (computerNumberCount[2] > three)
            {
                for (int i = 0; i < computerNumberCount[2] - 1; i++)
                {
                    computerDice[Array.BinarySearch(computerDice, 3)] = 0;
                }
            }
            if (computerNumberCount[3] > four)
            {
                for (int i = 0; i < computerNumberCount[3] - 1; i++)
                {
                    computerDice[Array.BinarySearch(computerDice, 4)] = 0;
                }
            }
            if (computerNumberCount[4] > five)
            {
                for (int i = 0; i < computerNumberCount[4] - 1; i++)
                {
                    computerDice[Array.BinarySearch(computerDice, 5)] = 0;
                }
            }
            if (computerNumberCount[5] > six)
            {
                for (int i = 0; i < computerNumberCount[5] - 1; i++)
                {
                    computerDice[Array.BinarySearch(computerDice, 6)] = 0;
                }
            }
        }

        public void Straight()
        {
            Array.Sort(computerDice);

            if (((computerNumberCount[0] == 1) && (computerNumberCount[1] == 1) && (computerNumberCount[2] == 1) && (computerNumberCount[3] == 1) && (computerNumberCount[4] == 1)) || ((computerNumberCount[1] == 1) && (computerNumberCount[2] == 1) && (computerNumberCount[3] == 1) && (computerNumberCount[4] == 1) && (computerNumberCount[5] == 1)))
            {
                computerRollCount = 3;
            }

            if (computerRollCount < 3)
            {
                if ((computerNumberCount[0] == 1) && (computerNumberCount[1] == 1) && (computerNumberCount[2] == 1))
                {
                    DiceReset(1, 1, 1, 1, 1, 5);
                }
                if ((computerNumberCount[1] == 1) && (computerNumberCount[2] == 1) && (computerNumberCount[3] == 1))
                {
                    DiceReset(1, 1, 1, 1, 1, 5);
                }
                if ((computerNumberCount[2] == 1) && (computerNumberCount[3] == 1) && (computerNumberCount[4] == 1))
                {
                    DiceReset(5, 1, 1, 1, 1, 1);
                }
                if ((computerNumberCount[3] == 1) && (computerNumberCount[4] == 1) && (computerNumberCount[5] == 1))
                {
                    DiceReset(5, 1, 1, 1, 1, 1);
                }
            }
            if (computerRollCount == 3)
            {
                if ((computerLowerSection[4] == -1) && (((computerNumberCount[0] == 1) && (computerNumberCount[1] == 1) && (computerNumberCount[2] == 1) && (computerNumberCount[3] == 1) && (computerNumberCount[4] == 1)) || ((computerNumberCount[1] == 1) && (computerNumberCount[2] == 1) && (computerNumberCount[3] == 1) && (computerNumberCount[4] == 1) && (computerNumberCount[5] == 1))))
                {
                    computerLowerSection[4] = 40;
                }
                else if ((computerLowerSection[3] == -1) && (((computerNumberCount[0] == 1) && (computerNumberCount[1] == 1) && (computerNumberCount[2] == 1) && (computerNumberCount[3] == 1)) || ((computerNumberCount[1] == 1) && (computerNumberCount[2] == 1) && (computerNumberCount[3] == 1) && (computerNumberCount[4] == 1)) || ((computerNumberCount[2] == 1) && (computerNumberCount[3] == 1) && (computerNumberCount[4] == 1) && (computerNumberCount[5] == 1))))
                {
                    computerLowerSection[3] = 30;
                }
                else if (computerLowerSection[6] == -1)
                {
                    int temp = 0;

                    for (int i = 0; i < computerDice.Length; i++)
                    {
                        temp += computerDice[i];
                    }

                    computerLowerSection[6] = temp;
                }
                else
                {
                    bool categoryChosen = false;
                    for (int i = 0; i < computerUpperSection.Length - 3; i++)
                    {
                        if ((computerUpperSection[i] == -1) && (categoryChosen == false))
                        {
                            computerUpperSection[i] = -2;
                            categoryChosen = true;
                        }
                    }
                    if (categoryChosen == false)
                    {
                        for (int i = 0; i < computerLowerSection.Length - 3; i++)
                        {
                            if ((computerLowerSection[i] == -1) && (categoryChosen == false))
                            {
                                computerLowerSection[i] = -2;
                                categoryChosen = true;
                            }
                        }
                    }
                }
            }
        }

        public void Yahtzee()
        {
            if (computerLowerSection[5] == -1)
            {
                computerRollCount = 3;
                computerLowerSection[5] = 50;
            }
            else if (computerLowerSection[7] == -1)
            {
                computerRollCount = 3;
                computerLowerSection[7] = 100;
            }
            else if (computerUpperSection[computerDice[0]-1] == -1)
            {
                computerUpperSection[computerDice[0]-1] = 5 * computerDice[0];
            }
            else if (computerLowerSection[0] == -1)
            {
                int temp = 0;

                for (int i = 0; i < computerDice.Length; i++)
                {
                    temp += computerDice[i];
                }

                computerLowerSection[0] = temp;
            }
            else if (computerLowerSection[1] == -1)
            {
                int temp = 0;

                for (int i = 0; i < computerDice.Length; i++)
                {
                    temp += computerDice[i];
                }

                computerLowerSection[1] = temp;
            }
            else if (computerLowerSection[6] == -1)
            {
                int temp = 0;

                for (int i = 0; i < computerDice.Length; i++)
                {
                    temp += computerDice[i];
                }

                computerLowerSection[6] = temp;
            }
            else
            {
                bool categoryChosen = false;
                for (int i = 0; i < computerUpperSection.Length - 3; i++)
                {
                    if ((computerUpperSection[i] == -1) && (categoryChosen == false))
                    {
                        computerUpperSection[i] = -2;
                        categoryChosen = true;
                    }
                }
                if (categoryChosen == false)
                {
                    for (int i = 0; i < computerLowerSection.Length - 3; i++)
                    {
                        if ((computerLowerSection[i] == -1) && (categoryChosen == false))
                        {
                            computerLowerSection[i] = -2;
                            categoryChosen = true;
                        }
                    }
                }
            }
        }

        public void FullHouse()
        {
            if (computerLowerSection[2] == -1)
            {
                computerRollCount = 3;
                computerLowerSection[2] = 25;
            }
            else if (computerLowerSection[0] == -1)
            {
                int temp = 0;

                for (int i = 0; i < computerDice.Length; i++)
                {
                    temp += computerDice[i];
                }

                computerLowerSection[0] = temp;
            }
            else if (computerLowerSection[6] == -1)
            {
                int temp = 0;

                for (int i = 0; i < computerDice.Length; i++)
                {
                    temp += computerDice[i];
                }

                computerLowerSection[6] = temp;
            }
            else if ((computerUpperSection[Array.BinarySearch(computerNumberCount, 2)] == -1) && (computerUpperSection[Array.BinarySearch(computerNumberCount, 3)] == -1))
            {
                bool categoryChosen = false;
                for (int i = 0; i < computerUpperSection.Length - 3; i++)
                {
                    if ((computerUpperSection[i] == -1) && (categoryChosen == false))
                    {
                        computerUpperSection[i] = -2;
                        categoryChosen = true;
                    }
                }
                if (categoryChosen == false)
                {
                    for (int i = 0; i < computerLowerSection.Length - 3; i++)
                    {
                        if ((computerLowerSection[i] == -1) && (categoryChosen == false))
                        {
                            computerLowerSection[i] = -2;
                            categoryChosen = true;
                        }
                    }
                }
            }
        }

        //Computer Score View Button
        public void button11_Click(object sender, EventArgs e)
        {
            ComputerDataTransfer();
            if (computerPointsTable.BackColor == Color.FromArgb(255, 255, 210))
            {
                computerPointsTable.BackColor = Color.FromArgb(255, 255, 211);
            }
            else
            {
                computerPointsTable.BackColor = Color.FromArgb(255, 255, 210);
            }
            if (computerPointsTable.Visible == false)
            {
                computerPointsTable.Show();
            }
            else
            {
                computerPointsTable.Hide();
            }
        }

        private void ComputerDataTransfer()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + @"\AI Score\AITemp.txt"))
            {
                File.Delete(Directory.GetCurrentDirectory() + @"\AI Score\AITemp.txt");
            }
            else
            {
                File.Create(Directory.GetCurrentDirectory() + @"\AI Score\AITemp.txt").Close();
            }

            using (StreamWriter sw = File.AppendText(Directory.GetCurrentDirectory() + @"\AI Score\AITemp.txt"))
            {
                for (int i = 0; i < computerUpperSection.Length; i++)
                {
                    sw.Write(computerUpperSection[i] + ",");
                }
                sw.WriteLine();
                for (int i = 0; i < computerLowerSection.Length; i++)
                {
                    sw.Write(computerLowerSection[i] + ",");
                }
                sw.Close();
            }
        }
    }
}
