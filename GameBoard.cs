using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    /// <summary>
    /// The GameBoard class stores the status for each spot on the gameboard,
    /// updates the game board based on user moves, and determines if the game
    /// has been won.
    /// </summary>
    public class GameBoard
    {
        public const int BOARD_ROWS = 6;
        public const int BOARD_COLUMNS = 7;

        private List<List<BoardSpotStatuses>> _board = new List<List<BoardSpotStatuses>>();

        public GameBoard()
        {
            for (int row = 0; row < BOARD_ROWS; row++)
            {
                _board.Add(new List<BoardSpotStatuses>());
                for (int column = 0; column < BOARD_COLUMNS; column++)
                {
                    _board[row].Add(BoardSpotStatuses.Blank);
                }
            }
        }

        public BoardSpotStatuses GetSpot(int row, int column)
        {
            if ((_board.Count - 1) >= row && ((_board[row].Count - 1) >= column))
                return _board[row][column];
            else
                throw new ArgumentException("Invalid game board spot specified.");
        }

        public bool IsValidColumnForMove(int column)
        {
            if (column >= 0 && column < BOARD_COLUMNS)
            {
                if (GetSpot(BOARD_ROWS - 1, column) == BoardSpotStatuses.Blank)
                    return true;
            }
            return false;
        }
        private void SetSpot(int row, int column, BoardSpotStatuses status)
        {
            if (row >= 0 && row < BOARD_ROWS && column >= 0 && column < BOARD_COLUMNS)
            {
                _board[row][column] = status;
            }
            else
                throw new ArgumentException("Invalid game board spot specified.");
        }

        public int AddPlayerToColumn(Players player, int column)
        {
            for (int i = 0; i < BOARD_ROWS; i++)
            {
                if (GetSpot(i, column) == BoardSpotStatuses.Blank)
                {
                    SetSpot(i, column, player == Players.PlayerA ? BoardSpotStatuses.PlayerA : BoardSpotStatuses.PlayerB);
                    return i;
                }
            }

            return -1;
        }

        public bool CheckForTie()
        {
            for (int i = 0; i < BOARD_COLUMNS; i++)
            {
                if (IsValidColumnForMove(i))
                {
                    return false;
                }
            }

            return true;
        }

        public bool CheckForWin()
        {
            for (int row = 0; row < BOARD_ROWS; row++)
            {
                for (int column = 0; column < BOARD_ROWS; column++)
                {
                    if (CheckForWin(row, column))
                        return true;
                }
            }

            return false;
        }

        private bool CheckForWin(int row, int column)
        {
            bool couldBeRight = column <= BOARD_COLUMNS - 4;
            bool couldBeLeft = column > 3;
            bool couldBeUp = row <= BOARD_ROWS - 4;
            bool couldBeDown = row > 3;

            BoardSpotStatuses[] rightSpots = couldBeRight ? _board[row].GetRange(column, 4).ToArray() : null;
            BoardSpotStatuses[] leftSpots = couldBeLeft ? _board[row].GetRange(column - 4, 4).ToArray() : null;
            BoardSpotStatuses[] downSpots = couldBeDown ? new BoardSpotStatuses[] { GetSpot(row, column), GetSpot(row - 1, column), GetSpot(row - 2, column), GetSpot(row - 3, column) } : null;
            BoardSpotStatuses[] upSpots = couldBeUp ? new BoardSpotStatuses[] { GetSpot(row, column), GetSpot(row + 1, column), GetSpot(row + 2, column), GetSpot(row + 3, column) } : null;

            return CheckSpotsForWin(rightSpots) || CheckSpotsForWin(leftSpots) || CheckSpotsForWin(downSpots) || CheckSpotsForWin(upSpots);
        }

        private static bool CheckSpotsForWin(BoardSpotStatuses[] spots)
        {
            if (spots == null || spots.Length != 4)
                return false;

            if (spots[0] == BoardSpotStatuses.Blank)
                return false;

            for (int i = 1; i < spots.Length; i++)
            {
                if (spots[i] != spots[0])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
