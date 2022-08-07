using System;
using System.Text;
using ConsoleUserInterface;

namespace User_Interface
{
    internal class GameBoardView
    {
        private readonly char[,] m_GameBoard;

        public GameBoardView(GameBoard gb)
        {
            m_GameBoard = new char[gb.Width, gb.Length];

            /// for testing:
            for(int j = 0; j < m_GameBoard.GetLength(0); j++)
            {
                for (int i = 0; i < m_GameBoard.GetLength(1); i++)
                {
                    m_GameBoard[j, i] = 'K';
                }
            }
        }

        // console print the board
        public void ShowBoard()
        {
            Console.Write("    ");
            for (char c = 'A'; c < 'A' + m_GameBoard.GetLength(1); c++)
            {
                Console.Write(string.Format(@"{0}  ", c));
            }

            Console.WriteLine();
            Console.WriteLine(new StringBuilder().Append('=', m_GameBoard.GetLength(1) * 4));

            for (int j = 1; j <= m_GameBoard.GetLength(0); j++)
            {
                Console.Write(string.Format("{0}  |", j));
                for (int i = 0; i < m_GameBoard.GetLength(1); i++)
                {
                    Console.Write(string.Format("{0} |", m_GameBoard[j - 1, i]));
                }

                Console.WriteLine();
                Console.WriteLine(new StringBuilder().Append('=', m_GameBoard.GetLength(1) * 4));
                Console.WriteLine();
            }
        }
    }
}
