using System;
using ConsoleUserInterface;

namespace MemoryCardGame
{
    internal class Program
    {
        // TODO  finshe: chang the acssbily modify in the progect 
        // TODO => el: add comitis 
        public static void Main()
        {
            Game<char> game = new Game<char>();
            game.Start();

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
