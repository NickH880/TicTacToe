using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class TicTacToe : Form
    {
        private int turnStat = 1;
        private int pOneScore = 0;
        private int pTwoScore = 0;

        private Dictionary<String, String> activeGame = new Dictionary<string, string>();

        private List<Button> gameButtons = new List<Button>();

        private List<List<String>> gameWins = new List<List<String>>();

        public TicTacToe()
        {
            InitializeComponent();
            BuildLists();
        }

        #region List building before game starts
        public void BuildLists()        //list of wins to compare game too & all buttons for logic
        {
            gameWins.Add(new List<String> { "topLeft", "topMid", "topRight" });
            gameWins.Add(new List<String> { "topLeft", "midLeft", "bottomLeft" });
            gameWins.Add(new List<String> { "topMid", "midMid", "bottomMid" });
            gameWins.Add(new List<String> { "topRight", "midRight", "bottomRight" });
            gameWins.Add(new List<String> { "midLeft", "midMid", "midRight" });
            gameWins.Add(new List<String> { "bottomLeft", "bottomMid", "bottomRight" });
            gameWins.Add(new List<String> { "topLeft", "midMid", "bottomRight" });
            gameWins.Add(new List<String> { "topRight", "midMid", "bottomLeft" });
            gameButtons.Add(topLeft);
            gameButtons.Add(topRight);
            gameButtons.Add(topMid);
            gameButtons.Add(bottomLeft);
            gameButtons.Add(bottomMid);
            gameButtons.Add(bottomRight);
            gameButtons.Add(midLeft);
            gameButtons.Add(midMid);
            gameButtons.Add(midRight);
        }
        #endregion

        public void TakeTurn(Button b)      //To make easy to modify, don't do things directly in this function
        {
            if (b.Text == "")
            {
                ScoreMe(b);
                RegisterMove(b);
                JudgeGame(b);
            }
        }

        private void JudgeGame(Button b)
        {
            if (activeGame.Count >= 5)       //You can't win ttt before 5 turns
            {
                foreach (List<string> i in gameWins)
                {
                    if (i.Contains(b.Name))
                    {
                        try
                        {
                            string lOne = activeGame[i[0]];
                            string lTwo = activeGame[i[1]];
                            string lThree = activeGame[i[2]];

                            if (lOne == lTwo && lOne == lThree) //Comparing possible wins to scored gameButtons isn't fancy but works
                            {
                                WinGame();
                                return; // Exit after a win is found
                            }
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); }
                    }
                }
            }

            //Check for draw
            if (activeGame.Count == 9) // All spaces filled and no winner
            {
                DrawGame(); // Call the DrawGame method if it's a draw
            }
        }

        private void WinGame()
        {
            try
            {
                foreach (Button b in gameButtons)
                {
                    b.Enabled = false;
                }

                switch (turnStat)
                {
                    case 1:
                        gameState.Text = "Player Two Wins!!";
                        pTwoScore++;
                        pTwoWins.Text = $"Player Two: {pTwoScore}";
                        break;

                    case 2:
                        gameState.Text = "Player One Wins!!";
                        pOneScore++;
                        pOneWins.Text = $"Player One: {pOneScore}";
                        break;

                    default:
                        gameState.Text = "Ready to Tic Tac Toe??";
                        break;
                }

                turnStat = 0;
                startButton.Text = "New Game";
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        // New method to handle a draw game
        private void DrawGame()
        {
            foreach (Button b in gameButtons)
            {
                b.Enabled = false; // Disable all buttons
            }

            gameState.Text = "It's a Draw!"; // Update game state text
            turnStat = 0; // Set turn stat to indicate game is over
            startButton.Text = "New Game"; // Prompt for a new game
        }

        private void RegisterMove(Button b) //register scored button to dict
        {
            activeGame.Add(b.Name, b.Text);
        }

        private void ScoreMe(Button b) //score button - add check against replaying space
        {
            try
            {
                switch (turnStat)
                {
                    case 1:
                        b.Text = "X";
                        b.BackColor = Color.Blue;
                        turnStat = 2;
                        b.Enabled = false;
                        gameState.Text = "Player Two's Turn";
                        break;

                    case 2:
                        b.Text = "O";
                        b.BackColor = Color.Red;
                        turnStat = 1;
                        b.Enabled = false;
                        gameState.Text = "Player One's Turn";
                        break;

                    default:
                        b.Text = "";
                        break;
                }
            }

            catch (Exception e) { Console.WriteLine(e.Message); }

        }

        private void StartGame()
        {
            try
            {
                startButton.Text = "Restart Game";
                gameState.Text = "Player One's Turn";
                turnStat = 1;
                activeGame.Clear();

                foreach (Button b in gameButtons)
                {
                    b.Text = "";
                    b.Enabled = true;
                    b.BackColor = Color.White;
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        private void TopLeft_Click(object sender, EventArgs e)
        {
            TakeTurn(topLeft);
        }

        private void TopMid_Click(object sender, EventArgs e)
        {
            TakeTurn(topMid);
        }

        private void TopRight_Click(object sender, EventArgs e)
        {
            TakeTurn(topRight);
        }

        private void MidLeft_Click(object sender, EventArgs e)
        {
            TakeTurn(midLeft);
        }

        private void MidMid_Click(object sender, EventArgs e)
        {
            TakeTurn(midMid);
        }

        private void MidRight_Click(object sender, EventArgs e)
        {
            TakeTurn(midRight);
        }

        private void BottomLeft_Click(object sender, EventArgs e)
        {
            TakeTurn(bottomLeft);
        }

        private void BottomMid_Click(object sender, EventArgs e)
        {
            TakeTurn(bottomMid);
        }

        private void BottomRight_Click(object sender, EventArgs e)
        {
            TakeTurn(bottomRight);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartGame();
        }
    }
}
