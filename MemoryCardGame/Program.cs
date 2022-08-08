using System;
using ConsoleUserInterface;

namespace MemoryCardGame
{
    internal class Program
    {
        public static void Main()
        {
            Game<char> game = new Game<char>();
            game.Start();

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
