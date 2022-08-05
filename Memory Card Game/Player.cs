using System;
using System.Collections.Generic;

namespace Memory_Card_Game
{
    internal class Player
    {
        //public const string k_msgSlotIsTaken = "This slot {0} is taken";
        //public const string k_msgInvalidInput = "The slot {0} Does not exist";
        //public const string k_msgToAskForPlayerChoice = "Please choose a slot from the available slots\n.In the format: a capital letter for a column and then a number for a row(without a space),Like: E3 \nThen enter";

        private byte m_Score;
        private string m_Name;
        private readonly bool m_isHuman;
        // private AIPlayer m_AiPlayer;
        public Player(string i_Username)
        {
            m_Name = i_Username;
            m_Score = 0;
            //m_AiPlayer = null;
            m_isHuman = true;
        }

        public byte Score { get => m_Score; set => m_Score = value; }
        public void increaseScore(int i_add)
        {

        }
        public string Name { get => m_Name; set => m_Name = value; }

        public bool IsHuman
        {
            get { return m_isHuman; }
        }


        /// <summary>
        /// constructor for create Human Player 
        /// </summary>
        /// <param name="i_Username"></param>

        /// <summary>
        /// constructor for create AI
        /// </summary>
        /// <param name="i_AI"></param>
        public Player(bool i_AI)
        {
            m_Name = "pc";
            m_Score = 0;
            //m_AiPlayer = new AIPlayer();
            m_isHuman = false;
        }

        private class AIPlayer
        {
            internal static string getAIPlayerChoice()
            {
                return null;
            }
        }

        public string getPlayerChoice()
        {
            string str;
            if(IsHuman)
            {
                str = getHumanPlayerChoice();
            }
            else
            {
                // userInput
                str = AIPlayer.getAIPlayerChoice();
            }

            return str;
        }

        private string getHumanPlayerChoice()
        {
            return null;
        }

        // TODO: parameters ctor
        // TODO: play func 
        // TODO: nested class of AI player
    }
}
