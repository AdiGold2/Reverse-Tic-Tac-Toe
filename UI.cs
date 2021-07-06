using System;
using System.Linq;

namespace XMixDrix
{
    public class UI
    {
        public static eGameTypes ShowMenu(ref int o_BoardSize)
        {
            eGameTypes type = 0;
            int gameType = 0;
            Console.WriteLine("Hello and welcome to the game!");
            while (o_BoardSize > 9 || o_BoardSize < 3)
            {
                Console.WriteLine("please choose the size of the board game, enter number between 3 to 9:");
                int.TryParse(Console.ReadLine(), out o_BoardSize);
                if (o_BoardSize > 9 || o_BoardSize < 3)
                {
                    Console.WriteLine("invalid input");
                }
            }

            while (gameType != 1 && gameType != 2)
            {
                Console.WriteLine("choose type of game: 1 for Human vs Human, 2 for human vs computer");
                int.TryParse(Console.ReadLine(), out gameType);
                if (gameType == 1)
                {
                    type = eGameTypes.HumanVsHuman;
                }
                else if (gameType == 2)
                {
                    type = eGameTypes.HumanVsPc;
                }
                else
                {
                    Console.WriteLine("invalid input");
                }
            }

            return type;
        }

        public static void WinUI(int i_WinnerNum)
        {
            Console.WriteLine("Player " + i_WinnerNum + " WON this round");
        }

        public static bool DrawUI(int i_Player1Score, int i_Player2Score)
        {
            Console.WriteLine("Draw");
            bool v_PlayAgain = ShowScoreboard(i_Player1Score, i_Player2Score);

            return v_PlayAgain;
        }

        public static bool ShowScoreboard(int i_Player1Score, int i_Player2Score)
        {
            bool v_AnotherRound = false;
            Console.WriteLine("The current result is Player 1: " + i_Player1Score + " Player 2: " + i_Player2Score);
            Console.WriteLine("If you wish to play another round enter 'Y' , else press any key");
            string wishToPlayAgain = Console.ReadLine();
            if (wishToPlayAgain.Equals("Y"))
            {
                v_AnotherRound = true;
            }
            else
            {
                Console.WriteLine("Bye Bye!");
            }

            return v_AnotherRound;
        }

        public static Cell AskForMove(ref Player i_Player, ref bool o_IsQuitting)
        {
            int row = 0, col = 0;
            Console.WriteLine("Player " + i_Player.PlayerNum + " please choose a cell in the format of: (row) (col)");
            string input = Console.ReadLine();
            string[] formattedInput = input.Split(' ');
            while((formattedInput.Length != 2) && formattedInput[0] != "Q" && formattedInput[0] != "q") 
            {
                Console.WriteLine("Invalid input");
                input = Console.ReadLine();
                formattedInput = input.Split(' ');
            }

            if (!int.TryParse(formattedInput[0], out row))
            {
                if (formattedInput[0] == "Q" || formattedInput[0] == "q")
                {                                       ////Case Q is the input
                    o_IsQuitting = true;
                }
            }
            else
            {
                int.TryParse(formattedInput[1], out col);
            }

            Cell chosenCell = new Cell(row, col);

            return chosenCell;
        }
    }
}