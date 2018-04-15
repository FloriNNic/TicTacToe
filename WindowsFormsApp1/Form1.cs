using System;
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
        bool turn = true; // x turn = true; o turn = false;
        int turnCount = 0;
        bool against_computer = false;
        

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

            p2.Focus();
            checkForWinner();

            if (!turn && against_computer)
            {
                computer_makes_move();
            }
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
                {
                    winner = p2.Text;
                    o_win_count.Text = (Int16.Parse(o_win_count.Text) + 1).ToString();
                }
                else
                {
                    winner = p1.Text;
                    x_win_count.Text = (Int16.Parse(x_win_count.Text) + 1).ToString();
                }
                MessageBox.Show(winner + " is the winner", "Game over !");
                newGameToolStripMenuItem.PerformClick();
            }
            else if (turnCount == 9)
                {
                    draws_count.Text = (Int16.Parse(draws_count.Text) + 1).ToString();
                    MessageBox.Show("It's a draw", "Game over !");
                    newGameToolStripMenuItem.PerformClick();
                }
        }

        private void computer_makes_move()
        {
            //priority 1:  get tic tac toe
            //priority 2:  block x tic tac toe
            //priority 3:  go for corner space
            //priority 4:  pick open space

            Button move = null;

            //look for tic tac toe opportunities
            move = look_for_win_or_block("O"); //look for win
            if (move == null)
            {
                move = look_for_win_or_block("X"); //look for block
                if (move == null)
                {
                    move = look_for_corner();
                    if (move == null)
                    {
                        move = look_for_open_space();
                    }//end if
                }//end if
            }//end if

            move.PerformClick();
        }

        private Button look_for_open_space()
        {
            Button b = null;
            foreach (Control c in Controls)
            {
                b = c as Button;
                if (b != null)
                {
                    if (b.Text == "")
                        return b;
                }//end if
            }//end if

            return null;
        }

        private Button look_for_corner()
        {
            if (A1.Text == "O")
            {
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (A3.Text == "O")
            {
                if (A1.Text == "")
                    return A1;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (C3.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C1.Text == "")
                    return C1;
            }

            if (C1.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
            }

            if (A1.Text == "")
                return A1;
            if (A3.Text == "")
                return A3;
            if (C1.Text == "")
                return C1;
            if (C3.Text == "")
                return C3;

            return null;
        }

        private Button look_for_win_or_block(string mark)
        {
            //HORIZONTAL TESTS
            if ((A1.Text == mark) && (A2.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A2.Text == mark) && (A3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (A3.Text == mark) && (A2.Text == ""))
                return A2;

            if ((B1.Text == mark) && (B2.Text == mark) && (B3.Text == ""))
                return B3;
            if ((B2.Text == mark) && (B3.Text == mark) && (B1.Text == ""))
                return B1;
            if ((B1.Text == mark) && (B3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((C1.Text == mark) && (C2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((C2.Text == mark) && (C3.Text == mark) && (C1.Text == ""))
                return C1;
            if ((C1.Text == mark) && (C3.Text == mark) && (C2.Text == ""))
                return C2;

            //VERTICAL TESTS
            if ((A1.Text == mark) && (B1.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B1.Text == mark) && (C1.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C1.Text == mark) && (B1.Text == ""))
                return B1;

            if ((A2.Text == mark) && (B2.Text == mark) && (C2.Text == ""))
                return C2;
            if ((B2.Text == mark) && (C2.Text == mark) && (A2.Text == ""))
                return A2;
            if ((A2.Text == mark) && (C2.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B3.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B3.Text == mark) && (C3.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C3.Text == mark) && (B3.Text == ""))
                return B3;

            //DIAGONAL TESTS
            if ((A1.Text == mark) && (B2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B2.Text == mark) && (C3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B2.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B2.Text == mark) && (C1.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C1.Text == mark) && (B2.Text == ""))
                return B2;

            return null;
        }
        private void disableButtons()
        {          
                foreach (Control c in Controls)
                {
                    try
                    {
                        Button button = (Button)c;
                        button.Enabled = false;
                    }
                    catch { }
                }
            
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turnCount = 0;
            
                foreach (Control c in Controls)
                {
                    try
                    {
                        Button button = (Button)c;
                        button.Enabled = true;
                        button.Text = "";
                    }
                    catch { }
                }
            
        }

        private void button_enter(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if(button.Enabled)
            {
                if (turn)
                    button.Text = "X";
                else
                    button.Text = "O";
            }
        }

        private void button_leave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Enabled)
            {
                button.Text = "";
            }
        }

        private void resetGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x_win_count.Text = "0";
            o_win_count.Text = "0";
            draws_count.Text = "0";
        }

        private void p2_TextChanged(object sender, EventArgs e)
        {
            if (p2.Text.ToUpper() == "COMPUTER")
                against_computer = true;
            else
                against_computer = false;
        }
    }
}
