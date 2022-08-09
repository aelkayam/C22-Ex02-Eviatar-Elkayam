using System;
using System.Collections.Generic;
using ConsoleUserInterface;

namespace MemoryCardGame
{
    internal class Player
    {
        private readonly bool m_IsHuman;
        private byte m_Score;
        private string m_Name;

        // TODO => el : AIPlayer m_AiPlayer 
        private AIPlayer m_AiPlayer;
        public byte Score { get => m_Score; set => m_Score = value; }

        public void IncreaseScore()
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
        public Player()
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

        public string GetPlayerChoice(List<string> i_validSlotTOChase)
        {
            string returnChosice;

            if (IsHuman)
            {
                bool validInput;
                do
                {
                    returnChosice = UserInput.GetPlayerGameMove();
                    validInput = checkSlotAvailable(returnChosice, i_validSlotTOChase);
                    if (validInput)
                    {
                        Screen.ShowErrorMessage();
                    }
                }
                while (validInput);
            }
            else
            {
                // AI stuff
                returnChosice = AIPlayer.GetAIPlayerChoice(i_validSlotTOChase);
            }

            return returnChosice;
        }

        private static bool checkSlotAvailable(string i_slotForTest, List<string> i_validSlotTOChase)
        {
            bool slotFree = false;
            foreach (string slot in i_validSlotTOChase)
            {
                if (slot == i_slotForTest)
                {
                    slotFree = true;
                    break;
                }
            }

            return slotFree;
        }

        public override string ToString()
        {
            return string.Format(" ,{0} : {1} ", m_Name, m_Score);
        }

        // TODO: AI
        private class AIPlayer
        {
            private static Random m_Random = new Random();
            private List<string> m_Memory = new List<string>(); // string formt cher in card - slot  


            // m_Memory => sort if ther ar 2 cher thet 
            //


            public string? GetAIPlayerChoice_smert(List<string> i_validSlotTOChase)
            {
                foreach (string ch in i_validSlotTOChase)
                {
                    if (!m_Memory.Contains(ch))
                    {
                    }
                }

                return null;
            }
            
            // the not smart way 
            internal static string GetAIPlayerChoice(List<string> i_validSlotTOChase)
            {
                int rend = m_Random.Next(i_validSlotTOChase.Count);
                return i_validSlotTOChase[rend];
            }
        }

    }
}
