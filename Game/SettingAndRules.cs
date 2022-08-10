using System;

namespace Game
{
    public static class SettingAndRules
    {
        private const bool v_IsFixed = true;
        private const string k_trowFixedMsg = "The value is fixed";
        //===============================================
        //Dimensions
        //===============================================
        private const byte k_UpperBound = 6;
        private const byte k_LowerBound = 4;
        private const string k_trowDimensionsMsg = "The game dimensions heva a fix size";

        public static Rules Rows = new Rules("Num of Rows", k_UpperBound, k_LowerBound, !v_IsFixed , k_trowDimensionsMsg);
        public static Rules Columns = new Rules("Num of Rows", k_UpperBound, k_LowerBound, !v_IsFixed , k_trowDimensionsMsg);
        
        public static bool dimensionsValid(byte i_rowForChak , byte i_columnsForChak)
        {
            bool isValid =Rows.isValid(i_rowForChak)&&Columns.isValid(i_columnsForChak);
            bool isMultiplierOfParity = IsEven(i_rowForChak, i_columnsForChak);

            return isValid && isMultiplierOfParity;
        }

        public static bool IsEven(params byte[] argForTest)
        {
            bool answer = false;

            foreach (byte i in argForTest)
            {
                if (i%2 == 0)
                {
                    answer = true;
                    break;   
                }
            }

            return answer;
        }

        // TODO : Remove the     //isInputInTheCorrectFormat  //dimensionsValid 
        // or add to user input  

        /******     number of players       ******/
        private const byte numOfParticipants = 2;
        public static Rules numOfPlayers = new Rules("num Of Players", numOfParticipants, numOfParticipants, v_IsFixed, k_trowFixedMsg);

        /******     input format       ******/
        private const string k_inputFormatMsg = "The input format of the game is Capital letter between A - F , and a number between 1-6";
        private static Rules[] inputFormat = { new Rules("input Format Letter",(byte)'A', (byte)'F', !v_IsFixed, k_inputFormatMsg),
            new Rules("input Format number",(byte)'1', (byte)'6', !v_IsFixed, k_inputFormatMsg) };

        public static bool isInputInTheCorrectFormat(string i_ValueToCheck)
        {
            bool isCorrectSize = inputFormat.Length == i_ValueToCheck.Length;
            bool isCorrectFormt = true;

            if (isCorrectSize)
            {
                for(int i = 0; i < inputFormat.Length; i++)
                {
                    isCorrectFormt = inputFormat[i].isValid((byte)i_ValueToCheck[i]);
                    if (!isCorrectFormt)
                    {
                        break;
                    }
                }
            }

            return isCorrectSize && isCorrectFormt;
        }

        /******     number of players       ******/
        public const int k_sleepBetweenTurns = 2000;
        //public static Rules sleepTime = new Rules (k_sleepBetweenTurns , k_sleepBetweenTurns*3 , !v_IsFixed," The game can cheng the sleep time bet ")

        /****** number of Choice In players Turn ******/
        private const int k_NumOfChoiceInPlayerTurn = 2;
        public static Rules NumOfChoiceInTurn = new Rules("Num Of Choice In player Turn",
            k_NumOfChoiceInPlayerTurn , k_NumOfChoiceInPlayerTurn, v_IsFixed, k_trowFixedMsg);

        //need to move this for byte to <T>
        public struct Rules
        {
            public readonly string m_name;
            public readonly byte m_UpperBound;
            public readonly byte m_lowerBound;
            public readonly bool v_IsFixed;
            private readonly string m_trowMsg;

            public Rules(string i_name,byte i_UpperBound ,byte i_lowerBound , bool i_isFixed , string i_torwStr)
            {
                m_name = i_name;
                m_UpperBound = i_UpperBound;
                m_lowerBound = i_lowerBound;
                v_IsFixed = i_isFixed;
                m_trowMsg = i_torwStr;
            }
            public bool isValid(byte i_Valuechecked)
            {
                if (!v_IsFixed)
                {
                    throw new ArgumentException();
                }

                return isBetween(i_Valuechecked, m_UpperBound, m_lowerBound);
            }

            public static bool isBetween(byte i_Valuechecked, byte i_upperBound, byte i_lowerBound)
            {
                return i_Valuechecked >= i_upperBound && i_Valuechecked <= i_lowerBound;
            }

            public string ToString()
            {
                return string.Format(" the {0} between {1} to {2} ", m_name, m_lowerBound, m_UpperBound);
            }
        }
    }
}
