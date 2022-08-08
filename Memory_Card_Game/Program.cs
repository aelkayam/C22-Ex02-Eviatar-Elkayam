using System;
using ConsoleUserInterface;

namespace MemoryCardGame
{
    internal class Program
    {
        public static void Main()
        {
            //Game<T> game = new Game<T>();
            //game.Start();

            // initial set up
            UserInput.GetUserInitialInput(out string[] names, out bool versusNPC, out byte length, out byte width);
            Console.WriteLine("==========================================");
            Console.WriteLine("player1: " + names[0]);
            Console.WriteLine("player2: " + names[1]);
            Console.WriteLine("vs. NPC? " + versusNPC);
            Console.WriteLine("length: " + length);
            Console.WriteLine("width: " + width);
            Console.WriteLine("==========================================");

            /// showing board and getting user move and clear
            GameBoard gb = new GameBoard(width, length);

            GameBoardView.ShowBoard(gb);
            UserInput.GetPlayerGameMove(out byte row, out char col, gb);
            Console.WriteLine("row: " + row);
            Console.WriteLine("col: " + col);
            gbv.ClearBoard();

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
