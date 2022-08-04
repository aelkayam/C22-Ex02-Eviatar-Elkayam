using System;
using System.Collections.Generic;

namespace Memory_Card_Game
{
    internal struct Card<T>
    {
        private T m_Value;
        private bool m_Flipped;

        public bool Flipped
        {
            get { return m_Flipped; }
            set { m_Flipped = value; }
        }

        // TODO: add parameter ctor
        // TODO: add show func (send to interface)
        // TODO: implement operator==
    }
}
