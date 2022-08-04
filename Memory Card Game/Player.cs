using System;
using System.Collections.Generic;

namespace Memory_Card_Game
{
    internal class Player
    {
        public const string k_msgSlotIsTaken = "This slot {0} is taken";
        public const string k_msgInvalidInput = "The slot {0} Does not exist";
        public const string k_msgToAskForPlayerChoice = "Please choose a slot from the available slots\n.In the format: a capital letter for a column and then a number for a row(without a space),Like: E3 \nThen enter";


        private byte m_Score;
        private string m_Name;
        private readonly bool m_isHuman;

        // TODO: parameters ctor
        // TODO: play func 
        // TODO: nested class of AI player
    }
}
