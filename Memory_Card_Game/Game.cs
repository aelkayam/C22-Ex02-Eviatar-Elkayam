using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User_Interface;

namespace Memory_Card_Game
{
    // add user interface and the guy's DLL
    internal class Game<T>
    {
        private const int km_maxSizeCardBoard = 6;
        private const int km_minSizeCardBoard = 4;
        public readonly int Km_numOfRow;
        public readonly int Km_numOfCol;
        private GameBoard m_CardBoard;
        private byte m_TotalPLayers;
        private int m_TurnCounter;
        private bool m_isPlaying;
        private Player[] m_CurrentPlayers; // TODO: find a good name
        private byte m_FlippedCardsCounter;

        public Game()
        {
            Km_numOfRow = 0;
            Km_numOfCol = 0;
            m_CardBoard = new GameBoard(Km_numOfRow, Km_numOfCol);
            m_TotalPLayers = 0;
            m_TurnCounter = 0;
            m_isPlaying =false;
            m_CurrentPlayers = null;
            m_FlippedCardsCounter = 0;

        // TODO: call User input from User and get stuff
        // TODO: init m_TotalPLayers
        // TODO: init m_TurnCounter
        // TODO: init m_CurrentPlayers
        // TODO: init m_isPlaying
        // TODO: init m_CardBoard
    }

        // TODO: shuffle cards func
        // TODO: add preFix to the maxtix
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
            m_isPlaying = true;

            // add msg
            do
            {
                /// 1St player  choice
                // srceen show bord
                // screen show mssg
                int indexInPlayersArr = getPlayerIndex();
                string indexChoice = m_CurrentPlayers[indexInPlayersArr].getPlayerChoice();
                char ch = m_CardBoard[indexChoice]; // do

                //
                // m_CurrentPlayers[indexInPlayersArr].showChang();
                // sleep 
                // srceen show bord
                // screen show mssg

                /// 2nd player  choice
                indexChoice = m_CurrentPlayers[indexInPlayersArr].getPlayerChoice();

                // check is scor go up
                // int scor = m_CurrentPlayers.check();
                // m_CurrentPlayers[indexInPlayersArr].addScor(scor);

                /// end of indexInPlayersArr turn 
                m_TurnCounter++;
            }
            while (isRunning());

            // print scor playrs
        }
    }
}
