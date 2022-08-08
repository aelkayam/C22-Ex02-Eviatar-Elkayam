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
    internal class Game<T>
    {
        private const int k_sleepBetweenTurns = 2000;
        private const byte k_MaxGameBoardLength = 6;
        private const byte k_MinGameBoardLength = 4;
        private const byte m_TotalPLayers = 2;

        // private byte m_numOfRow;
        // private byte m_numOfCol;
        private readonly Player[] m_CurrentPlayers; // TODO: find a good name
        private GameBoard m_GameBoard;
        private byte m_TurnCounter;
        private bool m_isPlaying;
        private byte m_FlippedCardsCounter;

        public Game()
        {
            m_GameBoard = null;
            m_TurnCounter = 0;
            m_isPlaying = false;
            m_CurrentPlayers = null;
            m_FlippedCardsCounter = 0;
            m_CurrentPlayers = new Player[m_TotalPLayers];

            bool isItComputer = false;
            for (int i = 0; i < m_TotalPLayers; i++)
            {
                if (!isItComputer)
                {
                    string message = string.Format("Please Enter player {0} name:", i);
                    GameBoardView.ShowMessage(message);
                    string playerName = UserInput.GetPlayersNames();
                    m_CurrentPlayers[i] = new Player(playerName);
                }
                else
                {
                    m_CurrentPlayers[i] = new Player();
                }

                if (!(i == m_TotalPLayers - 1))
                {
                    GameBoardView.ShowMessage("Is the next player AI? (2 = vs PC /1 = 2 player )");
                    isItComputer = UserInput.GetBooleanAnswer();
                }
            }

            // TODO: call User input from User and get stuff
            // TODO: init m_TotalPLayers
            // TODO: init m_TurnCounter
            // TODO: init m_CurrentPlayers
            // TODO: init m_isPlaying
            // TODO: init m_CardBoard
        }

        // TODO: shuffle cards function
        // TODO: add preFix to the matrix
        private bool isRunning()
        {
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
                GameBoardView.ShowMessage(message);
                UserInput.GetBoardDimensions(out byte o_BoardLength, out byte o_BoardWidth);

                m_GameBoard = new GameBoard(o_BoardLength, o_BoardWidth);
                m_TurnCounter = 0;
                m_FlippedCardsCounter = 0;
                playTheGame();
                if (m_isPlaying)
                {
                    GameBoardView.ShowMessage("Do you want another game?");
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
                    Player currentlyPlayingPlayer = m_CurrentPlayers[getPlayerIndex()];
                    string firstPlayerScore = gameStage(currentlyPlayingPlayer);
                    string secondPlayerScore = gameStage(currentlyPlayingPlayer);

                    bool isFindNewPair = m_GameBoard.DoThePlayersChoicesMatch(firstPlayerScore, secondPlayerScore);
                    drawBoard();

                    if (isFindNewPair)
                    {
                        currentlyPlayingPlayer.IncreaseScore();
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
            drawBoard();
            GameBoardView.ShowMessage(string.Format("{0} choose a tile", i_currentlyPlayingPlayer.Name));
            List<string> validSlotForChose = m_GameBoard.GetAllValidTilesForChoice();
            string indexChoice = i_currentlyPlayingPlayer.GetPlayerChoice(validSlotForChose);
            m_GameBoard.Flipped(indexChoice, true);

            return indexChoice;
        }

        private void drawBoard()
        {
            GameBoardView.ClearBoard();
            GameBoardView.ShowBoard(m_GameBoard.GetBoardToDraw());
            GameBoardView.ShowMessage(getPlayersScoreLine());
        }

        private string getPlayersScoreLine()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Player player in m_CurrentPlayers)
            {
                sb.Append(player.ToString());
            }

            return sb.ToString();
        }
    }
}
