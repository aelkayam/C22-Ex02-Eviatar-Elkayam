using System;
using System.Text;
using Ex02.ConsoleUtils;

namespace ConsoleUserInterface
{
    public enum ePromptType
    {
        PlayerName,
        AIPlayer,
        GameMode,
        GameBoardDimensions,
        GameBoardWidth,
        GameBoardHeight,
        GameMove,
        AnotherGame,
        Quit,
    }

    public enum eErrorType
    {
        IllegalValue,
        OddNumberOfTiles,
        OutOfBounds,
        CardTaken,
    }

    public static class Screen
    {
        // prompt messages:
        public const string k_PlayerNameMsg = "Please enter player {0} name:";
        public const string k_GameModeMsg = "Please enter 1 to play vs. NPC or 2 to play PVP";
        public const string k_AiPlayerMsg = "Is the next player AI? (Y/N)";

        /// need to insert cons sizes here >>
        public const string k_GameBoardDimensionsMsg = @"Enter the game board dimensions:
The minimum available size is {0}x{0} and maximum is {1}x{1}.
Game board must have even number of tiles.";

        public const string k_GameBoardWidthMsg = "Please enter width size:";
        public const string k_GameBoardHeightMsg = "Please enter height size:";
        public const string k_GameMoveMsg = "Enter your move in the following format: <Column><Number>, or enter 'Q' to exit";
        public const string k_AnotherGameMsg = "Do you want to play another game? (Y/N)";
        public const string k_QuitMsg = "Goodbye! Press 'enter' to exit";

        // error messages:
        public const string k_IllegalValueMsg = "Illegal input! ";
        public const string k_OddNumberOfTilesMsg = "Odd number of tiles! ";
        public const string k_OutOfBoundsMsg = "The tile {0} does not exist! ";
        public const string k_CardTakenMsg = "The card at {0} is taken! ";

        public static void ShowBoard(char[,] i_GameBoard)
        {
            Console.Write("    ");
            for (char c = 'A'; c < 'A' + i_GameBoard.GetLength(1); c++)
            {
                Console.Write(string.Format(@"{0}  ", c));
            }

            Console.WriteLine();
            Console.WriteLine(new StringBuilder().Append('=', i_GameBoard.GetLength(1) * 4));

            for (int j = 1; j <= i_GameBoard.GetLength(0); j++)
            {
                Console.Write(string.Format("{0}  |", j));
                for (int i = 0; i < i_GameBoard.GetLength(1); i++)
                {
                    Console.Write(string.Format("{0} |", i_GameBoard[j - 1, i]));
                }

                Console.WriteLine();
                Console.WriteLine(new StringBuilder().Append('=', i_GameBoard.GetLength(1) * 4));
            }
        }

        public static void ShowMessage(string i_Message, params string[] i_ContexVariables)
        {
            if (i_ContexVariables != null)
            {
                Console.WriteLine(string.Format(i_Message, i_ContexVariables));
            }
            else
            {
                Console.WriteLine(i_Message);
            }
        }

        // prints user prompts
        public static void ShowPrompt(ePromptType promptType, params string[] i_ContexVariables)
        {
            switch(promptType)
            {
                case ePromptType.PlayerName:
                    ShowMessage(k_PlayerNameMsg, i_ContexVariables);
                    break;
                case ePromptType.AIPlayer:
                    ShowMessage(k_AiPlayerMsg);
                    break;
                case ePromptType.GameMode:
                    ShowMessage(k_GameModeMsg);
                    break;
                case ePromptType.GameBoardDimensions:
                    ShowMessage(k_GameBoardDimensionsMsg, i_ContexVariables);
                    break;
                case ePromptType.GameBoardHeight:
                    ShowMessage(k_GameBoardHeightMsg);
                    break;
                case ePromptType.GameBoardWidth:
                    ShowMessage(k_GameBoardWidthMsg);
                    break;
                case ePromptType.GameMove:
                    ShowMessage(k_GameMoveMsg);
                    break;
                case ePromptType.AnotherGame:
                    ShowMessage(k_AnotherGameMsg);
                    break;
                case ePromptType.Quit:
                    ShowMessage(k_QuitMsg);
                    break;
            }
        }

        // prints errors
        public static void ShowError(eErrorType errorType, string i_ContexVariable = "")
        {
            switch (errorType)
            {
                case eErrorType.IllegalValue:
                    ShowMessage(k_IllegalValueMsg + "Please try again.");
                    break;
                case eErrorType.OddNumberOfTiles:
                    ShowMessage(k_OddNumberOfTilesMsg + "Please try again.");
                    break;
                case eErrorType.OutOfBounds:
                    ShowMessage(k_OutOfBoundsMsg + "Please try again.", i_ContexVariable);
                    break;
                case eErrorType.CardTaken:
                    ShowMessage(k_CardTakenMsg + "Please try again.", i_ContexVariable);
                    break;
            }
        }

        // clear console
        public static void ClearBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }
    }
}
