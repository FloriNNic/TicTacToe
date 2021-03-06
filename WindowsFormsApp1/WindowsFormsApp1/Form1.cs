﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        bool turn = true; // x turn = true; y turn = false;
        int turnCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("It's just a game","By FloriNN");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_click(object sender, EventArgs e)
        {
            turnCount++;
            Button button = (Button)sender;
            if (turn)
                button.Text = "X";
            else
                button.Text = "O";

            turn = !turn;
            button.Enabled = false;

            checkForWinner();
        }

        private void checkForWinner()
        {
            bool thereIsAWinner = false;

            if (A1.Text == A2.Text && A2.Text == A3.Text && !A1.Enabled)
                thereIsAWinner = true;
            else if (B1.Text == B2.Text && B2.Text == B3.Text && !B1.Enabled)
                thereIsAWinner = true;
            else if (C1.Text == C2.Text && C2.Text == C3.Text && !C1.Enabled)
                thereIsAWinner = true;

            else if (A1.Text == B1.Text && B1.Text == C1.Text && !A1.Enabled)
                thereIsAWinner = true;
            else if (A2.Text == B2.Text && B2.Text == C2.Text && !A2.Enabled)
                thereIsAWinner = true;
            else if (A3.Text == B3.Text && B3.Text == C3.Text && !A3.Enabled)
                thereIsAWinner = true;

            else if (A1.Text == B2.Text && B2.Text == C3.Text && !A1.Enabled)
                thereIsAWinner = true;
            else if (A3.Text == B2.Text && B2.Text == C1.Text && !C3.Enabled)
                thereIsAWinner = true;

            if (thereIsAWinner)
            {
                disableButtons();
                string winner = "";
                if (turn)
                    winner = "O";
                else
                    winner = "X";

                MessageBox.Show(winner + " is the winner", "Game over !");
            }
            else if (turnCount == 9)
                {
                    MessageBox.Show("It's a draw", "Game over !");
                }
        }

        private void disableButtons()
        {
            try
            {
                foreach (Control c in Controls)
                {
                    Button button = (Button)c;
                    button.Enabled = false;
                }
            }
            catch { }
            
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turn = true;
            turnCount = 0;

            try
            {
                foreach (Control c in Controls)
                {
                    Button button = (Button)c;
                    button.Enabled = true;
                    button.Text = "";
                }
            }
            catch { }
        }
    }
}
