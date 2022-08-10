using System;
using ConsoleUserInterface;
using Game;
// TODO: add the ref to Game

namespace ConsoleUserInterface
{
    public static class UserInput
    {
        private const char k_ExitTheGame = 'Q';
        private const byte k_MaxGameBoardLength = 6;
        private const byte k_MinGameBoardLength = 4;

        private enum e_InputType
        {
            GameMode,            // 1 = NPC,  2 = PVP    (numbers)
            BoardDimensions,     // k_MaxGameBoardLength < x < k_MinGameBoardLength  (numbers)
            GameMove,            // a letter followed by number
            AnotherGame,         // y = another game, n = quit  (Y/N)
        }

        public static void GetBoardDimensions(out byte o_GameBoardLength, out byte o_GameBoardWidth)
        {

            string userInputString;
            bool isViableInput;
            bool isEvenBoard;
            do
            {
                do
                {
                    // width (left to right):
                    Screen.ShowPrompt(ePromptType.GameBoardWidth);
                    userInputString = GetUserInput();
                    isViableInput = authenticate(userInputString, e_InputType.BoardDimensions);
                }
                while (!isViableInput);
                o_GameBoardLength = byte.Parse(userInputString);

                do
                {
                    // height (top to bottom):
                    Screen.ShowPrompt(ePromptType.GameBoardHeight);
                    userInputString = GetUserInput();
                    isViableInput = authenticate(userInputString, e_InputType.BoardDimensions);
                }
                while (!isViableInput);
                o_GameBoardWidth = byte.Parse(userInputString);

                isEvenBoard = (o_GameBoardLength * o_GameBoardWidth) % 2 == 0;
                if (!isEvenBoard)
                {
                    Screen.ShowError(eErrorType.OddNumberOfTiles);
                }
            }
            while (!isEvenBoard);
        }

        public static string GetPlayersNames()
        {
            return GetUserInput();
        }

        public static bool GetBooleanAnswer()
        {
            bool isOkay;
            string answer;
            do
            {
                answer = GetUserInput();
                isOkay = authenticate(answer, e_InputType.AnotherGame);
                if (!isOkay)
                {
                    Screen.ShowError(eErrorType.IllegalValue);
                }
            }
            while (!isOkay);

            return (answer == "Y") || (answer == "y");
        }

        // get player move (a letter and number combined)
        public static string GetPlayerGameMove()
        {
            string playerInputString;
            bool isOkayMove;

            do
            {
                Screen.ShowPrompt(ePromptType.GameMove);
                playerInputString = GetUserInput();
                if (checkIfQuit(playerInputString))
                {
                    /// need to quit current game
                    Screen.ShowPrompt(ePromptType.Quit);
                    throw new Exception("quiting");
                }

                isOkayMove = authenticateValidity(playerInputString, e_InputType.GameMove);
            }
            while (!isOkayMove);

            return playerInputString;
        }

        // read strings from console
        public static string GetUserInput()
        {
            return Console.ReadLine().Trim();
        }

        // validate the user inputs
        private static bool authenticate(string i_StringBeforeAuth, e_InputType i_InputType, char[,] i_GameBoard = null)
        {
            bool isValid, isWithinLimits;

            isValid = authenticateValidity(i_StringBeforeAuth, i_InputType);
            if (isValid)
            {
                isWithinLimits = authenticateWithinLimits(i_StringBeforeAuth, i_InputType, i_GameBoard);
                if (!isWithinLimits)
                {
                    Screen.ShowError(eErrorType.OutOfBounds);
                }
            }
            else
            {
                isWithinLimits = false;
                Screen.ShowError(eErrorType.IllegalValue);
            }

            return isValid && isWithinLimits;
        }

        // check if the input is a valid in terms of syntax.
        private static bool authenticateValidity(string i_StringBeforeAuth, e_InputType i_InputType)
        {
            bool isValid = false;
            switch (i_InputType)
            {
                case e_InputType.GameMode:
                    isValid = checkIfNumber(i_StringBeforeAuth);
                    break;
                case e_InputType.BoardDimensions:
                    isValid = checkIfNumber(i_StringBeforeAuth);
                    break;
                case e_InputType.GameMove:
                    isValid = checkIfGameMove(i_StringBeforeAuth) || checkIfQuit(i_StringBeforeAuth);
                    break;
                case e_InputType.AnotherGame:
                    isValid = checkIfCharacter(i_StringBeforeAuth);
                    break;
            }

            return isValid;
        }

        private static bool checkIfCharacter(string i_StringToConvert)
        {
            return char.TryParse(i_StringToConvert, out _);
        }

        private static bool checkIfNumber(string i_StringToConvert)
        {
            return byte.TryParse(i_StringToConvert, out byte _);
        }

        private static bool checkIfGameMove(string i_StringToConvert)
        {
            if (i_StringToConvert.Length >= 2)
            {
                return char.IsLetter(i_StringToConvert[0]) && byte.TryParse(i_StringToConvert.Substring(1), out _);
            }

            return false;
        }

        private static bool checkIfQuit(string i_StringToConvert)
        {
            if (char.TryParse(i_StringToConvert, out char quitTheGame))
            {
                return char.ToUpper(quitTheGame) == k_ExitTheGame;
            }

            return false;
        }

        // check if the input is a valid in terms of game rules.
        private static bool authenticateWithinLimits(string i_StringBeforeAuth, e_InputType i_InputType, char[,] i_GameBoard)
        {
            bool isWithinLimits = false;

            switch (i_InputType)
            {
                case e_InputType.GameMode:
                    isWithinLimits = isMultipleOptions(i_StringBeforeAuth);
                    break;
                case e_InputType.BoardDimensions:
                    isWithinLimits = isWithinGameLimits(i_StringBeforeAuth);
                    break;
                case e_InputType.GameMove:
                    isWithinLimits = isLegalGameMove(i_StringBeforeAuth, i_GameBoard);
                    break;
                case e_InputType.AnotherGame:
                    isWithinLimits = isBooleanOption(i_StringBeforeAuth);
                    break;
            }

            return isWithinLimits;
        }

        private static bool isMultipleOptions(string i_StringBeforeAuth)
        {
            byte answer = byte.Parse(i_StringBeforeAuth);
            return answer > 0 && answer < 3;
        }

        private static bool isBooleanOption(string i_StringToCheck)
        {
            char answer = char.Parse(i_StringToCheck);
            char[] availableOptions = { 'y', 'Y', 'n', 'N' };
            return Array.Exists(availableOptions, x => x == answer);
        }

        private static bool isWithinGameLimits(string i_StringTocheck)
        {
            byte boardDimension = byte.Parse(i_StringTocheck);
            bool isWithinDimensions = boardDimension >= k_MinGameBoardLength && boardDimension <= k_MaxGameBoardLength;
            return isWithinDimensions;
        }

        // check if the move is within the current game board.
        // should we have the actual board as a member? or something else...?
        private static bool isLegalGameMove(string i_StringBeforeAuth, char[,] i_GameBoard)
        {
            if (i_GameBoard != null)
            {
                if (checkIfQuit(i_StringBeforeAuth))
                {
                    return true;
                }
                else
                {
                    char column = char.ToUpper(i_StringBeforeAuth[0]);
                    byte row = byte.Parse(i_StringBeforeAuth.Substring(1));
                    return column >= 'A' && column < 'A' + i_GameBoard.GetLength(0) - 1 && row > 0 && row <= i_GameBoard.GetLength(1);
                }
            }

            return false;
        }

        public static byte GetUserInputByte()
        {
            return (byte)0;
        }
    }
}
