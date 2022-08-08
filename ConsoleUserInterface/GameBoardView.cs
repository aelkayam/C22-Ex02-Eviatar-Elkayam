using System;
using System.Text;
using Ex02.ConsoleUtils;

namespace ConsoleUserInterface
{
    public static class GameBoardView
    {
        // console print the board
        public static void ShowBoard(char[,] i_GameBoard)
        {
            Console.Write("    ");
            for (char c = 'A'; c < 'A' + i_GameBoard.GetLength(1); c++)
            {
                Console.Write(string.Format(@"{0}  ", c));
            }

            Console.WriteLine();
            Console.WriteLine(new StringBuilder().Append('=', i_GameBoard.GetLength(1) * 4));

            for (int j = 1; j <= i_GameBoard.GetLength(0); j++)
            {
                Console.Write(string.Format("{0}  |", j));
                for (int i = 0; i < i_GameBoard.GetLength(1); i++)
                {
                    Console.Write(string.Format("{0} |", i_GameBoard[j - 1, i]));
                }

                Console.WriteLine();
                Console.WriteLine(new StringBuilder().Append('=', i_GameBoard.GetLength(1) * 4));
                Console.WriteLine();
            }
        }

        public static void ShowMessage(string i_Message)
        {
            Console.WriteLine(i_Message);
        }

        public static void ShowErrorMessage()
        {
            // add more options
            ShowMessage("error, try again");
        }

        // clear console
        public static void ClearBoard()
        {
            Screen.Clear();
        }

        public static void QuitMessage()
        {
            ShowMessage("GoodBye!");
        }
    }
}
