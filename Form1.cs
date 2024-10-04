using System;
using System.Windows.Forms;

namespace TicTacToe
{
    partial class TicTacToe : Form //Class name to TicTacToe
    {
        private string currentPlayer; //Track current player
        private int turnCount; //Count
        private int playerXScore; //Track score player X
        private int playerOScore; //Track score player O

        
        private TableLayoutPanel tableLayoutPanel1;
        private Label scoreLabel;

        public TicTacToe() //TicTacToe
        {
            InitializeComponent(); //Start U
            InitializeGame(); //Start game
        }

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.scoreLabel = new Label();

           
            this.tableLayoutPanel1.ColumnCount = 3; //Table Layout
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.Dock = DockStyle.Fill;

            // Create and add buttons to the tableLayoutPanel
            for (int i = 1; i <= 9; i++)
            {
                Button button = new Button();
                button.Name = $"button{i}";
                button.Size = new System.Drawing.Size(60, 60);
                button.Click += new EventHandler(this.ButtonClick); //Click event
                this.tableLayoutPanel1.Controls.Add(button, (i - 1) % 3, (i - 1) / 3); //Buttons in 3x3
            }

            this.scoreLabel.Location = new System.Drawing.Point(10, 10); //Adjust position as needed
            this.scoreLabel.Text = "Player X: 0 | Player O: 0"; //Score display

            this.Controls.Add(this.tableLayoutPanel1); //Table Panel
            this.Controls.Add(this.scoreLabel);
            this.Text = "Tic Tac Toe";
            this.Size = new System.Drawing.Size(300, 300);
        }

        private void InitializeGame() //For reset
        {
            currentPlayer = "X"; //Start with player X
            turnCount = 0; //Reset turn count
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Button button)
                {
                    button.Text = string.Empty;
                    button.Enabled = true; //Buttons
                }
            }
            UpdateScoreDisplay(); //Update score
        }

        private void CheckForWinner() //Check for a winner
        {
            //Rows and columns
            string[,] buttons = new string[3, 3];
            int index = 0;

            //Fill buttons
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Button button)
                {
                    buttons[index / 3, index % 3] = button.Text; //Button text
                    index++;
                }
            }

            //Check win
            if ((buttons[0, 0] == currentPlayer && buttons[0, 1] == currentPlayer && buttons[0, 2] == currentPlayer) ||
                (buttons[1, 0] == currentPlayer && buttons[1, 1] == currentPlayer && buttons[1, 2] == currentPlayer) ||
                (buttons[2, 0] == currentPlayer && buttons[2, 1] == currentPlayer && buttons[2, 2] == currentPlayer) ||
                (buttons[0, 0] == currentPlayer && buttons[1, 0] == currentPlayer && buttons[2, 0] == currentPlayer) ||
                (buttons[0, 1] == currentPlayer && buttons[1, 1] == currentPlayer && buttons[2, 1] == currentPlayer) ||
                (buttons[0, 2] == currentPlayer && buttons[1, 2] == currentPlayer && buttons[2, 2] == currentPlayer) ||
                (buttons[0, 0] == currentPlayer && buttons[1, 1] == currentPlayer && buttons[2, 2] == currentPlayer) ||
                (buttons[0, 2] == currentPlayer && buttons[1, 1] == currentPlayer && buttons[2, 0] == currentPlayer))
            {
                MessageBox.Show($"Player {currentPlayer} Wins"); //Show winner
                UpdateScore(); //Update score 
                ResetGame(); //Reset the game
            }
            else if (turnCount == 9) //Check for a draw
            {
                MessageBox.Show("It's a draw!"); //Show draw message
                ResetGame(); //Reset the game after a draw
            }
        }

        private void UpdateScore() //Update scores based on the winner
        {
            if (currentPlayer == "X") //Check if player X wins
            {
                playerXScore++; //Increment player X score
            }
            else //Player O wins
            {
                playerOScore++; //Increment player O score
            }
            UpdateScoreDisplay(); //Update score display after scoring
        }

        private void UpdateScoreDisplay() //Update the score 
        {
            scoreLabel.Text = $"Player X: {playerXScore} | Player O: {playerOScore}"; //Display score
        }

        private void ResetGame() //Reset the game 
        {
            InitializeGame();
        }

        private void TogglePlayer() //Current player
        {
            currentPlayer = (currentPlayer == "X") ? "O" : "X"; //Switch players
        }

        private void ButtonClick(object sender, EventArgs e) //Button click logic
        {
            MessageBox.Show("Button clicked!"); // Debugging message

            Button button = sender as Button; //Green line might have a problem
            if (button != null)
            {
                button.Text = currentPlayer; //Set button text to current player
                button.Enabled = false; //Disable the button after it's clicked
                turnCount++; //increase turn count here
                CheckForWinner(); //Check for a winner or draw
                if (turnCount < 9) //Toggle player if game is not over
                {
                    TogglePlayer(); //Switch players
                }
            }
        }
    }
}
