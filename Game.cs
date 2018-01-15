using System;

namespace ConnectFour
{
    /// <summary>
    /// The Game class handles the lifecycle of a single instance of Connect Four.
    /// </summary>
    public class Game
    {
        private readonly GameBoard _board;
        private Players _currentPlayer = Players.Unknown;

        public GameBoard Board
        {
            get { return _board; }
        }

        public Players CurrentPlayer
        {
            get { return _currentPlayer; }
        }

        public Game()
        {
            _board = new GameBoard();
            Random rnd = new Random();
            _currentPlayer = rnd.Next(1, 2) == 1 ? Players.PlayerA : Players.PlayerB;
        }

        public void Start()
        {
            while (GameConsole.PrintGame(this))
            {
                SwitchPlayers();

                GameConsole.PrintGame(this);

                Int32 moveColumn = GameConsole.GetMove(this);
                if (moveColumn == -1)
                {
                    return;
                }

                _board.AddPlayerToColumn(_currentPlayer, moveColumn);
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private void SwitchPlayers()
        {
            if (_currentPlayer == Players.PlayerA)
                _currentPlayer = Players.PlayerB;
            else
                _currentPlayer = Players.PlayerA;
        }
    }
}
