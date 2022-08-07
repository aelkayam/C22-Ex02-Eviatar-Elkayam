using System;
using ConsoleUserInterface;

namespace User_Interface
{
    internal class Testing
    {
        public static void Main()
        {
            // initial set up
            UserInput.GetUserInitialInput(out string[] names, out bool versusNPC, out byte length, out byte width);
            Console.WriteLine("==========================================");
            Console.WriteLine("player1: " + names[0]);
            Console.WriteLine("player2: " + names[1]);
            Console.WriteLine("vs. NPC? " + versusNPC);
            Console.WriteLine("length: " + length);
            Console.WriteLine("width: " + width);
            Console.WriteLine("==========================================");

            /// showing board and getting user move
            GameBoard gb = new GameBoard(width, length);
            GameBoardView gbv = new GameBoardView(gb);
            gbv.ShowBoard();
            UserInput.GetPlayerGameMove(out byte row, out char col, gb);
            Console.WriteLine("row: " + row);
            Console.WriteLine("col: " + col);

            Console.ReadLine();
        }
    }
}
