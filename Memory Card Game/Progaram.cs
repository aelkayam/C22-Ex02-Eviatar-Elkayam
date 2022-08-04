using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Card_Game
{
    public class Program
    {
        public static void Main()
        {
            Game <char> game = new Game<char>();
            game.Start();
            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }
    }
}
