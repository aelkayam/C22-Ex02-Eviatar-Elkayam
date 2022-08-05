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
