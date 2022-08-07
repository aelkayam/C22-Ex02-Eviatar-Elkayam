using System;

namespace ConsoleUserInterface
{
    internal class UserInput
    {
        private const char k_ExitTheGame = 'Q';
        private const byte k_MaxGameBoardLength = 6;
        private const byte k_MinGameBoardLength = 4;

        enum e_InputType
        {
            GameMode,           // 1 = NPC,  2 = PVP    (numbers)
            BoardDimensions,    // k_MaxGameBoardLength < x < k_MinGameBoardLength  (numbers)
            GameMove,           // a letter followed by number 
            AnotherGame         // 1 = quit, 2 = another one    (numbers)
        }

        // get user initial inputs:
        // names(array), single-player or multi-player and  game board size(length and width)
        public static void GetUserInitialInput(out string[] o_PlayersNames, out bool o_VersusNPC, out byte o_GameBoardLength, out byte o_GameBoardWidth)
        {
            // player 1 name
            byte playerNum = 1;
            string message = string.Format("Please Enter player {0} name:", playerNum);
            ShowMessage(message);
            string playerOneName = Console.ReadLine();
            /// create Player1 object(?)

            // choose game mode
            bool isViableInput;
            string userInputString;
            do
            {
                message = string.Format(@"Do you want to play against NPC or another player/s?
Enter 1 for NPC
Enter 2 for Player vs Player");
                ShowMessage(message);
                userInputString = Console.ReadLine();
                isViableInput = authenticate(userInputString, e_InputType.GameMode);
            } while (!isViableInput);

            // return TRUE for player vs. NPC, FALSE for PVP:
            o_VersusNPC = (byte.Parse(userInputString) == 1);

            // for NPC - create AI
            // for PVP - get other players names
            if (o_VersusNPC)
            {
                /// create AI player ///

                // return player names array:
                o_PlayersNames = new string[2];
                o_PlayersNames[0] = playerOneName;
                o_PlayersNames[1] = "AI";
            }
            else
            {
                /// add ONE additional player. can be expanded to more than 1 (with a loop) ///
                playerNum++;
                message = string.Format("Please Enter player {0} name:", playerNum);
                ShowMessage(message);
                string playerTwoName = Console.ReadLine();

                // return player names array:
                o_PlayersNames = new string[playerNum];
                o_PlayersNames[0] = playerOneName;
                o_PlayersNames[1] = playerTwoName;

            }


            // choose board size
            message = String.Format(@"Enter the game board dimensions:
the maximum available size is {0}x{0} and minimum is {1}x{1}.", k_MaxGameBoardLength, k_MinGameBoardLength);
            ShowMessage(message);
            // length:
            do
            {
                message = "Please enter length size";
                ShowMessage(message);
                userInputString = Console.ReadLine();
                isViableInput = authenticate(userInputString, e_InputType.BoardDimensions);
                o_GameBoardLength = byte.Parse(userInputString);
            } while (!isViableInput);

            // width:
            do
            {
                message = "Please enter width size";
                ShowMessage(message);
                userInputString = Console.ReadLine();
                isViableInput = authenticate(userInputString, e_InputType.BoardDimensions);
                o_GameBoardWidth = byte.Parse(userInputString);
            } while (!isViableInput);

            // show board
        }

        // get player move (a letter and number combined)
        public static void GetPlayerGameMove(out byte o_ChosenRow, out char o_ChosenCol, GameBoard i_GameBoard)
        {
            string playerInputString;
            bool isOkayMove;

            do
            {
                ShowMessage("Enter your move, or 'Q' to exit");
                playerInputString = Console.ReadLine();
                isOkayMove = authenticate(playerInputString, e_InputType.GameMove, i_GameBoard);

            } while (!isOkayMove);

            if (checkIfQuit(playerInputString))
            {
                /// need to quit current game
                QuitMessage();
                o_ChosenRow = 0;
                o_ChosenCol = '#';
                return;
            }

            o_ChosenCol = char.ToUpper(playerInputString[0]);
            o_ChosenRow = byte.Parse( playerInputString.Substring(1));

        }
        
        // validate the user inputs
        private static bool authenticate(string i_StringBeforeAuth, e_InputType i_InputType, GameBoard i_GameBoard = null)
        {
            bool isValid, isWithinLimits;

            isValid = authenticateValidity(i_StringBeforeAuth, i_InputType);
            if (isValid)
            {
                isWithinLimits = authenticateWithinLimits(i_StringBeforeAuth, i_InputType, i_GameBoard);
                if (!isWithinLimits)
                {
                    ShowMessage("input not within defined limits, please try again");
                }
            }
            else
            {
                isWithinLimits = false;
                ShowMessage("illegal input, please try again");

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
                    isValid = checkIfNumber(i_StringBeforeAuth);
                    break;
            }
            
            return isValid;
        }

        private static bool checkIfNumber(string i_StringToConvert)
        {
            return byte.TryParse(i_StringToConvert, out byte _);
        }
    
        private static bool checkIfGameMove(string i_StringToConvert)
        {
            if (i_StringToConvert.Length >= 2)
            {
                return (char.IsLetter(i_StringToConvert[0])) && byte.TryParse(i_StringToConvert.Substring(1), out _);
            }

            return false;

        }

        private static bool checkIfQuit(string i_StringToConvert)
        {
             if(char.TryParse(i_StringToConvert, out char quitTheGame))
            {
                return char.ToUpper(quitTheGame) == k_ExitTheGame;
            }

            return false;
        }

        // check if the input is a valid in terms of game rules.
        private static bool authenticateWithinLimits(string i_StringBeforeAuth, e_InputType i_InputType, GameBoard gameBoard)
        {
            bool isWithinLimits = false;

            switch (i_InputType)
            {
                case e_InputType.GameMode:
                    isWithinLimits = isBooleanOption(i_StringBeforeAuth);
                    break;
                case e_InputType.BoardDimensions:
                    isWithinLimits = isWithinGameLimits(i_StringBeforeAuth);
                    break;
                case e_InputType.GameMove:
                    isWithinLimits = isLegalGameMove(i_StringBeforeAuth, gameBoard);
                    break;
                case e_InputType.AnotherGame:
                    isWithinLimits = isBooleanOption(i_StringBeforeAuth);
                    break;
            }

            return isWithinLimits;
        }

        private static bool isBooleanOption(string i_StringToCheck)
        {
            byte gameMode = byte.Parse(i_StringToCheck);
            bool isBoolOption = (gameMode > 0 && gameMode < 3 );
            return isBoolOption; 
        }

        private static bool isWithinGameLimits(string i_StringTocheck)
        {
            byte boardDimension = byte.Parse(i_StringTocheck);
            bool isWithinDimensions = (boardDimension >= k_MinGameBoardLength && boardDimension <= k_MaxGameBoardLength );
            return isWithinDimensions;
        }

        // check if the move is within the current game board.
        // should we have the actual board as a member? or something else...?
        private static bool isLegalGameMove(string i_StringBeforeAuth, GameBoard i_GameBoard)
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
                    return (column >= 'A' && column < 'A' + i_GameBoard.Width - 1 && row > 0 && row <= i_GameBoard.Length);
                }
            }

            return false;
        }
        
        public static void ShowMessage(string i_Message)
        {
            Console.WriteLine(i_Message);
        }

        public static void QuitMessage()
        {
            ShowMessage("Goodbye!");
        }
    }
}
