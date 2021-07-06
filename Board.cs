using System;
using System.Text;

namespace XMixDrix
{
    public class Board
    {
        private char[,] m_Matrix = null;
        private int m_EdgeSize;

        public Board(int i_EdgeSize)
        {
            m_EdgeSize = i_EdgeSize;
        }

        public char[,] Matrix
        {
            get
            {
                return m_Matrix;
            }

            set
            {
                m_Matrix = value;
            }
        }

        public int EdgeSize
        {
            get
            {
                return m_EdgeSize;
            }

            set
            {
                m_EdgeSize = value;
            }
        }

        public void InitBoard()
        {
            if (m_Matrix == null)
            {
                m_Matrix = new char[m_EdgeSize, m_EdgeSize];
            }

            for (int i = 0; i < m_EdgeSize; i++)
            {
                for (int j = 0; j < m_EdgeSize; j++)
                {
                    m_Matrix[i, j] = ' ';
                }
            }
        }

        public void PrintBoard()
        {
            StringBuilder topHeader = new StringBuilder("    ");
            for(int i = 0; i < EdgeSize; i++)
            {
                topHeader.AppendFormat("{0}   ", i + 1);
            }

            Console.WriteLine(topHeader);
            for (int i = 0; i < EdgeSize; i++)
            {
                Console.Write(" " + (i + 1) + "|");
                for (int j = 0; j < EdgeSize; j++)
                {
                    Console.Write(" " + m_Matrix[i, j] + " |");
                }

                Console.Write(Environment.NewLine);
                Console.Write("  ");
                for (int j = 0; j < EdgeSize; j++)
                {
                    Console.Write("====");
                }

                Console.Write("=");
                Console.Write(Environment.NewLine);
            }
        }
    }
}