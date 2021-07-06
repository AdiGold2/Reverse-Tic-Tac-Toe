using System;

namespace XMixDrix
{
    public class Logic
    {
        private int m_CurrentPlayerNum = 1;
        private Board m_Board;
        private Player m_PlayerOne, m_PlayerTwo;

        public Logic(ref Board i_Board, ref Player i_PlayerOne, ref Player i_PlayerTwo)
        {
            m_Board = i_Board;
            m_PlayerOne = i_PlayerOne;
            m_PlayerTwo = i_PlayerTwo;
        }

        public int CurrentPlayer
        {
            get
            {
                return m_CurrentPlayerNum;
            }

            set
            {
                m_CurrentPlayerNum = value;
            }
        }

        public void MakeMove(ref bool o_IsQuitting)
        {
            bool v_isQuitting = false, v_isValid;
            Player currentPlayer = CurrentPlayer == 1 ? m_PlayerOne : m_PlayerTwo;
            if (currentPlayer.IsHuman)
            {
                Cell playerCell = UI.AskForMove(ref currentPlayer, ref v_isQuitting);
                if (v_isQuitting)
                {
                    o_IsQuitting = true;
                    return;                     ////case a player quits no need to continue move
                }

                v_isValid = CheckMove(playerCell);
                if (v_isValid)
                {
                    m_Board.Matrix[playerCell.m_Row - 1, playerCell.m_Col - 1] = currentPlayer.Sign;
                    SwitchTurn();
                }
                else
                {                                   ////invalid cell chosen
                    Console.WriteLine("Invalid cell choose valid one");
                    MakeMove(ref o_IsQuitting);
                }
            }
            else
            {
                Console.WriteLine("PC taking it's move");
                System.Threading.Thread.Sleep(1000);
                Cell pcMove = AI.ChooseNextMove(m_Board, currentPlayer.Sign);
                m_Board.Matrix[pcMove.m_Row, pcMove.m_Col] = currentPlayer.Sign;
                SwitchTurn();
            }
        }

        public bool CheckMove(Cell i_MoveToCheck)
        {
            bool v_IsValid = true;
            int row = i_MoveToCheck.m_Row, col = i_MoveToCheck.m_Col;
            int edgeSize = m_Board.EdgeSize;
            if ((col > edgeSize) || (col < 1))
            {                                           ////out of range
                v_IsValid = false;
            }
            else if ((row > edgeSize) || (row < 1))
            {                                           ////out of range
                v_IsValid = false;
            }
            else if (m_Board.Matrix[row - 1, col - 1] != ' ')
            {                                           ////case it's full
                v_IsValid = false;
            }

            return v_IsValid;
        }

        public void SwitchTurn()
        {
            CurrentPlayer = (CurrentPlayer == 1) ? 2 : 1;
        }

        public bool CheckRowWin()
        {
            bool v_FoundWin = false;
            int row = 0;
            while ((row < m_Board.EdgeSize) && (!v_FoundWin))
            {
                v_FoundWin = CheckRow(row);
                row++;
            }

            return v_FoundWin;
        }

        public bool CheckRow(int i_Row)
        {
            bool res = true;
            char checkWith = m_Board.Matrix[i_Row, 0];
            if (checkWith == ' ')
            {
                res = false;
            }
            else
            {
                for (int i = 0; i < m_Board.EdgeSize; i++)
                {
                    if (m_Board.Matrix[i_Row, i] != checkWith)
                    {
                        res = false;
                    }
                }
            }

            return res;
        }

        public bool CheckColWin()
        {
            bool v_FoundWin = false;
            int col = 0;
            while ((col < m_Board.EdgeSize) && (!v_FoundWin))
            {
                v_FoundWin = CheckCol(col);
                col++;
            }

            return v_FoundWin;
        }

        public bool CheckCol(int i_Col)
        {
            bool res = true;
            char checkWith = m_Board.Matrix[0, i_Col];
            if (checkWith == ' ')
            {
                res = false;
            }
            else
            {
                for (int i = 0; i < m_Board.EdgeSize; i++)
                {
                    if (m_Board.Matrix[i, i_Col] != checkWith)
                    {
                        res = false;
                    }
                }
            }

            return res;
        }

        public bool CheckSlashWin()
        {
            bool v_FoundWin = true;
            char checkWith = m_Board.Matrix[m_Board.EdgeSize - 1, 0];
            for (int i = 1; i < m_Board.EdgeSize; i++)
            {
                if ((m_Board.Matrix[m_Board.EdgeSize - 1 - i, i] != checkWith) || (checkWith == ' '))
                {
                    v_FoundWin = false;
                }
            }

            return v_FoundWin;
        }

        public bool CheckBackSlashWin()
        {
            bool v_FoundWin = true;
            char checkWith = m_Board.Matrix[0, 0];
            for (int i = 1; i < m_Board.EdgeSize; i++)
            {
                if ((m_Board.Matrix[i, i] != checkWith) || (checkWith == ' '))
                {
                    v_FoundWin = false;
                }
            }

            return v_FoundWin;
        }

        public bool CheckWin()
        {
            return CheckBackSlashWin() || CheckSlashWin() || CheckRowWin() || CheckColWin();
        }
    }
}