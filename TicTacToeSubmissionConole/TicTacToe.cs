using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeRendererLib.Enums;
using TicTacToeRendererLib.Renderer;

namespace TicTacToeSubmissionConole
{
    public class TicTacToe
    {
        private TicTacToeConsoleRenderer _boardRenderer;
        private PlayerEnum currentPlayer;
        private PlayerEnum?[,] board; // PlayerEnum type array, nullable to handle empty spots

        public TicTacToe()
        {
            _boardRenderer = new TicTacToeConsoleRenderer(10, 6);
            currentPlayer = PlayerEnum.X; // Set the initial player to 'X'
            board = new PlayerEnum?[3, 3]; // Initialize a 3x3 board with all spots null (empty)
            _boardRenderer.Render();
        }

        public void Run()
        {
            bool gameOver = false;

            while (!gameOver)
            {
                // Display which player's turn it is
                Console.SetCursorPosition(2, 19);
                Console.Write($"Player {currentPlayer}");

                // Ask user for the row
                Console.SetCursorPosition(2, 20);
                Console.Write("Please Enter Row (0-2): ");
                int row = GetValidInput(0, 2);

                // Ask user for the column
                Console.SetCursorPosition(2, 22);
                Console.Write("Please Enter Column (0-2): ");
                int column = GetValidInput(0, 2);

                // Check if the selected cell is free
                if (board[row, column] == null)
                {
                    // Place the move on the board
                    board[row, column] = currentPlayer;
                    _boardRenderer.AddMove(row, column, currentPlayer, true);

                    // Determine if there is a winner or the game is tied
                    if (CheckWin(row, column))
                    {
                        Console.SetCursorPosition(2, 24);
                        Console.WriteLine($"Player {currentPlayer} wins!");
                        gameOver = true;
                    }
                    else if (IsBoardFull())
                    {
                        Console.SetCursorPosition(2, 24);
                        Console.WriteLine("It's a draw!");
                        gameOver = true;
                    }
                    else
                    {
                        // Switch the current player
                        currentPlayer = (currentPlayer == PlayerEnum.X) ? PlayerEnum.O : PlayerEnum.X;
                    }
                }
                else
                {
                    // Notify user the chosen cell is taken
                    Console.SetCursorPosition(2, 24);
                    Console.WriteLine("That cell is already occupied. Try again.");
                }
            }
        }

        // Helper method for retrieving valid input within a specific range
        private int GetValidInput(int min, int max)
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input < min || input > max)
            {
                // Show error if input is out of range
                Console.SetCursorPosition(2, 24);
                Console.Write($"Invalid input. Please enter a number between {min} and {max}: ");
            }
            return input;
        }

        // Check if the player has won based on the last move
        private bool CheckWin(int row, int column)
        {
            // Check if all cells in the selected row are occupied by the current player
            if (board[row, 0] == currentPlayer && board[row, 1] == currentPlayer && board[row, 2] == currentPlayer)
                return true;

            // Check if all cells in the selected column are occupied by the current player
            if (board[0, column] == currentPlayer && board[1, column] == currentPlayer && board[2, column] == currentPlayer)
                return true;

            // Check the diagonals if applicable
            if ((board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer) ||
                (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer))
                return true;

            return false;
        }

        // Check if all cells are filled, leading to a draw
        private bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // If any cell is empty, the game continues
                    if (board[i, j] == null)
                        return false;
                }
            }
            return true; // All cells are filled
        }
    }
}
