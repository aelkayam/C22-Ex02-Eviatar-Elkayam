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
                bool validInput;
                do
                {
                    returnChosice = UserInput.GetPlayerGameMove();
                    validInput = checkSlotAvailable(returnChosice, i_validSlotTOChase);
                    if (validInput)
                    {
                        Screen.ShowError(eErrorType.CardTaken);

                        // Console.WriteLine("try Again");
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

        private static bool checkSlotAvailable(string i_slotForTest, List<string> i_ValidSlotToChoose)
        {
            bool slotFree = false;
            foreach (string slot in i_ValidSlotToChoose)
            {
                if (slot == i_slotForTest)
                {
                    slotFree = true;
                    break;
                }
            }

            return slotFree; //TODO:  rename 
        }
        public void showBoard(Game.GameLogic m_GameBoard)
        {
            if (!IsHuman)
            {
                m_AiPlayer.showBoard(m_GameBoard);
            }
        }

        public void restartNewGame()
        {
            if(!IsHuman)
            {
                m_AiPlayer.resetMemory();
            }
        }

        public override string ToString()
        {
            return string.Format(" ,{0} : {1} ", m_Name, m_Score);
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
            public  void  showBoard(Game.GameLogic m_GameBoard)
            {

            }

            public string GetAIPlayerChoiceSmart(List<string> i_validSlotTOChase)
            {
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
