using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using ConsoleUserInterface;
using Game;
using Setting = Game.SettingAndRules;
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
        public const bool v_flippedTheCard = true;

        private Player[] m_AllPlayersInGame;
        private GameLogic m_GameBoard;
        private byte m_TurnCounter;
        private bool m_isPlaying;
        private byte m_TotalPLayers;
        private int k_sleepBetweenTurns = Setting.k_sleepBetweenTurns;
        // ===================================================================
        //  constructor  and methods that the constructor uses
        // ===================================================================
        public Game()
        {
            m_GameBoard = null;
            m_AllPlayersInGame = null;
            m_isPlaying = false;
            m_TurnCounter = 0;

            /******     number of players       ******/
            if (Setting.numOfPlayers.v_IsFixed)
            {
                m_TotalPLayers = Setting.numOfPlayers.m_UpperBound;
            }
            else
            {

                m_TotalPLayers = inputFromTheUserAccordingToTheRules(Setting.numOfPlayers);

            }
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

        public byte inputFromTheUserAccordingToTheRules(Setting.Rules i_rule)
        {
            string strMsg = string.Format("Please enter the {}", i_rule.ToString());
            byte returnVal = 0;
            bool isInputValid = false;
            do
            {
                Screen.ShowMessage(strMsg);
                returnVal = UserInput.GetUserInputByte();
                isInputValid = i_rule.isValid(returnVal);
            } while (!isInputValid);

            return returnVal;
        }

        private bool isRunning()
        {
            return m_isPlaying || m_GameBoard.HeveMoreMoves;
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
Game board must have even number of tiles.", Setting.Rows.m_UpperBound,
Setting.Rows.m_lowerBound);
                Screen.ShowMessage(message);
                UserInput.GetBoardDimensions(out byte o_BoardLength, out byte o_BoardWidth);

                m_GameBoard = new GameLogic(o_BoardLength, o_BoardWidth);
                m_TurnCounter = 0;
                playTheGame();
                if (m_isPlaying)
                {
                    // TODO => el: add to enum in screen  
                    Screen.ShowMessage("Do you want another game?");
                    m_isPlaying = UserInput.GetBooleanAnswer();

                    if (m_isPlaying)
                    {
                        foreach(Player player in m_AllPlayersInGame)
                        {
                            player.restartNewGame();
                        }
                    }
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
                    List<string> playerChois = new List<string>();

                    // TODO : ?? NumOfChoiceInTurn = fix it to be a val ??
                    for (int i = 0; i < Setting.NumOfChoiceInTurn.m_UpperBound; i++)
                    {
                        playerChois.Add(gameStage(currentlyPlayingPlayer));
                    }

                    // Show all players the board
                    showAllPlayersTheBoard();

                    bool isThePlyerHaveAnderTurn = m_GameBoard.DoThePlayersChoicesMatch(out byte o_scoreForTheTurn , playerChois.ToArray());

                    if (! isThePlyerHaveAnderTurn)
                    {
                        m_TurnCounter++;
                    }

                    currentlyPlayingPlayer.IncreaseScore(o_scoreForTheTurn);
                    Thread.Sleep(k_sleepBetweenTurns);

                }
                while (isRunning());
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
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
                if (!userInputValid)
                {
                    Console.WriteLine("try Agin !! ");
                }
                indexChoice = i_currentlyPlayingPlayer.GetPlayerChoice(validSlotForChose);
                userInputValid = validSlotForChose.Contains(indexChoice);// ? 
            } while (userInputValid);
            m_GameBoard.Flipped(indexChoice, v_flippedTheCard);

            return indexChoice;
        }

        private void drawBoard()
        {
            Screen.ClearBoard();
            Screen.ShowBoard(m_GameBoard.GetBoardToDraw());
            Screen.ShowMessage(getPlayersScoreLine());
        }

        private void showAllPlayersTheBoard()
        {
            drawBoard();
            foreach(Player player in m_AllPlayersInGame)
            {
                player.showBoard(m_GameBoard);
            }
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
