using System;
using System.Collections.Generic;

namespace MemoryCardGame
{
    // TODOS
    // TODO: Create a function that returns an array of char for printing
    // TODO: Create a variable that says how many face down cards are in the pack
    // TODO: If there is an even number of face-down cards and checks if there are any cards that do not have a pair
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
        private readonly Card[,] m_GameBoard;

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

            Console.WriteLine();
            shuffleCard(ref chars);
            foreach (char c in chars)
            {
                Console.Write(c + " ");
            }

            Console.WriteLine();

            m_GameBoard = new Card[km_NumOfRows, km_NumOfCols];
            byte indexInChars = 0;
            for (int i = 0; i < km_NumOfRows; i++)
            {
                for (int j = 0; j < km_NumOfCols; j++)
                {
                    m_GameBoard[i, j] = new Card(chars[indexInChars++], false);
                    Console.Write(string.Format("name {0}  val: {1}||", m_GameBoard[i, j], m_GameBoard[i, j].Value));
                }

                Console.WriteLine();
            }
        }

        // ===================================================================
        // methods that the constructor uses
        // ===================================================================
        private char getCharForSlat(byte i_index)
        {
            return ABC[i_index >> 1];
        }

        /// function to Shuffle array the char array before creation
        /// need to change to any array
        /// <exception cref="ArgumentNullException"></exception>
        private char[] shuffleCard(ref char[] i_charArrToShuffle)
        {
            int len = i_charArrToShuffle.Length;
            if (len == 0)
            {
                throw new ArgumentNullException(nameof(i_charArrToShuffle));
            }

            for (int s = 0; s < i_charArrToShuffle.Length - 1; s++)
            {
                int indexOfnewValueFor_s = generateAnotherNum(s, len); // note the range

                // swap procedure: note, char h to store initial i_charArrToShuffle[s] value
                (i_charArrToShuffle[indexOfnewValueFor_s], i_charArrToShuffle[s]) = (i_charArrToShuffle[s], i_charArrToShuffle[indexOfnewValueFor_s]);
            }

            return i_charArrToShuffle;
        }

        /// Let unknown GenerateAnotherNum be a random number
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
            Console.WriteLine(string.Format("Flipped the index {0}  to {1} , the Card val: {2} ", i_index, i_Value, this[i_index].Flipped));
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
        // methods that use to draw the board
        // ===================================================================
        public char[,] GetBoardToDraw()
        {
            char[,] boardToDraw = new char[km_NumOfRows, km_NumOfCols];
            for (int i = 0; i < km_NumOfRows; i++)
            {
                for (int j = 0; j < km_NumOfCols; j++)
                {
                    boardToDraw[i, j] = m_GameBoard[i, j].Value;
                }
            }

            return boardToDraw;
        }

        // ===================================================================
        // methods that are used to select a new tile
        // ===================================================================
        public List<string> GetAllValidTilesForChoice()
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

        public bool CheckForNewPair()
        {
            return false;
        }

        /// function to configure the index from the format
        /// <exception cref="IndexOutOfRangeException"></exception>
        private void configIndexFormat(string i_IndexFormt, out int io_rowIndex, out int io_colIndex)
        {
            io_colIndex = 0;
            bool isUpper = false;
            char charToFindTheIndex = i_IndexFormt[0];
            char charToReplaceTheIndex = i_IndexFormt[1];
            bool isSuccessTryParse = int.TryParse(charToReplaceTheIndex.ToString(), out io_rowIndex);
            io_rowIndex--;

            // check if col exists
            for (int i = 0; i < km_NumOfCols; i++)
            {
                if (charToFindTheIndex == ABC[i])
                {
                    io_colIndex = i;
                    isUpper = true;
                    break;
                }
            }

            // Console.WriteLine(String.Format(
            // @"Index out of range in configIndexFormat =>
            // the string (index): {0}
            // subStrOfNum :{1}
            // charToFindTheIndex : {2}
            // isSuccessTryParse : {3}
            // isUpper : {4}
            // io_colIndex: {5}
            // io_rowIndex :{6}"
            // , i_IndexFormt, charToReplaceTheIndex, charToFindTheIndex, isSuccessTryParse, isUpper , io_colIndex , io_rowIndex));

            // isUpper = char.IsLetter(charToFindTheIndex)
            if (!isSuccessTryParse || !isUpper)
            {
                throw new IndexOutOfRangeException(string.Format(
@"Index out of range in configIndexFormat => 
the string (index): {0}
 subStrOfNum :{1} 
charToFindTheIndex : {2} 
isSuccessTryParse : {3} 
isUpper : {4}
m_GameBoard[io_rowIndex, io_colIndex] : {5}",
i_IndexFormt,
charToReplaceTheIndex,
charToFindTheIndex,
isSuccessTryParse,
isUpper,
m_GameBoard[io_rowIndex, io_colIndex]));
            }

            bool isInvalueRow = io_rowIndex < 0 || io_rowIndex >= km_NumOfRows;
            bool isInvalueCol = io_colIndex < 0 || io_colIndex >= km_NumOfCols;
            if (isInvalueRow || isInvalueCol)
            {
                throw new IndexOutOfRangeException("Index out of range in configIndexFormat");
            }
        }

        public struct Card
        {
            // private const string km_formatToPrint = " {} |";
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
                hashCode = (hashCode * -1521134295) + m_Value.GetHashCode();
                hashCode = (hashCode * -1521134295) + m_Flipped.GetHashCode();
                hashCode = (hashCode * -1521134295) + Value.GetHashCode();
                hashCode = (hashCode * -1521134295) + Flipped.GetHashCode();
                return hashCode;
            }

            // TODO: add show func (send to interface)  XX get (??)
        }
    }
}
