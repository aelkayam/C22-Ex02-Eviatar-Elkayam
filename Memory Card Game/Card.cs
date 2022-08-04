using System;
using System.Collections.Generic;

namespace Memory_Card_Game
{
    internal struct Card<T>
    {
        private const string km_formatToPrint = " {} |";
        private const char km_blankCard = ' ';
        private T m_Value;

        public T Value
        {
            get
            {
                T retunValue = default(T);
                if (Flipped)
                {
                    retunValue = m_Value;
                }

                return retunValue;
            }
        }

        public bool Flipped { get; set; }

        public static bool operator ==(Card<T> i_card1, Card<T> i_card2)
        {
            //return i_card1.Value == i_card2.Value;
            return i_card1.Equals(i_card2);
        }

        public static bool operator !=(Card<T> i_card1, Card<T> i_card2)
        {
            //return! (i_card1.m_Value != i_card2.m_Value);
            return !i_card1.Equals(i_card2);
        }


        // TODO: add parameter ctor 
        // TODO: add show func (send to interface)  XX get 
        // TODO: implement operator==
    }
}
