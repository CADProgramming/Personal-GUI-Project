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
    public partial class ComputerPointsTable : Form
    {
        //Global Variables

        Random rand = new Random();

        int[] computerUpperSection = new int[9];
        int[] computerLowerSection = new int[11];

        public ComputerPointsTable()
        {
            //Initialization
            InitializeComponent();
            Program.RefToComputerPoints = this;
            LoadGUI();
        }

        public void LoadGUI()
        {
            if (Program.ComputerPointsLoad == false)
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = 65;
                progressBar1.Step = 1;
                progressBar1.Value = 0;

                Label[] upperScoreLabels = { Pointlabel1, Pointlabel2, Pointlabel3, Pointlabel4, Pointlabel5, Pointlabel6, PointlabelTotal1, PointlabelBonus1, PointlabelTotal2 };
                Label[] lowerScoreLabels = { Label3oK, Label4oK, LabelFH, LabelSS, LabelLS, LabelYz, LabelChnc, LabelBnsYz, LabelTot1, LabelTot2, LabelGTot };

                foreach (Label label in upperScoreLabels)
                {
                    label.Text = "0";
                }
                foreach (Label label in lowerScoreLabels)
                {
                    label.Text = "0";
                }

                string[] computerUpperSectionTemp = File.ReadLines(Directory.GetCurrentDirectory() + @"\AI Score\AITemp.txt").Take(1).First().Split(',');
                for (int i = 0; i < computerUpperSection.Length; i++)
                {
                    computerUpperSection[i] = Convert.ToInt32(computerUpperSectionTemp[i]);
                }

                string[] computerLowerSectionTemp = File.ReadLines(Directory.GetCurrentDirectory() + @"\AI Score\AITemp.txt").Skip(1).Take(1).First().Split(',');
                for (int i = 0; i < computerLowerSection.Length; i++)
                {
                    computerLowerSection[i] = Convert.ToInt32(computerLowerSectionTemp[i]);
                }

                for (int i = 0; i < computerUpperSection.Length; i++)
                {
                    if (computerUpperSection[i] > 0)
                    {
                        upperScoreLabels[i].Text = Convert.ToString(computerUpperSection[i]);
                    }
                    else if (computerUpperSection[i] == -1)
                    {
                        upperScoreLabels[i].Text = "0";
                    }
                    else if (computerUpperSection[i] == -2)
                    {
                        upperScoreLabels[i].Text = "0";
                    }
                }

                for (int i = 0; i < computerLowerSection.Length; i++)
                {
                    if (computerLowerSection[i] > 0)
                    {
                        lowerScoreLabels[i].Text = Convert.ToString(computerLowerSection[i]);
                    }
                    else if (computerLowerSection[i] == -1)
                    {
                        lowerScoreLabels[i].Text = "0";
                    }
                    else if (computerLowerSection[i] == -2)
                    {
                        lowerScoreLabels[i].Text = "X";
                    }
                }

                if (computerUpperSection[6] < progressBar1.Maximum)
                {
                    if (computerUpperSection[6] != -1)
                    {
                        progressBar1.Value = computerUpperSection[6];
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
            }
            else
            {
                loading code write it here!!!
                Label[] upperScoreLabels = { Pointlabel1, Pointlabel2, Pointlabel3, Pointlabel4, Pointlabel5, Pointlabel6, PointlabelTotal1, PointlabelBonus1, PointlabelTotal2 };
                Label[] lowerScoreLabels = { Label3oK, Label4oK, LabelFH, LabelSS, LabelLS, LabelYz, LabelChnc, LabelBnsYz, LabelTot1, LabelTot2, LabelGTot };

                string[] computerUpperSectionTemp = File.ReadLines(Directory.GetCurrentDirectory() + @"\AI Score\AITemp.txt").Take(1).First().Split(',');
                for (int i = 0; i < computerUpperSection.Length; i++)
                {
                    computerUpperSection[i] = Convert.ToInt32(computerUpperSectionTemp[i]);
                }

                string[] computerLowerSectionTemp = File.ReadLines(Directory.GetCurrentDirectory() + @"\AI Score\AITemp.txt").Skip(1).Take(1).First().Split(',');
                for (int i = 0; i < computerLowerSection.Length; i++)
                {
                    computerLowerSection[i] = Convert.ToInt32(computerLowerSectionTemp[i]);
                }

                for (int i = 0; i < upperScoreLabels.Length; i++)
                {
                    upperScoreLabels[i].Text = Convert.ToString(computerUpperSection[i]);
                }
                for (int i = 0; i < lowerScoreLabels.Length; i++)
                {
                    lowerScoreLabels[i].Text = Convert.ToString(computerLowerSection[i]);
                }

                for (int i = 0; i < computerUpperSection.Length; i++)
                {
                    if (computerUpperSection[i] > 0)
                    {
                        upperScoreLabels[i].Text = Convert.ToString(computerUpperSection[i]);
                    }
                    else if (computerUpperSection[i] == -1)
                    {
                        upperScoreLabels[i].Text = "0";
                    }
                    else if (computerUpperSection[i] == -2)
                    {
                        upperScoreLabels[i].Text = "0";
                    }
                }

                for (int i = 0; i < computerLowerSection.Length; i++)
                {
                    if (computerLowerSection[i] > 0)
                    {
                        lowerScoreLabels[i].Text = Convert.ToString(computerLowerSection[i]);
                    }
                    else if (computerLowerSection[i] == -1)
                    {
                        lowerScoreLabels[i].Text = "0";
                    }
                    else if (computerLowerSection[i] == -2)
                    {
                        lowerScoreLabels[i].Text = "X";
                    }
                }

                if (computerUpperSection[6] < progressBar1.Maximum)
                {
                    if (computerUpperSection[6] != -1)
                    {
                        progressBar1.Value = computerUpperSection[6];
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

                progressBar1.Minimum = 0;
                progressBar1.Maximum = 65;
                progressBar1.Step = 1;
                progressBar1.Value = computerUpperSection[7];
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

        private void ComputerPointsTable_BackColorChanged(object sender, EventArgs e)
        {
            LoadGUI();
        }
    }
}
