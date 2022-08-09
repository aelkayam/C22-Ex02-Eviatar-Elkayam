using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using ConsoleUserInterface;

/// <summary>
/// initial:
/// 1. player 1 is ask to enter his name
/// 2. Is it a game with two players or against the computer?
/// 3. Create a second player
/// 4.Receiving from the user the dimensions of the board 4-6
/// 5.The size of the squares will be an even number
///
///  game play:
/// 6.Cleaning the screen and drawing the board
/// 7.A request from the user to select a slot
/// 8.Checking whether the slot is valid
/// 9.do 6-8 1 more time
/// 10 If the player find a pair,
///         10.1 the turn stays with the player
///    else
///         10.2 show the board for two seconds
///         10.2 the turn goes to the other player
///
/// end:
/// 11. show the winner
/// 12. Ask the player if he wants another game
/// 13. if true go to 4
/// 14 else end game
/// </summary>
namespace MemoryCardGame
{
    // add user interface and the guy's DLL

    // TODO: T ? in the end  
    public class Game<T>
    {
        private const int k_sleepBetweenTurns = 2000;
        public const byte k_MaxGameBoardLength = 6;
        public const byte k_MinGameBoardLength = 4;
        private const byte m_TotalPLayers = 2;

        // TODO => ev: add redonly bool 

        private Player[] m_AllPlayersInGame; 
        private GameBoard m_GameBoard;
        private byte m_TurnCounter;
        private bool m_isPlaying;
        private byte m_FlippedCardsCounter;

        public Game()
        {
            m_GameBoard = null;
            m_TurnCounter = 0;
            m_isPlaying = false;
            m_AllPlayersInGame = null;
            m_FlippedCardsCounter = 0;
            m_AllPlayersInGame = new Player[m_TotalPLayers];

            bool isItComputer = false;
            for (int i = 0; i < m_TotalPLayers; i++)
            {
                if (!isItComputer)
                {
                    string message = string.Format("Please Enter player {0} name:", i);
                    Screen.ShowMessage(message);
                    string playerName = UserInput.GetPlayersNames();
                    m_AllPlayersInGame[i] = new Player(playerName);
                }
                else
                {
                    m_AllPlayersInGame[i] = new Player();
                }

                if (!(i == m_TotalPLayers - 1))
                {
                    Screen.ShowMessage("Is the next player AI? (2 = vs PC /1 = 2 player )");
                    isItComputer = UserInput.GetBooleanAnswer();
                }
            }
        }

        private bool isRunning()
        {
            // TODO => ev: move m_FlippedCardsCounter to gameBord 
            return m_isPlaying || m_FlippedCardsCounter == m_GameBoard.Length;
        }

        private int getPlayerIndex()
        {
            return m_TurnCounter % m_TotalPLayers;
        }

        public void Start()
        {
            m_isPlaying = true;
            do
            {
                // ask for board Dimensions
                // choose board size
                string message = string.Format(
@"Enter the game board dimensions:
The maximum available size is {0}x{0} and minimum is {1}x{1}.
Game board must have even number of tiles.", k_MaxGameBoardLength,
k_MinGameBoardLength);
                Screen.ShowMessage(message);
                UserInput.GetBoardDimensions(out byte o_BoardLength, out byte o_BoardWidth);

                m_GameBoard = new GameBoard(o_BoardLength, o_BoardWidth);
                m_TurnCounter = 0;
                m_FlippedCardsCounter = 0;
                playTheGame();
                if (m_isPlaying)
                {
                    // TODO => el: add to enum in screen  
                    Screen.ShowMessage("Do you want another game?");
                    m_isPlaying = UserInput.GetBooleanAnswer();
                }
            }
            while (m_isPlaying);
        }

        private void playTheGame()
        {
            try
            {
                do
                {
                    Player currentlyPlayingPlayer = m_AllPlayersInGame[getPlayerIndex()];
                    string firstPlayerScore = gameStage(currentlyPlayingPlayer);
                    string secondPlayerScore = gameStage(currentlyPlayingPlayer);
                    drawBoard();
                    bool isFindNewPair = m_GameBoard.DoThePlayersChoicesMatch(firstPlayerScore, secondPlayerScore);

                    if (isFindNewPair)
                    {
                        currentlyPlayingPlayer.IncreaseScore();
                        // TODO=> ev: remov the ++ after 
                        m_FlippedCardsCounter++;
                        m_FlippedCardsCounter++;
                    }
                    else
                    {
                        Thread.Sleep(k_sleepBetweenTurns);
                        m_TurnCounter++;
                    }
                }
                while (isRunning());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                m_isPlaying = false;
            }
        }

        private string gameStage(Player i_currentlyPlayingPlayer)
        {
            bool userInputValid = true;
            string indexChoice;
            string mag = string.Format("{0} choose a tile", i_currentlyPlayingPlayer.Name);
            List<string> validSlotForChose = m_GameBoard.GetAllValidTilesForChoice();
            do
            {
                drawBoard();
                Screen.ShowMessage(mag);
                if(!userInputValid)
                {
                    Console.WriteLine("try Agin !! "); 
                }
                indexChoice = i_currentlyPlayingPlayer.GetPlayerChoice(validSlotForChose);
                userInputValid=validSlotForChose.Contains(indexChoice);// ? 
            } while (userInputValid);
            m_GameBoard.Flipped(indexChoice, true);// 

            return indexChoice;
        }

        private void drawBoard()
        {
            Screen.ClearBoard();
            Screen.ShowBoard(m_GameBoard.GetBoardToDraw());
            Screen.ShowMessage(getPlayersScoreLine());
        }

        private string getPlayersScoreLine()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Player player in m_AllPlayersInGame)
            {
                sb.Append(player.ToString());
            }

            return sb.ToString();
        }
    }
}
