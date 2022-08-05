using System;
using System.Collections.Generic;

namespace Memory_Card_Game
{
    // TODOS
    // TODO:1.Create a function that returns an array of char for printing
    // TODO:2.Create a variable that says how many face down cards are in the pack
    // TODO:3 If there is an even number of face-down cards and checks if there are any cards that do not have a pair
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

        private readonly int km_NumOfRows;
        private readonly int km_NumOfCols;
        private Card[,] m_GameBoard;

         // TODO: find a good name
        public GameBoard(int i_height, int i_width)
        {
            this.km_NumOfRows = i_height;
            this.km_NumOfCols = i_width;
            m_GameBoard = new Card[i_height, i_width];
.
            char[] chars = new char[Length];

            // TODOS
            // TODO:1.Create an array of values that are going to be entered m_GameBoard
            // TODO:2.ShuffleCard the array => finish the function ShuffleCard
            // TODO:3.value for each of the cards
        }

        public int Length
        {
            get
            {
                return km_NumOfRows * km_NumOfCols;
            }
         }

        /// indexsr
        public char this[string i_indexFormt]
        {
            get
            {
                configIndexFormt(i_indexFormt, out int io_rowIndex, out int io_colIndex);
                return m_GameBoard[io_rowIndex, io_colIndex].Value;
            }

            private set
            {
                configIndexFormt(i_indexFormt, out int io_rowIndex, out int io_colIndex);
                m_GameBoard[io_rowIndex, io_colIndex] = new Card(value);
            }
        }

        /// func to Shuffle arrey the cher arry befuer creat
        /// <exception cref="ArgumentNullException"></exception>
        private char[] ShuffleCard(char[] i_charArrToShuffle)
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
            Random random = new Random(0);
            return random.Next(from, to);
        }

        /// func to get a arry of all the Hidden Cards
        public List<string> FindAllNonHiddenCards()
        {
            List<string> indexOfAllHiddenCards = new List<string>();
            foreach (Card card in this.m_GameBoard)
            {
                if (card.Flipped)
                {
                    indexOfAllHiddenCards.Add("need to add ");
                }
            }

            return indexOfAllHiddenCards;
        }

        /// func to config the index from the formt
        /// <exception cref="IndexOutOfRangeException"></exception>
        private void configIndexFormt(string i_IndexFormt, out int io_rowIndex, out int io_colIndex)
        {
            io_rowIndex = 0;
            bool isUpper = false;
            char charToFindTheIndex = i_IndexFormt[0];
            string subStrOfNum = i_IndexFormt.Substring(0, i_IndexFormt.Length - 1);
            bool isSuccessTryParse = int.TryParse(subStrOfNum, out io_colIndex);

            for (int i = 0; i < ABC.Length; i++)
            {
                if (charToFindTheIndex == ABC[i])
                {
                    io_colIndex = i;
                    isUpper = true;
                    break;
                }
            }

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

        internal struct Card
        {
            private const string km_formatToPrint = " {} |";
            private const char m_default = ' ';
            private char m_Value;
            private bool m_Flifpped;

            public Card(char value, bool flipped)
                : this()
             {
                m_Value = value;
                m_Flifpped = flipped;
             }

            public Card(char value)
                : this()
            {
                m_Value = value;
                m_Flifpped = false;
            }

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
                    return m_Flifpped;
                }

                set
                {
                    m_Flifpped = value;
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

            public bool Equals(Card i_comperTo)
            {
                return this.Value == i_comperTo.Value;
            }

            // TODO: add parameter ctor
            // TODO: add show func (send to interface)  XX get
            // TODO: implement operator==
        }
    }
}
