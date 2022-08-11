using System;
using System.Collections.Generic;
using ConsoleUserInterface;
using Game;

namespace MemoryCardGame
{
    internal class Player
    {
        private readonly bool m_IsHuman;
        private byte m_Score;
        public byte Score { get => m_Score; set => m_Score = value; }

        private string m_Name;
        public string Name { get => m_Name; set => m_Name = value; }

        private AIPlayer m_AiPlayer;

        /// <summary>
        /// constructor for create Human Player
        /// </summary>
        /// <param name="i_Username"></param>

        /// <summary>
        /// constructor for create AI
        /// </summary>
        public Player()
        {
            m_Name = "PC";
            m_Score = 0;
            m_IsHuman = false;
            m_AiPlayer = null; // TODO: meke it NUlllabul 
        }

        public Player(string i_Name)
        {
            Score = 0;
            m_IsHuman = true;
            Name = i_Name;
            m_AiPlayer = new AIPlayer();
        }

        public void IncreaseScore(byte i_scoreInTheTurn)
        {
            this.m_Score += i_scoreInTheTurn;
        }


        public bool IsHuman
        {
            get { return m_IsHuman; }
        }


        public string GetPlayerChoice(List<string> i_validSlotTOChase)
        {
            string returnChosice;

            if (IsHuman)
            {
                 returnChosice = UserInput.GetPlayerGameMove();
            }
            else
            {
                // AI stuff
                returnChosice = AIPlayer.GetAIPlayerChoice(i_validSlotTOChase);
            }

            return returnChosice;
        }

        public void showBoard(char[,] m_GameBoard)
        {
            if (!IsHuman)
            {
                m_AiPlayer.showBoard(m_GameBoard);
            }
        }


        /// <summary>
        /// chang name 
        /// </summary>
        public void restartNewGame()
        {
            if(!IsHuman)
            {
                m_AiPlayer.resetMemory();
            }
        }

        public override string ToString()
        {
            return string.Format("{0} : {1} ", m_Name, m_Score);
        }

        private class AIPlayer
        {
            private static Random m_Random = new Random();
            private List<string> m_Memory = new List<string>();

            public AIPlayer( List<string> memory)
            {
                Memory = memory;
            }
            public AIPlayer() : this(new List<string>()) { }


            public List<string> Memory
            { 
                get 
                { 
                    return m_Memory;
                } 
                set 
                {
                    m_Memory = value;
                } 
            }

            public void resetMemory()
            {
                Memory.Clear();
            }
            public  void  showBoard(char[,] m_GameBoard)
            {
                byte row = 0;
                byte col = 0;

                 foreach (char ch in m_GameBoard)
                {
                    if(col < m_GameBoard.Rank)
                    {
                        col = 0;
                        row++;
                    }

                    if(ch != ' ')
                    {
                        string str = string.Format("{0} {1}{2}", ch, GameLogic.ABC[col], row);
                        if (m_Memory.Contains(str))
                        {
                            m_Memory.Add(str);
                        }
                    }

                    col++;
                    
                }
            }

            public string GetAIPlayerChoiceSmart(List<string> i_validSlotTOChase)
            {

                ///  1st 
                ///  pc : A , B = I C , A = pc :
                m_Memory.Sort();// 

                //2nd
                // A = A   

                foreach (string ch in i_validSlotTOChase)
                {
                    if (!m_Memory.Contains(ch))
                    {

                    }
                }

                return null;
            }

            internal static string GetAIPlayerChoice(List<string> i_validSlotTOChase)
            {
                int randomTile = m_Random.Next(i_validSlotTOChase.Count);
                return i_validSlotTOChase[randomTile];
            }
        }

        // TODO: parameters ctor
        // TODO: play function
        // TODO: nested class of AI player
    }
}
