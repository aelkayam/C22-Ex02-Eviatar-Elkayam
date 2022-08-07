using System;
using ConsoleUserInterface;

namespace User_Interface
{
    internal class Testing
    {
        public static void Main()
        {

            UserInput.GetUserInitialInput(out string[] names, out bool versusNPC, out byte length, out byte width);
            Console.WriteLine("==========================================");
            Console.WriteLine("player1: " + names[0]);
            Console.WriteLine("player2: " + names[1]);
            Console.WriteLine("vs. NPC? " + versusNPC);
            Console.WriteLine("length: " + length);
            Console.WriteLine("width: " + width);
            Console.WriteLine("==========================================");
        

            GameBoardView gbv = new GameBoardView(new GameBoard(6, 6));
            gbv.ShowBoard();
            UserInput.GetPlayerGameMove(out byte row, out char col, new GameBoard(6, 6));
            Console.WriteLine("row: " + row);
            Console.WriteLine("col: " + col);

            Console.ReadLine();

        }
    }
}
