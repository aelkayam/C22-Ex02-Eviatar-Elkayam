using System;
using System.Collections.Generic;
using ConsoleUserInterface;

namespace MemoryCardGame
{
    internal class Player
    {
        // public const string k_msgSlotIsTaken = "This slot {0} is taken";
        // public const string k_msgInvalidInput = "The slot {0} Does not exist";
        // public const string k_msgToAskForPlayerChoice = "Please choose a slot from the available slots\n.In the format: a capital letter for a column and then a number for a row(without a space),Like: E3 \nThen enter";
        private byte m_Score;
        private string m_Name;
        private readonly bool m_IsHuman;

        // private AIPlayer m_AiPlayer;
        public byte Score { get => m_Score; set => m_Score = value; }

        public void increaseScore()
        {
            this.m_Score++;
        }

        public string Name { get => m_Name; set => m_Name = value; }

        public bool IsHuman
        {
            get { return m_IsHuman; }
        }

        /// <summary>
        /// constructor for create Human Player 
        /// </summary>
        /// <param name="i_Username"></param>

        /// <summary>
        /// constructor for create AI
        /// </summary>
        public Player(bool i_AI)
        {
            m_Name = "PC";
            m_Score = 0;
            m_IsHuman = false;
        }

        public Player(string i_Name)
        {
            Score = 0;
            m_IsHuman = true;
            Name = i_Name;
        }

        public string getPlayerChoice(List<string> i_validSlotTOChase, GameBoard gameBoard)
        {
            string returnChosice;

            if (IsHuman)
            {
                // use UserInput here
                returnChosice = UserInput.GetPlayerGameMove(out byte row, out char col, gameBoard.GetCard());
            }
            else
            {
                // ai stuff
                returnChosice = AIPlayer.getAIPlayerChoice(i_validSlotTOChase);
            }

            return returnChosice;
        }

        //private string getHumanPlayerChoice(List<string> i_validSlotTOChase)
        //{
        //    bool isChiseVlid = false;
        //    string humanChose;
        //    do
        //    {
        //        humanChose = User_Interface.UserInput.getInSpu();
        //        foreach (string ch in i_validSlotTOChase)
        //        {
        //            if (ch == humanChose)
        //            {
        //                isChiseVlid = true;
        //                break;
        //            }
        //        }
        //        if (!isChiseVlid)
        //        {
        //            screen.showMsgEror("The slot is already taken");
        //        }
        //    }
        //    while (isChiseVlid);

        //    return humanChose;
        //}

        public override string ToString()
        {
            return string.Format(" ,{0} : {1} ", m_Name, m_Score);
        }

        private class AIPlayer
        {
            private static Random m_Random = new Random();
            private List<string> m_Memory = new List<string>();

            public string getAIPlayerChoice_smert(List<string> i_validSlotTOChase)
            {
                foreach (string ch in i_validSlotTOChase)
                {
                    if (!m_Memory.Contains(ch))
                    {

                    }
                }

                return null;
            }

            internal static string getAIPlayerChoice(List<string> i_validSlotTOChase)
            {
                int rend = m_Random.Next(i_validSlotTOChase.Count);
                return i_validSlotTOChase[rend];
            }
        }

        // TODO: parameters ctor
        // TODO: play function 
        // TODO: nested class of AI player
    }
}
