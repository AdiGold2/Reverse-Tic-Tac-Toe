using System;

namespace XMixDrix
{
    public class Game
    {
        private Board m_GameBoard;
        private Player m_Player1;
        private Player m_Player2;
        private int m_SizeOfEdge = 0;
        private UI m_UI;                ////Maybe Take OFF
        private Logic m_Logic;

        public Game()
        {
            m_UI = new UI();
            eGameTypes gameType = UI.ShowMenu(ref m_SizeOfEdge);
            m_GameBoard = new Board(m_SizeOfEdge);
            bool v_IsHuman = true;
            m_Player1 = new Player('X', 1, v_IsHuman);
            if (gameType == eGameTypes.HumanVsHuman)
            {
                m_Player2 = new Player('O', 2, v_IsHuman);
            }
            else
            {       ////case it's vs PC
                m_Player2 = new Player('O', 2, !v_IsHuman);
            }

            m_Logic = new Logic(ref m_GameBoard, ref m_Player1, ref m_Player2);
        }

        public void ShowBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            m_GameBoard.PrintBoard();
        }

        public void InitGame()
        {
            bool v_PlayAgain = true;
            while (v_PlayAgain)
            {
                m_GameBoard.InitBoard();
                v_PlayAgain = InitRound();
            }

            Environment.Exit(0);
        }

        public bool InitRound()
        {
            m_Logic.CurrentPlayer = 1;
            ShowBoard();
            int moveCount = 0, maxMoves = m_SizeOfEdge * m_SizeOfEdge;
            bool v_WinFlag = false, v_QuitFlag = false, v_PlayAgain = false;
            while ((moveCount < maxMoves) && (!v_WinFlag))
            {
                m_Logic.MakeMove(ref v_QuitFlag);

                moveCount++;
                ShowBoard();
                if (m_Logic.CheckWin() || v_QuitFlag)
                {
                    if (v_QuitFlag)
                    {
                        m_Logic.SwitchTurn();
                    }

                    if (m_Logic.CurrentPlayer == 1)
                    {
                        m_Player1.Win();
                        v_PlayAgain = UI.ShowScoreboard(m_Player1.Score, m_Player2.Score);
                    }
                    else
                    {
                        m_Player2.Win();
                        v_PlayAgain = UI.ShowScoreboard(m_Player1.Score, m_Player2.Score);
                    }

                    v_WinFlag = true;
                }
                else if (moveCount == maxMoves)
                {
                    v_PlayAgain = UI.DrawUI(m_Player1.Score, m_Player2.Score);
                }
            }

            return v_PlayAgain;
        }
    }
}