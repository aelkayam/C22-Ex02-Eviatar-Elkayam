using System;
using System.Collections.Generic;

namespace MemoryCardGame
{
    // TODOS
    // TODO:1.Create a function that returns an array of char for printing
    // TODO:2.Create a variable that says how many face down cards are in the pack
    // TODO:3 If there is an even number of face-down cards and checks if there are any cards that do not have a pair
    // TODO: find a good name
    public class GameBoard
    {
        private static readonly char[] ABC =
        {
            'A',
            'B',
            'C',
            'D',
            'E',
            'F',
            'G',
            'H',
            'I',
            'J',
            'K',
            'L',
            'M',
            'N',
            'O',
            'P',
            'Q',
            'R',
            'S',
            'T',
            'U',
            'V',
            'W',
            'X',
            'Y',
            'Z',
        };

        private readonly byte km_NumOfRows;
        private readonly byte km_NumOfCols;
        private Card[,] m_GameBoard;

        /// <summary>
        /// constructor
        /// </summary>
        public GameBoard(byte i_height, byte i_width)
        {
            // TODOS
            // TODO:1.Create an array of values that are going to be entered m_GameBoard
            // TODO:2.ShuffleCard the array => finish the function ShuffleCard
            // TODO:3.value for each of the cards
            this.km_NumOfRows = i_width;
            this.km_NumOfCols = i_height;

            char[] chars = new char[km_NumOfRows * km_NumOfCols];
            for (byte j = 0; j < chars.Length; j++)
            {
                chars[j] = getCharForSlat(j);
            }

            foreach (char c in chars)
            {
                Console.Write(c + " ");
            }

            ShuffleCard(ref chars);
            foreach (char c in chars)
            {
                Console.Write(c + " ");
            }

            m_GameBoard = new Card[km_NumOfRows, km_NumOfCols];
            byte indexInChars = 0;
            for (int i = 0; i < km_NumOfRows; i++)
            {
                for (int j = 0; j < km_NumOfCols; j++)
                {
                    m_GameBoard[i, j] = new Card(chars[indexInChars++], false);
                }
            }
        }

        // ===================================================================
        // methods that the constructor uses
        // ===================================================================
        private char getCharForSlat(byte i_index)
        {
            return ABC[i_index >> 1];
        }

        /// func to Shuffle arrey the cher arry befuer creat
        /// need to chng to any arry 
        /// <exception cref="ArgumentNullException"></exception>
        private char[] ShuffleCard(ref char[] i_charArrToShuffle)
        {
            int len = i_charArrToShuffle.Length;
            if (len == 0)
            {
                throw new ArgumentNullException(nameof(i_charArrToShuffle));
            }

            for (int s = 0; s < i_charArrToShuffle.Length - 1; s++)
            {
                int indexOfnewValueFor_s = generateAnotherNum(s, len); // pleace, note the range

                // swap procedure: note, char h to store initial i_charArrToShuffle[s] value
                char currentValueInIndex_s = i_charArrToShuffle[s];
                i_charArrToShuffle[s] = i_charArrToShuffle[indexOfnewValueFor_s];
                i_charArrToShuffle[indexOfnewValueFor_s] = currentValueInIndex_s;
            }

            return i_charArrToShuffle;
        }

        /// Let unknown GenerateAnotherNum be a random
        private int generateAnotherNum(int from, int to)
        {
            Random random = new Random();
            return random.Next(from, to);
        }

        // ===================================================================
        // Properties
        // ===================================================================

        /// <summary>
        /// Length is the total number of slots in the array
        /// </summary>
        public int Length
        {
            get
            {
                return km_NumOfRows * km_NumOfCols;
            }
        }

        public Card[,] GetCard()
        {
            return m_GameBoard;
        }

        /// indexer:
        public Card this[string i_indexFormt]
        {
            get
            {
                configIndexFormat(i_indexFormt, out int io_rowIndex, out int io_colIndex);
                return m_GameBoard[io_rowIndex, io_colIndex];
            }

            set
            {
                configIndexFormat(i_indexFormt, out int io_rowIndex, out int io_colIndex);
                m_GameBoard[io_rowIndex, io_colIndex] = value;
            }
        }

        public void Flipped(string i_index, bool i_Value)
        {
            Card c = this[i_index];
            c.Flipped = i_Value;
            this[i_index] = c;
        }

        public bool DoThePlayersChoicesMatch(string i_firstPlayerChoices, string i_SecondPlayerChoices)
        {
            bool isPair = this[i_firstPlayerChoices] == this[i_SecondPlayerChoices];
            if (!isPair)
            {
                Flipped(i_firstPlayerChoices, false);
                Flipped(i_SecondPlayerChoices, false);
            }

            return isPair;
        }

        // ===================================================================
        // methods that use to drow the bord
        // ===================================================================
        public char[,] getBoardToDraw()
        {
            char[,] boardToDraw = new char[km_NumOfRows, km_NumOfCols];
            foreach (Card card in m_GameBoard)
            {
                // how to do it
                boardToDraw[1, 0] = card.Value;
            }

            return boardToDraw;
        }

        // ===================================================================
        // methods that use to to Select a new slot
        // ===================================================================
        public List<string> getAllValidSlotsForChoice()
        {
            List<string> validSlots = new List<string>();
            for (int i = 0; i < km_NumOfRows; i++)
            {
                for (int j = 0; j < km_NumOfCols; j++)
                {
                    bool isCardFlip = m_GameBoard[i, j].Flipped;
                    if (isCardFlip)
                    {
                        validSlots.Add(string.Format("{0}{1}", ABC[i], j));
                    }
                }
            }

            return validSlots;
        }

        public bool checkForNewPair()
        {
            return false;
        }

        /// func to config the index from the formt
        /// <exception cref="IndexOutOfRangeException"></exception>
        private void configIndexFormat(string i_IndexFormt, out int io_rowIndex, out int io_colIndex)
        {
            io_rowIndex = 0;
            bool isUpper = false;
            char charToFindTheIndex = i_IndexFormt[0];
            string subStrOfNum = i_IndexFormt.Substring(0, i_IndexFormt.Length - 1);
            bool isSuccessTryParse = int.TryParse(subStrOfNum, out io_colIndex);

            // check if col exists
            for (int i = 0; i < ABC.Length; i++)
            {
                if (charToFindTheIndex == ABC[i])
                {
                    io_colIndex = i;
                    isUpper = true;
                    break;
                }
            }

            //isUpper = char.IsLetter(charToFindTheIndex)

            if (!isSuccessTryParse || !isUpper)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            bool isInvalueRow = io_rowIndex <= 0 || io_rowIndex >= km_NumOfRows;
            bool isInvalueCol = io_colIndex <= 0 || io_colIndex >= km_NumOfCols;

            if (!isInvalueRow || !isInvalueCol)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }
        }

        public struct Card
        {
            private const string km_formatToPrint = " {} |";
            private const char m_default = ' ';

            private char m_Value;
            private bool m_Flipped;

            /// constructor
            public Card(char value, bool flipped)
            {
                m_Value = value;
                m_Flipped = flipped;
            }

            public Card(char value)
                : this()
            {
                m_Value = value;
                m_Flipped = false;
            }

            // ===================================================================
            // Properties
            // ===================================================================
            public char Value
            {
                get
                {
                    char retunValue = m_default;
                    if (Flipped)
                    {
                        retunValue = m_Value;
                    }

                    return retunValue;
                }

                set
                {
                    m_Value = value;
                }
            }

            public bool Flipped
            {
                get
                {
                    return m_Flipped;
                }

                set
                {
                    m_Flipped = value;
                }
            }

            public static bool operator ==(Card i_card1, Card i_card2)
            {
                return i_card1.Equals(i_card2);
            }

            public static bool operator !=(Card i_card1, Card i_card2)
            {
                return !i_card1.Equals(i_card2);
            }

            public override bool Equals(object i_comperTo)
            {
                return this.Value == ((Card)i_comperTo).Value;
            }

            // delete maybe
            public override int GetHashCode()
            {
                int hashCode = 1148891178;
                hashCode = hashCode * -1521134295 + m_Value.GetHashCode();
                hashCode = hashCode * -1521134295 + m_Flipped.GetHashCode();
                hashCode = hashCode * -1521134295 + Value.GetHashCode();
                hashCode = hashCode * -1521134295 + Flipped.GetHashCode();
                return hashCode;
            }

            // TODO: add show func (send to interface)  XX get (??)
        }
    }
}
