using System;
using System.Collections.Generic;
using Ex02.ConsoleUtils;

namespace User_Interface
{
    public class UserInput
    {
        private const char km_exitTheGame = 'Q';
        // TODO: get user board size 4x4 to 6x6, even only (static)
        // TODO: get amount of player: 1 vs NPC or 1 vs 1 
        // TODO: get user game choice, within limits of the board
        // TODO: check for legal move
        // TOOD: check if player wants to quit
        // TODO: value type location in board (struct or enum) <strong>nested class</strong>

        public static void getBoardDimensionsFormUser(out int io_numOfRow, out int io_numOfCol, int i_minSizeCardBoard, int i_mixSizeCardBoard)
        {
            bool validValue=false;
            io_numOfRow=0;
            io_numOfCol=0;
            string asqForDimensions = "Please enter the number of columns";

            do
            {
                GameScreen screen = new GameScreen();
                screen.showMsg(asqForDimensions);
                io_numOfRow = Console.Read();
                screen.showMsg(asqForDimensions);
                io_numOfCol = Console.Read();
                bool isDimensionsInReng = (isInReng(io_numOfRow, i_minSizeCardBoard, i_minSizeCardBoard)) && (isInReng(io_numOfCol, i_minSizeCardBoard, i_minSizeCardBoard));
                bool isDimensionsEven = isEven(io_numOfRow) & isEven(io_numOfCol);
                if (!isDimensionsInReng)
                {
                    screen.showMsgEror("The dimensions are not within the legal range");
                }
                if (!isDimensionsEven)
                {
                    screen.showMsgEror("The dimensions are not even ");
                }
                validValue = isDimensionsInReng && isDimensionsEven;

            }
            while (!validValue);
        }

        private static bool isInReng(int i_numTocheck ,int i_upperLimit , int i_lowerLimit)
        {
            return (i_numTocheck <= i_upperLimit) && (i_numTocheck >= i_lowerLimit);
        }

        private static bool isEven(int i_num) 
        {
            return i_num % 2 == 0;
        }

    }
}
