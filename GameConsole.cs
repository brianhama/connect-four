using System;

namespace ConnectFour
{
    /// <summary>
    /// The GameConsole is resposible for printing the current game board
    /// state to the console. It also handles receiving player input and
    /// determining when the game has been won or is a tie.
    /// </summary>
    public class GameConsole
    {
        public static int GetMove(Game game)
        {
            var moveKey = Console.ReadKey();

            while (moveKey.Key != ConsoleKey.Escape)
            {
                if (Char.IsDigit(moveKey.KeyChar))
                {
                    int column = Convert.ToInt32(moveKey.KeyChar.ToString());
                    if (game.Board.IsValidColumnForMove(column))
                    {
                        return column;
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Error: invalid column selected.");
                PrintMovePrompt(game);
                moveKey = Console.ReadKey();
            }

            return -1;
        }

        public static bool PrintGame(Game game)
        {
            Console.Clear();

            Console.Write("Current player: ");

            PrintCurrentPlayerName(game);

            Console.WriteLine();

            PrintGameBoard(game.Board);

            if (game.Board.CheckForWin())
            {
                Console.Write("Game Winner: ");
                PrintCurrentPlayerName(game);
                Console.WriteLine();
                return false;
            }
            else if (game.Board.CheckForTie())
            {
                Console.WriteLine("Game is a tie!");
                return false;
            }
            else
            {
                PrintMovePrompt(game);
                return true;
            }
        }

        private static void PrintMovePrompt(Game game)
        {
            PrintCurrentPlayerName(game);
            Console.Write(" please select a valid column (");
            Console.Write(GetValidMoves(game.Board));
            Console.Write("): ");
        }

        private static void PrintCurrentPlayerName(Game game)
        {
            switch (game.CurrentPlayer)
            {
                case Players.Unknown:
                    Console.Write("Unknown");
                    break;
                case Players.PlayerA:
                    Console.Write("Player A");
                    break;
                case Players.PlayerB:
                    Console.Write("Player B");
                    break;
            }
        }

        private static string GetValidMoves(GameBoard board)
        {
            String validMoves = "";

            for (int i = 0; i < GameBoard.BOARD_COLUMNS; i++)
            {
                if (board.IsValidColumnForMove(i))
                {
                    validMoves = validMoves + (validMoves.Length > 0 ? "," : "") + i.ToString();
                }
            }

            return validMoves;
        }

        private static void PrintGameBoard(GameBoard board)
        {
            String validMoves = "";

            for (int i = 0; i < GameBoard.BOARD_COLUMNS; i++)
            {
                if (board.IsValidColumnForMove(i))
                {
                    Console.Write("-{0:N0}-", i);
                    validMoves = (validMoves.Length > 0 ? "," : "") + i.ToString();
                }
                else
                {
                    Console.Write("-X-");
                }
            }
            Console.WriteLine();

            for (int i = 0; i < GameBoard.BOARD_COLUMNS; i++)
            {
                Console.Write("___");
            }
            Console.WriteLine();

            for (int i = GameBoard.BOARD_ROWS - 1; i >= 0; i--)
            {
                for (int col = 0; col < GameBoard.BOARD_COLUMNS; col++)
                {
                    switch (board.GetSpot(i, col))
                    {
                        case BoardSpotStatuses.Blank:
                            Console.Write("- -");
                            break;
                        case BoardSpotStatuses.PlayerA:
                            Console.Write("-A-");
                            break;
                        case BoardSpotStatuses.PlayerB:
                            Console.Write("-B-");
                            break;
                    }
                }
                Console.WriteLine();
            }

            for (int i = 0; i < GameBoard.BOARD_COLUMNS; i++)
            {
                Console.Write("___");
            }
            Console.WriteLine();
        }
    }
}
