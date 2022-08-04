using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Card_Game
{
    public class Progaram<T>
    {
        public static void Main()
        {
            Game<T> game = new Game<T>();
            game.Start();
        }
    }
}
