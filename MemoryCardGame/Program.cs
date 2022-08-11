using System;
using ConsoleUserInterface;

namespace MemoryCardGame
{
    internal class Program
    {
        // TODO: check if some of the departments are not 4
        // TODO  finshe: chang the acssbily modify in the progect 
        // TODO => el: add comitis 
        public static void Main()
        {
            GameEngine<char> game = new GameEngine<char>();
            game.Start();

        }
    }
}
