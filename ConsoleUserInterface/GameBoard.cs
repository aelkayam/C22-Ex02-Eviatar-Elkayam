using System;
using System.Collections.Generic;
using Ex02.ConsoleUtils;

namespace ConsoleUserInterface
{

    internal class GameBoard
    {
        private byte m_Length;
        private byte m_Width;

        public GameBoard(byte Length, byte width)
        {
            m_Length = Length;
            m_Width = width;
            
        }

        public byte Length
        {
            get { return m_Length; }
        }
        public byte Width
        {
            get { return m_Width; }
        }

        

            // TODO: show screen
        // TODO: clear screen
        // TODO: show message
        // TODO: show game board
    }
}
