using System;

namespace XMixDrix
{
    public class Player
    {
        private char m_Sign;
        private int m_PlayerNum;
        private int m_Score = 0;
        private bool m_IsHuman;

        public Player(char i_Sign, int i_PlayerNum, bool i_IsHuman)
        {
            m_Sign = i_Sign;
            m_PlayerNum = i_PlayerNum;
            m_IsHuman = i_IsHuman;
        }

        public void Win()
        {
            m_Score++;
            UI.WinUI(PlayerNum);
        }

        public char Sign
        {
            get
            {
                return m_Sign;
            }

            set
            {
                m_Sign = value;
            }
        }

        public int PlayerNum
        {
            get
            {
                return m_PlayerNum;
            }

            set
            {
                m_PlayerNum = value;
            }
        }

        public bool IsHuman
        {
            get
            {
                return m_IsHuman;
            }

            set
            {
                m_IsHuman = value;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }
    }
}