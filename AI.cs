using System;
using System.Linq;

namespace XMixDrix
{
    public class AI
    {
        public static Cell ChooseNextMove(Board i_Board, char i_Sign)
        {                                               ////AI rates every cell with worst case scenarios and chooses min rated cell
            int bestOptionRow = 0, bestOptionCol = 0;
            int bestRating = i_Board.EdgeSize + 1;      ////initiation that any open cell improves
            for (int i = 0; i < i_Board.EdgeSize; i++)
            {
                for (int j = 0; j < i_Board.EdgeSize; j++)
                {
                    if (i_Board.Matrix[i, j] == ' ')
                    {
                        int currentRating = RateOptionOverall(i_Board, i, j, i_Sign);
                        if (currentRating < bestRating)
                        {
                            bestOptionRow = i;
                            bestOptionCol = j;
                            bestRating = currentRating;
                        }
                    }
                }
            }

            return new Cell(bestOptionRow, bestOptionCol);
        }

        public static int RateOptionByRow(Board i_Board, int i_Row, char i_Sign)
        {
            int resRate = 0;
            for (int i = 0; i < i_Board.EdgeSize; i++)
            {
                if (i_Board.Matrix[i_Row, i] == i_Sign)
                {
                    resRate++;
                }
                else if (i_Board.Matrix[i_Row, i] != ' ')
                {                                           ////case it's the opposite sign
                    resRate = -1;
                    break;
                }
            }

            return resRate;
        }

        public static int RateOptionByCol(Board i_Board, int i_Col, char i_Sign)
        {
            int resRate = 0;
            for (int i = 0; i < i_Board.EdgeSize; i++)
            {
                if (i_Board.Matrix[i, i_Col] == i_Sign)
                {
                    resRate++;
                }
                else if (i_Board.Matrix[i, i_Col] != ' ')
                {                                           ////case it's the opposite sign
                    resRate = -1;
                    break;
                }
            }

            return resRate;
        }

        public static int RateOptionBySlash(Board i_Board, char i_Sign)
        {
            int resRate = 0;
            for (int i = 0; i < i_Board.EdgeSize; i++)
            {
                if (i_Board.Matrix[i_Board.EdgeSize - 1 - i, i] == i_Sign)
                {
                    resRate++;
                }
                else if (i_Board.Matrix[i_Board.EdgeSize - 1 - i, i] != ' ')
                {                                           ////case it's the opposite sign
                    resRate = -1;
                    break;
                }
            }

            return resRate;
        }

        public static int RateOptionByBackSlash(Board i_Board, char i_Sign)
        {
            int resRate = 0;
            for (int i = 0; i < i_Board.EdgeSize; i++)
            {
                if (i_Board.Matrix[i, i] == i_Sign)
                {
                    resRate++;
                }
                else if (i_Board.Matrix[i, i] != ' ')
                {                                           ////case it's the opposite sign
                    resRate = -1;
                    break;
                }
            }

            return resRate;
        }

        public static int RateOptionOverall(Board i_Board, int i_Row, int i_Col, char i_Sign)
        {
            int[] optionRatings = new int[4];
            optionRatings[0] = RateOptionByRow(i_Board, i_Row, i_Sign);
            optionRatings[1] = RateOptionByCol(i_Board, i_Col, i_Sign);
            optionRatings[2] = ((i_Row + i_Col) == i_Board.EdgeSize - 1) ? RateOptionBySlash(i_Board, i_Sign) : -1;
            optionRatings[3] = (i_Row == i_Col) ? RateOptionByBackSlash(i_Board, i_Sign) : -1;
            return optionRatings.Max();
        }
    }
}