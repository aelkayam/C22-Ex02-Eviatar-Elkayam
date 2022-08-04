using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Card_Game
{
    // add user interface and the guy's DLL
    internal class Game<T>
    {
        private const int km_maxSizeCardBoard = 6;
        private const int km_minSizeCardBoard = 4;

        private Card<T>[] m_CardBoard;
        private byte m_TotalPLayers;
        private int m_TurnCounter;
        private bool m_isPlaying;
        private Player[] m_CurrentPlayers;

        private byte m_FlippedCardsCounter;

        public Game()
        {
            // TODO: call User input from User and get stuff

            // TODO: init m_TotalPLayers
            // TODO: init m_TurnCounter
            // TODO: init m_CurrentPlayers
            // TODO: init m_isPlaying
            // TODO: init m_CardBoard
        }


        // TODO: shuffle cards func

        private bool isRunning()
        {
            return m_isPlaying || m_FlippedCardsCounter == m_CardBoard.Length;
        }

        private int getPlayerIndex()
        {
            return m_TurnCounter % m_TotalPLayers;
        }

        public void Start()
        {

        }
    }
}
